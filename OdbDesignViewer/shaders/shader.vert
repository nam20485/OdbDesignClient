#version 330 core

// vertex data input
layout (location = 0) in vec3 aPosition;

// specify a color output to the fragment shader
out vec4 vertexColor; 

void main()
{
    gl_Position = vec4(aPosition, 1.0);
    vertexColor = vec4(0.5, 0.0, 0.0, 1.0); // set the output variable to a dark-red color
}