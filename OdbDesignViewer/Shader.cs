using System.Reflection.Metadata;

using OpenTK.Graphics.OpenGL4;

namespace Odb.Client.Viewer
{
    public class Shader : IDisposable
    {        
        public int Handle { get; }

        private bool _disposed;

        public Shader(string vertexPath, string fragmentPath)
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
            GL.UseProgram(Handle);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // (set large fields to null)
                GL.DeleteProgram(Handle);                

                _disposed = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Shader()
        {
            // Do not change this code! Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);

            if (! _disposed)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
