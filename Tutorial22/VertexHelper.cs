using System;
using System.Numerics;

namespace Tutorial22
{
    internal static class VertexHelper
    {
        public static void CalculateNormals(uint[] indices, Vertex[] vertices)
        {
            for (int i = 0; i < indices.Length; i += 3)
            {
                uint index0 = indices[i];
                uint index1 = indices[i + 1];
                uint index2 = indices[i + 2];

                Vector3 v1 = vertices[index1].Position - vertices[index0].Position;
                Vector3 v2 = vertices[index2].Position - vertices[index0].Position;
                Vector3 normal = -Vector3.Cross(v1, v2);
                normal = Vector3.Normalize(normal);

                vertices[index0].Normal += normal;
                vertices[index1].Normal += normal;
                vertices[index2].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = Vector3.Normalize(vertices[i].Normal);
        }

        public static void CalculateMaxMinPosition(Vertex[] vertices, ref Vector3 maxPosition, ref Vector3 minPosition)
        {
            foreach (Vertex vertex in vertices)
            {
                minPosition.X = MathF.Min(minPosition.X, vertex.Position.X);
                minPosition.Y = MathF.Min(minPosition.Y, vertex.Position.Y);
                minPosition.Z = MathF.Min(minPosition.Z, vertex.Position.Z);

                maxPosition.X = MathF.Max(maxPosition.X, vertex.Position.X);
                maxPosition.Y = MathF.Max(maxPosition.Y, vertex.Position.Y);
                maxPosition.Z = MathF.Max(maxPosition.Z, vertex.Position.Z);
            }
        }
    }
}