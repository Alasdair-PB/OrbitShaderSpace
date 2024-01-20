Shader "Tessellation" {
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    { 
        _MainTex("Base (RGB)", 2D) = "white" {}
        _DispTex("Disp Texture", 2D) = "gray" {}
        _NormalMap("Normalmap", 2D) = "bump" {}
        _Displacement("Displacement", Range(0, 1.0)) = 0.3
        _Color("Color", color) = (1,1,1,0)
        _SpecColor("Spec color", color) = (0.5,0.5,0.5,0.5)
    }

            // The SubShader block containing the Shader code. 
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 300

        Pass
        {
                
            HLSLPROGRAM // The HLSL code block. Unity SRP uses the HLSL language.
            #pragma vertex vert // This line defines the name of the vertex shader. 
            #pragma fragment frag // This line defines the name of the fragment shader. 

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            
            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
            struct Attributes //
            {
                // The positionOS variable contains the vertex positions in object space.
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct Varyings
            {
                // The positions in this struct must have the SV_POSITION semantic.
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);

            SAMPLER(sampler_MainTex);

            // To make the Unity shader SRP Batcher compatible, declare all properties related to a Material in a a single CBUFFER block with  the name UnityPerMaterial.
            CBUFFER_START(UnityPerMaterial) 
                half4 _Color;  
                float4 _MainTex_ST;
            CBUFFER_END

            // The vertex shader definition with properties defined in the Varyings structure. The type of the vert function must match the type (struct) that it returns.
            Varyings vert(Attributes IN)
            {
                // Declaring the output object (OUT) with the Varyings struct.
                Varyings OUT; 
                // The TransformObjectToHClip function transforms vertex positions from object space to homogenous space
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                // The TRANSFORM_TEX macro performs the tiling and offset transformation.
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

                return OUT;
            }

            // The fragment shader definition.            
            half4 frag(Varyings IN) : SV_Target
            {
                //half4 customColor;
                //customColor = half4(0.5, 0, 0, 1);
                //return customColor;

                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                return color;
                // return _BaseColor;

            }

            /*SAMPLER(sampler_DisapTex);
            SAMPLER(sampler_Displacement);

            void disp(Appdata IN)
            {
                float d = tex2Dlod(sampler_DisapTex, float4(IN.texcoord.xy, 0, 0)).r * sampler_Displacement;
                IN.vertex.xyz += IN.normal * d;
            }
             //
            struct Input {
                float2 uv_MainTex;
            };

            SAMPLER(_MainTex);
            SAMPLER(_NormalMap);
            float4(_Color);

            void surf(Input IN, inout SurfaceOutput o) {
                half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
                o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            }*/


            ENDHLSL
        }
    }
}