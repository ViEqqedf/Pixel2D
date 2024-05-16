Shader "Voxel/Procedural"
{
    // shader for Universal render pipeline
    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "PreviewType" = "Plane" }
        LOD 100

        Lighting Off
        Cull Off ZWrite On ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "VOXEL PROCEDURAL URP"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment VoxelPassFrag
            #include "./PassesUniversal.hlsl"

            StructuredBuffer<VoxelVert> _Vertices;
            int _BaseVertex;

            Varyings vert(uint id : SV_VertexID)
            {
#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE)
                // BaseVertexLocation is not automatically added to SV_VertexID
                id += _BaseVertex;
#endif
                VoxelVert v = _Vertices[id];
                return VoxelPassVertex(v);
            }
            ENDHLSL
        }
    }
}