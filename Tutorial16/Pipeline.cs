using System.Numerics;

namespace Tutorial16
{
    internal class Pipeline
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

        public void SetPerspective(
            float fieldOfViewAngleInRadians,
            float width,
            float height,
            float nearPlaneDistance,
            float farPlaneDistance)
        {
            _fieldOfView = fieldOfViewAngleInRadians;
            _width = width;
            _height = height;
            _nearPlaneDistance = nearPlaneDistance;
            _farPlaneDistance = farPlaneDistance;
        }

        public void SetCamera(Vector3 cameraPos, Vector3 cameraTarget, Vector3 cameraUp)
        {
            _cameraPos = cameraPos;
            _cameraTarget = cameraTarget;
            _cameraUp = cameraUp;
        }

        public Matrix4x4 GetTransformation()
        {
            Matrix4x4 scale = Matrix4x4.CreateScale(_scaleData);
            Matrix4x4 rotateX = Matrix4x4.CreateRotationX(_rotateData.X);
            Matrix4x4 rotateY = Matrix4x4.CreateRotationY(_rotateData.Y);
            Matrix4x4 rotateZ = Matrix4x4.CreateRotationZ(_rotateData.Z);
            Matrix4x4 translate = Matrix4x4.CreateTranslation(_positionData);
            Matrix4x4 camera = Matrix4x4.CreateLookAt(_cameraPos, _cameraTarget + _cameraPos, _cameraUp);
            Matrix4x4 perspective = Matrix4x4.CreatePerspectiveFieldOfView(
                _fieldOfView,
                _width / _height,
                _nearPlaneDistance,
                _farPlaneDistance);

            return scale * rotateX * rotateY * rotateZ * translate * camera * perspective;
        }

        Vector3 _scaleData;
        Vector3 _positionData;
        Vector3 _rotateData;

        float _fieldOfView;
        float _width = 1f;
        float _height = 1f;
        float _nearPlaneDistance;
        float _farPlaneDistance;

        Vector3 _cameraPos;
        Vector3 _cameraTarget;
        Vector3 _cameraUp;
    };
}