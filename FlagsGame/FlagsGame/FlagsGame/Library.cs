using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

public class Flag
{
    public enum Designs
    {
        HorizonalStripes = 1,
        VerticalStripes = 2
    };
    public Designs Design { get; set; }
    public string Name { get; set; }
    public string[] Colours { get; set; }
}

public class Library
{
    private const string app_title = "Flags Game";
    private const int size = 3;

    private List<Flag> flags = new List<Flag>()
    {
        new Flag()
        {
            Name = "Armenia",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE1018", "002984", "EF7b21" }
        },
        new Flag()
        {
            Name = "Austria",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE1018", "FFFFFF", "DE1018" }
        },
        new Flag()
        {
            Name = "Belgium",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "000000", "FFEF08", "DE1018" }
        },
        new Flag()
            {
            Name = "Bulgaria",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "FFFFFF", "109452", "DE1018" }
        },
        new Flag()
        {
            Name = "Estonia",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "295AA5", "000000", "FFFFFF" }
        },
        new Flag()
        {
            Name = "France",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "080863", "FFFFFF", "D60818" }
        },
        new Flag() {
            Name = "Gabon",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "109452", "FFEF08", "002984" }
        },
        new Flag()
        {
            Name = "Germany",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "000000", "DE0008", "FFDE08" }
        },
        new Flag()
        {
            Name = "Guinea",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "DE1018", "FFEF08", "109452" }
        },
        new Flag()
        {
            Name = "Hungary",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE0008", "FFFFFF", "087B39" }
        },
        new Flag()
        {
            Name = "Ireland",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "007339", "FFFFFF", "E76300" }
        },
        new Flag()
        {
            Name = "Italy",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "109452", "FFFFFF", "DE1018" }
        },
        new Flag()
        {
            Name = "Lithuania",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "FFEF08", "109452", "DE1018" }
        },
        new Flag()
        {
            Name = "Luxembourg",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE1018", "FFFFFF", "2984B5" }
        },
        new Flag()
        {
            Name = "Mali",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "109452", "FFEF08", "DE1018" }
        },
        new Flag()
        {
            Name = "Netherlands",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE1018", "FFFFFF", "002984" }
        },
        new Flag()
        {
            Name = "Nigeria",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "087B39", "FFFFFF", "087B39" }
        },
        new Flag()
        {
            Name = "Romania",
            Design = Flag.Designs.VerticalStripes,
            Colours = new string[] { "002984", "FFEF08", "DE1018" }
        },
        new Flag()
        {
            Name = "Russia",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "FFFFFF", "0852A5", "DE1018" }
        },
        new Flag()
        {
            Name = "Sierra Leone",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "5AB521", "FFFFFF", "0852A5" }
        },
        new Flag()
        {
            Name = "Yemen",
            Design = Flag.Designs.HorizonalStripes,
            Colours = new string[] { "DE1018", "FFFFFF", "000000" }
        }
    };

    private ContentPage _page;
    private Random _random = new Random((int)DateTime.Now.Ticks);
    private List<int> _indexes = new List<int>();
    private List<int> _choices = new List<int>();
    private int _turns = 0;
    private string _country = string.Empty;
    private bool _lost = false;

    private List<int> Shuffle(int start, int total)
    {
        return Enumerable.Range(start, total).OrderBy(r => _random.Next(start, total)).ToList();
    }

    public void Show(string content, string title)
    {
        Device.BeginInvokeOnMainThread(() => {
            _page.DisplayAlert(title, content, "Ok");
        });
    }

    private StackLayout GetFlag(ref string name, int index)
    {
        int pos = _indexes[index];
        Flag flag = flags[pos];
        name = flag.Name;
        bool horizontal = flag.Design == Flag.Designs.HorizonalStripes;
        StackLayout panel = new StackLayout()
        {
            InputTransparent = true,
            HeightRequest = 60,
            WidthRequest = 60,
            Orientation = horizontal ? StackOrientation.Vertical : StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Spacing = 0
        };
        for (int f = 0; f < 3; f++)
        {
            BoxView box = new BoxView()
            {
                InputTransparent = true,
                WidthRequest = horizontal ? 60 : 20,
                HeightRequest = horizontal ? 20 : 60,
                BackgroundColor = Color.FromHex($"#FF{flag.Colours[f]}"),
            };
            panel.Children.Add(box);
        }
        return panel;
    }

    private void Choose(ref Grid grid, ref Label text)
    {
        int choice = _choices[_turns];
        int index = _indexes[choice];
        _country = flags[index].Name;
        text.Text = _country;
        _turns++;
    }

    private void Add(Grid grid, Label text, int row, int column, int index)
    {
        string name = string.Empty;
        StackLayout view = GetFlag(ref name, index);
        Grid element = new Grid()
        {
            ClassId = name,
            BackgroundColor = Color.WhiteSmoke,
            HeightRequest = 80,
            WidthRequest = 80
        };
        element.Children.Add(view);
        TapGestureRecognizer tapped = new TapGestureRecognizer();
        tapped.Tapped += (sender, e) =>
        {
            if (!_lost)
            {
                element = ((Grid)sender);
                row = (int)element.GetValue(Grid.RowProperty);
                column = (int)element.GetValue(Grid.ColumnProperty);
                string current = element.ClassId;
                if (_country == current)
                {
                    element.Opacity = 0;
                    if (_turns < 9)
                    {
                        Choose(ref grid, ref text);
                    }
                    else
                    {
                        text.Text = string.Empty;
                        Show("You Won!", app_title);
                    }
                }
                else
                {
                    _lost = true;
                }
            }
            if (_lost)
            {
                Show("Game Over!", app_title);
            }
        };
        element.GestureRecognizers.Add(tapped);
        element.SetValue(Grid.ColumnProperty, column);
        element.SetValue(Grid.RowProperty, row);
        grid.Children.Add(element);
    }

    private void Layout(ref Grid grid, ref Label text)
    {
        text.Text = string.Empty;
        grid.HeightRequest = 300;
        grid.WidthRequest = 300;
        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        // Setup Grid
        for (int layout = 0; layout < size; layout++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        // Setup Board
        int index = 0;
        for (int column = 0; column < size; column++)
        {
            for (int row = 0; row < size; row++)
            {
                Add(grid, text, row, column, index);
                index++;
            }
        }
    }

    public void New(ContentPage page, ref Grid grid, ref Label text)
    {
        _page = page;
        _lost = false;
        _turns = 0;
        text.Text = string.Empty;
        _indexes = Shuffle(0, flags.Count);
        _choices = Shuffle(0, 9);
        Layout(ref grid, ref text);
        Choose(ref grid, ref text);
    }
}