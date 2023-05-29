#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_5_0
	#define PS_SHADERMODEL ps_5_0
#endif

Texture2D SpriteTexture;

uniform float4 u_in = float4(0,0,0,0);

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
	//500
	//500
	
	float sharpnes	= 20;
	float s_min = 0.5;

	float2 Window = float2(1366,768);

	float4 color0;
	float4 color1 = float4(0,0,0,0);
	
	
	float bright1 = 0;

	float bright2 = 0;

	float bright3 = 0;

	float bright4 = 0;


	float2 POS = input.TextureCoordinates;
	POS[0] *= Window[0];
	POS[1] *= Window[1];

	float k[3][3];

	k[0][0] = 1; k[1][0] = 0; k[2][0] = -1;
	k[0][1] = 2; k[1][1] = 0; k[2][1] = -2;
	k[0][2] = 1; k[1][2] = 0; k[2][2] = -1;

	float k2[3][3];

	k2[0][0] =-1; k2[1][0] =-2; k2[2][0] =-1;
	k2[0][1] = 0; k2[1][1] = 0; k2[2][1] = 0;
	k2[0][2] = 1; k2[1][2] = 2; k2[2][2] = 1;

	for(float i = -1 ; i <= 1 ; i++){
		for(float j = -1 ; j <= 1 ; j++){
			if(i + POS[0] >= 0 && i + POS[0] <= Window[0] && j + POS[1] >= 0 && j + POS[1] <= Window[1]){
				float2 POS_2 =  float2((POS[0] + i)/Window[0] , (POS[1] + j)/Window[1]);
				float4 c = tex2D(SpriteTextureSampler,POS_2);
				

				bright1 += (c.r + c.g + c.b)/3 * k[i+1][j+1];
				bright2 += (c.r + c.g + c.b)/3 * k[i+1][j+1]*-1;

				bright3 += (c.r + c.g + c.b)/3 * k2[i+1][j+1];
				bright4 += (c.r + c.g + c.b)/3 * k2[i+1][j+1]*-1;


			}
		}	
	}
	bright1 *= sharpnes;
	bright2 *= sharpnes;
	bright3 *= sharpnes;
	bright4 *= sharpnes;
	
	if(bright1 < s_min){
		bright1 = 0;
	}
	if(bright2 < s_min){
		bright2 = 0;
	}
	if(bright3 < s_min){
		bright3 = 0;
	}
	if(bright4 < s_min){
		bright4 = 0;
	}

	float bright = bright1 + bright2 + bright3 + bright4 ;
	
	if(bright > 1){
		bright = 1;
	}

	bright*=bright;
	
	color1 = float4(bright,bright,bright,1);

	color0 = tex2D(SpriteTextureSampler,input.TextureCoordinates);

	


	color0 =  color1;
	

	//color0 = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	//color0 = color1

	color0.a = 1;
	return  color0 * input.Color;
}


technique SpriteDrawing
{
	pass P0
	{
		
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};