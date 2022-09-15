using System.Reflection;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Common
{
    public static class ResourceLoader
    {
        public static WriteableBitmap LoadTestTexture()
        {
            return LoadImage("test.png");
        }
        
        static WriteableBitmap LoadImage(string fileName)
        {
            Stream stream = typeof(ResourceLoader).GetTypeInfo().Assembly.GetManifestResourceStream(ContentPrefix + fileName);
            
            if (stream == null)
                return null;

            using (stream)
            {
                return WriteableBitmap.Decode(stream);
            }
        }
        
        const string ContentPrefix = "Common.Content.";
    }
}