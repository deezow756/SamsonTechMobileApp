using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockPage : ContentPage
	{
        bool multiSelection = false;
        List<Item> multiSelectionList;

        bool firstBoot = true;

        List<Item> items;
        List<Item> curItems = new List<Item>();
        List<Item> lstItems;

		public StockPage ()
		{
			InitializeComponent ();
            FileManager fileManager = new FileManager();
            items = new List<Item>(fileManager.LoadItems());
            Setup();
            
        }

        protected override void OnAppearing()
        {
            if (!firstBoot)
            {
                FileManager fileManager = new FileManager();
                items = new List<Item>(fileManager.LoadItems());
                Setup();

            }
            else
            {
                firstBoot = false;
            }
        }

        private void Setup()
        {
            liststock.ItemsSource = null;
            if (items.Count > 0)
            {
                lstItems = new List<Item>();
                if(curItems.Count == 0)
                {
                    txtTitle.Text = "Stocks";
                    for (int i = 0; i < items.Count; i++)
                    {
                        if(string.IsNullOrEmpty(items[i].Catergory))
                        {
                            lstItems.Add(items[i]);
                        }
                    }                    
                }
                else
                {
                    txtTitle.Text = curItems[curItems.Count - 1].Name;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if(items[i].Catergory == curItems[curItems.Count - 1].Name)
                        {
                            lstItems.Add(items[i]);
                        }
                    }
                }

                if(lstItems != null)
                {
                    liststock.ItemsSource = lstItems;
                }
                else
                {
                    liststock.ItemsSource = new List<Item>() { new Item() { Name = "No Items In This Catergory"} };
                }
            }
            else
            {
                liststock.ItemsSource = new List<Item>() { new Item() { Name = "No Items In This Catergory" } };
            }
        }

        private void BtnAddStock_Clicked(object sender, EventArgs e)
        {
            if(curItems.Count == 0)
                Navigation.PushAsync(new AddStock(null));
            else
                Navigation.PushAsync(new AddStock(curItems[curItems.Count - 1]));
        }

        private void TextCell_Tapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);
            liststock.SelectedItem = null;

            if (!multiSelection)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ClassId == vc.ClassId)
                    {
                        if (items[i].CatergoryType == false)
                        {
                            Navigation.PushAsync(new StockInfo(items[i]));
                        }
                        else
                        {
                            curItems.Add(items[i]);
                            Setup();
                        }
                    }
                }
            }
            else
            {
                foreach(Item item in lstItems)
                {
                    if(item.ClassId == vc.ClassId)
                    {
                        bool catHasItems = false;
                        if(item.CatergoryType)
                        {
                            for (int i = 0; i < items.Count; i++)
                            {
                                if (item.Name == items[i].Catergory)
                                {
                                    catHasItems = true;
                                    ToastManager.Show("This Catergory currently has items within it, please delete items before deleting catergory");
                                    break;
                                }
                            }
                        }

                        if (!catHasItems)
                        {
                            if (item.Selection)
                            {
                                if (multiSelectionList.Count > 0)
                                    multiSelectionList.Remove(item);
                                item.Selection = false;
                            }
                            else
                            {
                                multiSelectionList.Add(item);
                                item.Selection = true;
                            }                            
                            Setup();
                        }
                        break;

                    }
                }
            }
        }

        public void ToggleMultiSelection()
        {
            if (multiSelection)
            {
                multiSelection = false;
                if(multiSelectionList != null)
                {
                    multiSelectionList.Clear();
                }

                if (lstItems != null)
                {
                    foreach (Item item in lstItems)
                    {
                        item.MultiSelectionMode = false;
                    }
                }

                Setup();
            }
            else
            {
                multiSelection = true;
                multiSelectionList = new List<Item>();
                if (lstItems != null)
                {
                    foreach (Item item in lstItems)
                    {
                        item.MultiSelectionMode = true;
                    }
                }

                Setup();
            }
        }

        public void DeleteItems()
        {
            if (multiSelection)
            {
                FileManager fileManager = new FileManager();
                fileManager.DeleteItems(multiSelectionList);
                
                items = new List<Item>(fileManager.LoadItems());
                ToggleMultiSelection();
                Setup();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Back();
            return true;
        }

        private void Back()
        {
            if (!multiSelection)
            {
                if (curItems.Count > 0)
                {
                    curItems.RemoveAt(curItems.Count - 1);
                    Setup();
                }
                else
                {
                    Navigation.PopAsync();
                }
            }
            else
            {
                ToggleMultiSelection();
            }
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if(multiSelection)
            {
                DeleteItems();
            }
            else
            {
                ToggleMultiSelection();
            }
        }
    }
}