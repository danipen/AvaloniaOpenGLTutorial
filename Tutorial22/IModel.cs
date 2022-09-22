using System.Numerics;
using System.Runtime.InteropServices;

namespace Tutorial22
{
    internal interface IModel
    {
        internal Mesh[] Meshes { get; }
        internal Vector3 MinPosition { get; }
        internal Vector3 MaxPosition { get; }

        void LoadMesh();
    }
}