using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

public class GameGrid : Grid
{
    public int Value { get; set; }
}

public class Library
{
    private const string app_title = "Touch Game";
    private const int size = 2;
    private const int speed = 800;
    private const int light = 400;
    private const int click = 200;
    private const int level = 100;

    private Color[] _colours =
    {
        Color.Crimson, Color.Green,
        Color.Blue, Color.Gold
    };
    private Color clicked = Color.Accent;
    private Color lighted = Color.WhiteSmoke;

    private ContentPage _page;
    private int _turn = 0;
    private int _count = 0;
    private bool _play = false;
    private bool _isTimer = false;
    private bool _isHighlight = false;
    private List<int> _items = new List<int>();
    private Random _random = new Random((int)DateTime.Now.Ticks);

    public void Show(string content, string title)
    {
        Device.BeginInvokeOnMainThread(() => {
            _page.DisplayAlert(title, content, "Ok");
        });
    }

    private void Highlight(Grid grid, int value, int period, Color background)
    {
        GameGrid element = (GameGrid)grid.Children.Single(s =>
            ((GameGrid)s).Value == value);
        Device.BeginInvokeOnMainThread(() =>
        {
            element.BackgroundColor = background; // New Background
        });
        Device.StartTimer(TimeSpan.FromMilliseconds(period), () =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                element.BackgroundColor = _colours[element.Value]; // Original Background
            });
            return false;
        });
    }

    private List<int> Shuffle(int start, int finish, int total)
    {
        int number;
        List<int> numbers = new List<int>();
        while ((numbers.Count < total)) // Select Numbers
        {
            // Random non-unique Number between Start and Finish
            number = _random.Next(start, finish + 1);
            numbers.Add(number); // Add Number
        }
        return numbers;
    }

    private void Add(Grid grid, int row, int column, int count)
    {
        GameGrid element = new GameGrid()
        {
            HeightRequest = 120,
            WidthRequest = 120,
            Value = count,
            BackgroundColor = _colours[count]
        };
        TapGestureRecognizer tapped = new TapGestureRecognizer();
        tapped.Tapped += (sender, e) =>
        {
            if (_play)
            {
                int value = ((GameGrid)sender).Value;
                Highlight(grid, value, click, clicked);
                if (value == _items[_count])
                {
                    if (_count < _turn)
                    {
                        _count++;
                    }
                    else
                    {
                        _play = false;
                        _turn++;
                        _count = 0;
                        _isTimer = true;
                    }
                }
                else
                {
                    _isTimer = false;
                    Show($"Game Over! You scored {_turn}!", app_title);
                    _play = false;
                    _turn = 0;
                    _count = 0;
                }
            }
        };
        element.GestureRecognizers.Add(tapped);
        element.SetValue(Grid.ColumnProperty, column);
        element.SetValue(Grid.RowProperty, row);
        grid.Children.Add(element);
    }

    private void Layout(ref Grid grid)
    {
        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        // Setup Grid
        for (int index = 0; (index < size); index++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        int count = 0;
        // Setup Board
        for (int column = 0; (column < size); column++)
        {
            for (int row = 0; (row < size); row++)
            {
                Add(grid, row, column, count);
                count++;
            }
        }
    }

    public void New(ContentPage page, Grid grid)
    {
        _page = page;
        Layout(ref grid);
        _items = Shuffle(0, 3, level);
        _play = false;
        _turn = 0;
        _count = 0;
        _isTimer = true;
        Device.StartTimer(TimeSpan.FromMilliseconds(speed), () =>
        {
            if (_isTimer)
            {
                if (_count <= _turn)
                {
                    Highlight(grid, _items[_count], light, lighted);
                    _count++;
                }
                if (_count > _turn)
                {
                    _isTimer = false;
                    _play = true;
                    _count = 0;
                }
            }
            return true;
        });
    }
}