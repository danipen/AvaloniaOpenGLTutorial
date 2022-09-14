using System.Numerics;
using Avalonia;
using Avalonia.Automation.Provider;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.OpenGL.Controls;

namespace Tutorial14
{
    partial class OpenGlControl : Camera.IChangedCallback
    {
        const float cameraPositionStepAmount = 1.1f;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    _camera.MoveForward(cameraPositionStepAmount);
                    e.Handled = true;
                    break;
                case Key.S:
                case Key.Down:
                    _camera.MoveBackward(cameraPositionStepAmount);
                    e.Handled = true;
                    break;
                case Key.A:
                case Key.Left:
                    _camera.MoveLeft(cameraPositionStepAmount);
                    e.Handled = true;
                    break;
                case Key.D:
                case Key.Right:
                    _camera.MoveRight(cameraPositionStepAmount);
                    e.Handled = true;
                    break;
            }
        }

        public void PositionChanged(Vector3 oldVector, Vector3 newVector)
        {
            RaisePropertyChanged(CameraPositionXProperty, oldVector.X, newVector.X);
            RaisePropertyChanged(CameraPositionYProperty, oldVector.Y, newVector.Y);
            RaisePropertyChanged(CameraPositionZProperty, oldVector.Z, newVector.Z);
        }

        public void TargetChanged(Vector3 oldVector, Vector3 newVector)
        {
            RaisePropertyChanged(CameraTargetXProperty, oldVector.X, newVector.X);
            RaisePropertyChanged(CameraTargetYProperty, oldVector.Y, newVector.Y);
            RaisePropertyChanged(CameraTargetZProperty, oldVector.Z, newVector.Z);
        }

        public void UpChanged(Vector3 oldVector, Vector3 newVector)
        {
            RaisePropertyChanged(CameraUpXProperty, oldVector.X, newVector.X);
            RaisePropertyChanged(CameraUpYProperty, oldVector.Y, newVector.Y);
            RaisePropertyChanged(CameraUpZProperty, oldVector.Z, newVector.Z);
        }
    }
}