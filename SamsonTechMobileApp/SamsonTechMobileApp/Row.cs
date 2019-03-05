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
        public StackLayout section = new StackLayout();
        public StackLayout view = new StackLayout();
        public Label Quantity = new Label();
        public ImageButton plus = new ImageButton();
        public ImageButton take = new ImageButton();

        public Row(StockInfo stockInfo, string name, string quantity, StackLayout Rows)
        {
            Info = stockInfo;

            section.Orientation = StackOrientation.Horizontal;
            section.HorizontalOptions = LayoutOptions.CenterAndExpand;

            take.Source = "btnMinus";
            take.WidthRequest = 40;
            take.HeightRequest = 40;
            take.BackgroundColor = Color.Transparent;
            take.Clicked += TakeOnClicked;
            if (int.Parse(quantity) == 0) { take.IsEnabled = false; }
            section.Children.Add(take);

            view.Orientation = StackOrientation.Vertical;
            view.VerticalOptions = LayoutOptions.CenterAndExpand;

            Name.Text = name;
            Name.FontSize = 20;
            Name.HorizontalTextAlignment = TextAlignment.Center;
            Name.TextColor = Color.LimeGreen;
            Name.BackgroundColor = Color.Transparent;
            view.Children.Add(Name);

            StockQuantity = quantity;
            Quantity.Text = quantity;
            Quantity.FontSize = 15;
            Quantity.HorizontalTextAlignment = TextAlignment.Center;
            Quantity.TextColor = Color.LimeGreen;
            Quantity.BackgroundColor = Color.Transparent;
            view.Children.Add(Quantity);

            section.Children.Add(view);

            plus.Source = "btnPlus";
            plus.WidthRequest = 40;
            plus.HeightRequest = 40;
            plus.BackgroundColor = Color.Transparent;
            plus.Clicked += PlusOnClicked;
            section.Children.Add(plus);

            Rows.Children.Add(section);
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
