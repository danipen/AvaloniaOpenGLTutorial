using System.Linq;
using System.Numerics;

namespace Tutorial22
{
    internal class MultiModelLoader : IModelLoader
    {
        public MultiModelLoader(params IModelLoader[] models)
        {
            _model = new Model()
            {
                Indices = models.SelectMany(x => x.Model.Indices).ToList(),
                Positions = models.SelectMany(x => x.Model.Positions).ToList(),
                Normals = models.SelectMany(x => x.Model.Normals).ToList(),
                TexCoords = models.SelectMany(x => x.Model.TexCoords).ToList(),
                Meshes = models.SelectMany(x => x.Model.Meshes).ToList(),
            };
            VertexHelper.CalculateMaxMinPosition(_model);
        }

        Model IModelLoader.Model => _model;

        void IModelLoader.LoadMesh()
        {
        }

        Model _model;

        readonly Vector3 mMinPosition;
        readonly Vector3 mMaxPosition;
    }
}