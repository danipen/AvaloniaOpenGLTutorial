using System.Numerics;

namespace Tutorial14
{
    internal class Camera
    {
        public interface IChangedCallback
        {
            void PositionChanged(Vector3 oldVector, Vector3 newVector);
            void TargetChanged(Vector3 oldVector, Vector3 newVector);
            void UpChanged(Vector3 oldVector, Vector3 newVector);
        }

        public Vector3 CameraPosition
        {
            get => _cameraPosition;
            set
            {
                Vector3 oldVector = _cameraPosition;
                _cameraPosition = value;
                _changedCallback.PositionChanged(oldVector, value);
            }
        }

        public Vector3 CameraTarget
        {
            get => _cameraTarget;
            set
            {
                Vector3 oldVector = _cameraTarget;
                _cameraTarget = value;

                CalculateLeftVector();

                _changedCallback.TargetChanged(oldVector, value);
            }
        }

        public Vector3 CameraUp
        {
            get => _cameraUp;
            set
            {
                Vector3 oldVector = _cameraUp;
                _cameraUp = value;

                CalculateLeftVector();

                _changedCallback.UpChanged(oldVector, value);
            }
        }

        public Camera(IChangedCallback changedCallback)
        {
            _changedCallback = changedCallback;
        }

        public void MoveForward(float stepAmount)
        {
            CameraPosition += CameraTarget * stepAmount;
        }

        public void MoveBackward(float stepAmount)
        {
            CameraPosition -= CameraTarget * stepAmount;
        }

        public void MoveLeft(float stepAmount)
        {
            CameraPosition += _left * stepAmount;
        }

        public void MoveRight(float stepAmount)
        {
            CameraPosition -= _left * stepAmount;
        }

        public void InitFields()
        {
            _cameraPosition = Vector3.Zero;
            _cameraTarget = Vector3.UnitZ;
            _cameraUp = Vector3.UnitY;
        }

        void CalculateLeftVector()
        {
            _left = Vector3.Cross(CameraTarget, CameraUp);
            _left = Vector3.Normalize(_left);
        }

        Vector3 _cameraPosition;
        Vector3 _cameraTarget;
        Vector3 _cameraUp;
        Vector3 _left;

        readonly IChangedCallback _changedCallback;
    }
}