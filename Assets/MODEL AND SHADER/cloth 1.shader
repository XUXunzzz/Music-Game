Shader "Unlit/cloth1"
{
    Properties
    {
        _DiffuseColor("Diffuse Color基本色",color)=(1,1,1,1)
        _Diffuse_Color_Mixing_C("Diffuse Color混合系数",Range(0,1)) = 0.6
        _MetalSpecularLevel("Metal Specular Leve金属高光强度",Range(0,10))=1
        _Saturation("_Saturation基础色贴图饱和度",Range(0,5))=1
        _Overallbrightness("Overall brightness整体亮度",Range(0,5))=1
        _Metalbrightness("Metal brightness金属亮度",Range(0,5))=1
        _OutlineWidth("OutlineWidth描边宽度",Range(0,0.1))=0.002
        _rimoffset("rimoffset边缘光宽度",Range(0,10))=6
        _rimThreshold("rimThreshold边缘光阀门",Range(0,0.1))=0.03
        _rimStrength("rimStrength边缘光强度",Range(0,1))=0.6
        _rimMax("rimMax边缘光最大值",Range(0,1))=0.6
        _fresnelPower("fresnelPower边缘光菲涅尔强度",Range(0,10))=5
        _fresnelclamp("fresnelclamp边缘光菲涅尔钳制",Range(0,1))=0.8
   
        
        _AmbientColor("Ambient Color",color)=(1,1,1,1)
        _ShadowColor("Shadow Color",color)=(1,1,1,1)
        _SpecularColourCast("Specular Colour Cast高光偏色",color)=(1,1,1,1)
     


       
        _MainTex ("MainTex", 2D) = "white" {}
        _SpecularMatCapTex("Specular MatCap Tex",2D)="white"{}
        _MetalMatCapTex("Metal MatCap Tex",2D) = "white"{}

        _ILM("ILM",2D) = "white" {}
        _RampTex("Ramp Tex",2D) = "white" {}

        _RampMapRow0("Ramp Map Row0 采样RampV行数",Range(1,5)) = 1
        _RampMapRow1("Ramp Map Row1 采样RampV行数",Range(1,5)) = 4
        _RampMapRow2("Ramp Map Row2 采样RampV行数",Range(1,5)) = 3
        _RampMapRow3("Ramp Map Row3 采样RampV行数",Range(1,5)) = 5
        _RampMapRow4("Ramp Map Row4 采样RampV行数",Range(1,5)) = 2

        _OutlineWidth("OutlineWidth描边宽度",Range(0,1))=0.05

    
    }
    SubShader
    {
        Lod 100
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull off

            HLSLPROGRAM
         
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            // Material Keywords
           #pragma shader_feature_local_fragment _ALPHATEST_ON
           #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //GPU Instancing
           #pragma multi_compile_instancing
           #pragma multi_compile _ DOTS_INSTANCING_ON

            //Universal Pipeline keywords
            // This is used during shadow map generation to differentiate between directional and punctual light shadows，,as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
         

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
          

            ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags{"LightMode" = "DepthNormals"}
            ZWrite On
            Cull off
            HLSLPROGRAM
            #pragma exclude_renderers gles gles3 glcore
            #pragma target 4.5

            #pragma vertex   DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED#pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A


            //GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitDepthNormalsPass.hlsl"
            ENDHLSL

        }

        Pass
    {
        Name "DrawObject"
        Tags 
        { 
        "RenderPipeline"="UniversalPipeline"
        "RenderType"="Opaque" 
        "LightMode" = "UniversalForward"
        }
       Cull off

            HLSLPROGRAM
            #pragma multi_compile _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _SHADOWS_SOFT

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

        
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
     

            CBUFFER_START(UnityPerMaterial)
            half4  _AmbientColor;
            half4  _DiffuseColor;
            half4  _ShadowColor;
            half4  _SpecularColourCast;
            float4 _MainTex_ST;
            float4 _SpecularMatCapTex_ST;
            float4 _MetalMatCapTex_ST;
            float4 _ILM_ST;
            float4 _RampTex_ST;
            float _RampMapRow0;
            float _RampMapRow1;
            float _RampMapRow2;
            float _RampMapRow3;
            float _RampMapRow4;
            float _Diffuse_Color_Mixing_C;
            float _MetalSpecularLevel;
            float _Saturation;
            float _Overallbrightness;
            float _Metalbrightness;
            float _rimoffset;
            float _rimThreshold;
            float _rimStrength;
            float _rimMax;
            float _fresnelPower;
            float _fresnelclamp;
            float _OutlineWidth;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_SpecularMatCapTex);
            SAMPLER(sampler_SpecularMatCapTex);
             TEXTURE2D(_MetalMatCapTex);
            SAMPLER(sampler_MetalMatCapTex);
            TEXTURE2D(_ILM);
            SAMPLER(sampler_ILM);
            TEXTURE2D(_RampTex);
            SAMPLER(sampler_RampTex);

            struct Attributes
            {
                float4 vertex     : POSITION;
                float2 uv         : TEXCOORD0;
                float3 normalOS   : NORMAL;
                float4 tangentOS  : TANGENT;
            };


            struct Varyings
            {
                float4 pos      : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float3 normalOS : TEXCOORD1;
                float4 positionNDC : TEXCOORD3;   
                float3 positionWS : TEXCOORD2;
                float3 positionVS : TEXCOORD4;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
                output.pos = TransformObjectToHClip(input.vertex.xyz);
                output.uv =  TRANSFORM_TEX(input.uv,_MainTex);
                output.normalOS = input.normalOS;
                output.positionNDC = vertexInput.positionNDC;
                output.positionWS = vertexInput.positionWS;
                output.positionVS = vertexInput.positionVS;

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                //float3 V = normalize(mul(UNITY_MATRIX_V, float4(normalWS,0)).xyz);
                float3 V = normalize(mul((float3x3)UNITY_MATRIX_I_V,input.positionVS * (-1)));

                //float3 normalVS = normalize(TransformWorldToView(normalWS));
                //mul(GetWorldToViewMatrix(), float4(positionWS, 1.0)).xyz;
                //UNITY_MATRIX_V
               //float3 normalVS = normalize(mul(UNITY_MATRIX_V, float4(normalWS, 0)).xyz);
                float3 normalVS = normalize(mul((float3x3)UNITY_MATRIX_V,normalWS));
                float2 MatCapUV = normalVS.xy * 0.5 + 0.5;
                float3 NoV = dot(normalWS,V);
                //MatCapUV = TRANSFORM_TEX(MatCapUV,_SpecularMatCapTex);//备注掉了，没出问题
                Light mainLight = GetMainLight(); 
                real4 MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                /*饱和度
                fixed luminance = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
                fixed3 luminanceColor = fixed3(luminance, luminance, luminance);
                finalColor = lerp(luminanceColor, finalColor, _Saturation);
                */                               
                float  luminance = 0.2125 * MainTex.r + 0.7154 * MainTex.g + 0.0721 * MainTex.b;
                float3 luminanceColor = float3(luminance, luminance, luminance);
                float3 finalColor = float3(MainTex.r,MainTex.g,MainTex.b);
                finalColor = lerp(luminanceColor, finalColor, _Saturation);
                real4 SpecularMatCapTex = SAMPLE_TEXTURE2D(_SpecularMatCapTex, sampler_SpecularMatCapTex, MatCapUV);
                real4 MetalMatCapTex = SAMPLE_TEXTURE2D(_MetalMatCapTex, sampler_MetalMatCapTex, MatCapUV);
                //(x - t1) / (t2 - t1) * (s2 - s1) + s1;
                SpecularMatCapTex =  saturate((SpecularMatCapTex - 0)/(1-0)*(1 - 0.6) + 0.6); //材质捕捉，非blinn_phong高光方案（探索），这里是重映射
                SpecularMatCapTex = pow(SpecularMatCapTex,3);//次方
                real4 lightColor = real4(mainLight.color,1); 
                //real3 normalWS = normalize(normalWS); 
                real3 lightDir = normalize(mainLight.direction); 
                real  lightAten = saturate(dot(lightDir,normalWS));
                real  halflambert = lightAten * 0.5 + 0.5;  //半兰伯特
                real  powhalflambert = pow(halflambert,2); //pow半兰伯特

                half4 BaseColor = _AmbientColor; 
                BaseColor =  saturate(lerp(BaseColor,BaseColor * _DiffuseColor,_Diffuse_Color_Mixing_C));
                BaseColor = BaseColor * MainTex;


                real4 ilm = SAMPLE_TEXTURE2D(_ILM, sampler_ILM, input.uv);
               

                //ilm
                float matEunm0 = 0.0; //Hand = 0.0
                float matEunm1 = 0.3; //Soft = 0.3
                float matEunm2 = 0.5; //Metal = 0.5
                float matEunm3 = 0.7; //Silk = 0.7
                float matEunm4 = 1.0; //Skin = 1.0

                float ramp0 = _RampMapRow0 / 10  -  0.05;
                float ramp1 = _RampMapRow1 / 10  -  0.05;
                float ramp2 = _RampMapRow2 / 10  -  0.05;
                float ramp3 = _RampMapRow3 / 10  -  0.05;
                float ramp4 = _RampMapRow4 / 10  -  0.05;

                float dayRampV = lerp(ramp4,ramp3,step(ilm.a,(matEunm4 + matEunm3)/2));
                dayRampV = lerp(dayRampV,ramp2,step(ilm.a,(matEunm3 + matEunm2)/2));
                dayRampV = lerp(dayRampV,ramp1,step(ilm.a,(matEunm2 + matEunm1)/2));
                dayRampV = lerp(dayRampV,ramp0,step(ilm.a,(matEunm1 + matEunm0)/2));
                float nightRampV = dayRampV + 0.5;

                //float rampClampMin = 0.003;
                //float rampClampMax = 0.997;//0.2,0.55//
                /*float remap(float x, float t1, float t2, float s1, float s2)
                {
                  return (x - t1) / (t2 - t1) * (s2 - s1) + s1;
                }
                */

                float CustomLightPow =  dot(lightDir,normalWS)+0.955;
                float CustomLightPowForNPRMetal = step(0.5,CustomLightPow) * 0.5 + 0.5;
                //float CustomLightPow = clamp(lightAten,-0.997,0.997);
                //float rampGrayU = clamp(smoothstep(0,1,CustomLightPow),0.003,0.997);
                float rampGrayU = clamp(smoothstep(0,1,CustomLightPow),0.03,0.997); //重映射，可以考虑把0-0.5的值都开放
                float2 rampDayGrayUV = float2(rampGrayU,1 - dayRampV);
                float2 rampNightGrayUV = float2(rampGrayU,1 - nightRampV);

                //float rampDarkU = 0.005;//黑色阴影部分，采到最低颜色即可
                float2 rampDayDarkUV = float2(0.005,1 - dayRampV);
                float2 rampNightDarkUV = float2(0.05,1 - nightRampV);

                float isDay = (lightDir.y + 1) / 2; //lightDir.y = -1 to 1 -> 1 to 2 -> 0.5 -1,看看怎么调好看，这里是isDay混合系数
                float3 rampGaryColor = lerp(SAMPLE_TEXTURE2D(_RampTex , sampler_RampTex , rampNightGrayUV),SAMPLE_TEXTURE2D(_RampTex , sampler_RampTex , rampDayGrayUV),isDay);
                float3 rampDarkColor = lerp(SAMPLE_TEXTURE2D(_RampTex , sampler_RampTex , rampNightDarkUV),SAMPLE_TEXTURE2D(_RampTex , sampler_RampTex , rampDayDarkUV),isDay);

                float CustomLightPow_Step = clamp(smoothstep(0.8,1,CustomLightPow),0,1);



                float3 GaryShadowColor = (BaseColor) * (rampGaryColor) * _ShadowColor ;
                float3 DarkShadowColor = (BaseColor) * (rampDarkColor) * _ShadowColor ;

               

                float3 diffuse = lerp(GaryShadowColor*1.5,BaseColor,CustomLightPow_Step);
                diffuse = lerp(DarkShadowColor*1.5,diffuse,saturate(ilm.g*2));
                diffuse = lerp(diffuse,BaseColor,saturate(ilm.g-0.5)*2);
           
                //金属处理

                float isMetal = step(0.7,ilm.r);//诺艾尔的ilm.r金属，其他的要调
                float MetalDetails = clamp( ilm.b * _MetalSpecularLevel,0.35,1);

                MetalMatCapTex = clamp(MetalMatCapTex,0,1);
                float3 NPRMetal = MetalMatCapTex*1.2+0.2 * isMetal ;
                //滤色公式（Screen） C = 1-( (1-A) * (1-B) )// SpecularMatCapTex // MetalMatCapTex
                NPRMetal = 1 - ((1 - NPRMetal) * (1 - SpecularMatCapTex));
                NPRMetal = NPRMetal * MetalDetails * MainTex.rgb * _SpecularColourCast * CustomLightPowForNPRMetal * _Metalbrightness *isMetal;
                 //  NPRMetal = NPRMetal  * MainTex.rgb * _SpecularColourCast * CustomLightPowForNPRMetal * _Metalbrightness;
                

                diffuse = lerp(diffuse*2,NPRMetal,isMetal);
                diffuse = _Overallbrightness*diffuse;

                float2 screenUV = input.positionNDC.xy/input.positionNDC.w;
                float rawDepth = SampleSceneDepth(screenUV) ;
                float linearDepth = LinearEyeDepth( rawDepth , _ZBufferParams);
                float2 screenOffset = float2(lerp(-1, 1, step(0 , normalVS.x))* _rimoffset /_ScreenParams.x / max(1,pow(linearDepth,2)),0);
                float offsetDepth = SampleSceneDepth(screenUV + screenOffset);
                float offsetLinearDepth = LinearEyeDepth(offsetDepth,_ZBufferParams);
                float rim = saturate(offsetLinearDepth - linearDepth);
                rim = step(_rimThreshold,rim) * clamp(rim * _rimStrength,0, _rimMax);

                //float fresnelPower = 6;
               // float fresnelclamp = 0.8;

                float fresnel = 1 - saturate(NoV);
                fresnel = pow(fresnel, _fresnelPower);
                fresnel = fresnel * _fresnelclamp +(1 - _fresnelclamp); 
             //   float alpha = _Alpha * baseTex.a * toonTex.a * sphereTex.a;
             //   alpha = saturate(min(max( IsFacing,_DoubleSided), alpha));
             //   float4 col = float4(albedo,alpha );
               
             //   clip(col.a - 0.5);


                //float4 col = float4(diffuse,alpha);
               float4 col = float4(1,1,1,1);
               col.rgb = fresnel * rim;
    
               col.rgb = (col.rgb*diffuse*5) + diffuse;
               clip(col.a - 0.5);
              //step(0.5,1,col.rgb);

               // col.rgb = MixFog(col.rgb,i.fooord);        
        /*
        Skin = 1.0
        Silk = 0.7
        Metal = 0.5
        Soft = 0.3
        Hand = 0.0
        */             
               // NoV = saturate(NoV); 
                // pow(NoV);
                //NoV = NoV *2;
                return float4(col);
                //powhalflambert+0.2
                //float4(rampGaryColor,1);
            }
            ENDHLSL
        }
           Pass
        {
            Name "Outline"
            Tags 
           { 
           "RenderPipeline"="UniversalPipeline"
           "RenderType"="Opaque" 
           }
           Cull front

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)

            float _OutlineWidth;

            CBUFFER_END
          

        
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

          

            v2f vert (appdata v)
            {
                   v2f o;
              o.vertex = TransformObjectToHClip(v.vertex);
              float3 worldNormal = TransformObjectToWorldNormal(v.normal);
              float3 ndcNormal = normalize(mul(UNITY_MATRIX_VP, worldNormal)) * o.vertex.w;

               o.vertex.xy += ndcNormal.xy * _OutlineWidth *_ScreenParams.y*rcp(_ScreenParams.xy);

              float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
              
               worldPos += worldNormal *_OutlineWidth;
    
              //相机到物体的距离
             float dist = distance(unity_ObjectToWorld._m03_m13_m23, _WorldSpaceCameraPos);
           

           float4 farVertex =   TransformWorldToHClip(worldPos);
  
           //0.2 is magical number
               o.vertex = lerp(o.vertex, farVertex, saturate(dist*0.2));
               return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return float4(0,0,0,1);
            }
            ENDHLSL
        }

   
        
    }
}
