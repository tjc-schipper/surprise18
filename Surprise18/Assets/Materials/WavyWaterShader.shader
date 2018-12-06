// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Surprise/Water"
{
	Properties
	{
		_NoiseOne("NoiseOne", 2D) = "white" {}
		_Speed("Speed", Float) = 0.2
		_WaterColor("WaterColor", Color) = (0,0,0,0)
		_Color0("Color 0", Color) = (0,0,0,0)
		_Float0("Float 0", Float) = 0.84
		_WaveHeight("WaveHeight", Range( 0 , 1)) = 0
		_NoiseTwo("NoiseTwo", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NoiseOne;
		uniform float _Speed;
		uniform float4 _NoiseOne_ST;
		uniform sampler2D _NoiseTwo;
		uniform float4 _NoiseTwo_ST;
		uniform float _WaveHeight;
		uniform float4 _WaterColor;
		uniform float4 _Color0;
		uniform float _Float0;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float temp_output_13_0 = ( _Time.y * _Speed );
			float2 uv_NoiseOne = v.texcoord.xy * _NoiseOne_ST.xy + _NoiseOne_ST.zw;
			float2 panner8 = ( temp_output_13_0 * float2( 1,0 ) + uv_NoiseOne);
			float2 uv_NoiseTwo = v.texcoord.xy * _NoiseTwo_ST.xy + _NoiseTwo_ST.zw;
			float2 panner4 = ( temp_output_13_0 * float2( 0,1 ) + uv_NoiseTwo);
			float4 temp_output_10_0 = ( tex2Dlod( _NoiseOne, float4( panner8, 0, 0.0) ) * tex2Dlod( _NoiseTwo, float4( panner4, 0, 0.0) ) );
			v.vertex.xyz += ( float4( ase_vertexNormal , 0.0 ) * temp_output_10_0 * _WaveHeight ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_13_0 = ( _Time.y * _Speed );
			float2 uv_NoiseOne = i.uv_texcoord * _NoiseOne_ST.xy + _NoiseOne_ST.zw;
			float2 panner8 = ( temp_output_13_0 * float2( 1,0 ) + uv_NoiseOne);
			float2 uv_NoiseTwo = i.uv_texcoord * _NoiseTwo_ST.xy + _NoiseTwo_ST.zw;
			float2 panner4 = ( temp_output_13_0 * float2( 0,1 ) + uv_NoiseTwo);
			float4 temp_output_10_0 = ( tex2D( _NoiseOne, panner8 ) * tex2D( _NoiseTwo, panner4 ) );
			float4 lerpResult27 = lerp( _WaterColor , _Color0 , saturate( temp_output_10_0 ).r);
			o.Albedo = lerpResult27.rgb;
			o.Smoothness = _Float0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
370;726;1566;1022;844.1544;374.538;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;11;-1172.729,555.3761;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;0.2;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;23;-1321.083,161.5345;Float;True;Property;_NoiseTwo;NoiseTwo;6;0;Create;True;0;0;False;0;None;fd5cd720e5df5054eacfdfa6808bf23e;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleTimeNode;12;-1170.893,441.5578;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;3;-1324.451,-52.75483;Float;True;Property;_NoiseOne;NoiseOne;0;0;Create;True;0;0;False;0;e24b2c680edaa90458d31f11544d79ca;fd5cd720e5df5054eacfdfa6808bf23e;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1090.077,193.9539;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-976.3004,487.4523;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-1093.083,14.53455;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;4;-784.2399,197.903;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;8;-798.2969,3.077852;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-573,164.5;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-575,-56.5;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;141.1949,79.2639;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;25;55.98932,-309.8787;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;14;446.0664,784.8378;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;26;-177.7875,-467.7362;Float;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0,0,0;0.3208438,0.6239977,0.6603774,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;-178.7678,-639.161;Float;False;Property;_WaterColor;WaterColor;2;0;Create;True;0;0;False;0;0,0,0,0;0.07008723,0.2803821,0.3301887,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;369.6774,1060.408;Float;False;Property;_WaveHeight;WaveHeight;5;0;Create;True;0;0;False;0;0;0.226;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;27;225.2126,-489.8356;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;948.8348,68.42645;Float;False;Property;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.84;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;615.0664,925.2372;Float;False;3;3;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1163.277,-21.87633;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Surprise/Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;2;23;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;24;2;3;0
WireConnection;4;0;5;0
WireConnection;4;1;13;0
WireConnection;8;0;24;0
WireConnection;8;1;13;0
WireConnection;2;0;23;0
WireConnection;2;1;4;0
WireConnection;1;0;3;0
WireConnection;1;1;8;0
WireConnection;10;0;1;0
WireConnection;10;1;2;0
WireConnection;25;0;10;0
WireConnection;27;0;16;0
WireConnection;27;1;26;0
WireConnection;27;2;25;0
WireConnection;15;0;14;0
WireConnection;15;1;10;0
WireConnection;15;2;22;0
WireConnection;0;0;27;0
WireConnection;0;4;17;0
WireConnection;0;11;15;0
ASEEND*/
//CHKSM=5D5E808546979466BD89D3E378B8C4952ABFD742