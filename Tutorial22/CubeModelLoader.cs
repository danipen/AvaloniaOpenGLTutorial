using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Tutorial22
{
    internal class CubeModelLoader : IModelLoader
    {
        Model IModelLoader.Model => _model;

        public void LoadMesh()
        {
            Vector3[] vertices = new[]
            {
                // ----- Face 1 ----
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                // ----- Face 2 ----
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                // ----- Face 3 ----
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                // ----- Face 4 ----
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                // ----- Face 5 ----
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                // ----- Face 6 ----
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
            };

            Vector2[] texCoord = new[]
            {
                // ----- Face 1 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                // ----- Face 2 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                // ----- Face 3 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                // ----- Face 4 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                // ----- Face 5 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                // ----- Face 6 ----
                new Vector2(0f, 1f),
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
            };

            uint[] indices = new uint[]
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