#ifndef FBM_INCLUDED
#define FBM_INCLUDED

#include "FbmFile.cginc"

void Fbm_float (float2 uv, float2 _Time, float _octaves, float _color1, float _color2, out float3 fbmColor)
{
	float f = fbmFile(uv+fbmFile(5*uv + _Time.y, _octaves), _octaves);
	fbmColor = lerp(_color1, _color2, 2*f);
}

#endif
