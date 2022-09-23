using System;
using System.Numerics;
using Avalonia;
using Avalonia.Automation.Provider;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;

namespace Tutorial22
{
    partial class OpenGlControl : Camera.IChangedCallback
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _pressedKey = e.Key;

            if (ProcessInputKey(e.Key))
                e.Handled = true;
            
            InvalidateVisual();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == _pressedKey)
            {
                _pressedKey = Key.None;
                InvalidateVisual();
            }
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);
            Point p = e.GetPosition(this);
            
            _camera.OnMouse((float)Bounds.Width - (float)p.X, (float)p.Y);
        }

        protected override void OnPointerExited(PointerEventArgs e)
        {
            base.OnPointerExited(e);
            
            _camera.OnMouseExited();
        }

        public void PositionChanged(Vector3 oldVector, Vector3 newVector)
        {
            Dispatcher.UIThread.Post(
                () =>
                {
                    RaisePropertyChanged(CameraPositionXProperty, oldVector.X, newVector.X);
                    RaisePropertyChanged(CameraPositionYProperty, oldVector.Y, newVector.Y);
                    RaisePropertyChanged(CameraPositionZProperty, oldVector.Z, newVector.Z);
                });
        }

        public void TargetChanged(Vector3 oldVector, Vector3 newVector)
        {
            Dispatcher.UIThread.Post(
                () =>
                {
                    RaisePropertyChanged(CameraTargetXProperty, oldVector.X, newVector.X);
                    RaisePropertyChanged(CameraTargetYProperty, oldVector.Y, newVector.Y);
                    RaisePropertyChanged(CameraTargetZProperty, oldVector.Z, newVector.Z);
                });
        }

        public void UpChanged(Vector3 oldVector, Vector3 newVector)
        {
            Dispatcher.UIThread.Post(
                () =>
                {
                    RaisePropertyChanged(CameraUpXProperty, oldVector.X, newVector.X);
                    RaisePropertyChanged(CameraUpYProperty, oldVector.Y, newVector.Y);
                    RaisePropertyChanged(CameraUpZProperty, oldVector.Z, newVector.Z);
                });
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == BoundsProperty)
            {
                _camera.SetWindowSize((float)Bounds.Width, (float)Bounds.Height);
            }
        }

        bool ProcessInputKey(Key key)
        {
            switch (key)
            {
                case Key.PageUp:
                    _camera.MoveUp(POSITION_STEP_AMOUNT);
                    return true;
                case Key.PageDown:
                    _camera.MoveDown(POSITION_STEP_AMOUNT);
                    return true;
                case Key.W:
                case Key.Up:
                    _camera.MoveForward(POSITION_STEP_AMOUNT);
                    return true;
                case Key.S:
                case Key.Down:
                    _camera.MoveBackward(POSITION_STEP_AMOUNT);
                    return true;
                case Key.A:
                case Key.Left:
                    _camera.MoveLeft(POSITION_STEP_AMOUNT);
                    return true;
                case Key.D:
                case Key.Right:
                    _camera.MoveRight(POSITION_STEP_AMOUNT);
                    return true;
            }

            return false;
        }
        
        Key _pressedKey = Key.None;
        
        const float POSITION_STEP_AMOUNT = 0.05f;
    }
}