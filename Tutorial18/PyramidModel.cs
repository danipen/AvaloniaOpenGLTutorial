using System.Numerics;

namespace Tutorial18
{
    internal class PyramidModel : IModel
    {
        ushort[] IModel.Indices => _indices;

        Vertex[] IModel.Vertices => _vertices;

        internal PyramidModel()
        {
            _vertices = new[]
            {
                new Vertex()
                {
                    Position = new Vector3(-1.0f, -0.75f, 0.5773f),
                    TextCoord = new Vector2(0, 0)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, -0.75f, -1.15475f),
                    TextCoord = new Vector2(0.5f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(1.0f, -0.75f, 0.5773f),
                    TextCoord = new Vector2(1.0f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, 0.75f, 0.0f),
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

            VertexHelper.CalculateNormals(_indices, _vertices);
        }

        readonly ushort[] _indices;
        readonly Vertex[] _vertices;
    }
}