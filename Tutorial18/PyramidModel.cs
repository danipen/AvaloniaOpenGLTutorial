using System.Numerics;

namespace Tutorial18
{
    internal class PyramidModel
    {
        internal ushort[] Indices => _indices;

        internal Vertex[] Vertices => _vertices;
        
        internal PyramidModel()
        {
            _vertices = new[]
            {
                new Vertex()
                {
                    Position = new Vector3(-1.0f, -1.0f, 0),
                    TextCoord = new Vector2(0, 0)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, -1.0f, 1),
                    TextCoord = new Vector2(0.5f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(1.0f, -1.0f, 0),
                    TextCoord = new Vector2(1.0f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, 1.0f, 0.0f),
                    TextCoord = new Vector2(0.5f, 1.0f)
                },
            };

            _indices = new ushort[]
            {
                0, 3, 1,
                1, 3, 2,
                2, 3, 0,
                0, 1, 2
            };

            CalculateNormals(_indices, _vertices);
        }

        static void CalculateNormals(ushort[] indices, Vertex[] vertices)
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

        readonly ushort[] _indices;
        readonly Vertex[] _vertices;
    }
}