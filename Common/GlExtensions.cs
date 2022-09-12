using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.OpenGL;

using static Avalonia.OpenGL.GlConsts;

public static class GlExtensions
{
    public static void CheckError(
        this GlInterface gl,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        int err;
        while ((err = gl.GetError()) != GL_NO_ERROR)
            Console.WriteLine($"{sourceFilePath}.{memberName}:{sourceLineNumber} 0x{err:X}");
    }

    public static string GetShader(GlVersion glVersion, bool fragment, string shader)
    {
        int version = (glVersion.Type == GlProfileType.OpenGL ?
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? 150 : 120 :
            100);
        string data = "#version " + version + "\n";
        if (glVersion.Type == GlProfileType.OpenGLES)
            data += "precision mediump float;\n";
        if (version >= 150)
        {
            shader = shader.Replace("attribute", "in");
            if (fragment)
                shader = shader
                    .Replace("varying", "in")
                    .Replace("//DECLAREGLFRAG", "out vec4 outFragColor;")
                    .Replace("gl_FragColor", "outFragColor");
            else
                shader = shader.Replace("varying", "out");
        }

        data += shader;

        return data;
    }
}