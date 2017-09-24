using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightsControl
{
    public class Light : AbsoluteLayout
    {
        private const int rows = 8;

        private readonly int[][] table =
        {
            //  l w
            new int[] { 2, 4 },
            new int[] { 1, 6 },
            new int[] { 0, 8 },
            new int[] { 0, 8 },
            new int[] { 0, 8 },
            new int[] { 0, 8 },
            new int[] { 1, 6 },
            new int[] { 2, 4 },
        };

        private bool _isOn;

        public static readonly BindableProperty ForegroundProperty =
        BindableProperty.Create<Light, Color>(p => p.Foreground, Color.Black,
        BindingMode.Default, null, null);

        public static readonly BindableProperty ColourProperty =
        BindableProperty.Create<Light, Color>(p => p.Colour, Color.Black,
        BindingMode.Default, null, null);

        public static readonly BindableProperty SizeProperty =
        BindableProperty.Create<Light, int>(p => p.Size, 10,
        BindingMode.Default, null, null);

        public Color Foreground
        {
            get { return (Color)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public Color Colour
        {
            get { return (Color)GetValue(ColourProperty); }
            set { SetValue(ColourProperty, value); }
        }

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        private Frame AddElement(int left, int top, int width, int height)
        {
            Frame frame = new Frame()
            {
                WidthRequest = width,
                HeightRequest = height,
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
            frame.SetBinding(Frame.BackgroundColorProperty, "Foreground");
            frame.BindingContext = this;
            return frame;
        }

        public Light()
        {
            for (int row = 0; row < rows; row++)
            {
                int[] factors = table[row];
                this.Children.Add(AddElement(
                    Size * factors[0],
                    row == 0 ? 0 : Size * row,
                    Size * factors[1], Size));
            }
        }

        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                Foreground = value ? Colour : Color.Black;
            }
        }
    }

    public class Lights : StackLayout
    {
        private Light _red = new Light { Colour = Color.Red };
        private Light _orange = new Light { Colour = Color.Orange };
        private Light _green = new Light { Colour = Color.Green };

        private async Task<bool> Delay(int seconds = 2)
        {
            await Task.Delay(seconds * 1000);
            return true;
        }

        public Lights()
        {
            this.Orientation = StackOrientation.Vertical;
            this.Children.Add(_red);
            this.Children.Add(_orange);
            this.Children.Add(_green);
        }

        public async void Traffic()
        {
            _red.IsOn = false;
            _orange.IsOn = false;
            _green.IsOn = true;
            await Delay();
            _green.IsOn = false;
            await Delay();
            _orange.IsOn = true;
            await Delay();
            _orange.IsOn = false;
            await Delay();
            _red.IsOn = true;
            await Delay();
            _red.IsOn = true;
            await Delay();
            _orange.IsOn = true;
            await Delay();
            _red.IsOn = false;
            _orange.IsOn = false;
            _green.IsOn = true;
            await Delay();
        }
    }
}
