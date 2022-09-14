using System.Numerics;

namespace Tutorial14
{
    internal class Camera
    {
        public interface IChangedCallback
        {
            void PositionChanged(Vector3 old, Vector3 @new);
            void TargetChanged(Vector3 old, Vector3 @new);
            void UpChanged(Vector3 old, Vector3 @new);
        }

        public Vector3 CameraPosition
        {
            get => _cameraPosition;
            set
            {
                Vector3 oldPosition = _cameraPosition;
                _cameraPosition = value;
                _changedCallback.PositionChanged(oldPosition, value);
            }
        }

        public Vector3 CameraTarget
        {
            get => _cameraTarget;
            set
            {
                Vector3 oldPosition = _cameraTarget;
                _cameraTarget = value;

                CalculateLeftVector();

                _changedCallback.TargetChanged(oldPosition, value);
            }
        }

        public Vector3 CameraUp
        {
            get => _cameraUp;
            set
            {
                Vector3 oldPosition = _cameraUp;
                _cameraUp = value;

                CalculateLeftVector();

                _changedCallback.UpChanged(oldPosition, value);
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

        void CalculateLeftVector()
        {
            _left = Vector3.Cross(CameraTarget, CameraUp);
            _left = Vector3.Normalize(_left);
        }

        Vector3 _cameraPosition = new Vector3();
        Vector3 _cameraTarget = -Vector3.UnitZ;
        Vector3 _cameraUp = Vector3.UnitY;
        Vector3 _left;

        readonly IChangedCallback _changedCallback;
    }
}