using System.Numerics;
using System.Runtime.InteropServices;

namespace Tutorial22
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Mesh
    {
        public uint[] Indices;
        public Vertex[] Vertices;

        internal void Transform(Matrix4x4 transform)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Position = Vector3.Transform(Vertices[i].Position, transform);
                Vertices[i].TextCoord = Vertices[i].TextCoord;
                Vertices[i].Normal = Vector3.TransformNormal(Vertices[i].Normal, transform);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Vertex
    {
        public Vector3 Position;
        public Vector2 TextCoord;
        public Vector3 Normal;
    }
}