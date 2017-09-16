using System;
using System.Collections.Generic;
using Xamarin.Forms;

public class Library
{
    private Random random = new Random((int)DateTime.Now.Ticks);

    private List<int> Choose()
    {
        int number;
        List<int> numbers = new List<int>();
        while ((numbers.Count < 6)) // Select 6 Numbers
        {
            number = random.Next(1, 60);
            if ((!numbers.Contains(number)) || (numbers.Count < 1))
            {
                numbers.Add(number); // Add if not Chosen or None
            }
        }
        numbers.Sort();
        return numbers;
    }

    public void New(StackLayout stack)
    {
        stack.Children.Clear();
        List<int> numbers = Choose();
        foreach (int number in numbers)
        {
            Grid container = new Grid()
            {
                HeightRequest = 60,
                WidthRequest = 60,
                BackgroundColor = Color.WhiteSmoke
            };
            EllipseView ball = new EllipseView()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                StrokeWidth = 5,
                Margin = new Thickness(2),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            if (number >= 1 && number <= 9)
            {
                ball.Color = Color.White;
            }
            else if (number >= 10 && number <= 19)
            {
                ball.Color = Color.Cyan;
            }
            else if (number >= 20 && number <= 29)
            {
                ball.Color = Color.Magenta;
            }
            else if (number >= 30 && number <= 39)
            {
                ball.Color = Color.LawnGreen;
            }
            else if (number >= 40 && number <= 49)
            {
                ball.Color = Color.Yellow;
            }
            else if (number >= 50 && number <= 59)
            {
                ball.Color = Color.Purple;
            }
            container.Children.Add(ball);
            Label label = new Label()
            {
                TextColor = Color.Black,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = number.ToString()
            };
            container.Children.Add(label);
            stack.Children.Add(container);
        }
    }
}
