using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SamsonTechMobileApp
{
    public class CatergoryEntry
    {
        private AddStock addStock;
        private StackLayout stackLayout;
        private Label disName;
        public Entry txtName;

        public CatergoryEntry(AddStock addStock, StackLayout stackLayout)
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

            stackLayout.Children.Add(disName);
            stackLayout.Children.Add(txtName);
        }
    }
}
