using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

public class Library
{
    private const string app_title = "Four In Row";
    private const int size = 7;

    private ContentPage _page;
    private bool _won = false;
    private int[,] _board = new int[size, size];
    private int _player = 0;

    public void Show(string content, string title)
    {
        Device.BeginInvokeOnMainThread(() => {
            _page.DisplayAlert(title, content, "Ok");
        });
    }

    private async Task<bool> ConfirmAsync(string content,
        string title, string ok, string cancel)
    {
        return await _page.DisplayAlert(title, content, ok, cancel);
    }

    public bool Winner(int column, int row)
    {
        int total = 3; // Total Excluding Current
        int value = 0; // Value in Line
        int amend = 0; // Add or Remove
        // Check Vertical
        do
        {
            value++;
        }
        while (row + value < size &&
        _board[column, row + value] == _player);
        if (value > total)
        {
            return true;
        }
        value = 0;
        amend = 0;
        // Check Horizontal - From Left
        do
        {
            value++;
        }
        while (column - value >= 0 &&
        _board[column - value, row] == _player);
        if (value > total)
        {
            return true;
        }
        value -= 1; // Deduct Middle - Prevent double count
        // Then Right
        do
        {
            value++;
            amend++;
        }
        while (column + amend < size &&
        _board[column + amend, row] == _player);
        if (value > total)
        {
            return true;
        }
        value = 0;
        amend = 0;
        // Diagonal - Left Top
        do
        {
            value++;
        }
        while (column - value >= 0 && row - value >= 0 &&
        _board[column - value, row - value] == _player);
        if (value > total)
        {
            return true;
        }
        value -= 1; // Deduct Middle - Prevent double count
        // To Right Bottom
        do
        {
            value++;
            amend++;
        }
        while (column + amend < size && row + amend < size &&
        _board[column + amend, row + amend] == _player);
        if (value > total)
        {
            return true;
        }
        value = 0;
        amend = 0;
        // Diagonal - From Right Top
        do
        {
            value++;
        }
        while (column + value < size && row - value >= 0 &&
        _board[column + value, row - value] == _player);
        if (value > total)
        {
            return true;
        }
        value -= 1; // Deduct Middle - Prevent double count
        // To Left Bottom
        do
        {
            value++;
            amend++;
        }
        while (column - amend >= 0 &&
        row + amend < size &&
        _board[column - amend, row + amend] == _player);
        if (value > total)
        {
            return true;
        }
        return false;
    }

    private Grid Piece()
    {
        Grid grid = new Grid()
        {
            HeightRequest = 30,
            WidthRequest = 30,
        };
        if (_player == 1)
        {
            BoxView line1 = new BoxView()
            {
                Color = Color.Red,
                HeightRequest = 30,
                WidthRequest = 2,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Rotation = 45,
            };
            BoxView line2 = new BoxView()
            {
                Color = Color.Red,
                HeightRequest = 30,
                WidthRequest = 2,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Rotation = 135,
            };
            grid.Children.Add(line1);
            grid.Children.Add(line2);
        }
        else if (_player == 2)
        {
            EllipseView circle = new EllipseView()
            {
                Color = Color.Blue,
                HeightRequest = 30,
                WidthRequest = 30,
                StrokeWidth = 2,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            grid.Children.Add(circle);
        }
        return grid;
    }

    private bool Full()
    {
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                if (_board[column, row] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Place(Grid grid, int column, int row)
    {
        for (int i = size - 1; i > -1; i--)
        {
            if (_board[column, i] == 0)
            {
                _board[column, i] = _player;
                Grid element = (Grid)grid.Children.Single(
                w => Grid.GetRow((Grid)w) == i
                && Grid.GetColumn((Grid)w) == column);
                element.Children.Add(Piece());
                row = i;
                break;
            }
        }
        if (Winner(column, row))
        {
            _won = true;
            Show($"Player {_player} has won!", app_title);
        }
        else if (Full())
        {
            Show("Board Full!", app_title);
        }
        _player = _player == 1 ? 2 : 1; // Set Player
    }

    private void Add(Grid grid, int row, int column)
    {
        Grid element = new Grid()
        {
            HeightRequest = 40,
            WidthRequest = 40,
            BackgroundColor = Color.WhiteSmoke,
        };
        TapGestureRecognizer tapped = new TapGestureRecognizer();
        tapped.Tapped += (sender, e) =>
        {
            if (!_won)
            {
                element = ((Grid)(sender));
                row = (int)element.GetValue(Grid.RowProperty);
                column = (int)element.GetValue(Grid.ColumnProperty);
                if (_board[column, 0] == 0) // Check Free Row
                {
                    Place(grid, column, row);
                }
            }
            else
            {
                Show("Game Over!", app_title);
            }
        };
        element.GestureRecognizers.Add(tapped);
        element.SetValue(Grid.ColumnProperty, column);
        element.SetValue(Grid.RowProperty, row);
        grid.Children.Add(element);
    }

    private void Layout(ref Grid grid)
    {
        _player = 1;
        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        // Setup Grid
        for (int index = 0; (index < size); index++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        // Setup Board
        for (int column = 0; (column < size); column++)
        {
            for (int row = 0; (row < size); row++)
            {
                Add(grid, row, column);
                _board[row, column] = 0;
            }
        }
    }

    public async void New(ContentPage page, Grid grid)
    {
        _page = page;
        Layout(ref grid);
        _won = false;
        _player = await ConfirmAsync("Who goes First?", app_title, "X", "O") ? 1 : 2;
    }
}