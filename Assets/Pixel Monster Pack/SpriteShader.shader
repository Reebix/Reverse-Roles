Shader "Custom/SpriteColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _WhiteFactor ("White Factor", Range(0, 1)) = 0
        _DissolveFactor ("Dissolve Factor", Range(0, 1)) = 0
        _RemoveFactor ("Remove Factor", Range(0, 1)) = 0
    }
    SubShader
    {
        Cull Off
        Blend One OneMinusSrcAlpha
        
        Tags{"Queue" = "Transparent"}
    Pass{
        ZTest Off
        Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct v2f
            {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 _Color;
            float _WhiteFactor;
            float _DissolveFactor;
            float _RemoveFactor;

            
            
            fixed4 frag (v2f i) : SV_Target
            {
                    _WhiteFactor += _RemoveFactor *2;
                _WhiteFactor = clamp(_WhiteFactor, 0, 1);
                if(_RemoveFactor >= 0.5)
                {
                    _DissolveFactor += (_RemoveFactor - 0.45) * 2;
                }
                
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
              
                
                col.rgb *= col.a;
                col.rgb = lerp(col.rgb, 1, _WhiteFactor);
                col.a *=1- smoothstep(1-_DissolveFactor, 1-_DissolveFactor + 0.05, i.uv.y);
             
                return col;
            }
            
            ENDCG
        }
    }
}
