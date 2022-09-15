using System.Numerics;
using System.Runtime.InteropServices;

namespace Tutorial18;

[StructLayout(LayoutKind.Sequential)]
struct Vertex
{
    public Vector3 Position;
    public Vector2 TextCoord;
    public Vector3 Normal;
}