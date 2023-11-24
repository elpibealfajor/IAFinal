// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HoneyCombShield"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_speed("speed", Float) = -0.03
		_tile("tile", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			half ASEVFace : VFACE;
			float4 screenPosition2;
		};

		uniform sampler2D _TextureSample0;
		uniform float _speed;
		uniform float _tile;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 vertexPos2 = ase_vertex3Pos;
			float4 ase_screenPos2 = ComputeScreenPos( UnityObjectToClipPos( vertexPos2 ) );
			o.screenPosition2 = ase_screenPos2;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 color7 = IsGammaSpace() ? float4(0,1,0.9902081,0) : float4(0,1,0.9778692,0);
			o.Emission = color7.rgb;
			float4 appendResult18 = (float4(0.0 , _speed , 0.0 , 0.0));
			float2 temp_cast_2 = (_tile).xx;
			float2 uv_TexCoord17 = i.uv_texcoord * temp_cast_2;
			float2 panner16 = ( _Time.y * appendResult18.xy + uv_TexCoord17);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV3 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode3 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV3, 5.0 ) );
			float switchResult6 = (((i.ASEVFace>0)?(fresnelNode3):(0.0)));
			float4 ase_screenPos2 = i.screenPosition2;
			float4 ase_screenPosNorm2 = ase_screenPos2 / ase_screenPos2.w;
			ase_screenPosNorm2.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm2.z : ase_screenPosNorm2.z * 0.5 + 0.5;
			float screenDepth2 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm2.xy ));
			float distanceDepth2 = abs( ( screenDepth2 - LinearEyeDepth( ase_screenPosNorm2.z ) ) / ( 1.0 ) );
			o.Alpha = ( ( ( 1.0 - tex2D( _TextureSample0, panner16 ) ) * saturate( ( ( 1.0 - ase_vertex3Pos.y ) - 0.7 ) ) ) + switchResult6 + saturate( ( 1.0 - distanceDepth2 ) ) ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;513.6;1205;270;217.7723;-221.7971;1.680515;False;False
Node;AmplifyShaderEditor.RangedFloatNode;21;-716.8782,-81.15076;Inherit;False;Property;_tile;tile;2;0;Create;True;0;0;0;False;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-674.6912,48.13228;Inherit;False;Property;_speed;speed;1;0;Create;True;0;0;0;False;0;False;-0.03;-0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-548.1311,-98.84226;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;14;-507.3028,176.0545;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;18;-480.0859,26.3583;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PosVertexDataNode;22;-102.2459,69.79315;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;16;-276.0816,-99.20821;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PosVertexDataNode;10;-318.0285,268.8056;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;24;67.59524,117.2018;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;27;212.4702,117.7239;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-54.14831,-130.1853;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;9f68f5cda7b156d438ab21b15cc8b536;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;2;-103.9194,267.7426;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;365.87,117.7242;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;25;254.8953,-68.89828;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;3;-173.1375,547.9485;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;9;163.4233,265.9325;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwitchByFaceNode;6;168.0887,548.765;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;495.2263,22.84626;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;11;350.7726,259.2058;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;719.2119,259.1251;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;7;566.4929,-173.0891;Inherit;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;0;False;0;False;0,1,0.9902081,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1046.216,-5.827458;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;HoneyCombShield;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;21;0
WireConnection;18;1;19;0
WireConnection;16;0;17;0
WireConnection;16;2;18;0
WireConnection;16;1;14;0
WireConnection;24;0;22;2
WireConnection;27;0;24;0
WireConnection;1;1;16;0
WireConnection;2;1;10;0
WireConnection;28;0;27;0
WireConnection;25;0;1;0
WireConnection;9;0;2;0
WireConnection;6;0;3;0
WireConnection;23;0;25;0
WireConnection;23;1;28;0
WireConnection;11;0;9;0
WireConnection;8;0;23;0
WireConnection;8;1;6;0
WireConnection;8;2;11;0
WireConnection;0;2;7;0
WireConnection;0;9;8;0
ASEEND*/
//CHKSM=96FE1BE010C4B89A975A9B30D038C6AC89B419FC