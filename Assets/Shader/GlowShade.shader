Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0.001, 0.1)) = 0.01
    }
 
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _OutlineColor;
        float _OutlineThickness;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Sample the main texture
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;

            // Calculate the depth difference between the object and surrounding pixels
            float outline = 1.0 - saturate(dot(IN.viewDir, float3(0, 0, -1)));

            // Apply the outline effect
            o.Emission = _OutlineColor * outline * _OutlineThickness;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
