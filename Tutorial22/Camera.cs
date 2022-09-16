using System;
using System.Numerics;

namespace Tutorial22
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

                _changedCallback.UpChanged(oldUp, value);
            }
        }

        public Camera(IChangedCallback changedCallback)
        {
            _changedCallback = changedCallback;
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
            Vector3 right = Vector3.Normalize(Vector3.Cross(CameraUp, CameraTarget));
            right *= stepAmount;
            CameraPosition += right;
        }

        public void MoveRight(float stepAmount)
        {
            Vector3 left = Vector3.Normalize(Vector3.Cross(CameraTarget, CameraUp));
            left *= stepAmount;
            CameraPosition += left;
        }

        public void MoveUp(float stepAmount)
        {
            CameraPosition += CameraUp * stepAmount;
        }

        public void MoveDown(float stepAmount)
        {
            CameraPosition -= CameraUp * stepAmount;
        }

        public void SetWindowSize(float windowWidth, float windowHeight)
        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void OnMouseExited()
        {
            _isOnRightEdge = false;
            _isOnLowerEdge = false;
            _isOnLeftEdge = false;
            _isOnUpperEdge = false;
        }

        public void OnMouse(float x, float y)
        {
            float deltaX = x - _mousePosition.X;
            float deltaY = y - _mousePosition.Y;

            _mousePosition.X = x;
            _mousePosition.Y = y;

            _verticalAngle += deltaX / 20.0f;
            _horizontalAngle += deltaY / 20.0f;

            if (x <= MARGIN && x > 0)
            {
                _isOnLeftEdge = true;
            }
            else if (x >= (_windowWidth - MARGIN) && x < _windowWidth)
            {
                _isOnRightEdge = true;
            }
            else
            {
                _isOnLeftEdge = false;
                _isOnRightEdge = false;
            }

            if (y <= MARGIN && y > 0)
            {
                _isOnUpperEdge = true;
            }
            else if (y >= (_windowHeight - MARGIN) && y < _windowHeight)
            {
                _isOnLowerEdge = true;
            }
            else
            {
                _isOnUpperEdge = false;
                _isOnLowerEdge = false;
            }

            Update();
        }

        public void OnRender()
        {
            bool shouldUpdate = false;

            if (_isOnLeftEdge) {
                _verticalAngle -= EDGE_STEP;
                shouldUpdate = true;
            }
            else if (_isOnRightEdge) {
                _verticalAngle += EDGE_STEP;
                shouldUpdate = true;
            }

            if (_isOnUpperEdge) {
                if (_horizontalAngle > -90.0f) {
                    _horizontalAngle -= EDGE_STEP;
                    shouldUpdate = true;
                }
            }
            else if (_isOnLowerEdge) {
                if (_horizontalAngle < 90.0f) {
                    _horizontalAngle += EDGE_STEP;
                    shouldUpdate = true;
                }
            }

            if (shouldUpdate) {
                Update();
            }
        }

        internal void Init(float windowWidth, float windowHeight, Vector3 maxPosition, Vector3 minPosition)
        {
            _maxPosition = maxPosition;
            _minPosition = minPosition;

            InitCamera();

            SetWindowSize(windowWidth, windowHeight);

            Vector3 hTarget = Vector3.Normalize(new Vector3(CameraTarget.X, 0.0f, CameraTarget.Z));

            if (hTarget.Z >= 0.0f)
            {
                if (hTarget.X >= 0.0f)
                {
                    _verticalAngle = 360.0f - ToDegrees(MathF.Asin(hTarget.Z));
                }
                else
                {
                    _verticalAngle = 180.0f + ToDegrees(MathF.Asin(hTarget.Z));
                }
            }
            else
            {
                if (hTarget.X >= 0.0f)
                {
                    _verticalAngle = ToDegrees(MathF.Asin(-hTarget.Z));
                }
                else
                {
                    _verticalAngle = 180.0f - ToDegrees(MathF.Asin(-hTarget.Z));
                }
            }

            _horizontalAngle = ToDegrees(MathF.Asin(CameraTarget.Y));

            _isOnUpperEdge = false;
            _isOnLowerEdge = false;
            _isOnLeftEdge  = false;
            _isOnRightEdge = false;
            _mousePosition.X  = _windowWidth / 2;
            _mousePosition.Y  = _windowHeight / 2;
        }

        void Update()
        {
            Vector3 vAxis = new Vector3(0.0f, 1.0f, 0.0f);

            // Rotate the view vector by the horizontal angle around the vertical axis
            Vector3 view = new Vector3(1.0f, 0.0f, 0.0f);
            view = Vector3.Transform(view, Matrix4x4.CreateRotationY(ToRadians(_verticalAngle)));
            view = Vector3.Normalize(view);

            // Rotate the view vector by the vertical angle around the horizontal axis
            Vector3 hAxis = Vector3.Cross(vAxis, view);
            hAxis = Vector3.Normalize(hAxis);

            view = Vector3.Transform(view, Matrix4x4.CreateRotationX(ToRadians(_horizontalAngle)));

            CameraTarget = Vector3.Normalize(view);
            CameraUp = Vector3.Normalize(Vector3.Cross(CameraTarget, hAxis));
        }

        void InitCamera()
        {
            Vector3 oldPosition = _cameraPosition;
            Vector3 oldTarget = _cameraTarget;
            Vector3 oldUp = _cameraUp;

            _cameraPosition = new Vector3(
                CalculateCenter(_maxPosition.X, _minPosition.X),
                CalculateCenter(_maxPosition.Y, _minPosition.Y),
                CalculateZ(_maxPosition, _minPosition));

            _cameraTarget = new Vector3(0, 0, 1f);
            _cameraUp = new Vector3(0, 1, 0);

            _changedCallback.PositionChanged(oldPosition, _cameraPosition);
            _changedCallback.TargetChanged(oldTarget, _cameraTarget);
            _changedCallback.PositionChanged(oldUp, _cameraUp);
        }

        float CalculateZ(Vector3 maxPosition, Vector3 minPosition)
        {
            var width = maxPosition.X - minPosition.X;
            var height = maxPosition.Y - minPosition.Y;

            return -MathF.Max(width, height) * 2 + minPosition.Z;
        }

        static float CalculateCenter(float maxPosition, float minPosition)
        {
            float size = maxPosition - minPosition;
            return minPosition + size / 2 ;
        }

        static float ToDegrees(float radians)
        {
            return radians * 180 / MathF.PI;
        }

        static float ToRadians(float degrees)
        {
            return degrees * MathF.PI / 180;
        }

        Vector3 _maxPosition;
        Vector3 _minPosition;

        Vector3 _cameraPosition;
        Vector3 _cameraTarget;
        Vector3 _cameraUp;

        Vector2 _mousePosition;
        float _verticalAngle;
        float _horizontalAngle;

        bool _isOnLeftEdge;
        bool _isOnUpperEdge;
        bool _isOnLowerEdge;
        bool _isOnRightEdge;

        float _windowWidth;
        float _windowHeight;

        readonly IChangedCallback _changedCallback;

        const int MARGIN = 40;
        const float EDGE_STEP = 0.5f;
    }
}