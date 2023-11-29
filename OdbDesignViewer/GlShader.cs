using System.Xml.Linq;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Odb.Client.Viewer
{
    public class GlShader : GlHandleObj
    {
        public GlShader(string vertexPath, string fragmentPath)
        {
            // vertex shader: shader.vert
            var vertexShaderSource = File.ReadAllText(vertexPath);
            var hVertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(hVertexShader, vertexShaderSource);
            GL.CompileShader(hVertexShader);
            GL.GetShader(hVertexShader, ShaderParameter.CompileStatus, out var vertResult);
            if (vertResult == 0)
            {
                var infoLog = GL.GetShaderInfoLog(hVertexShader);
                throw new Exception($"Error compiling vertex shader: {infoLog}");
            }

            // fragment shader: shader.frag
            var fragmentShaderSource = File.ReadAllText(fragmentPath);
            var hFragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(hFragmentShader, fragmentShaderSource);
            GL.CompileShader(hFragmentShader);
            GL.GetShader(hFragmentShader, ShaderParameter.CompileStatus, out var fragResult);
            if (fragResult == 0)
            {
                var infoLog = GL.GetShaderInfoLog(hFragmentShader);
                throw new Exception($"Error compiling fragment shader: {infoLog}");
            }

            // link the shader program
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, hVertexShader);
            GL.AttachShader(Handle, hFragmentShader);
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkResult);
            if (linkResult == 0)
            {
                var infoLog = GL.GetProgramInfoLog(Handle);
                throw new Exception($"Error linking shader program: {infoLog}");
            }

            // cleanup
            GL.DetachShader(Handle, hVertexShader);
            GL.DetachShader(Handle, hFragmentShader);
            GL.DeleteShader(hFragmentShader);
            GL.DeleteShader(hVertexShader);
        }

        public void Use()
        {
            var rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(90.0f));
            var scale = Matrix4.CreateScale(0.5f, 0.5f, 0.5f);
            var trans = rotation * scale;

            GL.UseProgram(Handle);

            var location = GL.GetUniformLocation(Handle, "transform");
            GL.UniformMatrix4(location, true, ref trans);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void SetInt(string name, int value)
        {
            var location = GL.GetUniformLocation(Handle, name);
            GL.Uniform1(location, value);
        }

        protected override void Free()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
