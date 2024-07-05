#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 aTexCoord;
layout(location = 3) in vec4 aColor;

out vec2 texCoord;
out vec4 color;

out vec3 Normal_cameraspace;
out vec3 LightDirection_cameraspace;
flat out int NormalIsZero;
//-----------

uniform mat4 RspFixRotation;
uniform vec3 RspFixScale;
uniform vec3 RspFixPosition;

uniform mat4 mRotation;
uniform vec3 mPosition;
uniform vec3 mScale;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 CameraPosition;

void main(void)
{
    texCoord = aTexCoord;
    color = aColor;
   
    mat3 normal_matrix = transpose(inverse(mat3(mRotation)));
    vec3 vertex_normal = normal_matrix * aNormal;
 
    if (length(vertex_normal) > 0)
    {
        vertex_normal = normalize(vertex_normal);
    }
  
    NormalIsZero = 0;
    if(aNormal.x == 0 && aNormal.y == 0 && aNormal.z == 0)
    {
        NormalIsZero = 1;
    }

    vec4 temp1 = vec4(aPosition, 1.0) * vec4(mScale, 1.0);
    vec4 temp2 = temp1 * mRotation;
    vec4 temp3 = temp2.xyzw + vec4(mPosition, 0).xyzw;
    
    vec4 temp4 = temp3 * vec4(RspFixScale, 1.0);
    vec4 temp5 = temp4 * RspFixRotation;
    vec4 temp6 = temp5.xyzw + vec4(RspFixPosition, 0).xyzw;

    vec4 final = temp6;
    gl_Position = final * view * projection;


	// Vector that goes from the vertex to the camera, in camera space.
	// In camera space, the camera is at the origin (0,0,0).
	vec3 vertexPosition_cameraspace = ( view * final).xyz;
	vec3 EyeDirection_cameraspace = vec3(0,0,0) - vertexPosition_cameraspace;

	// Vector that goes from the vertex to the light, in camera space. M is ommited because it's identity.
	vec3 LightPosition_cameraspace = ( view * vec4(CameraPosition,1)).xyz;
	LightDirection_cameraspace = LightPosition_cameraspace + EyeDirection_cameraspace;
	
	// Normal of the the vertex, in camera space
	Normal_cameraspace = ( view * vec4(vertex_normal, 1)).xyz; // Only correct if ModelMatrix does not scale the model ! Use its inverse transpose if not.	
}
