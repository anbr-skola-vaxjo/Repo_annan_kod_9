#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_5_0
	#define PS_SHADERMODEL ps_5_0
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	
	float4 color0 = float4(0,0,0,0);
	

	float2 Window = float2(1366,768);

	float2 POS = input.TextureCoordinates;
	POS[0] *= Window[0];
	POS[1] *= Window[1];

	float distance = input.Color.r;
	
	float o =  1 + 39 *(1-distance);
	float k_width = 10;

	

	float count = 0;
	
	for(float i = -k_width ; i <= k_width ; i++){
		for(float j = -k_width ; j <= k_width ; j++){
			if(i + POS[0] >= 0 && i + POS[0] <= Window[0] && j + POS[1] >= 0 && j + POS[1] <= Window[1]){
				
				float e = 2.71828;
				//e = 2.71828^(-(x*x+y*y)/(2*o*o))
				float potens = ((i*i+j*j)/(2*o*o));
				
				for(float aa = 0 ; aa < potens ; aa++){
					e = e*e;
				}
				e = 1/e;
				
				float blur = 1/(2*o*o)*e;
				
				
				
				float2 POS_2 =  float2((POS[0] + i)/Window[0] , (POS[1] + j)/Window[1]);
				float4 c = tex2D(SpriteTextureSampler,POS_2)*blur;
				
				color0 += c;
				
				count+= blur;
			}
		}	
	}
	
	
	color0 = color0/count;

	return color0 * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};