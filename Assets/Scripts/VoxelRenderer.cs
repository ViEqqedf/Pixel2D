using UnityEngine;

namespace Voxel2D {
    public class VoxelRenderer {
        public Texture2D renderTex;
        private Material mat;

        public Material Initialize() {
            renderTex = new Texture2D(PublicBoard.WORLD_BLOCK_SIZE, PublicBoard.WORLD_BLOCK_SIZE);
            mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            mat.mainTexture = renderTex;

            return mat;
        }

        public void Tick(Color[] pixels) {
            renderTex.SetPixels(pixels);
            renderTex.Apply();
        }

        public void Dispose() {
        }
    }
}