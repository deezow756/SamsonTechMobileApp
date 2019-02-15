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
        AuthenticationResult authResult = null;
        IEnumerable<IAccount> accounts;

        public Menu ()
		{
			InitializeComponent ();
            Progress.Show();
            SignIn();
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

        private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            SignIn();
        }

        private async void btnSignOut_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Warning!!", "Are You Sure You Want To Sign Out", "Yes", "No");

            if (result)
            {
                accounts = await App.PCA.GetAccountsAsync();
                while (accounts.Any())
                {
                    await App.PCA.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await App.PCA.GetAccountsAsync();
                }
                await DisplayAlert("Sign Out", "Successfully Signed Out Of OneDrive Account", "Ok");
            }
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

        private void btnSync_Clicked(object sender, EventArgs e)
        {
            Progress.Show();
            SaveOrdersNStocks();
        }

        private void FillTodayOrders()
        {
            FileManager fileManager = new FileManager();
            Display display = new Display(fileManager.LoadOrders());
            display.OrdersToday(listOrders);
        }
        
        private async void SaveOrdersNStocks()
        {
            Stream orders;
            Stream stocks;
            try
            {
                orders = System.IO.File.Open(FileManager.ordersFilePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Reading File:", ex.Message, "Dismiss");
                return;
            }

            try
            {
                stocks = System.IO.File.Open(FileManager.stocksFilePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Reading File:", ex.Message, "Dismiss");
                return;
            }

            MemoryStream ordersMS = new MemoryStream();
            orders.CopyTo(ordersMS);

            orders.Flush();
            orders.Dispose();
            orders.Close();

            MemoryStream stocksMS = new MemoryStream();
            stocks.CopyTo(stocksMS);

            stocks.Flush();
            stocks.Dispose();
            stocks.Close();

            await UploadOrders(ordersMS.ToArray());
            await UploadStocks(stocksMS.ToArray());
        }

        private async void TryGetOrders()
        {
            Stream orders = await GetOrders();
            string lstOrders;
            using (StreamReader file = new StreamReader(orders))
            {
                lstOrders = file.ReadToEnd();
            }

            System.IO.File.WriteAllText(FileManager.ordersFilePath, lstOrders);

        }

        private async void TryGetStocks()
        {
            Stream stocks = await GetStocks();
            string lstStocks;
            using (StreamReader file = new StreamReader(stocks))
            {
                lstStocks = file.ReadToEnd();
            }

            System.IO.File.WriteAllText(FileManager.stocksFilePath, lstStocks);

        }

        private async void SignIn()
        {
            try
            {
                accounts = await App.PCA.GetAccountsAsync();
                // let's see if we have a user in our belly already
                try
                {
                    IAccount firstAccount = accounts.FirstOrDefault();
                    authResult = await App.PCA.AcquireTokenSilentAsync(App.Scopes, firstAccount);
                    await RefreshUserDataAsync(authResult.AccessToken).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    authResult = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                    await RefreshUserDataAsync(authResult.AccessToken);
                }

                TryGetOrders();
                TryGetStocks();
                FillTodayOrders();
            }
            catch (Exception ex)
            {
                Progress.Hide();
                await DisplayAlert("Could Not Sign In: ", ex.Message, "Dismiss");
            }
        }

        private async Task RefreshUserDataAsync(string token)
        {
            //get data from API
            if (App.graphClient == null)
            {
                // Create Microsoft Graph client.
                try
                {
                    App.graphClient = new GraphServiceClient(
                        "https://graph.microsoft.com/v1.0/",
                        new DelegateAuthenticationProvider(
                            async (requestMessage) =>
                            {
                                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

                            }));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Could not create a graph client: ", ex.Message, "Dismiss");
                }
            }
        }

        private async Task<Stream> GetOrders()
        {
            Stream currentOrdersTextStream = null;

            try
            {
                currentOrdersTextStream = await App.graphClient.Me.Drive.Root.ItemWithPath("/Documents/SamsonTech/Orders.txt").Content.Request().GetAsync();
                Progress.Hide();
            }
            //If the user account is MSA (not work or school), the service will throw an exception.
            catch (Exception e)
            {
                Progress.Hide();
                await DisplayAlert("Could Not Get Orders File From One Drive", e.Message, "Dismiss");
                return null;
            }

            return currentOrdersTextStream;
        }

        private async Task<Stream> GetStocks()
        {
            Stream currentStocksTextStream = null;

            try
            {
                currentStocksTextStream = await App.graphClient.Me.Drive.Root.ItemWithPath("/Documents/SamsonTech/Stock.txt").Content.Request().GetAsync();
                Progress.Hide();
            }
            //If the user account is MSA (not work or school), the service will throw an exception.
            catch (Exception e)
            {
                Progress.Hide();
                await DisplayAlert("Could Not Get Stocks File From One Drive", e.Message, "Dismiss");
                return null;
            }

            return currentStocksTextStream;
        }

        private async Task UploadOrders(byte[] file)
        {
            DriveItem uploadedItem = null;
            try
            {
                Stream fileStream = new MemoryStream(file);
                uploadedItem = await App.graphClient.Me.Drive.Root.ItemWithPath("/Documents/SamsonTech/Orders.txt").Content.Request().PutAsync<DriveItem>(fileStream);

                Progress.Hide();
            }
            catch (Exception e)
            {
                Progress.Hide();
                await DisplayAlert("Could Not Upload Orders ", e.Message, "Dismiss");
                return;
            }
        }

        private async Task UploadStocks(byte[] file)
        {
            DriveItem uploadedItem = null;
            try
            {
                Stream fileStream = new MemoryStream(file);
                uploadedItem = await App.graphClient.Me.Drive.Root.ItemWithPath("/Documents/SamsonTech/Stock.txt").Content.Request().PutAsync<DriveItem>(fileStream);

                Progress.Hide();
            }
            catch (Exception e)
            {
                Progress.Hide();
                await DisplayAlert("Could Not Upload Stocks ", e.Message, "Dismiss");
                return;
            }
        }
    }
}