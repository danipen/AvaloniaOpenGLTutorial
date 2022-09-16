using System.Reflection;
using Assimp;
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

        public static Scene LoadBusModel()
        {
            using (var imp = new AssimpContext())
            {
                Stream fileStream = LoadFile("bus.fbx");
                return imp.ImportFileFromStream(fileStream);
            }
        }

        public static Scene LoadCarModel()
        {
            using (var imp = new AssimpContext())
            {
                Stream fileStream = LoadFile("car.fbx");
                return imp.ImportFileFromStream(fileStream);
            }
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

        static Stream LoadFile(string fileName)
        {
            return typeof(ResourceLoader).GetTypeInfo().Assembly.GetManifestResourceStream(ContentPrefix + fileName);
        }

        const string ContentPrefix = "Common.Content.";
    }
}