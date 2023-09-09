Shader "Custom/Portal"
{
    
    SubShader
    {
        //portal이 다른것 보다 먼저 렌더링
        //geometry queue는 2000
        Tags { "Queue"="Geometry-1" }
        Pass
        {
            Zwrite off
            ColorMask 0
            Cull front
            Stencil{
                //stencil버퍼 값 1로 초기화
                Ref 1
                Comp always
                Pass replace
            }
        }
    }
    
}
