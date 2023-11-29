#version 330 core

// fragment color output
out vec4 FragColor;

// the input variable from the vertex shader (same name and same type)
in vec4 vertexColor;

// we set this variable in the OpenGL code.
uniform vec4 ourColor;

void main()
{
    FragColor = ourColor;
}