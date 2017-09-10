using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageRotate
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        public void Value_Completed(object sender, EventArgs e)
        {
            Display.Source = ImageSource.FromUri(new Uri(Value.Text));
        }

        public void RotateX_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Display.RotationX = RotateX.Value;
        }

        public void RotateY_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Display.RotationY = RotateY.Value;
        }

        public void RotateZ_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Display.Rotation = RotateZ.Value;
        }
    }
}
