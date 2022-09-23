using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Assimp;
using Assimp.Configs;
using Avalonia.Win32.Interop.Automation;
using Matrix4x4 = System.Numerics.Matrix4x4;

namespace Tutorial22
{
    internal class Model : IModel
    {
        Mesh[] IModel.Meshes => _meshes;
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

            List<Mesh> meshes = new();

            RecursiveLoadScene(_scene.RootNode, _scene, meshes, ToMatrix4x4(_scene.RootNode.Transform));

            _meshes = meshes.ToArray();
            VertexHelper.CalculateMaxMinPosition(_meshes, ref _maxPosition, ref _minPosition);
        }

        void RecursiveLoadScene(Node node, Scene scene, List<Mesh> meshes, Matrix4x4 parentTransform)
        {
            Matrix4x4 nodeTransform = ToMatrix4x4(node.Transform) * parentTransform;
            for (int m = 0; m < node.MeshCount; m++)
            {
                meshes.Add(ToMesh(scene.Meshes[node.MeshIndices[m]], nodeTransform));
            }

            foreach (Assimp.Node child in node.Children)
            {
                RecursiveLoadScene(child, scene, meshes, nodeTransform);
            }
        }

        Mesh ToMesh(Assimp.Mesh assimpMesh, Matrix4x4 transform)
        {
            List<Vertex> vertices = new();
            List<uint> indices = new();
            for(int i = 0; i < assimpMesh.Vertices.Count; i++)
            {
                Vertex vertex = new();

                Vector3 position = ToVector3(assimpMesh.Vertices[i]);
                vertex.Position =  Vector3.Transform(position, transform);

                if (assimpMesh.HasNormals)
                    vertex.Normal = Vector3.TransformNormal(ToVector3(assimpMesh.Normals[i]), transform);

                if (assimpMesh.HasTextureCoords(0))
                    vertex.TextCoord = ToVector2(assimpMesh.TextureCoordinateChannels[0][i]);

                vertices.Add(vertex);
            }

            foreach (Assimp.Face face in assimpMesh.Faces)
            {
                if (face.IndexCount != 3)
                    continue;

                indices.Add((uint) face.Indices[0]);
                indices.Add((uint) face.Indices[1]);
                indices.Add((uint) face.Indices[2]);
            }

            return new Mesh()
            {
                Indices = indices.ToArray(),
                Vertices = vertices.ToArray(),
            };
        }

        Matrix4x4 ToMatrix4x4(Assimp.Matrix4x4 m4x4)
        {
            return new Matrix4x4(
                m4x4.A1, m4x4.B1, m4x4.C1, m4x4.D1,
                m4x4.A2, m4x4.B2, m4x4.C2, m4x4.D2,
                m4x4.A3, m4x4.B3, m4x4.C3, m4x4.D3,
                m4x4.A4, m4x4.B4, m4x4.C4, m4x4.D4);
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
        Mesh[] _meshes;
    }
}