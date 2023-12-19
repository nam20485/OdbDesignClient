using System;
using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace Odb.Client.Viewer
{
    public class GlLine : GlShapePrimitive
    {
        public Vector3 Start { get; }
        public Vector3 End { get; }
        //public Vector3 Color { get; set; }

        protected override PrimitiveType PrimitiveType => PrimitiveType.Lines;

        public GlLine(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
        }

        public GlLine(Vector2 start, Vector2 end)
        {
            Start = new Vector3(start, 0.0f);
            End = new Vector3(end, 0.0f);
        }

        protected override List<float> GetVertices()
        {
            return new List<float>()
            {
                Start.X, Start.Y, Start.Z,
                End.X, End.Y, End.Z
            };
        }

        public override void Draw()
        {
            //glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "MVP"), 1, GL_FALSE, &MVP[0][0]);
            //glUniform3fv(glGetUniformLocation(shaderProgram, "color"), 1, &lineColor[0]);

            GL.BindVertexArray(_hVertexArrayObject);
            GL.DrawArrays(PrimitiveType, 0, GetVertices().Count / 2);
        }
    }
}
