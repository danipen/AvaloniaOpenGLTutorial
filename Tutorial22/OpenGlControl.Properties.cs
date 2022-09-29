using System;
using System.Numerics;
using Avalonia;

namespace Tutorial22
{
    unsafe partial class OpenGlControl
    {
        public static readonly DirectProperty<OpenGlControl, double> ScaleXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(ScaleX),
                o => o.ScaleX,
                (o, v) => o.ScaleX = v);

        public double ScaleX
        {
            get => _scaleX;
            set => SetAndRaise(ScaleXProperty, ref _scaleX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> ScaleYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(ScaleY),
                o => o.ScaleY,
                (o, v) => o.ScaleY = v);

        public double ScaleY
        {
            get => _scaleY;
            set => SetAndRaise(ScaleYProperty, ref _scaleY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> ScaleZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(ScaleZ),
                o => o.ScaleZ,
                (o, v) => o.ScaleZ = v);

        public double ScaleZ
        {
            get => _scaleZ;
            set => SetAndRaise(ScaleZProperty, ref _scaleZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(TranslateX),
                o => o.TranslateX,
                (o, v) => o.TranslateX = v);

        public double TranslateX
        {
            get => _translateX;
            set => SetAndRaise(TranslateXProperty, ref _translateX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(TranslateY),
                o => o.TranslateY,
                (o, v) => o.TranslateY = v);

        public double TranslateY
        {
            get => _translateY;
            set => SetAndRaise(TranslateYProperty, ref _translateY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> TranslateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(TranslateZ),
                o => o.TranslateZ,
                (o, v) => o.TranslateZ = v);

        public double TranslateZ
        {
            get => _translateZ;
            set => SetAndRaise(TranslateZProperty, ref _translateZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(RotateX),
                o => o.RotateX,
                (o, v) => o.RotateX = v);

        public double RotateX
        {
            get => _rotateX;
            set => SetAndRaise(RotateXProperty, ref _rotateX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(RotateY),
                o => o.RotateY,
                (o, v) => o.RotateY = v);

        public double RotateY
        {
            get => _rotateY;
            set => SetAndRaise(RotateYProperty, ref _rotateY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> RotateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(RotateZ),
                o => o.RotateZ,
                (o, v) => o.RotateZ = v);

        public double RotateZ
        {
            get => _rotateZ;
            set => SetAndRaise(RotateZProperty, ref _rotateZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> FieldOfViewAngleProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(FieldOfViewAngle),
                o => o.FieldOfViewAngle,
                (o, v) => o.FieldOfViewAngle = v);

        public double FieldOfViewAngle
        {
            get => _fieldOfView;
            set => SetAndRaise(FieldOfViewAngleProperty, ref _fieldOfView, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> NearPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(NearPlane),
                o => o.NearPlane,
                (o, v) => o.NearPlane = v);

        public double NearPlane
        {
            get => _nearPlane;
            set => SetAndRaise(NearPlaneProperty, ref _nearPlane, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> FarPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
               nameof(FarPlane),
                o => o.FarPlane,
                (o, v) => o.FarPlane = v);

        public double FarPlane
        {
            get => _farPlane;
            set => SetAndRaise(FarPlaneProperty, ref _farPlane, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraPositionX),
                o => o._camera.CameraPosition.X,
                (o, v) => o._camera.CameraPosition = o._camera.CameraPosition with { X = (float)v});

        public double CameraPositionX
        {
            get => GetValue(CameraPositionXProperty);
            set => SetValue(CameraPositionXProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraPositionY),
                o => o._camera.CameraPosition.Y,
                (o, v) => o._camera.CameraPosition = o._camera.CameraPosition with { Y = (float)v});

        public double CameraPositionY
        {
            get => GetValue(CameraPositionYProperty);
            set => SetValue(CameraPositionYProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraPositionZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraPositionZ),
                o => o._camera.CameraPosition.Z,
                (o, v) => o._camera.CameraPosition = o._camera.CameraPosition with { Z = (float)v});

        public double CameraPositionZ
        {
            get => GetValue(CameraPositionZProperty);
            set => SetValue(CameraPositionZProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetX),
                o => o._camera.CameraTarget.X,
                (o, v) => o._camera.CameraTarget = o._camera.CameraTarget with { X = (float)v});

        public double CameraTargetX
        {
            get => GetValue(CameraTargetXProperty);
            set => SetValue(CameraTargetXProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetY),
                o => o._camera.CameraTarget.Y,
                (o, v) => o._camera.CameraTarget = o._camera.CameraTarget with { Y = (float)v});

        public double CameraTargetY
        {
            get => GetValue(CameraTargetYProperty);
            set => SetValue(CameraTargetYProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraTargetZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraTargetZ),
                o => o._camera.CameraTarget.Z,
                (o, v) => o._camera.CameraTarget = o._camera.CameraTarget with { Z = (float)v});

        public double CameraTargetZ
        {
            get => GetValue(CameraTargetZProperty);
            set => SetValue(CameraTargetZProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraUpXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraUpX),
                o => o._camera.CameraUp.X,
                (o, v) => o._camera.CameraUp = o._camera.CameraUp with { X = (float)v});

        public double CameraUpX
        {
            get => GetValue(CameraUpXProperty);
            set => SetValue(CameraUpXProperty, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> CameraUpYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(CameraUpY),
                o => o._camera.CameraUp.Y,
                (o, v) => o._camera.CameraUp = o._camera.CameraUp with { Y = (float)v});

        public double CameraUpY
        {
            get => GetValue(CameraUpYProperty);
            set => SetValue(CameraUpYProperty, value);
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

        public static readonly DirectProperty<OpenGlControl, double> LightDirXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightDirX),
                o => o.LightDirX,
                (o, v) => o.LightDirX = v);

        public double LightDirX
        {
            get => _lightDirX;
            set => SetAndRaise(LightDirXProperty, ref _lightDirX, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightDirYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightDirY),
                o => o.LightDirY,
                (o, v) => o.LightDirY = v);

        public double LightDirY
        {
            get => _lightDirY;
            set => SetAndRaise(LightDirYProperty, ref _lightDirY, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightDirZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightDirZ),
                o => o.LightDirZ,
                (o, v) => o.LightDirZ = v);

        public double LightDirZ
        {
            get => _lightDirZ;
            set => SetAndRaise(LightDirZProperty, ref _lightDirZ, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightIntensityProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightIntensity),
                o => o.LightIntensity,
                (o, v) => o.LightIntensity = v);

        public double LightIntensity
        {
            get => _lightIntensity;
            set => SetAndRaise(LightIntensityProperty, ref _lightIntensity, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightColorRedProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightColorRed),
                o => o.LightColorRed,
                (o, v) => o.LightColorRed = v);

        public double LightColorRed
        {
            get => _lightColorRed;
            set => SetAndRaise(LightColorRedProperty, ref _lightColorRed, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightColorGreenProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightColorGreen),
                o => o.LightColorGreen,
                (o, v) => o.LightColorGreen = v);

        public double LightColorGreen
        {
            get => _lightColorGreen;
            set => SetAndRaise(LightColorGreenProperty, ref _lightColorGreen, value);
        }

        public static readonly DirectProperty<OpenGlControl, double> LightColorBlueProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                nameof(LightColorBlue),
                o => o.LightColorBlue,
                (o, v) => o.LightColorBlue = v);

        public double LightColorBlue
        {
            get => _lightColorBlue;
            set => SetAndRaise(LightColorBlueProperty, ref _lightColorBlue, value);
        }

        public void ResetCamera()
        {
            _camera.ResetCamera();
        }

        public void ResetView()
        {
            InitView();
        }

        public void ResetLight()
        {
            InitLight();
        }

        void InitLight()
        {
            LightDirX = -1f;
            LightDirY = 1f;
            LightDirZ = -0.5f;
            LightIntensity = 0.2f;
            LightColorRed = 1;
            LightColorGreen = 1;
            LightColorBlue = 1;
        }

        public OpenGlControl()
        {
            _camera = new Camera(this);
            InitView();
            InitLight();
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
                CameraUpZProperty,
                LightDirXProperty,
                LightDirYProperty,
                LightDirZProperty,
                LightIntensityProperty,
                LightColorRedProperty,
                LightColorGreenProperty,
                LightColorBlueProperty);
        }

        void InitView()
        {
            ScaleX = 1f;
            ScaleY = 1f;
            ScaleZ = 1f;
            TranslateX = 0;
            TranslateY = 0;
            TranslateZ = 0;
            RotateX = Math.PI * 2;
            RotateY = Math.PI * 2;
            RotateZ = Math.PI * 2;
            FieldOfViewAngle = 60 * MathF.PI / 180;
            NearPlane = 0.01f;
            FarPlane = 1000;
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

        double _lightDirX;
        double _lightDirY;
        double _lightDirZ;
        double _lightIntensity;
        double _lightColorRed;
        double _lightColorGreen;
        double _lightColorBlue;

        readonly Camera _camera;
    }
}