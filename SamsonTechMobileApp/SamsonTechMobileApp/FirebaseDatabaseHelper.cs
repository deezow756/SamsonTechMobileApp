using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace SamsonTechMobileApp
{
    public class FirebaseDatabaseHelper : CloudStorage
    {
        Menu menu;
        FirebaseClient firebase;

        public FirebaseDatabaseHelper(Menu menu)
        {
            this.menu = menu;
        }

        public override async void Sync()
        {
            if (firebase != null)
            {
                if (System.IO.File.Exists(FileManager.ordersFilePath))
                {
                    try
                    {
                        // FirebaseMetaData metaDataOrders = await firebase
                        //.Child("Documents")
                        //.Child("SamsonTech")
                        //.Child("Orders.txt")
                        //.GetMetaDataAsync();
                        // DateTime firebaseDateOrders = metaDataOrders.Updated;

                        List<FirebaseDataTime> firebaseDatas = await GetDateTimeOrders();
                        FirebaseDataTime firebaseDateOrders = firebaseDatas[0];

                        DateTime dateOrders = System.IO.File.GetLastWriteTime(FileManager.ordersFilePath);

                        if (firebaseDateOrders.Year == dateOrders.Year)
                        {
                            if (firebaseDateOrders.Month == dateOrders.Month)
                            {
                                if (firebaseDateOrders.Day == dateOrders.Day)
                                {
                                    if (firebaseDateOrders.Hour == dateOrders.Hour)
                                    {
                                        if (firebaseDateOrders.Minute == dateOrders.Minute)
                                        {
                                            if (firebaseDateOrders.Second < dateOrders.Second) SaveOrders();
                                            else TryGetOrders();
                                        }
                                        else if (firebaseDateOrders.Minute < dateOrders.Minute) SaveOrders();
                                        else TryGetOrders();
                                    }
                                    else if (firebaseDateOrders.Hour < dateOrders.Hour) SaveOrders();
                                    else TryGetOrders();
                                }
                                else if (firebaseDateOrders.Day < dateOrders.Day) SaveOrders();
                                else TryGetOrders();
                            }
                            else if (firebaseDateOrders.Month < dateOrders.Month) SaveOrders();
                            else TryGetOrders();
                        }
                        else if (firebaseDateOrders.Year < dateOrders.Year) SaveOrders();
                        else TryGetOrders();
                    }
                    catch (Exception ex)
                    {
                        SaveOrders();
                    }
                }
                else
                {
                    TryGetOrders();
                }



                if (System.IO.File.Exists(FileManager.stocksFilePath))
                {
                    try
                    {
                        //FirebaseMetaData metaDataStocks = await new FirebaseStorage("samsontech-ebf50.appspot.com")
                        //    .Child("Documents")
                        //    .Child("SamsonTech")
                        //    .Child("Items.txt")
                        //    .GetMetaDataAsync();

                        //DateTime firebaseDateStock = metaDataStocks.Updated;
                        List<FirebaseDataTime> firebaseDatas = await GetDateTimeStocks();
                        FirebaseDataTime firebaseDateStock = firebaseDatas[0];

                        DateTime dateStocks = System.IO.File.GetLastWriteTime(FileManager.stocksFilePath);

                        if (firebaseDateStock.Year == dateStocks.Year)
                        {
                            if (firebaseDateStock.Month == dateStocks.Month)
                            {
                                if (firebaseDateStock.Day == dateStocks.Day)
                                {
                                    if (firebaseDateStock.Hour == dateStocks.Hour)
                                    {
                                        if (firebaseDateStock.Minute == dateStocks.Minute)
                                        {
                                            if (firebaseDateStock.Second < dateStocks.Second) SaveStock();
                                            else TryGetStock();
                                        }
                                        else if (firebaseDateStock.Minute < dateStocks.Minute) SaveStock();
                                        else TryGetStock();
                                    }
                                    else if (firebaseDateStock.Hour < dateStocks.Hour) SaveStock();
                                    else TryGetStock();
                                }
                                else if (firebaseDateStock.Day < dateStocks.Day) SaveStock();
                                else TryGetStock();
                            }
                            else if (firebaseDateStock.Month < dateStocks.Month) SaveStock();
                            else TryGetStock();
                        }
                        else if (firebaseDateStock.Year < dateStocks.Year) SaveStock();
                        else TryGetStock();
                    }
                    catch (Exception ex)
                    {
                        SaveStock();
                    }
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
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<List<FirebaseDataTime>> GetDateTimeOrders()
        {

            return (await firebase
              .Child("DateTimeOrder")
              .OnceAsync<FirebaseDataTime>()).Select(item => new FirebaseDataTime
              {
                  Year = item.Object.Year,
                  Month = item.Object.Month,
                  Day = item.Object.Day,
                  Hour = item.Object.Hour,
                  Minute = item.Object.Minute,
                  Second = item.Object.Second
              }).ToList();
        }

        public async Task<List<FirebaseDataTime>> GetDateTimeStocks()
        {

            return (await firebase
              .Child("DateTimeStock")
              .OnceAsync<FirebaseDataTime>()).Select(item => new FirebaseDataTime
              {
                  Year = item.Object.Year,
                  Month = item.Object.Month,
                  Day = item.Object.Day,
                  Hour = item.Object.Hour,
                  Minute = item.Object.Minute,
                  Second = item.Object.Second
              }).ToList();
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
        #region first try

        //public override void Signin()
        //{
        //    try
        //    {
        //        firebase = new FirebaseStorage("samsontech-ebf50.appspot.com");
        //    }
        //    catch(FirebaseStorageException ex)
        //    {
        //        DisplayMessage("Could not sign into firebase", ex.Message, "ok");
        //    }

        //    Sync();

        //    if (menu != null) menu.FillTodayOrders();
        //}
        //public override void Signout()
        //{
        //    firebase = null;
        //}

        //public override async void TryGetOrders()
        //{
        //    Stream orders = await DownloadFile(await firebase
        //       .Child("Orders.txt")
        //       .GetDownloadUrlAsync());

        //    string lstOrders;
        //    using (StreamReader file = new StreamReader(orders))
        //    {
        //        lstOrders = file.ReadToEnd();
        //    }

        //    System.IO.File.WriteAllText(FileManager.ordersFilePath, lstOrders);
        //}        

        //public override async void TryGetStock()
        //{
        //    Stream items = await DownloadFile(await firebase
        //       .Child("Items.txt")
        //       .GetDownloadUrlAsync());

        //    string lstStocks;
        //    using (StreamReader file = new StreamReader(items))
        //    {
        //        lstStocks = file.ReadToEnd();
        //    }

        //    System.IO.File.WriteAllText(FileManager.stocksFilePath, lstStocks);
        //}

        //async Task<Stream> DownloadFile(string url)
        //{
        //    var _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };

        //    try
        //    {
        //        using (var httpResponse = await _httpClient.GetAsync(url))
        //        {
        //            if (httpResponse.StatusCode == HttpStatusCode.OK)
        //            {
        //                return await httpResponse.Content.ReadAsStreamAsync();
        //            }
        //            else
        //            {
        //                //Url is Invalid
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //Handle Exception
        //        return null;
        //    }
        //}

        //public override async void SaveOrders()
        //{
        //    Stream orders;

        //    try
        //    {
        //        orders = System.IO.File.Open(FileManager.ordersFilePath, FileMode.Open);
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMessage("Error Reading File:", ex.Message, "Dismiss");
        //        return;
        //    }

        //    orders.Flush();
        //    orders.Dispose();
        //    orders.Close();
        //    try
        //    {
        //        var storeOrders = await firebase
        //            .Child("Orders.txt")
        //            .PutAsync(orders);
        //    }
        //    catch (FirebaseStorageException ex)
        //    {
        //        DisplayMessage("Could not save orders", ex.Message, "ok");
        //    }
        //}

        //public override async void SaveStock()
        //{
        //    Stream stocks;

        //    try
        //    {
        //        stocks = System.IO.File.Open(FileManager.stocksFilePath, FileMode.Open);
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMessage("Error Reading File:", ex.Message, "Dismiss");
        //        return;
        //    }

        //    stocks.Flush();
        //    stocks.Dispose();
        //    stocks.Close();

        //    try
        //    {
        //        var storeItems = await firebase
        //           .Child("Items.txt")
        //           .PutAsync(stocks);
        //    }
        //    catch (FirebaseStorageException ex)
        //    {
        //        DisplayMessage("Could not save stocks", ex.Message, "ok");
        //    }
        //}
        #endregion

        public override void Signin()
        {
            try
            {
                firebase = new FirebaseClient("https://samsontech-ebf50.firebaseio.com/");
            }
            catch (Exception ex)
            {
                DisplayMessage("Could not sign into firebase", ex.Message, "ok");
            }

            Sync();            
        }
        public override void Signout()
        {
            firebase = null;
        }

        public override async void TryGetOrders()
        {
            List<Order> orders = await GetAllOrders();
            string[] jsons = new string[orders.Count];

            for (int i = 0; i < orders.Count; i++)
            {
                jsons[i] = Newtonsoft.Json.JsonConvert.SerializeObject(orders[i]);
            }

            System.IO.File.WriteAllLines(FileManager.ordersFilePath, jsons);
        }

        async Task <List<Order>> GetAllOrders()
        {
            return (await firebase
              .Child("Orders")
              .OnceAsync<Order>(System.TimeSpan.FromSeconds(2))).Select(item => new Order
              {
                  ID = item.Object.ID,
                  Name = item.Object.Name,
                  Contact = item.Object.Contact,
                  Date = item.Object.Date,
                  Device = item.Object.Device,
                  Colour = item.Object.Colour,
                  ReasonForFix = item.Object.ReasonForFix,
                  Price = item.Object.Price,
                  Cost = item.Object.Cost,
                  Completed = item.Object.Completed
              }).ToList();
        }

        public override async void TryGetStock()
        {
            List<Item> stocks = await GetAllStocks();
            string[] jsons = new string[stocks.Count];

            for (int i = 0; i < stocks.Count; i++)
            {
                jsons[i] = Newtonsoft.Json.JsonConvert.SerializeObject(stocks[i]);
            }

            System.IO.File.WriteAllLines(FileManager.stocksFilePath, jsons);
        }

        async Task<List<Item>> GetAllStocks()
        {
            return (await firebase
              .Child("Stocks")
              .OnceAsync<Item>(System.TimeSpan.FromSeconds(2))).Select(item => new Item
              {
                  Catergory = item.Object.Catergory,
                  Name = item.Object.Name,
                  Quantity = item.Object.Quantity,
                  Description = item.Object.Description,
                  CatergoryType = item.Object.CatergoryType
              }).ToList();
        }
                             

        public override async void SaveOrders()
        {
            string[] jsons = File.ReadAllLines(FileManager.ordersFilePath);
            List<Order> orders = new List<Order>();
            for (int i = 0; i < jsons.Length; i++)
            {
                orders.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(jsons[i]));
            }           

            await DeleteOrders();

            await AddOrders(orders);

            DateTime date = DateTime.Now;
            FirebaseDataTime dataTime = new FirebaseDataTime()
            {
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                Hour = date.Hour,
                Minute = date.Minute,
                Second = date.Second
            };

            try
            {
                var toDelete = (await firebase
                      .Child("DateTimeOrder")
                      .OnceAsync<FirebaseDataTime>()).FirstOrDefault();

                await firebase
               .Child("DateTimeOrder")
               .Child(toDelete.Key)
               .DeleteAsync();

                await firebase
                  .Child("DateTimeOrder")
                  .PostAsync<FirebaseDataTime>(dataTime);
            }
            catch (Exception ex)
            {
                await firebase
                  .Child("DateTimeOrder")
                  .PostAsync<FirebaseDataTime>(dataTime);
            }
        }

        async Task AddOrders(List<Order> orders)
        {

            await firebase
           .Child("Orders")
           .PostAsync(orders);
                     
        }
        async Task DeleteOrders()
        {
            try
            {
                var toDeletePerson = (await firebase
              .Child("Orders")
              .OnceAsync<Order>()).ToList();

                for (int i = 0; i < toDeletePerson.Count; i++)
                {
                    await firebase
               .Child("Orders")
               .Child(toDeletePerson[i].Key)
               .DeleteAsync();
                }
                
            }
            catch (Exception ex)
            {
                DisplayMessage("Could not deleteorders", ex.Message, "ok");
            }
        }

        public override async void SaveStock()
        {
            string[] jsons = File.ReadAllLines(FileManager.stocksFilePath);
            List<Item> stocks = new List<Item>();
            for (int i = 0; i < jsons.Length; i++)
            {
                stocks.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(jsons[i]));
            }

            await DeleteStocks();

            await AddStocks(stocks);

            DateTime date = DateTime.Now;
            FirebaseDataTime dataTime = new FirebaseDataTime()
            {
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                Hour = date.Hour,
                Minute = date.Minute,
                Second = date.Second
            };

            try
            {
                var toDelete = (FirebaseObject<FirebaseDataTime>)(await firebase
                      .Child("DateTimeStock")
                      .OnceAsync<FirebaseDataTime>());

                await firebase
               .Child("DateTimeStock")
               .Child(toDelete.Key)
               .DeleteAsync();

                await firebase
                  .Child("DateTimeStock")
                  .PostAsync<FirebaseDataTime>(dataTime);
            }
            catch (Exception ex)
            {
                await firebase
                  .Child("DateTimeStock")
                  .PostAsync<FirebaseDataTime>(dataTime);
            }
        }

        async Task DeleteStocks()
        {
            try
            {
                var toDeletePerson = (await firebase
                  .Child("Stocks")
                  .OnceAsync<Item>()).ToList();

                for (int i = 0; i < toDeletePerson.Count; i++)
                {
                    await firebase
               .Child("Stocks")
               .Child(toDeletePerson[i].Key)
               .DeleteAsync();
                }
                
            }
            catch (Exception ex)
            {
                DisplayMessage("Could not delete stocks", ex.Message, "ok");
            }
        }

        async Task AddStocks(List<Item> stocks)
        {
            await firebase
            .Child("Stocks")
            .PostAsync(stocks);
        }
    }

}
