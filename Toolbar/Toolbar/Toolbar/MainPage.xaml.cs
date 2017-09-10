using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Toolbar
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        public void Toolbar_Click(object sender, EventArgs e)
        {
            DisplayAlert("Toolbar", ((ToolbarItem)sender).Text, "Ok");
        }
    }
}
