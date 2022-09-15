using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Tutorial15
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

            var viewPanelScroll = new ScrollViewer
            {
                Content = viewControls
            };

            var cameraPanelScroll = new ScrollViewer
            {
                Content = cameraControls
            };


            DockPanel.SetDock(viewPanelScroll, Dock.Left);
            DockPanel.SetDock(cameraPanelScroll, Dock.Right);

            panel.Children.Add(viewPanelScroll);
            panel.Children.Add(cameraPanelScroll);
            panel.Children.Add(_myControl);

            Content = panel;

            Width = 1500;
            Height = 750;
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
                BuildSlider("Camera position X", -10, 10, OpenGlControl.CameraPositionXProperty),
                BuildSlider("Camera position Y", -10, 10, OpenGlControl.CameraPositionYProperty),
                BuildSlider("Camera position Z", -10, 10, OpenGlControl.CameraPositionZProperty),
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
                BuildSlider("Rotate X", 0, 2 * MathF.PI, OpenGlControl.RotateXProperty),
                BuildSlider("Rotate Y", 0, 2 * MathF.PI, OpenGlControl.RotateYProperty),
                BuildSlider("Rotate Z", 0, 2 * MathF.PI, OpenGlControl.RotateZProperty),
                BuildSlider("Field of View", 0.1, MathF.PI  - 0.1f, OpenGlControl.FieldOfViewAngleProperty, true),
                BuildSlider("Near Clipping Plane", 0.01, 10, OpenGlControl.NearPlaneProperty),
                BuildSlider("Far Clipping Plane", 10.1, 1000, OpenGlControl.FarPlaneProperty)
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