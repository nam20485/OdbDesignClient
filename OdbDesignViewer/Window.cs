using System.Diagnostics;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Odb.Client.Viewer
{
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
            0.5f,  0.5f, 0.0f,  // top right
            0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f  // top left
        };

        private readonly uint[] _indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3  // second triangle
        };

        private int _hVertexBufferObject;
        private int _hVertexArrayObject;
        private int _hElementBufferObject;

        private Shader _shader;

        private readonly string _shadersPath;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string shadersPath)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _shadersPath = shadersPath;
        }     

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Get the state of the keyboard this frame
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);                        

            // create buffer
            _hVertexBufferObject = GL.GenBuffer();
            // bind buffer to target
            GL.BindBuffer(BufferTarget.ArrayBuffer, _hVertexBufferObject);
            // add our vertice data to the buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length*sizeof(float), _vertices, BufferUsageHint.StaticDraw);           

            _hVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_hVertexArrayObject);                       

            // tell OpenGL how to interpret our vertex data
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _hElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _hElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new Shader($"{_shadersPath}/shader.vert", $"{_shadersPath}/shader.frag");
            _shader.Use();

            _timer.Start();
        }

        private readonly Stopwatch _timer = new();

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            // update the uniform color
            var timeValue = _timer.Elapsed.TotalSeconds;
            var greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
            var vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

            GL.BindVertexArray(_hVertexArrayObject);              
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();           
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // map NDC to window
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            // manually free our buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_hVertexBufferObject);
            GL.DeleteVertexArray(_hVertexArrayObject);

            GL.DeleteProgram(_shader.Handle);                       

            _shader.Dispose();

            _timer.Stop();            
            
            base.OnUnload();
        }
    }
}
