using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SamsonTechMobileApp.Pages;

namespace SamsonTechMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        CloudStorageManager storageManager;     

        bool AfterStartUp = false;

        public Menu ()
		{
			InitializeComponent ();
            storageManager = new CloudStorageManager(this);
            Progress.Show();
            if (storageManager.clouds.Count > 0)
            {
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    storageManager.clouds[i].Signin();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (AfterStartUp)
            {
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    storageManager.clouds[i].Sync();
                }
            }

            FileManager fileManager = new FileManager();
            Display display = new Display(fileManager.LoadOrders());
            try
            {
                display.OrdersToday(listOrders);
            }
            catch (Exception) { }

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

        private void TextCell_Tapped(object sender, EventArgs e)
        {
            var tc = ((TextCell) sender);
            listOrders.SelectedItem = null;
            string order = tc.Text;
            string[] split = order.Split(':');

            FileManager fileManager = new FileManager();
            Order[] lstOrders = fileManager.LoadOrders();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].ID == split[0])
                {
                    ViewOrder viewOrder = new ViewOrder(lstOrders[i]) { Title = "Order"};
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
            Navigation.PushAsync(new Settings(storageManager));
        }
    }
}