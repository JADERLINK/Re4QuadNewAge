#version 330

uniform vec4 mColor;

flat in int _discard;

void main()
{
    if(_discard != 0)
    {
        discard;
    }

    gl_FragColor = mColor;
}