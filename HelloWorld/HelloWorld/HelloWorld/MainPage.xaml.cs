using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloWorld
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        public void Button_Click(object sender, EventArgs e)
        {
            DisplayAlert("Hello World", "Hello World", "Ok");
        }
    }
}
