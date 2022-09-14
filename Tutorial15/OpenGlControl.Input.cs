using System;
using System.Numerics;
using Avalonia;
using Avalonia.Automation.Provider;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.OpenGL.Controls;

namespace Tutorial15
{
    partial class OpenGlControl : Camera.IChangedCallback
    {
        const float POSITION_STEP_AMOUNT = 0.5f;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.PageUp:
                    _camera.MoveUp(POSITION_STEP_AMOUNT);
                    e.Handled = true;
                    break;
                case Key.PageDown:
                    _camera.MoveDown(POSITION_STEP_AMOUNT);
                    e.Handled = true;
                    break;
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

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);

            Point p = e.GetPosition(this);
            
            _camera.OnMouse((float)p.X, (float)p.Y);
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

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == BoundsProperty)
            {
                _camera.SetWindowSize((float)Bounds.Width, (float)Bounds.Height);                
            }
        }
    }
}