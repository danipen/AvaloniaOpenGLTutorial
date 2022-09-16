using System.Numerics;

namespace Tutorial18
{
    internal class CubeModel : IModel
    {
        ushort[] IModel.Indices => _indices;

        Vertex[] IModel.Vertices => _vertices;

        internal CubeModel()
        {
            _vertices = new Vertex[]
            {
                // ----- Face 1 ----
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(1f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
                // ----- Face 2 ----
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
                // ----- Face 3 ----
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
                // ----- Face 4 ----
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 0f)

                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
                // ----- Face 5 ----
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(1f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
                // ----- Face 6 ----
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(0f, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(0f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(1f, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
                    TextCoord = new Vector2(1f, 1f)
                },
            };

            _indices = new ushort[]
            {
                0, 1, 3,    // front face
                3, 1, 2,    // front face
                7, 5, 4,    // back face
                6, 5, 7,    // back face
                8, 9, 11,   // left face
                11, 9, 10,  // left face
                15, 13, 12, // right face
                14, 13, 15, // right face
                16, 17, 19, // top face
                19, 17, 18, // top face
                23, 21, 20, // bottom face
                22, 21, 23, // bottom face
            };

            VertexHelper.CalculateNormals(_indices, _vertices);
        }

        readonly Vertex[] _vertices;
        readonly ushort[] _indices;
    }
}