using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Assimp;
using Matrix4x4 = System.Numerics.Matrix4x4;

namespace Tutorial22
{
    internal class Model : IModel
    {
        uint[] IModel.Indices => _indices;
        Vertex[] IModel.Vertices => _vertices;
        Vector3 IModel.MinPosition => _minPosition;
        Vector3 IModel.MaxPosition => _maxPosition;

        public Model(Assimp.Scene scene)
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

        void RecursiveLoadScene(Assimp.Node node, Assimp.Scene scene, List<Vertex> vertices, List<uint> indices)
        {
            Matrix4x4 transform = ToMatrix4x4(node.Transform);
            for (int m = 0; m < node.MeshCount; m++)
            {
                Assimp.Mesh mesh = scene.Meshes[node.MeshIndices[m]];
                for(int i = 0; i < mesh.Vertices.Count; i++)
                {
                    Vertex vertex = new();

                    var position = ToVector3(mesh.Vertices[i]);
                    vertex.Position =  Vector3.Transform(position, transform);

                    if (mesh.HasNormals)
                        vertex.Normal = ToVector3(mesh.Normals[i]);

                    if (mesh.HasTextureCoords(0))
                        vertex.TextCoord = ToVector2(mesh.TextureCoordinateChannels[0][i]);

                    vertices.Add(vertex);
                }

                foreach (Assimp.Face face in mesh.Faces)
                {
                    if (face.IndexCount != 3)
                        continue;

                    indices.Add((uint) face.Indices[0]);
                    indices.Add((uint) face.Indices[1]);
                    indices.Add((uint) face.Indices[2]);
                }
            }

            foreach (Assimp.Node child in node.Children)
            {
                RecursiveLoadScene(child, scene, vertices, indices);
            }
        }

        Matrix4x4 ToMatrix4x4(Assimp.Matrix4x4 m4x4)
        {
            return new Matrix4x4(
                m4x4.A1, m4x4.A2, m4x4.A3, m4x4.A4,
                m4x4.B1, m4x4.B2, m4x4.B3, m4x4.B4,
                m4x4.C1, m4x4.C2, m4x4.C3, m4x4.C4,
                m4x4.D1, m4x4.D2, m4x4.D3, m4x4.D4);
        }

        Vector3 ToVector3(Assimp.Vector3D v3d)
        {
            return new Vector3(v3d.X, v3d.Y, v3d.Z);
        }

        Vector2 ToVector2(Assimp.Vector3D v3d)
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