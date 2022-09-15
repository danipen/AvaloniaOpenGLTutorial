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
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(0, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(0, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                    TextCoord = new Vector2(1, 0f)
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                    TextCoord = new Vector2(1, 1f)
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, 0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, 0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, 0.5f),
                },
                new()
                {
                    Position = new Vector3(-0.5f, -0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, -0.5f),
                },
                new()
                {
                    Position = new Vector3(0.5f, -0.5f, 0.5f),
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