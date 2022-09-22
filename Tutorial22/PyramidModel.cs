using System.Numerics;

namespace Tutorial22
{
    internal class PyramidModel : IModel
    {
        Mesh[] IModel.Meshes => _meshes;
        Vector3 IModel.MinPosition => _minPosition;
        Vector3 IModel.MaxPosition => _maxPosition;

        public void LoadMesh()
        {
            Vertex[] vertices =
            {
                new Vertex()
                {
                    Position = new Vector3(-1.0f, -0.75f, 0.5773f),
                    TextCoord = new Vector2(0, 0)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, -0.75f, -1.15475f),
                    TextCoord = new Vector2(0.5f, 0.25f)
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

            uint[] indices =
            {
                1, 3, 0, // right face
                2, 3, 1, // left face
                0, 3, 2, // back face
                2, 1, 0, // bottom face
            };

            _meshes = new[]
            {
                new Mesh()
                {
                    Indices = indices,
                    Vertices = vertices,
                },
            };

            VertexHelper.CalculateNormals(_meshes);
            VertexHelper.CalculateMaxMinPosition(_meshes, ref _maxPosition, ref _minPosition);
        }

        Vector3 _minPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 _maxPosition = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Mesh[] _meshes;
    }
}