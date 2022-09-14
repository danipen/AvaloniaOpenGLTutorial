using System;
using System.Numerics;
using Avalonia;

namespace Tutorial14
{
    unsafe partial class OpenGlControl
    {
        public static readonly DirectProperty<OpenGlControl, double> ScaleXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "ScaleX",
                o => o.ScaleX,
                (o, v) => o.ScaleX = v);

        public double ScaleX
        {
            get => _scaleX;
            set => SetAndRaise(ScaleXProperty, ref _scaleX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> ScaleYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "ScaleY",
                o => o.ScaleY,
                (o, v) => o.ScaleY = v);

        public double ScaleY
        {
            get => _scaleY;
            set => SetAndRaise(ScaleYProperty, ref _scaleY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> ScaleZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "ScaleZ",
                o => o.ScaleZ,
                (o, v) => o.ScaleZ = v);

        public double ScaleZ
        {
            get => _scaleZ;
            set => SetAndRaise(ScaleZProperty, ref _scaleZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "TranslateX",
                o => o.TranslateX,
                (o, v) => o.TranslateX = v);

        public double TranslateX
        {
            get => _translateX;
            set => SetAndRaise(TranslateXProperty, ref _translateX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "TranslateY",
                o => o.TranslateY,
                (o, v) => o.TranslateY = v);

        public double TranslateY
        {
            get => _translateY;
            set => SetAndRaise(TranslateYProperty, ref _translateY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "TranslateZ",
                o => o.TranslateZ,
                (o, v) => o.TranslateZ = v);

        public double TranslateZ
        {
            get => _translateZ;
            set => SetAndRaise(TranslateZProperty, ref _translateZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "RotateX",
                o => o.RotateX,
                (o, v) => o.RotateX = v);

        public double RotateX
        {
            get => _rotateX;
            set => SetAndRaise(RotateXProperty, ref _rotateX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "RotateY",
                o => o.RotateY,
                (o, v) => o.RotateY = v);

        public double RotateY
        {
            get => _rotateY;
            set => SetAndRaise(RotateYProperty, ref _rotateY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "RotateZ",
                o => o.RotateZ,
                (o, v) => o.RotateZ = v);

        public double RotateZ
        {
            get => _rotateZ;
            set => SetAndRaise(RotateZProperty, ref _rotateZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> FieldOfViewAngleProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "FieldOfViewAngle",
                o => o.FieldOfViewAngle,
                (o, v) => o.FieldOfViewAngle = v);

        public double FieldOfViewAngle
        {
            get => _fieldOfView;
            set => SetAndRaise(FieldOfViewAngleProperty, ref _fieldOfView, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> NearPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "NearPlane",
                o => o.NearPlane,
                (o, v) => o.NearPlane = v);

        public double NearPlane
        {
            get => _nearPlane;
            set => SetAndRaise(NearPlaneProperty, ref _nearPlane, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> FarPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "FarPlane",
                o => o.FarPlane,
                (o, v) => o.FarPlane = v);

        public double FarPlane
        {
            get => _farPlane;
            set => SetAndRaise(FarPlaneProperty, ref _farPlane, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraPositionX",
                o => o.CameraPositionX,
                (o, v) => o.CameraPositionX = v);

        public double CameraPositionX
        {
            get => _camera.CameraPosition.X;
            set
            {
                float old = _camera.CameraPosition.X;
                _camera.CameraPosition = _camera.CameraPosition with { X = (float)value };
                RaisePropertyChanged(CameraPositionXProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraPositionY",
                o => o.CameraPositionY,
                (o, v) => o.CameraPositionY = v);

        public double CameraPositionY
        {
            get => _camera.CameraPosition.Y;
            set
            {
                float old = _camera.CameraPosition.Y;
                _camera.CameraPosition = _camera.CameraPosition with { Y = (float)value };
                RaisePropertyChanged(CameraPositionYProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraPositionZ",
                o => o.CameraPositionZ,
                (o, v) => o.CameraPositionZ = v);

        public double CameraPositionZ
        {
            get => _camera.CameraPosition.Z;
            set
            {
                float old = _camera.CameraPosition.Z;
                _camera.CameraPosition = _camera.CameraPosition with { Z = (float)value };
                RaisePropertyChanged(CameraPositionZProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetX),
                o => o.CameraTargetX,
                (o, v) => o.CameraTargetX = v);

        public double CameraTargetX
        {
            get => _camera.CameraTarget.X;
            set
            {
                float old = _camera.CameraTarget.X;
                _camera.CameraTarget = _camera.CameraTarget with { X = (float)value };
                RaisePropertyChanged(CameraTargetXProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetY),
                o => o.CameraTargetY,
                (o, v) => o.CameraTargetY = v);

        public double CameraTargetY
        {
            get => _camera.CameraTarget.Y;
            set
            {
                float old = _camera.CameraTarget.Y;
                _camera.CameraTarget = _camera.CameraTarget with { Y = (float)value };
                RaisePropertyChanged(CameraTargetYProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetZ),
                o => o.CameraTargetZ,
                (o, v) => o.CameraTargetZ = v);

        public double CameraTargetZ
        {
            get => _camera.CameraTarget.Z;
            set
            {
                float old = _camera.CameraTarget.Z;
                _camera.CameraTarget = _camera.CameraTarget with { Z = (float)value };
                RaisePropertyChanged(CameraTargetZProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraUpXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraUpX),
                o => o.CameraUpX,
                (o, v) => o.CameraUpX = v);

        public double CameraUpX
        {
            get => _camera.CameraUp.X;
            set
            {
                float old = _camera.CameraUp.X;
                _camera.CameraUp = _camera.CameraUp with { X = (float)value };
                RaisePropertyChanged(CameraUpXProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraUpYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraUpY),
                o => o.CameraUpY,
                (o, v) => o.CameraUpY = v);

        public double CameraUpY
        {
            get => _camera.CameraUp.Y;
            set
            {
                float old = _camera.CameraUp.Y;
                _camera.CameraUp = _camera.CameraUp with { Y = (float)value };
                RaisePropertyChanged(CameraUpYProperty, old, value);
            }
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraUpZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraUpZ),
                o => o._camera.CameraUp.Z,
                (o, v) => o._camera.CameraUp = o._camera.CameraUp with { Z = (float)v});

        public double CameraUpZ
        {
            get => GetValue(CameraUpZProperty);
            set => SetValue(CameraUpZProperty, value);
        }

        public void ResetCamera()
        {
            InitCamera();
        }

        public void ResetView()
        {
            InitView();
        }

        public OpenGlControl()
        {
            _camera = new Camera(this);
            InitCamera();
            InitView();
            Focusable = true;
        }

        static OpenGlControl()
        {
            AffectsRender<OpenGlControl>(
                ScaleXProperty,
                ScaleYProperty,
                ScaleZProperty,
                TranslateXProperty,
                TranslateYProperty,
                TranslateZProperty,
                RotateXProperty,
                RotateYProperty,
                RotateZProperty,
                FieldOfViewAngleProperty,
                NearPlaneProperty,
                FarPlaneProperty,
                CameraPositionXProperty,
                CameraPositionYProperty,
                CameraPositionZProperty,
                CameraTargetXProperty,
                CameraTargetYProperty,
                CameraTargetZProperty,
                CameraUpXProperty,
                CameraUpYProperty,
                CameraUpZProperty);
        }

        void InitCamera()
        {
            _camera.ResetCamera();
        }

        void InitView()
        {
            ScaleX = 1f;
            ScaleY = 1f;
            ScaleZ = 1f;
            TranslateX = 0;
            TranslateY = 0;
            TranslateZ = 0;
            RotateX = 0;
            RotateY = 0;
            RotateZ = 0;
            FieldOfViewAngle = 60 * MathF.PI / 180;
            NearPlane = 0.01f;
            FarPlane = 100;
        }

        double _scaleX;
        double _scaleY;
        double _scaleZ;
        double _translateX;
        double _translateY;
        double _translateZ;
        double _rotateX;
        double _rotateY;
        double _rotateZ;
        double _fieldOfView;
        double _nearPlane;
        double _farPlane;

        readonly Camera _camera;
    }
}