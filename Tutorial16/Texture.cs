using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.OpenGL;
using static Avalonia.OpenGL.GlConsts;

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
        
        public void Load(GlInterface gl)
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
        }
        
        readonly WriteableBitmap _image;
        int _textureBuffer;
    }
}