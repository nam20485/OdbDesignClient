using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Odb.Client.Viewer
{
    public class GlWindow : GameWindow
    {
        private GlShapePrimitive.List _shapes = new();

        private readonly float[] _vertices =
        {
            // Position             Texture coordinates
            0.5f,  0.5f, 0.0f,      1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f,      1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f  // top left
        };

        //private readonly float[] texCoords =
        //{
        //    0.0f, 0.0f,  // lower-left corner  
        //    1.0f, 0.0f,  // lower-right corner
        //    0.5f, 1.0f   // top-center corner
        //};

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        //private int _hVertexBufferObject;
        //private int _hVertexArrayObject;
        //private int _hElementBufferObject;

        private GlShader _shader;        

        //private GlTexture _texture1;
        //private GlTexture _texture2;

        private readonly string _shadersPath;
        private readonly string _texturesPath;

        private Matrix4 _projection;
        private Matrix4 _view;
        private Matrix4 _model;

        private GlFpsCamera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;        

        //private readonly Stopwatch _timer = new();

        public GlWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string shadersPath, string texturesPath)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _shadersPath = shadersPath;
            _texturesPath = texturesPath;
        }

        private float _time;

        protected override void OnUpdateFrame(FrameEventArgs fe)
        {
            base.OnUpdateFrame(fe);

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 9.0f;
            const float sensitivity = 0.002f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)fe.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)fe.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)fe.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)fe.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)fe.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)fe.Time; // Down
            }

            _time += 0.0001f;

            var yaw = 0.0f;// _time;
            var pitch = _time;
            _camera.Rotate(pitch, yaw);

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                //_lastPos = new Vector2(Size.X / 2, Size.Y / 2);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                //if (mouse.IsButtonDown(MouseButton.Left))
                //{
                //    // rotate around origin
                //    // L/R: rotate around Y axis
                //    //_model *= Matrix4.CreateRotationY(deltaX * sensitivity);

                //    // U/D: rotate around X axis
                //    //_model *= Matrix4.CreateRotationX(deltaY * sensitivity);
                //    _camera.Rotate(deltaX * sensitivity, deltaY * sensitivity);
                //}

                //if (mouse.IsButtonDown(MouseButton.Right))
                //{
                //    _camera.Position -= deltaX * _camera.Right * cameraSpeed * (float)fe.Time; // Left/Right   
                //    _camera.Position += deltaY * _camera.Up * cameraSpeed * (float)fe.Time; // Up/Down                   
                //}                             

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                //_camera.Yaw += deltaX * sensitivity;
                //_camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //_timer.Start();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);           

            //_hVertexArrayObject = GL.GenVertexArray();
            //GL.BindVertexArray(_hVertexArrayObject);

            // create buffer
            //_hVertexBufferObject = GL.GenBuffer();
            // bind buffer to target
            //GL.BindBuffer(BufferTarget.ArrayBuffer, _hVertexBufferObject);
            // add our vertice data to the buffer
            //GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length*sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            //_hElementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _hElementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new GlShader($"{_shadersPath}/shader.vert", $"{_shadersPath}/shader.frag");            
            _shader.Use();

            // tell OpenGL how to interpret our vertex data
            //GL.VertexAttribPointer(vertexAttribLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(vertexAttribLocation);

            //var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            //GL.EnableVertexAttribArray(texCoordLocation);
            //GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            //_texture1 = GlTexture.LoadFromFile($"{_texturesPath}/container.jpg");
            //_texture1.Use(TextureUnit.Texture0);

            //_texture2 = GlTexture.LoadFromFile($"{_texturesPath}/awesomeface.png");
            //_texture2.Use(TextureUnit.Texture1);

            //_shader.SetInt("texture0", 0);
            //_shader.SetInt("texture1", 1);

            //_view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            //_view = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 3.0f),
            //                       new Vector3(0.0f, 0.0f, 0.0f),
            //                       new Vector3(0.0f, 1.0f, 0.0f));           

            //_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / Size.Y, 0.1f, 100.0f);

            _camera = new GlFpsCamera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            //CursorState = CursorState.Grabbed;
            //
            _model = Matrix4.Identity;

            CreateShapes();
            UpdateShapes();
        }

        protected void CreateShapes()
        {
            _shapes.Add(new GlLine(new System.Numerics.Vector3(0.0f, 0.0f, 0.0f), new System.Numerics.Vector3(3.9f, 0.0f, 0.0f)));
            _shapes.Add(new GlLine(new System.Numerics.Vector3(3.9f, 0.0f, 0.0f), new System.Numerics.Vector3(3.9f, 2.9f, 0.0f)));
            _shapes.Add(new GlLine(new System.Numerics.Vector3(3.9f, 2.9f, 0.0f), new System.Numerics.Vector3(0.0f, 2.9f, 0.0f)));
            _shapes.Add(new GlLine(new System.Numerics.Vector3(0.0f, 2.9f, 0.0f), new System.Numerics.Vector3(0.0f, 0.0f, 0.0f)));
        }
        
        protected void UpdateShapes()
        {
            var vertexAttribLocation = _shader.GetAttribLocation("aPosition");
            foreach (var shape in _shapes)
            {
                shape.BindData(vertexAttribLocation);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs fe)
        {
            base.OnRenderFrame(fe);

            //_time += 50.0 * fe.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.BindVertexArray(_hVertexArrayObject);
            
            //_texture1.Use(TextureUnit.Texture0);
            //_texture2.Use(TextureUnit.Texture1);
            _shader.Use();

            //var model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
            //_model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(45.0f));
            //_view = Matrix4.CreateTranslation(0.0f, 0.0f, (float)-_time/100);
            //_view = Matrix4.LookAt(position, position + front, up);
            _view = _camera.GetViewMatrix();
            _projection = _camera.GetProjectionMatrix();

            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", _view);
            _shader.SetMatrix4("projection", _projection);

            foreach (var shape in _shapes)
            {
                shape.Draw();                
            }

            //GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);            
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            SwapBuffers();           
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // map NDC to window
            GL.Viewport(0, 0, e.Width, e.Height);

            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        private const float mouseWheelSensitivity = 8.0f;

        // In the mouse wheel function, we manage all the zooming of the camera.
        // This is simply done by changing the FOV of the camera.
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);            

            _camera.Fov -= e.OffsetY*mouseWheelSensitivity;
        }

        protected override void OnUnload()
        {
            // manually free our buffer
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            //GL.DeleteBuffer(_hVertexBufferObject);
            //GL.DeleteVertexArray(_hVertexArrayObject);

            GL.DeleteProgram(_shader.Handle);                       

            _shader.Dispose();
            //_texture1.Dispose();
            //_texture2.Dispose();

            //_timer.Stop();
            
            foreach(var shape in _shapes)
            {
                shape.Dispose();
            }
            _shapes.Clear();
            
            base.OnUnload();
        }       
    }
}
