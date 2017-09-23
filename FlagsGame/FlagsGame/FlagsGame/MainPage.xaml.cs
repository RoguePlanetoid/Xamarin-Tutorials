using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlagsGame
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
            library.New(this, ref Display, ref Label);
        }
    }
}
