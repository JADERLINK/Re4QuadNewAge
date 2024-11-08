#version 330 core

layout(location = 0) in vec3 aPosition;

uniform vec3 Position;

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec4 newPos = vec4(aPosition + Position, 1.0);
    gl_Position = newPos * view * projection;
}
