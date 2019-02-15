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

        List<Row> rows = new List<Row>();

        string[] Names;
        string[] Quantities;

		public StockInfo (Stock stock)
		{
			InitializeComponent ();
            this.stock = stock;
            txtName.Text = stock.Name;
            Names = stock.Models.Split(',');
            Quantities = stock.Quantity.Split(',');
     
            for (int i = 0; i < Names.Length; i++)
            {
                Row row = new Row(this, Names[i], Quantities[i], Rows);
                rows.Add(row);
            }
		}

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            string quantities = "";

            for (int i = 0; i < Names.Length; i++)
            {
                if (i == Names.Length - 1)
                {
                    quantities += rows[i].StockQuantity;
                }
                else
                {
                    quantities += rows[i].StockQuantity + ",";
                }
            }

            stock.Quantity = quantities;

            FileManager fileManager = new FileManager();
            fileManager.SaveStockEdit(stock);
            DisplayAlert("Saved", "Stock Was Successfully Updated", "Ok");
            this.Navigation.PopAsync();
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        //private void BtnTake_Clicked(object sender, EventArgs e)
        //{
        //    int quantity = 0;
        //    try { quantity = int.Parse(stock.Quantity); }
        //    catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

        //    if (quantity > 0)
        //    {
        //        quantity--;
        //    }

        //    if (quantity == 0)
        //    {
        //        btnTake.IsEnabled = false;
        //    }

        //    stock.Quantity = quantity.ToString();
        //    btnSave.IsEnabled = true;
        //    txtQuantity.Text = stock.Quantity;
        //}

        //private void BtnAdd_Clicked(object sender, EventArgs e)
        //{
        //    int quantity = 0;
        //    try { quantity = int.Parse(stock.Quantity); }
        //    catch (Exception ex) { DisplayAlert("Error: ", ex.Message, "Dismiss"); }

        //    quantity++;

        //    stock.Quantity = quantity.ToString();
        //    btnTake.IsEnabled = true;
        //    btnSave.IsEnabled = true;
        //    txtQuantity.Text = stock.Quantity;
        //}



    }
}