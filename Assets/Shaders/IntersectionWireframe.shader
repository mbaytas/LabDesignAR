// chrismflynn.wordpress.com/2012/09/06/fun-with-shaders-and-the-depth-buffer/
// answers.unity3d.com/questions/857662/how-to-add-true-transparancy-to-this-wireframe-sha.html
// codeflow.org/entries/2012/aug/02/easy-wireframe-display-with-barycentric-coordinates/

Shader "LabDesignAR/IntersectionWireframe" 
{
    Properties 
    {
        _WireThickness("Wire Thickness", float) = 1
        _WireColor("Wire Color", Color) = (1,0,0,1)
        _FillColor("Fill Color", Color) = (0,0,0,0)
        _HighlightColor("Highlight Color", Color) = (0, 1, 0, .5)
        _HighlightThresholdMax("Highlight Threshold Max", Float) = 1
    }

    SubShader 
	{
		Pass
		{
			Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
			Blend SrcAlpha OneMinusSrcAlpha 
			ZWrite Off

			CGPROGRAM
				#pragma target 5.0
				#pragma vertex vert
				#pragma geometry geom
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform float _WireThickness;
				uniform float4 _WireColor;
				uniform float4 _FillColor;

				struct v2g
				{
					float4 pos : SV_POSITION; // vertex position
					float2 uv : TEXCOORD0; // vertex uv coordinate
				};

				struct g2f
				{
					float4 pos : SV_POSITION; // fragment position
					float2 uv : TEXCOORD0;	// fragment uv coordinate
					float3 dist : TEXCOORD1;	// distance to each edge of the triangle
				};
				
				v2g vert(appdata_base v)
				{
					v2g output;
					output.pos =  UnityObjectToClipPos(v.vertex);
					output.uv = v.texcoord;

					return output;
				}
				
				[maxvertexcount(3)]
				void geom(triangle v2g p[3], inout TriangleStream<g2f> triStream)
				{
					//points in screen space
					float2 p0 = _ScreenParams.xy * p[0].pos.xy / p[0].pos.w;
					float2 p1 = _ScreenParams.xy * p[1].pos.xy / p[1].pos.w;
					float2 p2 = _ScreenParams.xy * p[2].pos.xy / p[2].pos.w;
					
					//edge vectors
					float2 v0 = p2 - p1;
					float2 v1 = p2 - p0;
					float2 v2 = p1 - p0;

					//area of the triangle
				 	float area = abs(v1.x*v2.y - v1.y * v2.x);

					//values based on distance to the edges
					float dist0 = area / length(v0);
					float dist1 = area / length(v1);
					float dist2 = area / length(v2);
					
					g2f OUT;
					
					//add the first point
					OUT.pos = p[0].pos;
					OUT.uv = p[0].uv;
					OUT.dist = float3(dist0,0,0);
					triStream.Append(OUT);

					//add the second point
					OUT.pos =  p[1].pos;
					OUT.uv = p[1].uv;
					OUT.dist = float3(0,dist1,0);
					triStream.Append(OUT);
					
					//add the third point
					OUT.pos = p[2].pos;
					OUT.uv = p[2].uv;
					OUT.dist = float3(0,0,dist2);
					triStream.Append(OUT);
				}
				
				float4 frag(g2f IN) : COLOR
				{			
					//find the smallest distance
					float d = min(IN.dist.x, min(IN.dist.y, IN.dist.z));
					
					// calculate power to 2 to thin the line
					float fade = exp2(-1/_WireThickness * d * d);

					return lerp(_FillColor, _WireColor, fade);
				}
			ENDCG
		}

		Pass
		{
			Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
			Blend SrcAlpha OneMinusSrcAlpha 
			ZWrite Off
			
			CGPROGRAM
				#pragma target 5.0
				#pragma vertex vert
				#pragma geometry geom
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform float _WireThickness;
				uniform float4 _WireColor;
				uniform float4 _FillColor;

				struct v2g
				{
					float4 pos : SV_POSITION; // vertex position
					float2 uv : TEXCOORD0; // vertex uv coordinate
				};

				struct g2f
				{
					float4 pos : SV_POSITION; // fragment position
					float2 uv : TEXCOORD0;	// fragment uv coordinate
					float3 dist : TEXCOORD1;	// distance to each edge of the triangle
				};
				
				v2g vert(appdata_base v)
				{
					v2g output;
					output.pos =  UnityObjectToClipPos(v.vertex);
					output.uv = v.texcoord;

					return output;
				}
				
				[maxvertexcount(3)]
				void geom(triangle v2g p[3], inout TriangleStream<g2f> triStream)
				{
					//points in screen space
					float2 p0 = _ScreenParams.xy * p[0].pos.xy / p[0].pos.w;
					float2 p1 = _ScreenParams.xy * p[1].pos.xy / p[1].pos.w;
					float2 p2 = _ScreenParams.xy * p[2].pos.xy / p[2].pos.w;
					
					//edge vectors
					float2 v0 = p2 - p1;
					float2 v1 = p2 - p0;
					float2 v2 = p1 - p0;

					//area of the triangle
				 	float area = abs(v1.x*v2.y - v1.y * v2.x);

					//values based on distance to the edges
					float dist0 = area / length(v0);
					float dist1 = area / length(v1);
					float dist2 = area / length(v2);
					
					g2f OUT;
					
					//add the first point
					OUT.pos = p[0].pos;
					OUT.uv = p[0].uv;
					OUT.dist = float3(dist0,0,0);
					triStream.Append(OUT);

					//add the second point
					OUT.pos =  p[1].pos;
					OUT.uv = p[1].uv;
					OUT.dist = float3(0,dist1,0);
					triStream.Append(OUT);
					
					//add the third point
					OUT.pos = p[2].pos;
					OUT.uv = p[2].uv;
					OUT.dist = float3(0,0,dist2);
					triStream.Append(OUT);
				}
				
				float4 frag(g2f IN) : COLOR
				{			
					//find the smallest distance
					float d = min(IN.dist.x, min(IN.dist.y, IN.dist.z));
					
					// calculate power to 2 to thin the line
					float fade = exp2(-1/_WireThickness * d * d);

					return lerp(_FillColor, _WireColor, fade);
				}
			ENDCG
		}
	} 
}