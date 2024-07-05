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
    vec4 newPos = vec4(0.0, 0.0, 0.0, 1.0);
  
    if(aPosition.x == 1 && aPosition.z == 1)
    {
        newPos.x = TriggerZone[0][0];
        newPos.z = TriggerZone[2][0];
    }
    else if(aPosition.x == -1 && aPosition.z == 1)
    {
        newPos.x = TriggerZone[0][1];
        newPos.z = TriggerZone[2][1];
    } 
    else if(aPosition.x == -1 && aPosition.z == -1)
    {
        newPos.x = TriggerZone[0][2];
        newPos.z = TriggerZone[2][2];
    } 
    else if(aPosition.x == 1 && aPosition.z == -1)
    {
        newPos.x = TriggerZone[0][3];
        newPos.z = TriggerZone[2][3];
    }

    if(aPosition.y == 2)
    {
        newPos.y = TriggerZone[1][3];
    }
    else if(aPosition.y == 0)
    {
        newPos.y = TriggerZone[1][0];
    } 

    gl_Position = newPos * view * projection;
}
