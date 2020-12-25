﻿Shader "Coreficent/Anime"
{
    Properties
    {
        [Toggle(USE_PRECALCULATED_NORMAL)] _PrecalculatedNormal("Use Custom Normals", int) = 0
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineDarkness ("Outline Darkness", Range(0.0, 2.0)) = 0.0
        _OutlineThickness ("Outline Thickness", Range(0.0, 2.0)) = 1.0
        _ShadeDarkness ("Shade Darkness", Range(0.0, 1.0)) = 0.5
        _ShadeThreshold ("Shade Threshold", Range(-1.0, 1.0)) = 0.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "LightMode" = "ForwardBase"
	        "PassFlags" = "OnlyDirectional"
        }

        // normal shading
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "../HLSL/Illumination.hlsl"

            ENDCG
        }

        // outline shading
        Pass
        {
            Cull front
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature USE_PRECALCULATED_NORMAL

            #include "../HLSL/Outline.hlsl"

            ENDCG
        }
    }

    FallBack "Standard"
}
