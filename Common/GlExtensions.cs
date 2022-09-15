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

    public unsafe static void Uniform1i(this GlInterface glInterface, int location, int value)
    {
        const string EntryPoint = "glUniform1i";
        
        IntPtr procAddress = glInterface.GetProcAddress(EntryPoint);

        if (procAddress == IntPtr.Zero)
            throw new ArgumentException("Entry point not found: " + EntryPoint);
        
        var uniform1iDelegate = (delegate* unmanaged[Stdcall]<int,int,void>)procAddress;

        uniform1iDelegate(location, value);
    }

    public unsafe static void FrontFace(this GlInterface glInterface, int val)
    {
        const string EntryPoint = "glFrontFace";
        
        IntPtr procAddress = glInterface.GetProcAddress(EntryPoint);

        if (procAddress == IntPtr.Zero)
            throw new ArgumentException("Entry point not found: " + EntryPoint);
        
        var glFrontFaceDelegate = (delegate* unmanaged[Stdcall]<int,void>)procAddress;

        glFrontFaceDelegate(val);
    }

    public unsafe static void CullFace(this GlInterface glInterface, int val)
    {
        const string EntryPoint = "glCullFace";
        
        IntPtr procAddress = glInterface.GetProcAddress(EntryPoint);

        if (procAddress == IntPtr.Zero)
            throw new ArgumentException("Entry point not found: " + EntryPoint);
        
        var glCullFaceDelegate = (delegate* unmanaged[Stdcall]<int,void>)procAddress;

        glCullFaceDelegate(val);
    }

    public unsafe static void TexParameterf(this GlInterface glInterface, int target, int pName, float value)
    {
        const string EntryPoint = "glTexParameterf";
        
        IntPtr procAddress = glInterface.GetProcAddress(EntryPoint);

        if (procAddress == IntPtr.Zero)
            throw new ArgumentException("Entry point not found: " + EntryPoint);
        
        var glTexParameterfDelegate = (delegate* unmanaged[Stdcall]<int,int,float,void>)procAddress;

        glTexParameterfDelegate(target, pName, value);
    }
}