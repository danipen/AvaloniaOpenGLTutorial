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
                Vector3 oldTarget = _cameraTarget;
                _cameraTarget = value;

                CalculateLeftVector();

                _changedCallback.TargetChanged(oldTarget, value);
            }
        }

        public Vector3 CameraUp
        {
            get => _cameraUp;
            set
            {
                Vector3 oldUp = _cameraUp;
                _cameraUp = value;

                CalculateLeftVector();

                _changedCallback.UpChanged(oldUp, value);
            }
        }

        public Camera(IChangedCallback changedCallback)
        {
            _changedCallback = changedCallback;
            InitCamera();
        }

        public void ResetCamera()
        {
            InitCamera();
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
            CameraPosition -= _left * stepAmount;
        }

        public void MoveRight(float stepAmount)
        {
            CameraPosition += _left * stepAmount;
        }

        public void MoveUp(float stepAmount)
        {
            CameraPosition -= CameraUp * stepAmount;
        }

        public void MoveDown(float stepAmount)
        {
            CameraPosition += CameraUp * stepAmount;
        }

        void CalculateLeftVector()
        {
            _left = Vector3.Cross(CameraTarget, CameraUp);
            _left = Vector3.Normalize(_left);
        }

        void InitCamera()
        {
            Vector3 oldPosition = _cameraPosition;
            Vector3 oldTarget = _cameraTarget;
            Vector3 oldUp = _cameraUp;

            _cameraPosition = new Vector3(0, 0, -2);
            _cameraTarget = new Vector3(0, 0, 1f);
            _cameraUp = new Vector3(0, 1, 0);

            CalculateLeftVector();

            _changedCallback.PositionChanged(oldPosition, _cameraPosition);
            _changedCallback.TargetChanged(oldTarget, _cameraTarget);
            _changedCallback.PositionChanged(oldUp, _cameraUp);
        }

        Vector3 _cameraPosition;
        Vector3 _cameraTarget;
        Vector3 _cameraUp;
        Vector3 _left;

        readonly IChangedCallback _changedCallback;
    }
}