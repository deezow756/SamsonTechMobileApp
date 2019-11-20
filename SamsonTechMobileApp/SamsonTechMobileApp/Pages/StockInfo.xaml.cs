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
        Item item;

		public StockInfo (Item item)
		{
			InitializeComponent ();
            this.item = item;
            txtName.Text = item.Name;
            txtQuantity.Text = item.Quantity;
            txtDescription.Text = item.Description;
            
		}

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            item.Quantity = txtQuantity.Text;

            FileManager fileManager = new FileManager();
            fileManager.SaveStockEdit(item);
            ToastManager.Show("Stock Was Successfully Updated");
            this.Navigation.PopAsync();
        }

        private void BtnMinus_Clicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(txtQuantity.Text); }
            catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            quantity--;

            if (quantity == 0) { btnMinus.IsEnabled = false; }
            txtQuantity.Text = quantity.ToString();
            item.Quantity = quantity.ToString();
        }

        private void BtnPlus_Clicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(txtQuantity.Text); }
            catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            quantity++;

            txtQuantity.Text = quantity.ToString();
            item.Quantity = quantity.ToString();
            btnMinus.IsEnabled = true;
        }
    }
}