// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X
// source Amplify Shader Editor network: https://www.daniildemchenko.com/fastfood-interior
Shader "FastFood/sh_Flipbook_01"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Columns("Columns", Float) = 2
		_Rows("Rows", Float) = 2
		_Speed("Speed", Float) = 1
		_StartTime("StartTime", Int) = 0
		_Ditail("Ditail", 2D) = "white" {}
		_Power("Power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{


		Tags { "RenderType"="Opaque" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0



		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _MainTex;
			uniform float _Columns;
			uniform float _Rows;
			uniform float _Speed;
			uniform int _StartTime;
			uniform sampler2D _Ditail;
			uniform float4 _Ditail_ST;
			uniform float _Power;

			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;

				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv5 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				// *** BEGIN Flipbook UV Animation vars ***
				// Total tiles of Flipbook Texture
				float fbtotaltiles3 = _Columns * _Rows;
				// Offsets for cols and rows of Flipbook Texture
				float fbcolsoffset3 = 1.0f / _Columns;
				float fbrowsoffset3 = 1.0f / _Rows;
				// Speed of animation
				float fbspeed3 = _Time[ 1 ] * _Speed;
				// UV Tiling (col and row offset)
				float2 fbtiling3 = float2(fbcolsoffset3, fbrowsoffset3);
				// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
				// Calculate current tile linear index
				float fbcurrenttileindex3 = round( fmod( fbspeed3 + (float)_StartTime, fbtotaltiles3) );
				fbcurrenttileindex3 += ( fbcurrenttileindex3 < 0) ? fbtotaltiles3 : 0;
				// Obtain Offset X coordinate from current tile linear index
				float fblinearindextox3 = round ( fmod ( fbcurrenttileindex3, _Columns ) );
				// Multiply Offset X by coloffset
				float fboffsetx3 = fblinearindextox3 * fbcolsoffset3;
				// Obtain Offset Y coordinate from current tile linear index
				float fblinearindextoy3 = round( fmod( ( fbcurrenttileindex3 - fblinearindextox3 ) / _Columns, _Rows ) );
				// Reverse Y to get tiles from Top to Bottom
				fblinearindextoy3 = (int)(_Rows-1) - fblinearindextoy3;
				// Multiply Offset Y by rowoffset
				float fboffsety3 = fblinearindextoy3 * fbrowsoffset3;
				// UV Offset
				float2 fboffset3 = float2(fboffsetx3, fboffsety3);
				// Flipbook UV
				half2 fbuv3 = uv5 * fbtiling3 + fboffset3;
				// *** END Flipbook UV Animation vars ***
				float2 uv_Ditail = i.ase_texcoord.xy * _Ditail_ST.xy + _Ditail_ST.zw;


				finalColor = ( tex2D( _MainTex, fbuv3 ) * tex2D( _Ditail, uv_Ditail ) * _Power );
				return finalColor;
			}
			ENDCG
		}
	}

}
