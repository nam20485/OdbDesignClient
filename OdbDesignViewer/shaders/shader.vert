#version 330 core

// vertex data input
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;

// specify a color output to the fragment shader
out vec3 ourColor; 

void main()
{
    gl_Position = vec4(aPosition, 1.0);
    ourColor = aColor;
}