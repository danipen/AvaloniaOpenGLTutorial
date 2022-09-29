using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

namespace Tutorial22
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.AttachDevTools();
            BuildComponents();
        }

        void BuildComponents()
        {
            DockPanel panel = new DockPanel();

            _myControl = new OpenGlControl();

            StackPanel viewControls = BuildViewControls(_myControl);
            StackPanel cameraControls = BuildCameraControls(_myControl);
            StackPanel lightControls = BuildLightControls(_myControl);

            var viewPanelScroll = new ScrollViewer
            {
                Content = viewControls,
            };

            var cameraPanelScroll = new ScrollViewer
            {
                Content = cameraControls,
            };

            var lightPanelScroll = new ScrollViewer
            {
                Content = lightControls,
                Height = 200,
            };

            DockPanel.SetDock(viewPanelScroll, Dock.Left);
            DockPanel.SetDock(cameraPanelScroll, Dock.Right);
            DockPanel.SetDock(lightPanelScroll, Dock.Bottom);

            panel.Children.Add(viewPanelScroll);
            panel.Children.Add(cameraPanelScroll);
            panel.Children.Add(lightPanelScroll);
            panel.Children.Add(_myControl);

            Content = panel;

            Dispatcher.UIThread.Post(() =>
            {
                _myControl.Focus();
            }, DispatcherPriority.Input - 1);

            Width = 1500;
            Height = 850;
        }

        StackPanel BuildLightControls(OpenGlControl openGlControl)
        {
            StackPanel lightControls = new StackPanel()
            {
                Spacing = 10,
                Orientation = Orientation.Vertical,
                Margin = new Thickness(30, 20),
            };

            var lightSliderList = new List<Panel>()
            {
                BuildSlider("Light direction X", -1, 1, OpenGlControl.LightDirXProperty),
                BuildSlider("Light direction Y", -1, 1, OpenGlControl.LightDirYProperty),
                BuildSlider("Light direction Z", -1, 1, OpenGlControl.LightDirZProperty),
                BuildSlider("Light global intensity", 0, 1, OpenGlControl.LightGlobalIntensityProperty),
                BuildSlider("Light ambient intensity", 0, 1, OpenGlControl.LightAmbientIntensityProperty),
                BuildSlider("Light diffuse intensity", 0, 1, OpenGlControl.LightDiffuseIntensityProperty),
                BuildSlider("Light specular intensity", 0, 1, OpenGlControl.LightSpecularIntensityProperty),
                BuildSlider("Light color red", 0, 1, OpenGlControl.LightColorRedProperty),
                BuildSlider("Light color green", 0, 1, OpenGlControl.LightColorGreenProperty),
                BuildSlider("Light color blue", 0, 1, OpenGlControl.LightColorBlueProperty),
            };

            Button resetLightButton = new Button
            {
                Content = "Reset"
            };

            resetLightButton.Click += (_, _) =>
            {
                _myControl.ResetLight();
            };

            lightControls.Children.Add(resetLightButton);
            lightControls.Children.AddRange(lightSliderList);

            return lightControls;
        }

        StackPanel BuildCameraControls(OpenGlControl openGlControl)
        {
            StackPanel cameraControls = new StackPanel()
            {
                Spacing = 10,
                Orientation = Orientation.Vertical,
                Margin = new Thickness(30, 20)
            };

            var cameraSliderList = new List<Panel>()
            {
                BuildSlider("Camera position X", -100, 100, OpenGlControl.CameraPositionXProperty),
                BuildSlider("Camera position Y", -100, 100, OpenGlControl.CameraPositionYProperty),
                BuildSlider("Camera position Z", -100, 100, OpenGlControl.CameraPositionZProperty),
                BuildSlider("Camera target X", -1, 1, OpenGlControl.CameraTargetXProperty),
                BuildSlider("Camera target Y", -1, 1, OpenGlControl.CameraTargetYProperty),
                BuildSlider("Camera target Z", -1, 1, OpenGlControl.CameraTargetZProperty),
                BuildSlider("Camera up X", -1, 1, OpenGlControl.CameraUpXProperty),
                BuildSlider("Camera up Y", -1, 1, OpenGlControl.CameraUpYProperty),
                BuildSlider("Camera up Z", -1, 1, OpenGlControl.CameraUpZProperty),
            };

            Button resetCameraButton = new Button
            {
                Content = "Reset"
            };

            resetCameraButton.Click += (_, _) =>
            {
                _myControl.ResetCamera();
            };

            cameraControls.Children.Add(resetCameraButton);
            cameraControls.Children.AddRange(cameraSliderList);

            return cameraControls;
        }

        StackPanel BuildViewControls(OpenGlControl openGlControl)
        {
            StackPanel viewControls = new StackPanel()
            {
                Spacing = 10,
                Orientation = Orientation.Vertical,
                Margin = new Thickness(30, 20),
            };

            var viewSliderList = new List<Panel>()
            {
                BuildSlider("Scale X", 0, 4, OpenGlControl.ScaleXProperty),
                BuildSlider("Scale Y", 0, 4, OpenGlControl.ScaleYProperty),
                BuildSlider("Scale Z", 0, 4, OpenGlControl.ScaleZProperty),
                BuildSlider("Translate X", -2, 2, OpenGlControl.TranslateXProperty),
                BuildSlider("Translate Y", -2, 2, OpenGlControl.TranslateYProperty),
                BuildSlider("Translate Z", -4, 4, OpenGlControl.TranslateZProperty),
                BuildSlider("Rotate X", 0, 4 * MathF.PI, OpenGlControl.RotateXProperty),
                BuildSlider("Rotate Y", 0, 4 * MathF.PI, OpenGlControl.RotateYProperty),
                BuildSlider("Rotate Z", 0, 4 * MathF.PI, OpenGlControl.RotateZProperty),
                BuildSlider("Field of View", 0.1, MathF.PI  - 0.1f, OpenGlControl.FieldOfViewAngleProperty, true),
                BuildSlider("Near Clipping Plane", 0.01, 10, OpenGlControl.NearPlaneProperty),
                BuildSlider("Far Clipping Plane", 10.1, 10000, OpenGlControl.FarPlaneProperty)
            };

            Button resetViewButton = new Button
            {
                Content = "Reset"
            };

            resetViewButton.Click += (_, _) =>
            {
                _myControl.ResetView();
            };

            viewControls.Children.Add(resetViewButton);
            viewControls.Children.AddRange(viewSliderList);

            return viewControls;
        }

        Panel BuildSlider(string label, double min, double max, AvaloniaProperty property, bool convertToDegrees = false)
        {
            TextBlock labelTextBlock = new TextBlock();
            Slider slider = new Slider();

            slider[!Slider.ValueProperty] = _myControl[!property];

            SetLabelText(labelTextBlock, slider.Value);

            void SetLabelText(TextBlock textBlock, double sliderValue)
            {
                if (convertToDegrees)
                {
                    textBlock.Text = string.Format("{0} ({1}º)",
                        label,
                        (int)(sliderValue * 180 / MathF.PI));

                    return;
                }

                textBlock.Text = string.Format("{0} ({1})",
                    label,
                    Math.Round(sliderValue, 2));
            }

            slider.TickFrequency = 0.1;
            slider.MinWidth = 350;
            slider.Maximum = max;
            slider.Minimum = min;
            slider.PropertyChanged += (_, e) =>
            {
                if (e.Property != RangeBase.ValueProperty)
                    return;

                SetLabelText(labelTextBlock, slider.Value);
            };

            StackPanel panel = new StackPanel();
            panel.Children.Add(labelTextBlock);
            panel.Children.Add(slider);

            return panel;
        }

        OpenGlControl _myControl = null!;
    }
}