using System;
using System.Runtime.CompilerServices;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Odb.Client.Viewer
{
    public abstract class GlShapePrimitive : IDisposable
    {
        private int _hVertexBufferObject;
        protected int _hVertexArrayObject;        

        private bool _disposed;

        protected abstract PrimitiveType PrimitiveType { get; }

        public class List : List<GlShapePrimitive> { }

        protected abstract List<float> GetVertices();

        public abstract void Draw();
       
        public void BindData(int positionAttribLocation)
        {
            _hVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_hVertexArrayObject);
            
            _hVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _hVertexBufferObject);            

            GL.BufferData(BufferTarget.ArrayBuffer, GetVertices().Count * sizeof(float), GetVertices().ToArray(), BufferUsageHint.StaticDraw);

            //var vertexAttribLocation = _shader.GetAttribLocation("aPosition");
            // tell OpenGL how to interpret our vertex data
            GL.VertexAttribPointer(positionAttribLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(positionAttribLocation);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);          
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                Free();
                _disposed = true;
            }
        }

        protected virtual void Free()
        {            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            // Delete all the resources.
            GL.DeleteBuffer(_hVertexBufferObject);
            _hVertexArrayObject = -1;
            GL.DeleteVertexArray(_hVertexArrayObject);
            _hVertexBufferObject = -1;
        }

        ~GlShapePrimitive()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
