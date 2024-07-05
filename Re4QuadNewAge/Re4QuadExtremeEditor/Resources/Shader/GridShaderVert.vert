#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 RspFixRotation;
uniform float PositionY;
uniform int SpaceBetween;
uniform int Flip;

uniform mat4 view;
uniform mat4 projection;

flat out int _discard;

void main(void)
{   
    _discard = 0;

    vec4 pos =  vec4(aPosition, 1.0);
    pos.x *= SpaceBetween;

    if(aPosition.x == 3276)
    {
        pos.x = 3276;
    }

    if(pos.x > 3276)
    {
        _discard = 1;
    }

    if(Flip != 0)
    {
        pos.x *= -1;
    }

    vec4 final = pos * RspFixRotation;
    final.y = PositionY;

    gl_Position = final * view * projection;
}


