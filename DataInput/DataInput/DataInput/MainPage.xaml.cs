using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataInput
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        Library library = new Library();

        public void New_Clicked(object sender, EventArgs e)
        {
            Email.Text = string.Empty;
            Website.Text = string.Empty;
            Telephone.Text = string.Empty;
        }

        public void Open_Clicked(object sender, EventArgs e)
        {
            Email.Text = library.LoadSetting("Email");
            Website.Text = library.LoadSetting("Website");
            Telephone.Text = library.LoadSetting("Telephone");
        }

        public void Save_Clicked(object sender, EventArgs e)
        {
            library.SaveSetting("Email", Email.Text);
            library.SaveSetting("Website", Website.Text);
            library.SaveSetting("Telephone", Telephone.Text);
        }
    }
}
