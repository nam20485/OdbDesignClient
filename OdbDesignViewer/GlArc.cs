using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace Odb.Client.Viewer
{
    public class GlArc : GlShapePrimitive
    {
        protected override PrimitiveType PrimitiveType => throw new NotImplementedException();

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        protected override List<float> GetVertices()
        {
            throw new NotImplementedException();
        }
    }
}
