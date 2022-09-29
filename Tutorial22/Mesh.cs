using System.Numerics;
using System.Runtime.InteropServices;
namespace Tutorial22
{
    internal class Mesh
    {
        public int IndicesCount;
        public int PositionsCount;
        public Matrix4x4 Transform = Matrix4x4.Identity;
        public Material Material = new Material()
        {
            ColorDiffuse = Vector4.Zero,
            ColorAmbient = Vector4.One,
            ColorSpecular = Vector4.Zero,
        };
    }
    internal class Material
    {
        public Vector4 ColorDiffuse;
        public Vector4 ColorAmbient;
        public Vector4 ColorSpecular;
        public float Shininess;
        public float ShininessStrength;
    }
}