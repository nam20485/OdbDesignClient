using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odb.Client.Viewer
{
    public class GlLayer
    {
        public GlShapePrimitive.List Primitives { get; } = new GlShapePrimitive.List();

        protected GlTexture _texture;



    }
}
