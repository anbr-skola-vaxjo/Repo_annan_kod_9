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
	float cutoff = 0;

	float distance = input.Color.r-cutoff;
	if(distance<0){
		distance=0;
	}
	if(distance>1){
		distance=1;
	}
	
	
	float c_nr;
	float rb_scale;
	if(distance != 1){
		c_nr = distance*distance*distance*distance * 250 + 5;
		rb_scale = distance*distance*2;
		if(rb_scale > 1){
			rb_scale = 1;
		}
	}
	else{
		c_nr = 255;
		rb_scale = 1;
	}
	//float c_nr = 5 + 70*distance;
	float4 color1 = tex2D(SpriteTextureSampler,input.TextureCoordinates);

	/*
	float4 color0 = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	float tc = input.TextureCoordinates.y;

	float2 r_offset = input.TextureCoordinates+0.002*(1-rb_scale);
	float2 g_offset = input.TextureCoordinates-0.002*(1-rb_scale);
	float2 b_offset = input.TextureCoordinates;
	
	color0.r =  tex2D(SpriteTextureSampler,r_offset).r;
	color0.g =  tex2D(SpriteTextureSampler,g_offset).g;
	color0.b =  tex2D(SpriteTextureSampler,b_offset).b;

	color1 = color0;
	*/
	

	for(float i = 0 ; i < c_nr ; i++){
		if(color1.r < (i+1)/c_nr && color1.r > (i)/c_nr){
			color1.r = i/c_nr;
		}

		if(color1.g < (i+1)/c_nr && color1.g > (i)/c_nr){
			color1.g = i/c_nr;
		}

		if(color1.b < (i+1)/c_nr && color1.b > (i)/c_nr){
			color1.b = i/c_nr;
		}
	}

	

	return  color1;
}


technique SpriteDrawing
{
	pass P0
	{
		
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};