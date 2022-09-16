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

        public Model(Scene scene)
        {
            _scene = scene;
        }

        void IModel.LoadMesh()
        {
            if (_scene.MeshCount == 0)
                throw new InvalidOperationException("No meshes found");

            Mesh mesh = _scene.Meshes[0];
            List<Vertex> vertices = new();
            List<uint> indices = new();

            for (int i = 0 ; i < mesh.VertexCount ; i++)
            {
                Vertex vertex = new();
                vertex.Position = ToVector3(mesh.Vertices[i]);

                if (mesh.HasTextureCoords(0))
                    vertex.TextCoord = ToVector2(mesh.TextureCoordinateChannels[0][i]);

                if (mesh.HasNormals)
                    vertex.Normal = -ToVector3(mesh.Normals[i]);

                vertices.Add(vertex);
            }

            for (int i = 0 ; i < mesh.FaceCount ; i++)
            {
                Face face = mesh.Faces[i];
                indices.AddRange(face.Indices.Select(x => (uint)x).Reverse());
            }

            _indices = indices.ToArray();
            _vertices = vertices.ToArray();
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
    }
}