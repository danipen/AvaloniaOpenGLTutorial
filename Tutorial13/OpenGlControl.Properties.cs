using System;
using Avalonia;

namespace Tutorial13
{
    unsafe partial class OpenGlControl
    {
        public static readonly DirectProperty<OpenGlControl, float> ScaleXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "ScaleX",
                o => o.ScaleX,
                (o, v) => o.ScaleX = v);

        public float ScaleX
        {
            get => _scaleX;
            set => SetAndRaise(ScaleXProperty, ref _scaleX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> ScaleYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "ScaleY", 
                o => o.ScaleY,
                (o, v) => o.ScaleY = v);

        public float ScaleY
        {
            get => _scaleY;
            set => SetAndRaise(ScaleYProperty, ref _scaleY, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> ScaleZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "ScaleZ",
                o => o.ScaleZ,
                (o, v) => o.ScaleZ = v);

        public float ScaleZ
        {
            get => _scaleZ;
            set => SetAndRaise(ScaleZProperty, ref _scaleZ, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> TranslateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "TranslateX",
                o => o.TranslateX,
                (o, v) => o.TranslateX = v);

        public float TranslateX
        {
            get => _translateX;
            set => SetAndRaise(TranslateXProperty, ref _translateX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> TranslateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "TranslateY",
                o => o.TranslateY,
                (o, v) => o.TranslateY = v);

        public float TranslateY
        {
            get => _translateY;
            set => SetAndRaise(TranslateYProperty, ref _translateY, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> TranslateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "TranslateZ",
                o => o.TranslateZ,
                (o, v) => o.TranslateZ = v);

        public float TranslateZ
        {
            get => _translateZ;
            set => SetAndRaise(TranslateZProperty, ref _translateZ, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> RotateXProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "RotateX",
                o => o.RotateX,
                (o, v) => o.RotateX = v);

        public float RotateX
        {
            get => _rotateX;
            set => SetAndRaise(RotateXProperty, ref _rotateX, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> RotateYProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "RotateY",
                o => o.RotateY,
                (o, v) => o.RotateY = v);

        public float RotateY
        {
            get => _rotateY;
            set => SetAndRaise(RotateYProperty, ref _rotateY, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> RotateZProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "RotateZ",
                o => o.RotateZ,
                (o, v) => o.RotateZ = v);

        public float RotateZ
        {
            get => _rotateZ;
            set => SetAndRaise(RotateZProperty, ref _rotateZ, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> FieldOfViewAngleProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "FieldOfViewAngle",
                o => o.FieldOfViewAngle,
                (o, v) => o.FieldOfViewAngle = v);

        public float FieldOfViewAngle
        {
            get => _fieldOfView;
            set => SetAndRaise(FieldOfViewAngleProperty, ref _fieldOfView, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> NearPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "NearPlane",
                o => o.NearPlane,
                (o, v) => o.NearPlane = v);

        public float NearPlane
        {
            get => _nearPlane;
            set => SetAndRaise(NearPlaneProperty, ref _nearPlane, value);
        }
        
        public static readonly DirectProperty<OpenGlControl, float> FarPlaneProperty =
            AvaloniaProperty.RegisterDirect<OpenGlControl, float>(
                "FarPlane",
                o => o.FarPlane,
                (o, v) => o.FarPlane = v);

        public float FarPlane
        {
            get => _farPlane;
            set => SetAndRaise(FarPlaneProperty, ref _farPlane, value);
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
                FarPlaneProperty);
        }
        
        float _scaleX = 1;
        float _scaleY = 1;
        float _scaleZ = 1;
        float _translateX = 0;
        float _translateY = 0;
        float _translateZ = 0;
        float _rotateX = 0;
        float _rotateY = 0;
        float _rotateZ = 0;
        float _fieldOfView = 60 * MathF.PI / 180;
        float _nearPlane = 0.1f;
        float _farPlane = 100;
    }
}