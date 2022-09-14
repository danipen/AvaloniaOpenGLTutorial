using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Tutorial14
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

            _myControl = new OpenGlControl
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

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
            
            var cameraSliderList = new List<(Panel panel, Action setInitialValue)>
            {
                BuildSlider("Camera position X", -100, 100, openGlControl.CameraPositionX, (v) => openGlControl.CameraPositionX = v),
                BuildSlider("Camera position Y", -100, 100, openGlControl.CameraPositionY, (v) => openGlControl.CameraPositionY = v),
                BuildSlider("Camera position Z", -10, 10, openGlControl.CameraPositionZ, (v) => openGlControl.CameraPositionZ = v),
                BuildSlider("Camera target X", -100, 100, openGlControl.CameraTargetX, (v) => openGlControl.CameraTargetX = v),
                BuildSlider("Camera target Y", -100, 100, openGlControl.CameraTargetY, (v) => openGlControl.CameraTargetY = v),
                BuildSlider("Camera target Z", -100, 100, openGlControl.CameraTargetZ, (v) => openGlControl.CameraTargetZ = v),
                BuildSlider("Camera up X", -1, 1, openGlControl.CameraUpX, (v) => openGlControl.CameraUpX = v),
                BuildSlider("Camera up Y", -1, 1, openGlControl.CameraUpY, (v) => openGlControl.CameraUpY = v),
                BuildSlider("Camera up Z", -1, 1, openGlControl.CameraUpZ, (v) => openGlControl.CameraUpZ = v),
            };
            
            Button resetCameraButton = new Button
            {
                Content = "Reset"
            };
            
            resetCameraButton.Click += (_, _) =>
            {
                cameraSliderList.ForEach(x => x.setInitialValue());
            };
            
            cameraControls.Children.Add(resetCameraButton);
            cameraControls.Children.AddRange(cameraSliderList.Select(x => x.panel));

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
            
            var viewSliderList = new List<(Panel panel, Action setInitialValue)>
            {
                BuildSlider("Scale X", 0, 4, openGlControl.ScaleX, (v) => openGlControl.ScaleX = v),
                BuildSlider("Scale Y", 0, 4, openGlControl.ScaleY, (v) => openGlControl.ScaleY = v),
                BuildSlider("Scale Z", 0, 4, openGlControl.ScaleZ, (v) => openGlControl.ScaleZ = v),
                BuildSlider("Translate X", -2, 2, openGlControl.TranslateX, (v) => openGlControl.TranslateX = v),
                BuildSlider("Translate Y", -2, 2, openGlControl.TranslateY, (v) => openGlControl.TranslateY = v),
                BuildSlider("Translate Z", -4, 4, openGlControl.TranslateZ, (v) => openGlControl.TranslateZ = v),
                BuildSlider("Rotate X", 0, 2 * MathF.PI, openGlControl.RotateX, (v) => openGlControl.RotateX = v),
                BuildSlider("Rotate Y", 0, 2 * MathF.PI, openGlControl.RotateY, (v) => openGlControl.RotateY = v),
                BuildSlider("Rotate Z", 0, 2 * MathF.PI, openGlControl.RotateZ, (v) => openGlControl.RotateZ = v),
                BuildSlider("Field of View", 0.1, MathF.PI  - 0.1f, openGlControl.FieldOfViewAngle, (v) => openGlControl.FieldOfViewAngle = v, true),
                BuildSlider("Near Clipping Plane", 0.01, 10, openGlControl.NearPlane, (v) => openGlControl.NearPlane = v),
                BuildSlider("Far Clipping Plane", 10.1, 1000, openGlControl.FarPlane, (v) => openGlControl.FarPlane = v)
            };
            
            Button resetViewButton = new Button
            {
                Content = "Reset"
            };

            resetViewButton.Click += (_, _) =>
            {
                viewSliderList.ForEach(x => x.setInitialValue());
            };
            
            viewControls.Children.Add(resetViewButton);
            viewControls.Children.AddRange(viewSliderList.Select(x => x.panel));
            
            return viewControls;
        }

        (Panel, Action) BuildSlider(string label, double min, double max, double initialValue, Action<float> callback, bool convertToDegrees = false)
        {
            TextBlock labelTextBlock = new TextBlock();
            Slider slider = new Slider();
            SetLabelText(labelTextBlock, initialValue);
            
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

                callback((float)slider.Value);

                SetLabelText(labelTextBlock, slider.Value);
            };

            void SetInitialValue()
            {
                slider.Value = initialValue;
            }

            SetInitialValue();

            StackPanel panel = new StackPanel();
            panel.Children.Add(labelTextBlock);
            panel.Children.Add(slider);

            return (panel, SetInitialValue);
        }
        
        OpenGlControl _myControl = null!;
    }
}