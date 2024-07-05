#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 RspFixRotation;
uniform vec3 RspFixPosition;

uniform vec3 MinBoundary;
uniform vec3 MaxBoundary;

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec3 newPos = vec3(0,0,0);
  
    if(aPosition.x == 1)
    {
        newPos.x = MaxBoundary.x;
    }
    else if(aPosition.x == -1)
    {
        newPos.x = MinBoundary.x;
    } 

    if(aPosition.z == 1)
    {
        newPos.z = MaxBoundary.z;
    }
    else if(aPosition.z == -1)
    {
        newPos.z = MinBoundary.z;
    } 

    if(aPosition.y == 2)
    {
        newPos.y = MaxBoundary.y;
    }
    else if(aPosition.y == 0)
    {
        newPos.y = MinBoundary.y;
    } 

    vec4 temp = vec4(newPos, 1.0) * RspFixRotation;
    vec4 final = temp.xyzw + vec4(RspFixPosition, 0).xyzw;

    gl_Position = final * view * projection;
}
