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
	public partial class AddStock : ContentPage
	{
		public AddStock ()
		{
			InitializeComponent ();
		}

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            stock.Name = txtName.Text;
            stock.Quantity = txtQuantity.Text;
            FileManager fileManager = new FileManager();
            fileManager.SaveStock(this, stock);
            this.Navigation.PopAsync();
        }
    }
}