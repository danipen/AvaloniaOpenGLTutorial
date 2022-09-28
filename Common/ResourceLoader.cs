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
            return ImportFromResource("bus.fbx");
        }

        public static Scene LoadCarModel()
        {
            return ImportFromResource("car.fbx");
        }

        public static Scene LoadHelicopterModel()
        {
            return ImportFromResource("helicopter.fbx");
        }

        public static Scene LoadLanciaIntegraleModel()
        {
            return ImportFromResource("integraleHF.fbx");
        }

        public static Scene LoadDeathStarModel()
        {
            return ImportFromResource("deathstar.fbx");
        }

        public static Scene LoadSphereModel()
        {
            return ImportFromResource("sphere.stl");
        }

        public static Scene LoadCoyoteModel()
        {
            return ImportFromResource("coyote.glb");
        }

        public static string LoadFragmentShader(string resourceLocation)
        {
            using Stream fileStream = typeof(ResourceLoader)
                .GetTypeInfo()
                .Assembly
                .GetManifestResourceStream(resourceLocation + "fragment.shader")!;

            using StreamReader reader = new StreamReader(fileStream);

            return reader.ReadToEnd();
        }

        public static string LoadVertexShader(string resourceLocation)
        {
            using Stream fileStream = typeof(ResourceLoader)
                .GetTypeInfo()
                .Assembly
                .GetManifestResourceStream(resourceLocation + "fragment.shader")!;

            using StreamReader reader = new StreamReader(fileStream);

            return reader.ReadToEnd();
        }

        static Scene ImportFromResource(string resourceName)
        {
            using (var imp = new AssimpContext())
            {
                Stream fileStream = LoadFile(resourceName);

                return imp.ImportFileFromStream(fileStream, PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.FlipWindingOrder | PostProcessSteps.JoinIdenticalVertices);
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