// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HoneyCombShieldExtraLayer"
{
	Properties
	{
		_NoiseScale("NoiseScale", Float) = 0
		_NoiseTiling("NoiseTiling", Vector) = (1,5.4,0,0)
		_NoiseSpeed("NoiseSpeed", Float) = -0.33
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _NoiseSpeed;
		uniform float2 _NoiseTiling;
		uniform float _NoiseScale;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 color10 = IsGammaSpace() ? float4(0.3632075,0.9131644,1,0) : float4(0.1085262,0.8137144,1,0);
			o.Emission = color10.rgb;
			float4 appendResult3 = (float4(_NoiseSpeed , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord4 = i.uv_texcoord * _NoiseTiling;
			float2 panner6 = ( _Time.y * appendResult3.xy + uv_TexCoord4);
			float simplePerlin2D8 = snoise( panner6*_NoiseScale );
			simplePerlin2D8 = simplePerlin2D8*0.5 + 0.5;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			o.Alpha = ( step( simplePerlin2D8 , 0.22 ) * saturate( ( ( 1.0 - ase_vertex3Pos.y ) - 0.7 ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;708;1506;291;1145.719;-72.74094;1.3;True;True
Node;AmplifyShaderEditor.Vector2Node;2;-1946.854,161.4689;Inherit;False;Property;_NoiseTiling;NoiseTiling;1;0;Create;True;0;0;0;False;0;False;1,5.4;1.49,9.74;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;1;-1877.756,298.9151;Inherit;False;Property;_NoiseSpeed;NoiseSpeed;2;0;Create;True;0;0;0;False;0;False;-0.33;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;3;-1669.207,289.9079;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1741.878,148.4328;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1675.344,462.2449;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;12;-1049.962,394.9445;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;6;-1446.077,149.7886;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1415.976,282.3701;Inherit;False;Property;_NoiseScale;NoiseScale;0;0;Create;True;0;0;0;False;0;False;0;3.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;13;-880.1206,442.3531;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;8;-1130.472,149.0181;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1.89;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;14;-735.2456,442.8752;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;9;-841.3033,153.8762;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.22;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;15;-581.8458,442.8755;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-404.4348,-192.4265;Inherit;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;0;False;0;False;0.3632075,0.9131644,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-383.7935,282.7621;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;HoneyCombShieldExtraLayer;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;4;0;2;0
WireConnection;6;0;4;0
WireConnection;6;2;3;0
WireConnection;6;1;5;0
WireConnection;13;0;12;2
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;14;0;13;0
WireConnection;9;0;8;0
WireConnection;15;0;14;0
WireConnection;16;0;9;0
WireConnection;16;1;15;0
WireConnection;0;2;10;0
WireConnection;0;9;16;0
ASEEND*/
//CHKSM=E2C14050A69BF8452FD9A67D2CCF2D149405C964