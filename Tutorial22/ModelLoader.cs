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
    internal class ModelLoader : IModelLoader
    {
        Model IModelLoader.Model => _model;

        public ModelLoader(Assimp.Scene scene)
        {
            _scene = scene;
        }

        void IModelLoader.LoadMesh()
        {
            if (_scene.MeshCount == 0)
                throw new InvalidOperationException("No meshes found");

            _model = new Model();
            RecursiveLoadScene(_scene.RootNode, _scene, _model, ToMatrix4x4(_scene.RootNode.Transform));

            VertexHelper.CalculateMaxMinPosition(_model);
        }

        void RecursiveLoadScene(Node node, Scene scene, Model model, Matrix4x4 parentTransform)
        {
            Matrix4x4 nodeTransform = ToMatrix4x4(node.Transform) * parentTransform;
            for (int m = 0; m < node.MeshCount; m++)
            {
                Mesh mesh = ProcessMesh(scene.Meshes[node.MeshIndices[m]], model, nodeTransform);
                model.Meshes.Add(mesh);
            }

            foreach (Assimp.Node child in node.Children)
            {
                RecursiveLoadScene(child, scene, model, nodeTransform);
            }
        }

        Mesh ProcessMesh(Assimp.Mesh assimpMesh, Model model, Matrix4x4 transform)
        {
            for(int i = 0; i < assimpMesh.Vertices.Count; i++)
            {
                Vector3 position = ToVector3(assimpMesh.Vertices[i]);
                model.Positions.Add(position);

                if (assimpMesh.HasNormals)
                    model.Normals.Add(ToVector3(assimpMesh.Normals[i]));

                if (assimpMesh.HasTextureCoords(0))
                    model.TexCoords.Add(ToVector2(assimpMesh.TextureCoordinateChannels[0][i]));
            }

            foreach (Assimp.Face face in assimpMesh.Faces)
            {
                if (face.IndexCount != 3)
                    continue;

                model.Indices.Add((uint) face.Indices[0]);
                model.Indices.Add((uint) face.Indices[1]);
                model.Indices.Add((uint) face.Indices[2]);
            }

            return new Mesh()
            {
                IndicesCount = assimpMesh.Faces.Count * 3,
                PositionsCount = assimpMesh.Vertices.Count,
                Transform = transform,
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

        readonly Scene _scene;
        Model _model;
    }
}