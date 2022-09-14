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
                BuildSlider("Scale X", 0, 2, 1, (v) => openGlControl.SetScaleX(v)),
                BuildSlider("Scale Y", 0, 2, 1, (v) => openGlControl.SetScaleY(v)),
                BuildSlider("Scale Z", 0, 2, 1, (v) => openGlControl.SetScaleZ(v)),
                BuildSlider("Translate X", -2, 2, 0, (v) => openGlControl.SetTranslateX(v)),
                BuildSlider("Translate Y", -2, 2, 0, (v) => openGlControl.SetTranslateY(v)),
                BuildSlider("Translate Z", -2, 2, 0, (v) => openGlControl.SetTranslateZ(v)),
                BuildSlider("Rotate X", 0, 2 * MathF.PI, 0, (v) => openGlControl.SetRotateX(v)),
                BuildSlider("Rotate Y", 0, 2 * MathF.PI, 0, (v) => openGlControl.SetRotateY(v)),
                BuildSlider("Rotate Z", 0, 2 * MathF.PI, 0, (v) => openGlControl.SetRotateZ(v)),
                BuildSlider("Field of View", 0.1, MathF.PI  - 0.1f, 60 * MathF.PI / 180, (v) => openGlControl.SetFieldOfView(v)),
                BuildSlider("Near Clipping Plane", 0.01, 10, 1, (v) => openGlControl.SetNearPlane(v)),
                BuildSlider("Far Clipping Plane", 10, 1000, 100, (v) => openGlControl.SetFarPlane(v))
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

        (Panel, Action) BuildSlider(string label, double min, double max, double initialValue, Action<float> callback)
        {
            TextBlock labelTextBlock = new TextBlock();
            Slider slider = new Slider();
            SetLabelText(labelTextBlock, initialValue);
            
            void SetLabelText(TextBlock textBlock, double sliderValue)
            {
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