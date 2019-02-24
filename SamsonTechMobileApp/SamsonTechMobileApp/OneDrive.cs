using Microsoft.Graph;
using Microsoft.Identity.Client;
using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SamsonTechMobileApp
{
    public class OneDrive
    {
        public bool SignedIn = false;

        AuthenticationResult authResult = null;
        IEnumerable<IAccount> accounts;

        Menu menu;
        Settings settings;

        public OneDrive(Menu menu)
        {
            this.menu = menu;
        }

        public OneDrive(Settings settings)
        {
            this.settings = settings;
        }

        async void DisplayMessage(string title, string message, string ok)
        {
            if (menu != null)
            {
                await menu.DisplayAlert(title, message, ok);
            }
            else
            {
                await settings.DisplayAlert(title, message, ok);
            }
        }

        public async void SignIn()
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

                SignedIn = true;

                TryGetOrders();
                TryGetStocks();
                if (menu != null) menu.FillTodayOrders();
            }
            catch (Exception ex)
            {
                Progress.Hide();
                DisplayMessage("Could Not Sign In: ", ex.Message, "Dismiss");
            }
        }

        public async void SignOut()
        {
            var result = await settings.DisplayAlert("Warning!!", "Are You Sure You Want To Sign Out", "Yes", "No");

            if (result)
            {
                accounts = await App.PCA.GetAccountsAsync();
                while (accounts.Any())
                {
                    await App.PCA.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await App.PCA.GetAccountsAsync();
                }
                SignedIn = false;
                await settings.DisplayAlert("Sign Out", "Successfully Signed Out Of OneDrive Account", "Ok");
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
                    DisplayMessage("Could not create a graph client: ", ex.Message, "Dismiss");
                }
            }
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
                DisplayMessage("Could Not Get Orders File From One Drive", e.Message, "Dismiss");
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
                DisplayMessage("Could Not Get Stocks File From One Drive", e.Message, "Dismiss");
                return null;
            }

            return currentStocksTextStream;
        }

        public async void SaveOrdersNStocks()
        {
            Stream orders;
            Stream stocks;
            try
            {
                orders = System.IO.File.Open(FileManager.ordersFilePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Reading File:", ex.Message, "Dismiss");
                return;
            }

            try
            {
                stocks = System.IO.File.Open(FileManager.stocksFilePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Reading File:", ex.Message, "Dismiss");
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
                DisplayMessage("Could Not Upload Orders ", e.Message, "Dismiss");
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
                DisplayMessage("Could Not Upload Stocks ", e.Message, "Dismiss");
                return;
            }
        }
    }
}
