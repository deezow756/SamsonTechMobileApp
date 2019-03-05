using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockPage : ContentPage
	{
        Display display;
		public StockPage ()
		{
			InitializeComponent ();           
        }

        protected override void OnAppearing()
        {
            FileManager fileManager = new FileManager();
            display = new Display(fileManager.LoadStocks());
            display.AllStocks(liststock);
        }

        private void BtnAddStock_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStock());
        }

        private void TextCell_Tapped(object sender, EventArgs e)
        {
            var tc = ((TextCell) sender);
            display.DisplayStock(this, tc.Text);
        }
    }
}