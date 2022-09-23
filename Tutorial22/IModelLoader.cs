using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Tutorial22
{
    internal interface IModelLoader
    {
        internal Model Model { get; }
        void LoadMesh();
    }

    internal class Model
    {
        internal List<Mesh> Meshes = new List<Mesh>();
        internal List<uint> Indices = new List<uint>();
        internal List<Vector3> Positions = new List<Vector3>();
        internal List<Vector2> TexCoords = new List<Vector2>();
        internal List<Vector3> Normals = new List<Vector3>();
        internal Vector3 MinPosition;
        internal Vector3 MaxPosition;
    }
}