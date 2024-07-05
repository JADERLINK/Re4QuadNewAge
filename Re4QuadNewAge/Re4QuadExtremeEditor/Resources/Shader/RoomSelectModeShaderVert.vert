#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 mRotation;
uniform vec3 mPosition;
uniform vec3 mScale;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec4 temp1 = vec4(aPosition, 1.0) * vec4(mScale, 1.0);
    vec4 temp2 = temp1 * mRotation;
    vec4 temp3 = temp2.xyzw + vec4(mPosition, 0).xyzw;
    gl_Position = temp3 * view * projection;
}
