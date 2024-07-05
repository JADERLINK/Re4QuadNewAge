#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 TriggerZone;
// x = Corner0.x, y = TrueY, z = Corner0.z, w = ?
// x = Corner1.x, y = MoreHeight, z = Corner1.z, w = ?
// x = Corner2.x, y = CircleRadius, z = Corner2.z, w = ?
// x = Corner3.x, y = MoreHeight + TrueY, z = Corner3.z, w = ?

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec4 newPos = vec4(aPosition, 1.0);
    newPos.x = newPos.x * TriggerZone[1][2];
    newPos.z = newPos.z * TriggerZone[1][2];
    
    if(aPosition.y == 1)
    {
       newPos.y = newPos.y * TriggerZone[1][1];
    }
    
    newPos.x = newPos.x + TriggerZone[0][0];
    newPos.z = newPos.z + TriggerZone[2][0];
    newPos.y = newPos.y + TriggerZone[1][0];
  
    gl_Position = newPos * view * projection;
}
