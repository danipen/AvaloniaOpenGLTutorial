using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Assimp;

namespace Tutorial22
{
    internal class Model : IModel
    {
        uint[] IModel.Indices => _indices;
        Vertex[] IModel.Vertices => _vertices;
        Vector3 IModel.MinPosition => _minPosition;
        Vector3 IModel.MaxPosition => _maxPosition;

        public Model(Scene scene)
        {
            _scene = scene;
        }

        void IModel.LoadMesh()
        {
            if (_scene.MeshCount == 0)
                throw new InvalidOperationException("No meshes found");

            List<Vertex> vertices = new();
            List<uint> indices = new();

            RecursiveLoadScene(_scene.RootNode, _scene, vertices, indices);

            _indices = indices.ToArray();
            _vertices = vertices.ToArray();
            VertexHelper.CalculateMaxMinPosition(_vertices, ref _maxPosition, ref _minPosition);
        }

        void RecursiveLoadScene(Node node, Scene scene, List<Vertex> vertices, List<uint> indices)
        {
            for (int m = 0; m < node.MeshCount; m++)
            {
                Mesh mesh = scene.Meshes[node.MeshIndices[m]];
                for(int i = 0; i < mesh.Vertices.Count; i++)
                {
                    Vertex vertex = new();
                    vertex.Position = ToVector3(mesh.Vertices[i]);

                    if (mesh.HasNormals)
                        vertex.Normal = ToVector3(mesh.Normals[i]);

                    if (mesh.HasTextureCoords(0))
                        vertex.TextCoord = ToVector2(mesh.TextureCoordinateChannels[0][i]);

                    vertices.Add(vertex);
                }

                foreach (Face face in mesh.Faces)
                {
                    if (face.IndexCount != 3)
                        continue;

                    indices.Add((uint) face.Indices[0]);
                    indices.Add((uint) face.Indices[1]);
                    indices.Add((uint) face.Indices[2]);
                }
            }

            foreach (Node child in node.Children)
            {
                RecursiveLoadScene(child, scene, vertices, indices);
            }
        }

        Vector3 ToVector3(Vector3D v3d)
        {
            return new Vector3(v3d.X, v3d.Y, v3d.Z);
        }

        Vector2 ToVector2(Vector3D v3d)
        {
            return new Vector2(v3d.X, v3d.Y);
        }

        uint[] _indices;
        Vertex[] _vertices;

        readonly Scene _scene;
        Vector3 _minPosition;
        Vector3 _maxPosition;
    }
}