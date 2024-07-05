#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 PlaneZone;
// x = Corner0.x, y = pos.Y, z = Corner0.z, w = ?
// x = Corner1.x, y = pos.Y, z = Corner1.z, w = ?
// x = Corner2.x, y = pos.Y, z = Corner2.z, w = ?
// x = Corner3.x, y = pos.Y, z = Corner3.z, w = ?

uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    vec4 newPos = vec4(0.0, 0.0, 0.0, 1.0);
  
    if(aPosition.x == 1 && aPosition.z == 1)
    {
        newPos.x = PlaneZone[0][0];
        newPos.y = PlaneZone[1][0];
        newPos.z = PlaneZone[2][0];
    }
    else if(aPosition.x == -1 && aPosition.z == 1)
    {
        newPos.x = PlaneZone[0][1];
        newPos.y = PlaneZone[1][1];
        newPos.z = PlaneZone[2][1];
    } 
    else if(aPosition.x == -1 && aPosition.z == -1)
    {
        newPos.x = PlaneZone[0][2];
        newPos.y = PlaneZone[1][2];
        newPos.z = PlaneZone[2][2];
    } 
    else if(aPosition.x == 1 && aPosition.z == -1)
    {
        newPos.x = PlaneZone[0][3];
        newPos.y = PlaneZone[1][3];
        newPos.z = PlaneZone[2][3];
    }

    gl_Position = newPos * view * projection;
}
