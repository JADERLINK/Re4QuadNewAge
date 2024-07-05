#version 330 core

layout(location = 0) in vec3 aPosition;

uniform vec4 PosPlusItemRadius;
// x = pos.x, y = pos.y, z = pos.z, w = ItemTrigggerRadius

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec4 newPos = vec4(0.0, 0.0, 0.0, 1.0);
    newPos.x = (aPosition.x * PosPlusItemRadius.w) + PosPlusItemRadius.x;
    newPos.y = (aPosition.y * PosPlusItemRadius.w) + PosPlusItemRadius.y;
    newPos.z = (aPosition.z * PosPlusItemRadius.w) + PosPlusItemRadius.z;

    gl_Position = newPos * view * projection;
}
