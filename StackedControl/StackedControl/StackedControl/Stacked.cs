using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace StackedControl
{
    public class Stacked : Grid
    {
        private List<Color> _palette = new List<Color>();
        private List<double> _items = new List<double>();

        private List<double> Percentages()
        {
            List<double> results = new List<double>();
            double total = _items.Sum();
            foreach (double item in _items)
            {
                results.Add((item / total) * 100);
            }
            return results.OrderBy(o => o).ToList();
        }

        private BoxView GetBox(Color colour, int column)
        {
            BoxView box = new BoxView()
            {
                InputTransparent = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = colour
            };
            box.SetValue(Grid.ColumnProperty, column);
            return box;
        }

        private void Layout()
        {
            this.RowSpacing = 0;
            this.ColumnSpacing = 0;
            List<double> percentages = Percentages();
            this.ColumnDefinitions.Clear();
            for (int index = 0; index < percentages.Count(); index++)
            {
                double percentage = percentages[index];
                ColumnDefinition column = new ColumnDefinition()
                {
                    Width = new GridLength(percentage, GridUnitType.Star)
                };
                this.ColumnDefinitions.Add(column);
                Color colour = (index < _palette.Count()) ? _palette[index] : Color.Black;
                Children.Add(GetBox(colour, index));
            }
        }

        public List<Color> Palette
        {
            get { return _palette; }
            set { _palette = value; }
        }

        public List<double> Items
        {
            get { return _items; }
            set { _items = value; Layout(); }
        }

        public void Fibonacci(params Color[] colours)
        {
            Palette = colours.ToList();
            Func<int, int> fibonacci = null;
            fibonacci = value => value > 1 ?
            fibonacci(value - 1) + fibonacci(value - 2) : value;
            Items = Enumerable.Range(0, Palette.Count())
            .Select(fibonacci).Select(s => (double)s).ToList();
        }
    }
}