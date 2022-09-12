using Avalonia.OpenGL;

using static Avalonia.OpenGL.GlConsts;

namespace Avalonia.OpenGL;

public static class GlExtensions
{
    public static void CheckError(this GlInterface gl)
    {
        int err;
        while ((err = gl.GetError()) != GL_NO_ERROR)
            Console.WriteLine($"{err:X}");
    }
}