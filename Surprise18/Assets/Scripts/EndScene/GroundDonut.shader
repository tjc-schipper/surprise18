// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Surprise/GroundDonut"
{
	Properties
	{
		_NoiseTwo("NoiseTwo", 2D) = "white" {}
		_NoiseOne("NoiseOne", 2D) = "black" {}
		_RampTex("RampTex", 2D) = "white" {}
		_ScrollDelta("ScrollDelta", Range( -1 , 1)) = 0
		_ScrollSpeed("ScrollSpeed", Float) = 0
		_DonutAlpha("DonutAlpha", 2D) = "white" {}
		_SideScroll("SideScroll", Float) = 0
		_Brightness("Brightness", Range( 0 , 10)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _NoiseOne;
			uniform float _SideScroll;
			uniform float _ScrollSpeed;
			uniform float4 _NoiseOne_ST;
			uniform sampler2D _NoiseTwo;
			uniform float _ScrollDelta;
			uniform float4 _NoiseTwo_ST;
			uniform sampler2D _DonutAlpha;
			uniform float4 _DonutAlpha_ST;
			uniform sampler2D _RampTex;
			uniform float _Brightness;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float2 appendResult17 = (float2(_SideScroll , _ScrollSpeed));
				float2 uv_NoiseOne = i.ase_texcoord.xy * _NoiseOne_ST.xy + _NoiseOne_ST.zw;
				float2 panner11 = ( 1.0 * _Time.y * appendResult17 + uv_NoiseOne);
				float2 appendResult18 = (float2(( _SideScroll * -1.0 ) , ( _ScrollSpeed * ( 1.0 + _ScrollDelta ) )));
				float2 uv_NoiseTwo = i.ase_texcoord.xy * _NoiseTwo_ST.xy + _NoiseTwo_ST.zw;
				float2 panner12 = ( 1.0 * _Time.y * appendResult18 + uv_NoiseTwo);
				float2 uv_DonutAlpha = i.ase_texcoord.xy * _DonutAlpha_ST.xy + _DonutAlpha_ST.zw;
				float4 temp_output_7_0 = ( tex2D( _NoiseOne, panner11 ) * tex2D( _NoiseTwo, panner12 ) * tex2D( _DonutAlpha, uv_DonutAlpha ).r );
				float2 appendResult9 = (float2(temp_output_7_0.rg));
				
				
				finalColor = ( ( temp_output_7_0 * tex2D( _RampTex, appendResult9 ) ) * _Brightness );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15401
381;832;1634;770;876.799;398.9888;1.748044;True;True
Node;AmplifyShaderEditor.RangedFloatNode;14;-1249.976,511.9655;Float;False;Property;_ScrollDelta;ScrollDelta;3;0;Create;True;0;0;False;0;0;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1189.057,325.7615;Float;False;Property;_ScrollSpeed;ScrollSpeed;4;0;Create;True;0;0;False;0;0;-0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-967.2219,441.8514;Float;False;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1226.408,796.6734;Float;False;Property;_SideScroll;SideScroll;6;0;Create;True;0;0;False;0;0;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-817.7982,415.4154;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1510.703,19.30592;Float;True;Property;_NoiseTwo;NoiseTwo;0;0;Create;True;0;0;False;0;None;2a13bf2c333ecd14db8262ef97804bdc;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-988.6737,784.437;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-1516.233,-222.2398;Float;True;Property;_NoiseOne;NoiseOne;1;0;Create;True;0;0;False;0;None;22db466dcd827c24ab4381f5953ad0ba;False;black;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;-892.5103,-69.633;Float;False;FLOAT2;4;0;FLOAT;-0.1;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1247.8,133.4638;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1225.275,-135.9288;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;18;-841.9366,201.6261;Float;False;FLOAT2;4;0;FLOAT;0.1;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;12;-679.8699,131.5124;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;11;-689.0651,-132.8502;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;6;-291.0184,-226.5912;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-293.7766,25.79553;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-283.4437,551.061;Float;True;Property;_DonutAlpha;DonutAlpha;5;0;Create;True;0;0;False;0;None;75b58aea817e0ee4f8f04f339fe0eee1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;120.8006,-45.92105;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;9;159.4173,400.9276;Float;False;FLOAT2;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;337.3294,371.9651;Float;True;Property;_RampTex;RampTex;2;0;Create;True;0;0;False;0;None;ef731474274a4b14e8d0651d51bafd73;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;259.4297,731.9957;Float;False;Property;_Brightness;Brightness;7;0;Create;True;0;0;False;0;0;2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;560.0939,-26.65541;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;911.4502,-14.41913;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1238.472,-23.12337;Float;False;True;2;Float;ASEMaterialInspector;0;1;Surprise/GroundDonut;0770190933193b94aaa3065e307002fa;0;0;SubShader 0 Pass 0;2;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;-1;False;-1;-1;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Opaque;Queue=Transparent;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;16;1;14;0
WireConnection;15;0;13;0
WireConnection;15;1;16;0
WireConnection;23;0;22;0
WireConnection;17;0;22;0
WireConnection;17;1;13;0
WireConnection;4;2;2;0
WireConnection;3;2;1;0
WireConnection;18;0;23;0
WireConnection;18;1;15;0
WireConnection;12;0;4;0
WireConnection;12;2;18;0
WireConnection;11;0;3;0
WireConnection;11;2;17;0
WireConnection;6;0;1;0
WireConnection;6;1;11;0
WireConnection;5;0;2;0
WireConnection;5;1;12;0
WireConnection;7;0;6;0
WireConnection;7;1;5;0
WireConnection;7;2;20;1
WireConnection;9;0;7;0
WireConnection;8;1;9;0
WireConnection;21;0;7;0
WireConnection;21;1;8;0
WireConnection;24;0;21;0
WireConnection;24;1;25;0
WireConnection;0;0;24;0
ASEEND*/
//CHKSM=439A06164B9B81810F939121D1F636D3ACEC16C2