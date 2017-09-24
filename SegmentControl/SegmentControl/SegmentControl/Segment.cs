using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SegmentControl
{
    public class Segment : StackLayout
    {
        private readonly byte[][] table =
        {
            // a, b, c, d, e, f, g
            new byte[] { 1, 1, 1, 1, 1, 1, 0 }, // 0
            new byte[] { 0, 1, 1, 0, 0, 0, 0 }, // 1
            new byte[] { 1, 1, 0, 1, 1, 0, 1 }, // 2
            new byte[] { 1, 1, 1, 1, 0, 0, 1 }, // 3
            new byte[] { 0, 1, 1, 0, 0, 1, 1 }, // 4
            new byte[] { 1, 0, 1, 1, 0, 1, 1 }, // 5
            new byte[] { 1, 0, 1, 1, 1, 1, 1 }, // 6
            new byte[] { 1, 1, 1, 0, 0, 0, 0 }, // 7
            new byte[] { 1, 1, 1, 1, 1, 1, 1 }, // 8
            new byte[] { 1, 1, 1, 0, 0, 1, 1 }, // 9
            new byte[] { 0, 0, 0, 0, 0, 0, 0 }, // None
            new byte[] { 0, 0, 0, 0, 0, 0, 0 }, // Colon
        };
        private const int width = 5;
        private const int height = 25;

        private string _value;
        private int _count;

        public enum Sources
        {
            Value = 0,
            Time = 1
        }

        private AbsoluteLayout SetLayout(string name)
        {
            return this.Children.Cast<AbsoluteLayout>().FirstOrDefault(f => f.ClassId == name);
        }

        private Frame AddElement(string name, int left, int top, int width, int height)
        {
            Frame frame = new Frame()
            {
                ClassId = name,
                WidthRequest = width,
                HeightRequest = height,
                BackgroundColor = Color.Accent,
                InputTransparent = true
            };
            Rectangle rect = new Rectangle()
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };
            frame.SetValue(AbsoluteLayout.LayoutBoundsProperty, rect);
            return frame;
        }

        private Frame SetElement(AbsoluteLayout layout, string name)
        {
            return layout.Children.Cast<Frame>().FirstOrDefault(f => f.ClassId == name);
        }

        private void AddSegment(string name)
        {
            AbsoluteLayout layout = new AbsoluteLayout()
            {
                Margin = new Thickness(2),
                ClassId = name
            };
            layout.Children.Add(AddElement($"{name}.a", width, 0, height, width));
            layout.Children.Add(AddElement($"{name}.h", width + width + width, width + width + width, width, width));
            layout.Children.Add(AddElement($"{name}.f", 0, width, width, height));
            layout.Children.Add(AddElement($"{name}.b", height + width, width, width, height));
            layout.Children.Add(AddElement($"{name}.g", width, height + width, height, width));
            layout.Children.Add(AddElement($"{name}.e", 0, height + width + width, width, height));
            layout.Children.Add(AddElement($"{name}.c", height + width, height + width + width, width, height));
            layout.Children.Add(AddElement($"{name}.i", width + width + width, height + width + width + width + width, width, width));
            layout.Children.Add(AddElement($"{name}.d", width, height + height + width + width, height, width));
            this.Children.Add(layout);
        }

        private void SetSegment(string name, int digit)
        {
            AbsoluteLayout layout = SetLayout(name);
            byte[] values = table[digit];
            SetElement(layout, $"{name}.a").Opacity = values[0];
            SetElement(layout, $"{name}.b").Opacity = values[1];
            SetElement(layout, $"{name}.c").Opacity = values[2];
            SetElement(layout, $"{name}.d").Opacity = values[3];
            SetElement(layout, $"{name}.e").Opacity = values[4];
            SetElement(layout, $"{name}.f").Opacity = values[5];
            SetElement(layout, $"{name}.g").Opacity = values[6];
            SetElement(layout, $"{name}.h").Opacity = digit > 10 ? 1 : 0;
            SetElement(layout, $"{name}.i").Opacity = digit > 10 ? 1 : 0;
        }

        private void GetLayout()
        {
            char[] array = _value.ToCharArray();
            int length = array.Length;
            List<int> list = Enumerable.Range(0, length).ToList();
            if (_count != length)
            {
                this.Children.Clear();
                foreach (int item in list)
                {
                    AddSegment(item.ToString());
                }
                _count = length;
            }
            foreach (int item in list)
            {
                string val = array[item].ToString();
                int digit = val == ":" ? 11 : int.Parse(val);
                SetSegment(item.ToString(), digit);
            }
        }

        public Segment()
        {
            this.Spacing = 0;
            this.Orientation = StackOrientation.Horizontal;
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                if (this.IsEnabled)
                {
                    if (Source == Sources.Time)
                    {
                        Value = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
                return true;
            });
        }

        public static readonly BindableProperty SourceProperty =
        BindableProperty.Create<Segment, Sources>(p => p.Source, Sources.Value,
        BindingMode.Default, null, null);

        public Sources Source
        {
            get { return (Sources)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                GetLayout();
            }
        }
    }
}