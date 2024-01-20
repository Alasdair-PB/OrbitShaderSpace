#ifndef VORONOICUSTOM_INCLUDED
#define VORONOICUSTOM_INCLUDED

void VoronoiCustom_float (float2 UV, float CellDensity , float AngleOffset, out float Out, out float Cells, out float2 Center)
{
	float2 g = floor(UV * CellDensity);
	float2 f = frac(UV * CellDensity);
	float t = 8.0;
	float3 res = float3(8, 0.0, 0.0);
	float toCells; 
	float cell; 

	for(int y=-1; y<=1; y++)
	{
		for(int x=-1; x<=1; x++)
		{
			float2 lattice = float2(x,y);
			float2 sUV = lattice + g;
			float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
			sUV = frac(sin(mul(sUV, m)) * 46839.32);
			float2 offset = float2(sin(sUV.y*+AngleOffset)*0.5+0.5, cos(sUV.x*AngleOffset)*0.5+0.5);
			float d = distance(lattice + offset, f);
			float2 sign = (lattice + offset) - f;
			if(d < res.x)
			{
				res = float3(d, offset.x, offset.y);
				Cells = res.y;

				cell = offset; 
				toCells = sUV; // could be sign
				Center = (offset + lattice + g) / CellDensity;
			}
		}
	}
	// Second pass

	float minEdgeDistance; 
	for(int y=-1; y<=1; y++)
	{
		for(int x=-1; x<=1; x++)
		{
			float2 lattice = float2(x,y);
			float2 sUV = lattice + g;
			float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
			sUV = frac(sin(mul(sUV, m)) * 46839.32);
			float2 offset = float2(sin(sUV.y*+AngleOffset)*0.5+0.5, cos(sUV.x*AngleOffset)*0.5+0.5);
			float d = distance(lattice + offset, f);
			float2 sign = (lattice + offset) - f;

			float2 diffToClosestCell = abs(Cells - toCells);
			bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y < 0.1;
			if(!isClosestCell)
			{
				float2 toCenter = (toCells + sUV) * 0.1;
                float2 cellDifference = normalize(sUV - toCells);
                float edgeDistance = dot(toCenter, cellDifference);
                minEdgeDistance = min(minEdgeDistance, edgeDistance);
			}
		}
	}



	float isBorder = step(res.x, 0.5);
    float3 color = lerp(1, 0, isBorder);
	Out = minEdgeDistance;

}
#endif
