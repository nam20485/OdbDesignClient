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
            // Position        Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f,  // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly float[] texCoords =
        {
            0.0f, 0.0f,  // lower-left corner  
            1.0f, 0.0f,  // lower-right corner
            0.5f, 1.0f   // top-center corner
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _hVertexBufferObject;
        private int _hVertexArrayObject;
        private int _hElementBufferObject;

        private GlShader _shader;
        private readonly string _shadersPath;

        private GlTexture _texture1;
        private GlTexture _texture2;
        private string _texturesPath;

        //private readonly Stopwatch _timer = new();

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string shadersPath, string texturesPath)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _shadersPath = shadersPath;
            _texturesPath = texturesPath;
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

            //_timer.Start();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _hVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_hVertexArrayObject);

            // create buffer
            _hVertexBufferObject = GL.GenBuffer();
            // bind buffer to target
            GL.BindBuffer(BufferTarget.ArrayBuffer, _hVertexBufferObject);
            // add our vertice data to the buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length*sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _hElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _hElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new GlShader($"{_shadersPath}/shader.vert", $"{_shadersPath}/shader.frag");            
            _shader.Use();
           
            var vertexAttribLocation = _shader.GetAttribLocation("aPosition");
            // tell OpenGL how to interpret our vertex data
            GL.VertexAttribPointer(vertexAttribLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexAttribLocation);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture1 = GlTexture.LoadFromFile($"{_texturesPath}/container.jpg");
            _texture1.Use(TextureUnit.Texture0);

            _texture2 = GlTexture.LoadFromFile($"{_texturesPath}/awesomeface.png");
            _texture2.Use(TextureUnit.Texture1);

            _shader.SetInt("texture0", 0);
            _shader.SetInt("texture1", 1);
        }        

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(_hVertexArrayObject);
            
            _texture1.Use(TextureUnit.Texture0);
            _texture2.Use(TextureUnit.Texture1);
            _shader.Use();

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);            

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
            _texture1.Dispose();
            _texture2.Dispose();

            //_timer.Stop();            
            
            base.OnUnload();
        }
    }
}
