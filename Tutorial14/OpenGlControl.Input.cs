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
        const float POSITION_STEP_AMOUNT = 0.5f;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    _camera.MoveForward(POSITION_STEP_AMOUNT);
                    e.Handled = true;
                    break;
                case Key.S:
                case Key.Down:
                    _camera.MoveBackward(POSITION_STEP_AMOUNT);
                    e.Handled = true;
                    break;
                case Key.A:
                case Key.Left:
                    _camera.MoveLeft(POSITION_STEP_AMOUNT);
                    e.Handled = true;
                    break;
                case Key.D:
                case Key.Right:
                    _camera.MoveRight(POSITION_STEP_AMOUNT);
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