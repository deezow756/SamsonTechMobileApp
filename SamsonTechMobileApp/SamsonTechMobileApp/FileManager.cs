﻿using Newtonsoft.Json;
using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SamsonTechMobileApp
{
    public class FileManager
    {
        public static string ordersFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OrderS.json");
        string idCountFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Ids.txt");

        public void SaveOrder(AddOrder addOrder, Order order)
        {

            List<Order> lstOrders = LoadOrders().ToList<Order>();

            int highest = 0;
            for (int i = 0; i < lstOrders.Count; i++)
            {
                if (int.Parse(lstOrders[i].ID) > highest)
                {
                    highest = int.Parse(lstOrders[i].ID);
                }
            }

            order.ID = (highest + 1).ToString();
            order.Completed = "No";

            if (!File.Exists(ordersFilePath))
            {
                File.WriteAllText(ordersFilePath, JsonConvert.SerializeObject(order));
            }
            else
            {
                lstOrders.Add(order);

                Order[] orders = SortOrders(lstOrders.ToArray());
                string[] jsons = new string[orders.Length];

                for (int i = 0; i < orders.Length; i++)
                {
                    jsons[i] = JsonConvert.SerializeObject(orders[i]);
                }

                File.WriteAllLines(ordersFilePath, jsons);
            }
        }

        public void SaveEdit(Order order)
        {
            Order[] orders = LoadOrders();

            for (int i = 0; i < orders.Length; i++)
            {
                if (order.ID == orders[i].ID)
                {
                    orders[i] = order;
                    List<string> jsons = new List<string>();
                    for (int j = 0; j < orders.Length; j++)
                    {
                        jsons.Add(JsonConvert.SerializeObject(orders[j]));
                    }
                    File.WriteAllLines(ordersFilePath, jsons.ToArray());
                    break;
                }
            }
        }

        public Order[] LoadOrders()
        {
            if (File.Exists(ordersFilePath))
            {
                string[] allJson = File.ReadAllLines(ordersFilePath);
                Order[] lstOrders = new Order[allJson.Length];

                for (int i = 0; i < allJson.Length; i++)
                {
                    lstOrders[i] = JsonConvert.DeserializeObject<Order>(allJson[i]);
                }

                return lstOrders;
            }
            return null;
        }

        public Order RefreshOrder(Order order)
        {
            Order[] orders = LoadOrders();

            for (int i = 0; i < orders.Length; i++)
            {
                if (order.ID == orders[i].ID)
                {
                    return orders[i];
                }
            }

            return null;
        }

        public void DeleteOrder(Order order)
        {
            Order[] orders = LoadOrders();

            for (int i = 0; i < orders.Length; i++)
            {
                if (order.ID == orders[i].ID)
                {
                    orders[i] = order;
                    List<string> jsons = new List<string>();
                    for (int j = 0; j < orders.Length; j++)
                    {
                        jsons.Add(JsonConvert.SerializeObject(orders[j]));
                    }

                    jsons.Remove(jsons[i]);
                    File.WriteAllLines(ordersFilePath, jsons.ToArray());
                }
            }
        }

        public Order[] SortOrders(Order[] orders)
        {
            bool swap;
            int numOfIteration = 0;
            do
            {
                swap = false;
                for (int i = orders.Length - 1; i > numOfIteration; i--)
                {
                    if (orders[i - 1].Date.Year == orders[i].Date.Year)
                    {
                        if (orders[i - 1].Date.Month == orders[i].Date.Month)
                        {
                            if (orders[i - 1].Date.Day < orders[i].Date.Day)
                            {
                                Order temp = orders[i - 1];
                                orders[i - 1] = orders[i];
                                orders[i] = temp;
                                swap = true;
                            }
                        }
                        else if (orders[i - 1].Date.Month < orders[i].Date.Month)
                        {
                            Order temp = orders[i - 1];
                            orders[i - 1] = orders[i];
                            orders[i] = temp;
                            swap = true;
                        }
                    }
                    else if (orders[i - 1].Date.Year < orders[i].Date.Year)
                    {
                        Order temp = orders[i - 1];
                        orders[i - 1] = orders[i];
                        orders[i] = temp;
                        swap = true;
                    }
                }

                numOfIteration += 1;
            } while (swap);

            return orders;
        }

        public void SaveOrders(Order[] orders)
        {
            string[] jsons = new string[orders.Length];

            for (int i = 0; i < orders.Length; i++)
            {
                jsons[i] = JsonConvert.SerializeObject(orders[i]);
            }

            File.WriteAllLines(ordersFilePath, jsons);
        }
    }
}
