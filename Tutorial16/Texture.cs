using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.OpenGL;
using Avalonia.Platform;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial16
{
    public class Texture
    {
        public Texture(WriteableBitmap image)
        {
            _image = image;
        }

        public void Bind(GlInterface gl, int textureUnit)
        {
            gl.ActiveTexture(textureUnit);
            gl.BindTexture(GL_TEXTURE_2D, _textureBuffer);
        }

        public void Dispose(GlInterface gl)
        {
            gl.BindTexture(GL_TEXTURE_2D, 0);
            gl.DeleteTexture(_textureBuffer);
        }

        public unsafe void Load(GlInterface gl)
        {
            _textureBuffer = gl.GenTexture();
            gl.CheckError();
            gl.BindTexture(GL_TEXTURE_2D, _textureBuffer);
            gl.CheckError();

            using (var buffer = _image.Lock())
            {
                gl.TexImage2D(
                    GL_TEXTURE_2D, 
                    0, 
                    GL_RGBA8, 
                    (int)_image.Size.Width, 
                    (int)_image.Size.Height,
                    0,
                    GL_RGBA,
                    GL_UNSIGNED_BYTE, 
                    buffer.Address);
                gl.CheckError();
            }
            
            gl.TexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
            gl.TexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            gl.TexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            gl.TexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
            
            gl.BindTexture(GL_TEXTURE_2D, 0);
        }

        readonly WriteableBitmap _image;
        int _textureBuffer;
    }
}