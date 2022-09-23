using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Tutorial22
{
    internal class PyramidModelLoader : IModelLoader
    {
        Model IModelLoader.Model => _model;

        public void LoadMesh()
        {
            Vector3[] vertices =
            {
                 new Vector3(-1.0f, -0.75f, 0.5773f),
                 new Vector3(0.0f, -0.75f, -1.15475f),
                 new Vector3(1.0f, -0.75f, 0.5773f),
                 new Vector3(0.0f, 0.75f, 0.0f),
            };

            Vector2[] texCoord =
            {
                new Vector2(0, 0),
                new Vector2(0.5f, 0.25f),
                new Vector2(1.0f, 0.0f),
                new Vector2(0.5f, 1.0f),
            };

            uint[] indices =
            {
                1, 3, 0, // right face
                2, 3, 1, // left face
                0, 3, 2, // back face
                2, 1, 0, // bottom face
            };

            _model = new Model()
            {
                Meshes =
                {
                    new Mesh()
                    {
                        IndicesCount = indices.Length,
                        PositionsCount = vertices.Length,
                    },
                },
                Indices = indices.ToList(),
                Positions = vertices.ToList(),
                Normals = new List<Vector3>(vertices.Length),
                TexCoords = texCoord.ToList(),
            };

            VertexHelper.CalculateNormals(_model);
            VertexHelper.CalculateMaxMinPosition(_model);
        }

        Model _model;
    }
}