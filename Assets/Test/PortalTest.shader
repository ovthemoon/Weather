Shader "Unlit/PortalTest"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags { "Queue"="Geometry-1" }

        Pass
        {
            Zwrite off
            ColorMask 0
            Cull front
            Stencil
{

       Ref 1
Comp always
Pass replace
}
        }
    }
}
