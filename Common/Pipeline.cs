using System.Numerics;

namespace Common
{
    public class Pipeline
    {
        public Pipeline()
        {
            _scaleData = Vector3.One;
            _positionData = Vector3.Zero;
            _rotateData = Vector3.Zero;
        }

        public void Scale(float scaleX, float scaleY, float scaleZ)
        {
            _scaleData.X = scaleX;
            _scaleData.Y = scaleY;
            _scaleData.Z = scaleZ;
        }

        public void Position(float x, float y, float z)
        {
            _positionData.X = x;
            _positionData.Y = y;
            _positionData.Z = z;
        }

        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            _rotateData.X = radiansX;
            _rotateData.Y = radiansY;
            _rotateData.Z = radiansZ;
        }
        public Matrix4x4 GetTransformation()
        {
            Matrix4x4 scale = Matrix4x4.CreateScale(_scaleData);
            Matrix4x4 rotate = Matrix4x4.CreateFromYawPitchRoll(_rotateData.Y, _rotateData.X, _rotateData.Z);
            Matrix4x4 translate = Matrix4x4.CreateTranslation(_positionData);

            return translate * rotate * scale;
        }
        
        Vector3 _scaleData;
        Vector3 _positionData;
        Vector3 _rotateData;
    };
}