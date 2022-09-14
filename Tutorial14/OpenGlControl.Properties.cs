using System;
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
            get => _cameraPositionX;
            set => SetAndRaise(CameraPositionXProperty, ref _cameraPositionX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, double> CameraPositionYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraPositionY",
                o => o.CameraPositionY,
                (o, v) => o.CameraPositionY = v);

        public double CameraPositionY
        {
            get => _cameraPositionY;
            set => SetAndRaise(CameraPositionYProperty, ref _cameraPositionY, value);
        }
 
        public static readonly DirectProperty<OpenGlControl, double> CameraPositionZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraPositionZ",
                o => o.CameraPositionZ,
                (o, v) => o.CameraPositionZ = v);

        public double CameraPositionZ
        {
            get => _cameraPositionZ;
            set => SetAndRaise(CameraPositionZProperty, ref _cameraPositionZ, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, double> CameraTargetXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraTargetX",
                o => o.CameraTargetX,
                (o, v) => o.CameraTargetX = v);

        public double CameraTargetX
        {
            get => _cameraTargetX;
            set => SetAndRaise(CameraTargetXProperty, ref _cameraTargetX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, double> CameraTargetYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraTargetY",
                o => o.CameraTargetY,
                (o, v) => o.CameraTargetY = v);

        public double CameraTargetY
        {
            get => _cameraTargetY;
            set => SetAndRaise(CameraTargetYProperty, ref _cameraTargetY, value);
        }
 
        public static readonly DirectProperty<OpenGlControl, double> CameraTargetZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraTargetZ",
                o => o.CameraTargetZ,
                (o, v) => o.CameraTargetZ = v);

        public double CameraTargetZ
        {
            get => _cameraTargetZ;
            set => SetAndRaise(CameraTargetZProperty, ref _cameraTargetZ, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, double> CameraUpXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraUpX",
                o => o.CameraUpX,
                (o, v) => o.CameraUpX = v);

        public double CameraUpX
        {
            get => _cameraUpX;
            set => SetAndRaise(CameraUpXProperty, ref _cameraUpX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, double> CameraUpYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraUpY",
                o => o.CameraUpY,
                (o, v) => o.CameraUpY = v);

        public double CameraUpY
        {
            get => _cameraUpY;
            set => SetAndRaise(CameraUpYProperty, ref _cameraUpY, value);
        }
 
        public static readonly DirectProperty<OpenGlControl, double> CameraUpZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, double>(
                "CameraUpZ",
                o => o.CameraUpZ,
                (o, v) => o.CameraUpZ = v);

        public double CameraUpZ
        {
            get => _cameraUpZ;
            set => SetAndRaise(CameraUpZProperty, ref _cameraUpZ, value);
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
            InitCamera();
            InitView();
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
            CameraPositionX = 0;
            CameraPositionY = 0;
            CameraPositionZ = -2;
        
            CameraTargetX = 0;
            CameraTargetY = 0;
            CameraTargetZ = 0;
        
            CameraUpX = 0;
            CameraUpY = -1;
            CameraUpZ = 0;
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

        double _cameraPositionX;
        double _cameraPositionY;
        double _cameraPositionZ;
        
        double _cameraTargetX;
        double _cameraTargetY;
        double _cameraTargetZ;
        
        double _cameraUpX;
        double _cameraUpY;
        double _cameraUpZ;
    }
}