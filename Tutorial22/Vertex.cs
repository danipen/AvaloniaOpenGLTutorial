using System.Numerics;
using System.Runtime.InteropServices;

namespace Tutorial22
{
    [StructLayout(LayoutKind.Sequential)]
    struct Vertex
    {
        public Vector3 Position;
        public Vector2 TextCoord;
        public Vector3 Normal;
    }
}