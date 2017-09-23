using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

public class Piece : Grid
{
    public Piece(int index)
    {
        this.Margin = new Thickness(2);
        this.BackgroundColor = Color.Black;
        Label text = new Label()
        {
            FontSize = 20,
            Text = index.ToString(),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Color.White,
            InputTransparent = true,
        };
        this.Children.Add(text);
    }

    public int Row { get; set; }
    public int Column { get; set; }
}

public class Library : ContentPage
{
    private const string app_title = "Slide Game";
    private const int size = 4;

    private int _moves = 0;
    private int[,] _board = new int[size, size];
    private List<int> _values;
    private Random _random = new Random((int)DateTime.Now.Ticks);

    private List<int> Shuffle(int start, int total)
    {
        return Enumerable.Range(start, total).OrderBy(r => _random.Next(start, total)).ToList();
    }

    private void Show(string content, string title)
    {
        DisplayAlert(title, content, "Ok");
    }

    private bool Valid(int row, int column)
    {
        if (row < 0 || column < 0 || row > 3 || column > 3)
        {
            return false;
        }
        return (_board[row, column] == 0);
    }

    private bool Check()
    {
        int previous = _board[0, 0];
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                if (_board[row, column] < previous)
                {
                    return false;
                }
                previous = _board[row, column];
            }
        }
        return true;
    }

    private void Layout(AbsoluteLayout layout)
    {
        int height = 300;
        int width = 300;
        layout.HeightRequest = height;
        layout.WidthRequest = width;
        layout.Children.Clear();
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                if (_board[row, column] > 0)
                {
                    int index = _board[row, column];
                    Piece piece = new Piece(index)
                    {
                        WidthRequest = width / size,
                        HeightRequest = height / size,
                        Row = row,
                        Column = column
                    };
                    Rectangle rect = new Rectangle()
                    {
                        Top = (row * (height / size)),
                        Left = (column * (width / size)),
                        Bottom = (row * (height / size)) + piece.HeightRequest,
                        Right = (column * (width / size)) + piece.WidthRequest,
                    };
                    AbsoluteLayout.SetLayoutBounds(piece, rect);
                    TapGestureRecognizer tapped = new TapGestureRecognizer();
                    tapped.Tapped += (sender, e) =>
                    {
                        piece = (Piece)sender;
                        if (Valid(piece.Row - 1, piece.Column))
                        {
                            Move(layout, piece, piece.Row - 1, piece.Column);
                        }
                        else if (Valid(piece.Row, piece.Column + 1))
                        {
                            Move(layout, piece, piece.Row, piece.Column + 1);
                        }
                        else if (Valid(piece.Row + 1, piece.Column))
                        {
                            Move(layout, piece, piece.Row + 1, piece.Column);
                        }
                        else if (Valid(piece.Row, piece.Column - 1))
                        {
                            Move(layout, piece, piece.Row, piece.Column - 1);
                        }
                    };
                    piece.GestureRecognizers.Add(tapped);
                    layout.Children.Add(piece);
                }
            }
        }
    }

    private void Move(AbsoluteLayout layout, Piece piece, int row, int column)
    {
        _moves++;
        _board[row, column] = _board[piece.Row, piece.Column];
        _board[piece.Row, piece.Column] = 0;
        piece.Row = row;
        piece.Column = column;
        Layout(layout);
        if (Check())
        {
            Show($"Correct In {_moves} Moves", app_title);
        }
    }

    public void New(ref AbsoluteLayout layout)
    {
        int index = 1;
        _values = Shuffle(1, _board.Length);
        _values.Insert(0, 0);
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                _board[row, column] = _values[index++];
                if (index == size * size) index = 0;
            }
        }
        Layout(layout);
    }
}