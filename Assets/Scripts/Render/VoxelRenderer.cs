using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Voxel2D {
    public class VoxelRenderer {
        public static CommandBuffer renderCmd;

        private readonly Shader shader;
        private readonly int texID;
        private readonly int verticesID;
        private readonly int baseVertexID;

        private Material material;
        private readonly MaterialPropertyBlock matProperties = new MaterialPropertyBlock();

        // gpu buffer for vertex data
        private ComputeBuffer vtxBuf;
        // gpu buffer for indexes
        private GraphicsBuffer idxBuf;
        // gpu buffer for draw arguments
        private ComputeBuffer argBuf;
        // used to build argument buffer
        private readonly int[] drawArgs = new int[] { 0, 1, 0, 0, 0 };

        public VoxelRenderer(ShaderResourcesAsset shaderResAsset) {
            shader = shaderResAsset.shaders.procedural;
            texID = Shader.PropertyToID(shaderResAsset.propertyNames.tex);
            verticesID = Shader.PropertyToID(shaderResAsset.propertyNames.vertices);
            baseVertexID = Shader.PropertyToID(shaderResAsset.propertyNames.baseVertex);
        }

        public void Initialize() {
            material = new Material(shader) {
                hideFlags = HideFlags.HideAndDontSave&~HideFlags.DontUnloadUnusedAsset,
            };
        }

        public void Dispose() {
            if (material != null) {
                Object.Destroy(material);
                material = null;
            }

            vtxBuf?.Release();
            vtxBuf = null;
            idxBuf?.Release();
            idxBuf = null;
            argBuf?.Release();
            argBuf = null;
        }

        public void Tick(List<VoxelDrawVert> vertexList, List<ushort> triangleList) {
            renderCmd.Clear();
            RenderDrawList(vertexList, triangleList);
        }

        private void RenderDrawList(List<VoxelDrawVert> vertexList, List<ushort> triangleList) {
            UpdateBuffers(vertexList, triangleList);
            CreateDrawCommands();
        }

        private void UpdateBuffers(List<VoxelDrawVert> vertexList, List<ushort> triangleList) {
            int vertexCount = vertexList.Count;
            if (vtxBuf == null || vtxBuf.count < vertexCount) {
                CreateOrResizeVtxBuffer(ref vtxBuf, vertexCount);
            }

            int triIdxCount = triangleList.Count;
            if (idxBuf == null || idxBuf.count < triIdxCount) {
                CreateOrResizeIdxBuffer(ref idxBuf, triIdxCount);
            }

            if (argBuf == null || argBuf.count < 1) {
                CreateOrResizeArgBuffer(ref argBuf, 1);
            }

            vtxBuf.SetData(vertexList, 0, 0, vertexCount);
            idxBuf.SetData(triangleList, 0, 0, triIdxCount);

            drawArgs[0] = triIdxCount;
            argBuf.SetData(drawArgs);
        }

        void CreateOrResizeVtxBuffer(ref ComputeBuffer buffer, int count) {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new ComputeBuffer(num, UnsafeUtility.SizeOf<VoxelDrawVert>());
        }

        void CreateOrResizeIdxBuffer(ref GraphicsBuffer buffer, int count) {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new GraphicsBuffer(GraphicsBuffer.Target.Index, num, UnsafeUtility.SizeOf<ushort>());
        }

        void CreateOrResizeArgBuffer(ref ComputeBuffer buffer, int count) {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new ComputeBuffer(num, UnsafeUtility.SizeOf<int>(), ComputeBufferType.IndirectArguments);
        }

        private void CreateDrawCommands() {
            material.SetBuffer(verticesID, vtxBuf);

            // TODO: This part of the transformation is questionable
            float width = Screen.width;
            float height = Screen.height;
            renderCmd.SetViewport(new Rect(0, 0, width, height));
            renderCmd.SetViewProjectionMatrices(
                Matrix4x4.Translate(new Vector3(0.5f / width, 0.5f / height, 0f)),
                Matrix4x4.Ortho(0f, width, height, 0f, 0f, 1f));

            renderCmd.DrawProceduralIndirect(idxBuf, Matrix4x4.identity, material, -1, MeshTopology.Triangles, argBuf);
        }
    }
}