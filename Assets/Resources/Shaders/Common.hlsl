struct VoxelVert   // same layout as VoxelDrawVert
{
    float2 vertex   : POSITION;
    float2 uv       : TEXCOORD0;
    uint   color    : TEXCOORD1; // gets reordered when using COLOR semantics
};

struct Varyings
{
    float4 vertex   : SV_POSITION;
    float2 uv       : TEXCOORD0;
    half4  color    : COLOR;
};