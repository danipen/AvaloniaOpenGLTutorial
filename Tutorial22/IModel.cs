using System.Numerics;

namespace Tutorial22
{
    internal interface IModel
    {
        internal uint[] Indices { get; }
        internal Vertex[] Vertices { get;}
        internal Vector3 MinPosition { get; }
        internal Vector3 MaxPosition { get; }

        void LoadMesh();
    }
}