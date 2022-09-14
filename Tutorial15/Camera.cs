using System;
using System.Numerics;

namespace Tutorial15
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
        
        public void SetWindowSize(float windowWidth, float windowHeight)
        {
            m_windowWidth = windowWidth;
            m_windowHeight = windowHeight;
        }
        
        public void OnMouse(float x, float y)
        {
            float deltaX = x - m_mousePos.X;
            float deltaY = y - m_mousePos.Y;

            m_mousePos.X = x;
            m_mousePos.Y = y;

            m_AngleH += deltaX / 20.0f;
            m_AngleV += deltaY / 20.0f;

            if (deltaX == 0) {
                if (x <= MARGIN) {
                    //    m_AngleH -= 1.0f;
                    m_OnLeftEdge = true;
                }
                else if (x >= (m_windowWidth - MARGIN)) {
                    //    m_AngleH += 1.0f;
                    m_OnRightEdge = true;
                }
            }
            else {
                m_OnLeftEdge = false;
                m_OnRightEdge = false;
            }

            if (deltaY == 0) {
                if (y <= MARGIN) {
                    m_OnUpperEdge = true;
                }
                else if (y >= (m_windowHeight - MARGIN)) {
                    m_OnLowerEdge = true;
                }
            }
            else {
                m_OnUpperEdge = false;
                m_OnLowerEdge = false;
            }

            Update();
        }
        
        public void OnRender()
        {
            bool shouldUpdate = false;

            if (m_OnLeftEdge) {
                m_AngleH -= EDGE_STEP;
                shouldUpdate = true;
            }
            else if (m_OnRightEdge) {
                m_AngleH += EDGE_STEP;
                shouldUpdate = true;
            }

            if (m_OnUpperEdge) {
                if (m_AngleV > -90.0f) {
                    m_AngleV -= EDGE_STEP;
                    shouldUpdate = true;
                }
            }
            else if (m_OnLowerEdge) {
                if (m_AngleV < 90.0f) {
                    m_AngleV += EDGE_STEP;
                    shouldUpdate = true;
                }
            }

            if (shouldUpdate) {
                Update();
            }
        }

        internal void Init(float windowWidth, float windowHeight)
        {
            SetWindowSize(windowWidth, windowHeight);
            
            Vector3 hTarget = Vector3.Normalize(new Vector3(CameraTarget.X, 0.0f, CameraTarget.Z));
           
            if (hTarget.Z >= 0.0f)
            {
                if (hTarget.X >= 0.0f)
                {
                    m_AngleH = 360.0f - ToDegrees(MathF.Asin(hTarget.Z));
                }
                else
                {
                    m_AngleH = 180.0f + ToDegrees(MathF.Asin(hTarget.Z));
                }
            }
            else
            {
                if (hTarget.X >= 0.0f)
                {
                    m_AngleH = ToDegrees(MathF.Asin(-hTarget.Z));
                }
                else
                {
                    m_AngleH = 180.0f - ToDegrees(MathF.Asin(-hTarget.Z));
                }
            }

            m_AngleV = -ToDegrees(MathF.Asin(CameraTarget.Y));

            m_OnUpperEdge = false;
            m_OnLowerEdge = false;
            m_OnLeftEdge  = false;
            m_OnRightEdge = false;
            m_mousePos.X  = m_windowWidth / 2;
            m_mousePos.Y  = m_windowHeight / 2;
        }

        void Update()
        {
            Vector3 vAxis = new Vector3(0.0f, 1.0f, 0.0f);

            // Rotate the view vector by the horizontal angle around the vertical axis
            Vector3 view = new Vector3(1.0f, 0.0f, 0.0f);
            view = Vector3.Transform(view, Matrix4x4.CreateRotationY(ToRadians(m_AngleH)));
            view = Vector3.Normalize(view);

            // Rotate the view vector by the vertical angle around the horizontal axis
            Vector3 hAxis = Vector3.Cross(vAxis, view);
            hAxis = Vector3.Normalize(hAxis);
            
            view = Vector3.Transform(view, Matrix4x4.CreateRotationX(ToRadians(m_AngleV)));

            CameraTarget = Vector3.Normalize(view);
            CameraUp = Vector3.Normalize(Vector3.Cross(CameraTarget, hAxis));
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

        static float ToDegrees(float radians)
        {
            return radians * 180 / MathF.PI;
        }

        static float ToRadians(float degrees)
        {
            return degrees * MathF.PI / 180;
        }

        Vector3 _cameraPosition;
        Vector3 _cameraTarget;
        Vector3 _cameraUp;
        Vector3 _left;

        private Vector2 m_mousePos;
        private float m_AngleH;
        private float m_AngleV;

        private bool m_OnLeftEdge;
        private bool m_OnUpperEdge;
        private bool m_OnLowerEdge;
        private bool m_OnRightEdge;
        
        private const int MARGIN = 10;
        const float EDGE_STEP = 0.5f;

        private float m_windowWidth;
        private float m_windowHeight;
        
        readonly IChangedCallback _changedCallback;
    }
}