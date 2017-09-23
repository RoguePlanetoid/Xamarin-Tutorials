using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SlideGame
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        Library library = new Library();

        private void New_Clicked(object sender, EventArgs e)
        {
            library.New(ref Display);
        }
    }
}
