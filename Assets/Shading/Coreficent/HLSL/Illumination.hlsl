#ifndef ILLUMINATION_INCLUDED
#define ILLUMINATION_INCLUDED

#include "UnityCG.cginc"

float _ShadeDarkness;
float _ShadeThreshold;
float4 _MainTex_ST;
sampler2D _MainTex;


struct appdata
{
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
    float4 vertex : POSITION;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float3 worldNormal : NORMAL;
    float4 vertex : SV_POSITION;
    UNITY_FOG_COORDS(1)
};

v2f vert (appdata v)
{
    v2f o;

    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    UNITY_TRANSFER_FOG(o,o.vertex);

    return o;
}

fixed4 frag (v2f i) : SV_Target
{
    // calculate coordinates
    fixed4 col = tex2D(_MainTex, i.uv);

    UNITY_APPLY_FOG(i.fogCoord, col);

    // calculate the light intensity using dot product
    return col * (dot(_WorldSpaceLightPos0, normalize(i.worldNormal)) > _ShadeThreshold ? 1 : _ShadeDarkness);
}

#endif