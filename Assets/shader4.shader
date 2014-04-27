Shader "Custom/Shader4" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _MyColor ("Main Color", COLOR) = (1,1,1,1) 
	}
	SubShader {
		Tags { "Queue"="Geometry" "RenderType"="Opaque" }
		LOD 200
        Pass { 
            Material { 
                Diffuse [_MyColor] 
                Ambient [_MyColor] 
            } 
            Lighting On 
            ZTest NotEqual
            SetTexture [_MainTex] { 
                    Combine texture * primary DOUBLE 
            }
        }
	} 
	FallBack "Diffuse"
}
