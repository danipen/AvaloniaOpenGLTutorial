using System.Numerics;

namespace Tutorial22
{
    internal class PyramidModel : IModel
    {
        uint[] IModel.Indices => _indices;

        Vertex[] IModel.Vertices => _vertices;

        public void LoadMesh()
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

            _indices = new uint[]
            {
                0, 3, 1,
                1, 3, 2,
                2, 3, 0,
                0, 1, 2
            };

            VertexHelper.CalculateNormals(_indices, _vertices);
        }

        uint[] _indices;
        Vertex[] _vertices;
    }
}