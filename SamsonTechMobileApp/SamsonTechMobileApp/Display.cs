using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SamsonTechMobileApp
{
    public class Display
    {
        private Order[] lstOrders;
        private Stock[] lstStocks;

        public Display(Order[] lstOrders)
        {
            this.lstOrders = lstOrders;
        }

        public Display(Stock[] lstStocks)
        {
            this.lstStocks = lstStocks;
        }

        public void AllOrders(ListView listOrders, int month, int year)
        {
            FileManager fileManager = new FileManager();
            lstOrders = fileManager.SortOrders(lstOrders);
            fileManager.SaveOrders(lstOrders);

            List<string> lstItems = new List<string>();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].Date.Year == year)
                {
                    if (lstOrders[i].Date.Month == month)
                    {
                        lstItems.Add(lstOrders[i].ID + ": " + lstOrders[i].Date.Day + "/" + lstOrders[i].Date.Month + "/" + lstOrders[i].Date.Year + ", " + lstOrders[i].Name + ", Completed: " + lstOrders[i].Completed);
                    }
                }
            }

            listOrders.ItemsSource = lstItems;
        }

        public void AllStocks(ListView listStocks)
        {
            List<string> lstItems = new List<string>();

            if (lstStocks != null)
            {
                for (int i = 0; i < lstStocks.Length; i++)
                {
                    lstItems.Add(lstStocks[i].Name);
                }

                listStocks.ItemsSource = lstItems;
            }
        }

        public void OrdersToday(ListView listOrders)
        {
            DateTime dateTime = DateTime.Today;

            List<string> lstItems = new List<string>();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].Date.Year == dateTime.Year)
                {
                    if (lstOrders[i].Date.Month == dateTime.Month)
                    {
                        if (lstOrders[i].Date.Day == dateTime.Day)
                        {
                            lstItems.Add(lstOrders[i].ID + ": " + lstOrders[i].Date.Day + "/" + lstOrders[i].Date.Month + "/" + lstOrders[i].Date.Year + ", " + lstOrders[i].Name + ", Completed: " + lstOrders[i].Completed);
                        }
                    }
                }
            }

            if (lstItems.Count == 0)
            {
                lstItems.Add("No Orders Today");
                listOrders.ItemsSource = lstItems;
            }
            else
            {
                listOrders.ItemsSource = lstItems;
            }
        }

        public void DisplayOrder(ViewOrders viewOrders, string selectedItem)
        {
            string[] split = selectedItem.Split(':');

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].ID == split[0])
                {
                    ViewOrder viewOrder = new ViewOrder(lstOrders[i]);
                    viewOrders.Navigation.PushAsync(viewOrder);
                }
            }
        }

        public void DisplayStock(StockPage stockPage, string selectedItem)
        {
            string[] split = selectedItem.Split(':');

            for (int i = 0; i < lstStocks.Length; i++)
            {
                if (lstStocks[i].Name == split[0])
                {
                    StockInfo stockInfo = new StockInfo(lstStocks[i]);
                    stockPage.Navigation.PushAsync(stockInfo);
                }
            }
        }

        public void MonthStats(Label txtCost, Label txtEarning, Label txtProfit, int month, int year)
        {
            List<Order> orders = new List<Order>();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].Date.Year == year)
                {
                    if (lstOrders[i].Date.Month == month)
                    {
                        if (lstOrders[i].Completed == "Yes")
                        {
                            orders.Add(lstOrders[i]);
                        }
                    }
                }
            }

            double cost = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                cost += double.Parse(orders[i].Cost);
            }
            double earnings = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                earnings += double.Parse(orders[i].Price);
            }

            txtCost.Text = cost.ToString();
            txtEarning.Text = earnings.ToString();
            txtProfit.Text = (earnings - cost).ToString();
        }

        public void YearStats(Label txtCost, Label txtEarning, Label txtProfit, int year)
        {
            List<Order> orders = new List<Order>();

            for (int i = 0; i < lstOrders.Length; i++)
            {
                if (lstOrders[i].Date.Year == year)
                {
                    if (lstOrders[i].Completed == "Yes")
                    {
                        orders.Add(lstOrders[i]);
                    }
                }
            }

            double cost = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                cost += double.Parse(orders[i].Cost);
            }
            double earnings = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                earnings += double.Parse(orders[i].Price);
            }

            txtCost.Text = cost.ToString();
            txtEarning.Text = earnings.ToString();
            txtProfit.Text = (earnings - cost).ToString();
        }
    }
}
