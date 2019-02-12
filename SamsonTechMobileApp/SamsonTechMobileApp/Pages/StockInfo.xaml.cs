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
	public partial class StockInfo : ContentPage
	{
        Stock stock;
		public StockInfo (Stock stock)
		{
			InitializeComponent ();
            btnSave.IsEnabled = false;
            this.stock = stock;
            if(int.Parse(stock.Quantity) == 0)
            {
                btnTake.IsEnabled = false;
            }
		}

        private void FillBoxes()
        {
            txtName.Text = stock.Name;
            txtQuantity.Text = stock.Quantity;
        }

        private void BtnTake_Clicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(stock.Quantity); }
            catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            if (quantity > 0)
            {
                quantity--;
            }

            if (quantity == 0)
            {
                btnTake.IsEnabled = false;
            }

            stock.Quantity = quantity.ToString();
            btnSave.IsEnabled = true;
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(stock.Quantity); }
            catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            quantity++;

            stock.Quantity = quantity.ToString();
            btnTake.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();
            fileManager.SaveStockEdit(stock);
            DisplayAlert("Saved", "Stock Was Successfully Updated", "Ok");
            this.Navigation.PopAsync();
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}