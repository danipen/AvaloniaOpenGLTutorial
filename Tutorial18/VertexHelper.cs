using System.Numerics;

namespace Tutorial18
{
    internal class VertexHelper
    {
        public static void CalculateNormals(ushort[] indices, Vertex[] vertices)
        {
            for (int i = 0; i < indices.Length; i += 3)
            {
                ushort index0 = indices[i];
                ushort index1 = indices[i + 1];
                ushort index2 = indices[i + 2];

                Vector3 v1 = vertices[index1].Position - vertices[index0].Position;
                Vector3 v2 = vertices[index2].Position - vertices[index0].Position;
                Vector3 normal = Vector3.Cross(v1, v2);
                normal = Vector3.Normalize(normal);

                vertices[index0].Normal += normal;
                vertices[index1].Normal += normal;
                vertices[index2].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = Vector3.Normalize(vertices[i].Normal);
        }
    }
}