using System;
using System.Numerics;

namespace Tutorial22
{
    internal static class VertexHelper
    {
        public static void CalculateNormals(Model model)
        {
            int normalCount = model.Normals.Count;
            for (int i = 0; i < model.Positions.Count - normalCount; i++)
                model.Normals.Add(default);

            for (int i = 0; i < model.Indices.Count; i += 3)
            {
                int index0 = (int)model.Indices[i];
                int index1 = (int)model.Indices[i + 1];
                int index2 = (int)model.Indices[i + 2];

                Vector3 v1 = model.Positions[index1] - model.Positions[index0];
                Vector3 v2 = model.Positions[index2] - model.Positions[index0];
                Vector3 normal = -Vector3.Cross(v1, v2);
                normal = Vector3.Normalize(normal);

                model.Normals[index0] += normal;
                model.Normals[index1] += normal;
                model.Normals[index2] += normal;
            }

            for (int i = 0; i < model.Normals.Count; i++)
                model.Normals[i] = Vector3.Normalize(model.Normals[i]);
        }

        public static void CalculateMaxMinPosition(Model model)
        {
            int meshOffset = 0;
            foreach (Mesh mesh in model.Meshes)
            {
                for (int i = 0; i < mesh.PositionsCount; i++)
                {
                    Vector3 position = Vector3.Transform(model.Positions[i + meshOffset], mesh.Transform);
                    model.MinPosition.X = MathF.Min(model.MinPosition.X, position.X);
                    model.MinPosition.Y = MathF.Min(model.MinPosition.Y, position.Y);
                    model.MinPosition.Z = MathF.Min(model.MinPosition.Z, position.Z);

                    model.MaxPosition.X = MathF.Max(model.MaxPosition.X, position.X);
                    model.MaxPosition.Y = MathF.Max(model.MaxPosition.Y, position.Y);
                    model.MaxPosition.Z = MathF.Max(model.MaxPosition.Z, position.Z);
                }

                meshOffset += mesh.PositionsCount;
            }
        }
    }
}