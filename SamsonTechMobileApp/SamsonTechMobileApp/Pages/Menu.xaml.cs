using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SamsonTechMobileApp.Pages;

namespace SamsonTechMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        OneDrive oneDrive;       

        bool AfterStartUp = false;

        public Menu ()
		{
			InitializeComponent ();
            oneDrive = new OneDrive(this);
            Progress.Show();
            oneDrive.SignIn();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FileManager fileManager = new FileManager();
            Display display = new Display(fileManager.LoadOrders());
            try
            {
                display.OrdersToday(listOrders);
            }
            catch (Exception) { }

            if(AfterStartUp && oneDrive.SignedIn)
            {
                oneDrive.SaveOrdersNStocks();
            }
            AfterStartUp = true;
        }

        private void btnDisplayOrders_Clicked(object sender, EventArgs e)
        {
            ViewOrders displayOrders = new ViewOrders();
            this.Navigation.PushAsync(displayOrders);
        }

        private void btnStatistics_Clicked(object sender, EventArgs e)
        {
            ViewStatistics viewStatistics = new ViewStatistics();
            this.Navigation.PushAsync(viewStatistics);
        }

        private void btnAddOrder_Clicked(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            this.Navigation.PushAsync(addOrder);
        }

        private void BtnStock_Clicked(object sender, EventArgs e)
        {
            StockPage stockPage = new StockPage();
            this.Navigation.PushAsync(stockPage);
        }

        private void listOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string order = listOrders.SelectedItem.ToString();
            string[] split = order.Split(':');

            FileManager fileManager = new FileManager();
            Order[] lstOrders = fileManager.LoadOrders();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].ID == split[0])
                {
                    ViewOrder viewOrder = new ViewOrder(lstOrders[i]);
                    Navigation.PushAsync(viewOrder);
                }
            }
        }

        public void FillTodayOrders()
        {
            FileManager fileManager = new FileManager();
            Display display = new Display(fileManager.LoadOrders());
            display.OrdersToday(listOrders);
        }

        private void BtnSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings(oneDrive));
        }
    }
}