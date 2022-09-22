using System.Numerics;

namespace Tutorial22
{
    internal class CubeModel : IModel
    {
        Mesh[] IModel.Meshes => _meshes;
        Vector3 IModel.MinPosition => _minPosition;
        Vector3 IModel.MaxPosition => _maxPosition;

        public void LoadMesh()
        {
            var vertices = new Vertex[]
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

            var indices = new uint[]
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

        Vertex[] _vertices;
        uint[] _indices;
        Vector3 _minPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 _maxPosition = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Mesh[] _meshes;
    }
}