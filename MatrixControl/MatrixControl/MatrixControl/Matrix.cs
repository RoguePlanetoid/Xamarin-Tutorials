using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MatrixControl
{
    public class Matrix : StackLayout
    {
        private readonly byte[][] table =
        {
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 0
            new byte[] {
        0,0,0,0,0,0,0,0,
        0,0,0,1,1,0,0,0,
        0,1,1,1,1,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,0,0,0,0,0
        }, // 1 
            new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 2
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 3
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,0,0,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 4
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 5
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 6
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 7
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 8
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,1,1,1,1,1,1,0,
        0,1,1,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,1,1,0,
        0,1,1,1,1,1,1,0,
        0,0,0,0,0,0,0,0
        }, // 9
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0
        }, // Space
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,0,0,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,1,1,0,0,0,
        0,0,0,0,0,0,0,0
        }, // Colon
        new byte[] {
        0,0,0,0,0,0,0,0,
        0,0,0,0,0,1,1,0,
        0,0,0,0,1,1,0,0,
        0,0,0,1,1,0,0,0,
        0,0,1,1,0,0,0,0,
        0,1,1,0,0,0,0,0,
        0,0,0,0,0,0,0,0
        } // Slash
        };

        private readonly List<char> glyphs =
            new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ':', '/' };
        private const int columns = 8;
        private const int rows = 7;
        private const int padding = 1;

        private string _value;
        private int _count;

        public enum Sources
        {
            Value = 0,
            Time = 1,
            Date = 2,
            TimeDate = 3
        }

        public static readonly BindableProperty ForegroundProperty =
        BindableProperty.Create<Matrix, Color>(p => p.Foreground, Color.Accent,
        BindingMode.Default, null, null);

        public static readonly BindableProperty SourceProperty =
        BindableProperty.Create<Matrix, Sources>(p => p.Source, Sources.Value,
        BindingMode.Default, null, null);

        public static readonly BindableProperty SizeProperty =
        BindableProperty.Create<Matrix, int>(p => p.Size, 4,
        BindingMode.Default, null, null);

        public Color Foreground
        {
            get { return (Color)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public Sources Source
        {
            get { return (Sources)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        private AbsoluteLayout SetSection(string name)
        {
            return this.Children.Cast<AbsoluteLayout>().FirstOrDefault(f => f.ClassId == name);
        }

        private Frame AddElement(string name, int left, int top)
        {
            Frame frame = new Frame()
            {
                ClassId = name,
                WidthRequest = Size,
                HeightRequest = Size,
                BackgroundColor = Foreground,
                Padding = 0,
                InputTransparent = true,
            };
            Rectangle rect = new Rectangle()
            {
                Left = left,
                Top = top,
                Width = Size,
                Height = Size
            };
            frame.SetValue(AbsoluteLayout.LayoutBoundsProperty, rect);
            return frame;
        }

        private Frame SetElement(AbsoluteLayout layout, string name)
        {
            return layout.Children.Cast<Frame>().FirstOrDefault(f => f.ClassId == name);
        }

        private void AddSection(string name)
        {
            AbsoluteLayout layout = new AbsoluteLayout()
            {
                ClassId = name
            };
            int x = 0;
            int y = 0;
            int index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    layout.Children.Add(AddElement($"{name}.{index}", x, y));
                    x = (x + Size + padding);
                    index++;
                }
                x = 0;
                y = (y + Size + padding);
            }
            this.Children.Add(layout);
        }

        private void SetLayout(string name, char glyph)
        {
            AbsoluteLayout layout = SetSection(name);
            int pos = glyphs.IndexOf(glyph);
            byte[] values = table[pos];
            for (int index = 0; index < layout.Children.Count; index++)
            {
                SetElement(layout, $"{name}.{index}").Opacity = values[index];
            }
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
                    AddSection(item.ToString());
                }
                _count = length;
            }
            foreach (int item in list)
            {
                SetLayout(item.ToString(), array[item]);
            }
        }

        public Matrix()
        {
            this.Spacing = 0;
            this.Orientation = StackOrientation.Horizontal;
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                if (this.IsEnabled)
                {
                    if (Source != Sources.Value)
                    {
                        string format = string.Empty;
                        switch (Source)
                        {
                            case Sources.Time:
                                format = "HH:mm:ss";
                                break;
                            case Sources.Date:
                                format = "dd/MM/yyyy";
                                break;
                            case Sources.TimeDate:
                                format = "HH:mm:ss dd/MM/yyyy";
                                break;
                        }
                        Value = DateTime.Now.ToString(format);
                    }
                }
                return true;
            });
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