// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,5,fgcg:0,5,fgcb:0,5,fgca:1,fgde:0,01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:8714,x:32719,y:32712,varname:node_8714,prsc:2|emission-6353-RGB;n:type:ShaderForge.SFN_Tex2d,id:6353,x:32429,y:32617,ptovrint:False,ptlb:fondo,ptin:_fondo,varname:node_6353,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-506-OUT;n:type:ShaderForge.SFN_TexCoord,id:2922,x:31848,y:32568,varname:node_2922,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:262,x:31793,y:33085,varname:node_262,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6368,x:31979,y:33028,varname:node_6368,prsc:2|A-7484-OUT,B-262-TSL,C-7940-OUT;n:type:ShaderForge.SFN_Vector2,id:7484,x:31547,y:33085,varname:node_7484,prsc:2,v1:0,v2:1;n:type:ShaderForge.SFN_Add,id:506,x:32362,y:32821,varname:node_506,prsc:2|A-2922-UVOUT,B-6368-OUT,C-386-OUT;n:type:ShaderForge.SFN_Vector2,id:8284,x:31542,y:32756,varname:node_8284,prsc:2,v1:1,v2:0;n:type:ShaderForge.SFN_Multiply,id:386,x:31979,y:32852,varname:node_386,prsc:2|A-8284-OUT,B-262-TSL,C-6115-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6115,x:31542,y:32891,ptovrint:False,ptlb:velocidadX,ptin:_velocidadX,varname:node_6115,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:7940,x:31542,y:32995,ptovrint:False,ptlb:velocidadY,ptin:_velocidadY,varname:_velocidadX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:6353-7940-6115;pass:END;sub:END;*/

Shader "Unlit/lava" {
    Properties {
        _fondo ("fondo", 2D) = "white" {}
        _velocidadY ("velocidadY", Float ) = 0
        _velocidadX ("velocidadX", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _fondo; uniform float4 _fondo_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _velocidadX)
                UNITY_DEFINE_INSTANCED_PROP( float, _velocidadY)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float4 node_262 = _Time;
                float _velocidadY_var = UNITY_ACCESS_INSTANCED_PROP( Props, _velocidadY );
                float _velocidadX_var = UNITY_ACCESS_INSTANCED_PROP( Props, _velocidadX );
                float2 node_506 = (i.uv0+(float2(0,1)*node_262.r*_velocidadY_var)+(float2(1,0)*node_262.r*_velocidadX_var));
                float4 _fondo_var = tex2D(_fondo,TRANSFORM_TEX(node_506, _fondo));
                float3 emissive = _fondo_var.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
