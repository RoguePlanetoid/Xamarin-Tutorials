using System;
using Xamarin.Forms;

public class Library
{
    private byte[][] table =
    {
                 // a, b, c, d, e, f, g, h, i
        new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
        new byte[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // 1
        new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 }, // 2
        new byte[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 }, // 3
        new byte[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 }, // 4
        new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 }, // 5
        new byte[] { 1, 0, 1, 1, 0, 1, 1, 0, 1 }, // 6
    };
    private Random random = new Random((int)DateTime.Now.Ticks);

    private void Add(ref Grid grid, int row, int column, byte opacity)
    {
        EllipseView ellipse = new EllipseView()
        {
            IsFilled = true,
            Color = Color.Black,
            Margin = new Thickness(5),
            Opacity = opacity
        };
        ellipse.SetValue(Grid.ColumnProperty, column);
        ellipse.SetValue(Grid.RowProperty, row);
        grid.Children.Add(ellipse);
    }

    private Grid Dice(int value)
    {
        int size = 3;
        Grid grid = new Grid()
        {
            WidthRequest = 100,
            HeightRequest = 100,
            BackgroundColor = Color.WhiteSmoke,
            Padding = new Thickness(5)
        };
        // Setup Grid
        for (int index = 0; (index < size); index++)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        int count = 0;
        for (int row = 0; (row < size); row++)
        {
            for (int column = 0; (column < size); column++)
            {
                Add(ref grid, row, column, table[value][count]);
                count++;
            }
        }
        return grid;
    }

    private int Roll()
    {
        return random.Next(1, 7);
    }

    public void New(ref Grid grid)
    {
        grid.Children.Clear();
        grid.Children.Add(Dice(Roll()));
    }
}