using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SamsonTechMobileApp
{
    public class Row
    {        
        public StockInfo Info;

        public string StockQuantity;

        public Label Name = new Label();
        public StackLayout newButtons = new StackLayout();
        public Label Quantity = new Label();
        public Button plus = new Button();
        public Button take = new Button();

        public Row(StockInfo stockInfo, string name, string quantity, StackLayout Rows)
        {
            Info = stockInfo;
            
            Name.Text = name;
            Name.FontSize = 20;
            Name.HorizontalTextAlignment = TextAlignment.Center;
            Rows.Children.Add(Name);

            StockQuantity = quantity;
            Quantity.Text = quantity;
            Quantity.FontSize = 15;
            Quantity.HorizontalTextAlignment = TextAlignment.Center;
            Rows.Children.Add(Quantity);

            newButtons.Orientation = StackOrientation.Horizontal;
            newButtons.HorizontalOptions = LayoutOptions.CenterAndExpand;
            Rows.Children.Add(newButtons);

            take.Text = "-";
            take.FontSize = 20;
            take.Clicked += TakeOnClicked;
            if (int.Parse(quantity) == 0) { take.IsEnabled = false; }
            newButtons.Children.Add(take);

            plus.Text = "+";
            plus.FontSize = 20;
            plus.Clicked += PlusOnClicked;
            newButtons.Children.Add(plus);          
            
        }

        private void PlusOnClicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(Quantity.Text); }
            catch (Exception ex) { Info.DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            quantity++;

            Quantity.Text = quantity.ToString();
            StockQuantity = quantity.ToString();
            take.IsEnabled = true;
        }

        private void TakeOnClicked(object sender, EventArgs e)
        {
            int quantity = 0;
            try { quantity = int.Parse(Quantity.Text); }
            catch (Exception ex) { Info.DisplayAlert("Error: ", ex.Message, "Dismiss"); }

            quantity--;

            if(quantity == 0) { take.IsEnabled = false; }
            Quantity.Text = quantity.ToString();
            StockQuantity = quantity.ToString();
        }
    }
}
