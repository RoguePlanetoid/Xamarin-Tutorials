using Xamarin.Forms;
using System.Linq;

public class Library
{
    private const int size = 7;
    private const int on = 1;
    private const int off = 0;

    private readonly Color lightOn = Color.White;
    private readonly Color lightOff = Color.Black;

    private ContentPage _page;
    private int _moves = 0;
    private bool _won = false;
    private int[,] _board = new int[size, size];

    public void Show(string content, string title)
    {
        Device.BeginInvokeOnMainThread(() => {
            _page.DisplayAlert(title, content, "Ok");
        });
    }

    private bool Winner()
    {
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                if (_board[column, row] == on)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Toggle(Grid grid, int row, int column)
    {
        _board[row, column] =
            _board[row, column] == on ? off : on;
        Grid element = (Grid)grid.Children.Single(
            w => Grid.GetRow((Grid)w) == row
            && Grid.GetColumn((Grid)w) == column);
        element.BackgroundColor =
            _board[row, column] == on ? lightOn : lightOff;
    }

    private void Add(Grid grid, int row, int column)
    {
        Grid element = new Grid()
        {
            HeightRequest = 40,
            WidthRequest = 40,
            BackgroundColor = lightOn
        };
        TapGestureRecognizer tapped = new TapGestureRecognizer();
        tapped.Tapped += (sender, e) =>
        {
            if (!_won)
            {
                element = ((Grid)(sender));
                row = (int)element.GetValue(Grid.RowProperty);
                column = (int)element.GetValue(Grid.ColumnProperty);
                Toggle(grid, row, column);
                if (row > 0)
                {
                    Toggle(grid, row - 1, column); // Toggle Left
                }
                if (row < (size - 1))
                {
                    Toggle(grid, row + 1, column); // Toggle Right
                }
                if (column > 0)
                {
                    Toggle(grid, row, column - 1); // Toggle Above
                }
                if (column < (size - 1))
                {
                    Toggle(grid, row, column + 1); // Toggle Below
                }
                _moves++;
                if (Winner())
                {
                    Show("Well Done! You won in " + _moves + " moves!", "Light Game");
                    _won = true;
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
        _moves = 0;
        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        grid.BackgroundColor = Color.LightGray;
        // Setup Grid
        for (int index = 0; (index < size); index++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        for (int row = 0; (row < size); row++)
        {
            for (int column = 0; (column < size); column++)
            {
                Add(grid, row, column);
            }
        }
    }

    public void New(ContentPage page, Grid grid)
    {
        _page = page;
        Layout(ref grid);
        _won = false;
        // Setup Board
        for (int column = 0; (column < size); column++)
        {
            for (int row = 0; (row < size); row++)
            {
                _board[column, row] = on;
            }
        }
    }
}