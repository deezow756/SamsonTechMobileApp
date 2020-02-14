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
using Newtonsoft.Json;

namespace SamsonTechMobileApp
{
    public class OneDrive : CloudStorage
    {
        private const string odOrdersFilePath = "/Documents/SamsonTech/Orders.txt";
        private const string odStocksFilePath = "/Documents/SamsonTech/Items.txt";

        public bool SignedIn = false;

        AuthenticationResult authResult = null;
        IEnumerable<IAccount> accounts;

        Menu menu;

        public OneDrive(Menu menu)
        {
            this.menu = menu;
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

        public override async void Signin()
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

                Sync();

            }
            catch (Exception ex)
            {
                Progress.Hide();
                SignedIn = false;
                DisplayMessage("Could Not Sign In: ", ex.Message, "Dismiss");
            }
        }

        public override async void Signout()
        {
            try
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
                    ToastManager.Show("Successfully Signed Out");
                }
            }
            catch(Exception ex)
            {
                DisplayMessage("Already Signed Out", ex.Message, "Ok");
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

        public override async void Sync()
        {
            if (App.graphClient != null)
            {
                if (System.IO.File.Exists(FileManager.ordersFilePath))
                {
                    var odItemDateOrders = await App.graphClient.Me.Drive.Root.ItemWithPath(odOrdersFilePath).Versions.Request().GetAsync();
                    Date odDateOrders = new Date(odItemDateOrders.First().LastModifiedDateTime.ToString().Split(' ')[0]);
                    Time odTimeOrders = new Time(odItemDateOrders.First().LastModifiedDateTime.ToString().Split(' ')[1]);
                    DateTime dateOrders = System.IO.File.GetLastWriteTime(FileManager.ordersFilePath);

                    if (odDateOrders.Year == dateOrders.Year)
                    {
                        if (odDateOrders.Month == dateOrders.Month)
                        {
                            if (odDateOrders.Day == dateOrders.Day)
                            {
                                if (odTimeOrders.Hours == dateOrders.Hour)
                                {
                                    if (odTimeOrders.Minutes == dateOrders.Minute)
                                    {
                                        if (odTimeOrders.Seconds < dateOrders.Second) SaveOrders();
                                        else TryGetOrders();
                                    }
                                    else if (odTimeOrders.Minutes < dateOrders.Minute) SaveOrders();
                                    else TryGetOrders();
                                }
                                else if (odTimeOrders.Hours < dateOrders.Hour) SaveOrders();
                                else TryGetOrders();
                            }
                            else if (odDateOrders.Day < dateOrders.Day) SaveOrders();
                            else TryGetOrders();
                        }
                        else if (odDateOrders.Month < dateOrders.Month) SaveOrders();
                        else TryGetOrders();
                    }
                    else if (odDateOrders.Year < dateOrders.Year) SaveOrders();
                    else TryGetOrders();
                }
                else
                {
                    TryGetOrders();
                }



                if (System.IO.File.Exists(FileManager.stocksFilePath))
                {
                    var odItemDateStocks = await App.graphClient.Me.Drive.Root.ItemWithPath(odStocksFilePath).Versions.Request().GetAsync();
                    Date odDateStocks = new Date(odItemDateStocks.First().LastModifiedDateTime.ToString().Split(' ')[0]);
                    Time odTimeStocks = new Time(odItemDateStocks.First().LastModifiedDateTime.ToString().Split(' ')[1]);
                    DateTime dateStocks = System.IO.File.GetLastWriteTime(FileManager.stocksFilePath);

                    if (odDateStocks.Year == dateStocks.Year)
                    {
                        if (odDateStocks.Month == dateStocks.Month)
                        {
                            if (odDateStocks.Day == dateStocks.Day)
                            {
                                if (odTimeStocks.Hours == dateStocks.Hour)
                                {
                                    if (odTimeStocks.Minutes == dateStocks.Minute)
                                    {
                                        if (odTimeStocks.Seconds < dateStocks.Second) SaveStock();
                                        else TryGetStock();
                                    }
                                    else if (odTimeStocks.Minutes < dateStocks.Minute) SaveStock();
                                    else TryGetStock();
                                }
                                else if (odTimeStocks.Hours < dateStocks.Hour) SaveStock();
                                else TryGetStock();
                            }
                            else if (odDateStocks.Day < dateStocks.Day) SaveStock();
                            else TryGetStock();
                        }
                        else if (odDateStocks.Month < dateStocks.Month) SaveStock();
                        else TryGetStock();
                    }
                    else if (odDateStocks.Year < dateStocks.Year) SaveStock();
                    else TryGetStock();
                }
                else
                {
                    TryGetStock();
                }

                ToastManager.Show("Successfully Synced");

                try
                {
                    if (menu != null) menu.FillTodayOrders();
                }
                catch(Exception ex)
                {

                }
            }
        }


        public override async void TryGetOrders()
        {
            Stream orders = await GetOrders();
            string lstOrders;
            using (StreamReader file = new StreamReader(orders))
            {
                lstOrders = file.ReadToEnd();
            }

            System.IO.File.WriteAllText(FileManager.ordersFilePath, lstOrders);

        }

        public override async void TryGetStock()
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
                currentOrdersTextStream = await App.graphClient.Me.Drive.Root.ItemWithPath(odOrdersFilePath).Content.Request().GetAsync();
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
                currentStocksTextStream = await App.graphClient.Me.Drive.Root.ItemWithPath(odStocksFilePath).Content.Request().GetAsync();
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
        
        public override async void SaveOrders()
        {
            Stream orders;
            
            try
            {
                orders = System.IO.File.Open(FileManager.ordersFilePath, FileMode.Open);
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

            await UploadOrders(ordersMS.ToArray());            
        }

        public override async void SaveStock()
        {
            Stream stocks;

            try
            {
                stocks = System.IO.File.Open(FileManager.stocksFilePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Reading File:", ex.Message, "Dismiss");
                return;
            }

            MemoryStream stocksMS = new MemoryStream();
            stocks.CopyTo(stocksMS);

            stocks.Flush();
            stocks.Dispose();
            stocks.Close();

            await UploadStocks(stocksMS.ToArray());
        }

        private async Task UploadOrders(byte[] file)
        {
            DriveItem uploadedItem = null;
            try
            {
                Stream fileStream = new MemoryStream(file);
                uploadedItem = await App.graphClient.Me.Drive.Root.ItemWithPath(odOrdersFilePath).Content.Request().PutAsync<DriveItem>(fileStream);

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
                uploadedItem = await App.graphClient.Me.Drive.Root.ItemWithPath(odStocksFilePath).Content.Request().PutAsync<DriveItem>(fileStream);

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
