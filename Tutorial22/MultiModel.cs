using System.Linq;
using System.Numerics;

namespace Tutorial22
{
    internal class MultiModel : IModel
    {
        public MultiModel(params IModel[] models)
        {
            mMeshes = models.SelectMany(x => x.Meshes).ToArray();
            VertexHelper.CalculateMaxMinPosition(mMeshes, ref mMaxPosition, ref mMinPosition);
        }

        Mesh[] IModel.Meshes => mMeshes;

        Vector3 IModel.MinPosition => mMinPosition;

        Vector3 IModel.MaxPosition => mMaxPosition;

        void IModel.LoadMesh()
        {
        }

        readonly Mesh[] mMeshes;
        readonly Vector3 mMinPosition;
        readonly Vector3 mMaxPosition;
    }
}