#version 330

out vec4 outputColor;

in vec2 texCoord;
in vec4 color;

in vec3 Normal_cameraspace;
in vec3 LightDirection_cameraspace;
flat in int NormalIsZero;

uniform sampler2D texture0;
uniform sampler2D texture1;

uniform vec4 matColor;
uniform bool EnableNormals;
uniform bool EnableVertexColors;
uniform bool EnableAlphaChannel;

void main()
{
	vec3 vnormal = Normal_cameraspace;

    if(gl_FrontFacing)
    {
        vnormal = Normal_cameraspace * -1;
    }

    vec4 texColor = texture(texture0, texCoord);
    vec4 texAlpha = texture(texture1, texCoord);

	float alphaValue = 1.0;

	if(EnableVertexColors)
	{
		texColor = texColor * matColor * color;
		texAlpha = texAlpha * matColor * color;
	}

	if(EnableAlphaChannel)
	{
		alphaValue = texAlpha.a;
	}

    //ambient
    vec4 ambient_result = vec4(texColor.r, texColor.g, texColor.b, alphaValue);
    if(ambient_result.a <= 0.03)
    {
        discard;
    }

	// Light emission properties
	// You'll probably want to put them as uniforms
	vec3 LightColor = vec3(1,1,1);
	float LightPower = 50.0f;
	
	// Material properties
	vec3 MaterialDiffuseColor = texColor.rgb;
	vec3 MaterialAmbientColor = vec3(0.1,0.1,0.1) * MaterialDiffuseColor;

	// Distance to the light
	float distance = 7;

	// Normal of the computed fragment, in camera space
	vec3 n = normalize( vnormal ); //Normal_cameraspace
	// Direction of the light (from the fragment to the light)
	vec3 l = normalize( LightDirection_cameraspace );
	// Cosine of the angle between the normal and the light direction, 
	// clamped above 0
	//  - light is at the vertical of the triangle -> 1
	//  - light is perpendicular to the triangle -> 0
	//  - light is behind the triangle -> 0
	float cosTheta = clamp( dot( n,l ), 0,1 );
	
	outputColor = vec4(
		// Ambient : simulates indirect lighting
		MaterialAmbientColor +
		// Diffuse : "color" of the object
		MaterialDiffuseColor * LightColor * LightPower * cosTheta / (distance*distance)
        , alphaValue);

    if(!EnableNormals || NormalIsZero != 0) // sem uso de normal
	{
 		outputColor = ambient_result;
	}

}