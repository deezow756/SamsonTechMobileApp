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
            display.AllStocks(listStock);
        }

        private void ListStock_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            display.DisplayStock(this, listStock.SelectedItem.ToString());
        }

        private void BtnAddStock_Clicked(object sender, EventArgs e)
        {

        }
    }
}