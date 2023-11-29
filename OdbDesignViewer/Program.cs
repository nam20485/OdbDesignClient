using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Odb.Client.Viewer
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length > 1)
            {
                var nativeWindowSettings = new NativeWindowSettings()
                {
                    Size = new Vector2i(800, 600),
                    Title = "ODB Design Client Viewer",
                    // This is needed to run on macos
                    Flags = ContextFlags.ForwardCompatible
                };

                using var game = new Window(GameWindowSettings.Default, nativeWindowSettings, args[0], args[1]);
                game.Run();
                return 0;
            }
            else
            {
                throw new Exception("Invalid arguments: no shaders path specified (use --shaders-path)");
            }            
        }
    }
}