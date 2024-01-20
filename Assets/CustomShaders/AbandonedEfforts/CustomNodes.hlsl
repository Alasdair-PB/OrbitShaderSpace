// Unity Shader custom node

#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

void VectorLerp_float (float4 A, float4 B, float Alpha, out float4 Out){  // maybe float4? 
    Out = lerp(A, B, Alpha);
}

void VectorLerp_float(float3 A, float3 B, float Alpha, out float3 Out) {  // maybe float4? 
    Out = lerp(A, B, Alpha);
}



/*void VectorLerp_Vector3(Vector3 a, Vector3 b, float alpha, out Vector3 Out) {
    Out = lerp(A, B, T);
}*/
#endif