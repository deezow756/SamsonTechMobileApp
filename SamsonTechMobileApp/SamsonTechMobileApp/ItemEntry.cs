using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SamsonTechMobileApp.Pages;

namespace SamsonTechMobileApp
{
    public class ItemEntry
    {
        private AddStock addStock;
        private StackLayout stackLayout;
        private Label disName;
        public Entry txtName;
        private Label disQuantity;
        public Entry txtQuantity;
        private Label disDescription;
        public Entry txtDescription;

        public ItemEntry(AddStock addStock, StackLayout stackLayout)
        {
            this.addStock = addStock;
            this.stackLayout = stackLayout;
            Setup();
        }

        private void Setup()
        {
            disName = new Label();
            disName.Text = "Name";
            disName.TextColor = Color.LimeGreen;
            disName.FontSize = 30;
            txtName = new Entry();
            txtName.TextColor = Color.LimeGreen;
            txtName.FontSize = 30;
            txtName.WidthRequest = 200;

            disQuantity = new Label();
            disQuantity.Text = "Quantity";
            disQuantity.TextColor = Color.LimeGreen;
            disQuantity.FontSize = 30;
            txtQuantity = new Entry();
            txtQuantity.TextColor = Color.LimeGreen;
            txtQuantity.FontSize = 30;
            txtQuantity.WidthRequest = 200;

            disDescription = new Label();
            disDescription.Text = "Description";
            disDescription.TextColor = Color.LimeGreen;
            disDescription.FontSize = 30;
            txtDescription = new Entry();
            txtDescription.TextColor = Color.LimeGreen;
            txtDescription.FontSize = 30;
            txtDescription.WidthRequest = 200;

            stackLayout.Children.Add(disName);
            stackLayout.Children.Add(txtName);
            stackLayout.Children.Add(disQuantity);
            stackLayout.Children.Add(txtQuantity);
            stackLayout.Children.Add(disDescription);
            stackLayout.Children.Add(txtDescription);
        }
    }
}
