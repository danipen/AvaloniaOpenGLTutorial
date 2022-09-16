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
                0, 1, 3,
                3, 1, 2,
                4, 5, 7,
                7, 5, 6,
                8, 9, 11,
                11, 9, 10,
                12, 13, 15,
                15, 13, 14,
                16, 17, 19,
                19, 17, 18,
                20, 21, 23,
                23, 21, 22,
            };

            VertexHelper.CalculateNormals(_indices, _vertices);
        }

        readonly Vertex[] _vertices;
        readonly ushort[] _indices;
    }
}