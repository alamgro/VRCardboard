Shader "Unlit/Transparent Color"
{
    Properties
    {
        _Color ("Main Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent"}

        ZWrite Off
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            Color[_Color]
        }
    }
}
