using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Tutorial13
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
                Width = 600,
                Height = 600
            };

            StackPanel controls = BuildControls(_myControl);

            var scroll = new ScrollViewer
            {
                Content = controls
            };

            Panel openGlControlContainer = new Panel
            {
                Background = Brushes.LightGray
            };
            openGlControlContainer.Children.Add(_myControl);
            
            panel.Children.Add(scroll);
            panel.Children.Add(openGlControlContainer);

            Content = panel;

            Width = 1200;
            Height = 750;
        }

        StackPanel BuildControls(OpenGlControl openGlControl)
        {
            StackPanel controls = new StackPanel()
            {
                Spacing = 10,
                Orientation = Orientation.Vertical,
                Margin = new Thickness(30, 20),
            };

            var sliderList = new List<(Panel panel, Action setInitialValue)>
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

            Button resetButton = new Button
            {
                Content = "Reset"
            };

            resetButton.Click += (_, _) =>
            {
                sliderList.ForEach(x => x.setInitialValue());
            };

            controls.Children.Add(resetButton);
            controls.Children.AddRange(sliderList.Select(x => x.panel));

            DockPanel.SetDock(controls, Dock.Left);

            return controls;
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