#version 330 core

// fragment color output
out vec4 FragColor;
// the input variable from the vertex shader (same name and same type)
in vec3 ourColor;

void main()
{
    FragColor = vec4(ourColor, 1.0);
}