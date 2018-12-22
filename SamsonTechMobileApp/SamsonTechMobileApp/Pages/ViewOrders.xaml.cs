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
	public partial class ViewOrders : ContentPage
	{
        private Display display;

        public ViewOrders()
        {
            InitializeComponent();
            FileManager fileManager = new FileManager();
            display = new Display(fileManager.LoadOrders());
            lstMonth.SelectedIndex = DateTime.Today.Month - 1;
            lstYear.SelectedIndex = DateTime.Today.Year - 2018;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FileManager fileManager = new FileManager();
            display = new Display(fileManager.LoadOrders());
            display.AllOrders(listOrders, lstMonth.SelectedIndex + 1, lstYear.SelectedIndex + 2018);
        }

        private void listOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            display.DisplayOrder(this, listOrders.SelectedItem.ToString());
        }

        private void lstMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (display != null)
            {
                display.AllOrders(listOrders, lstMonth.SelectedIndex + 1, lstYear.SelectedIndex + 2018);
            }
        }

        private void lstYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (display != null)
            {
                display.AllOrders(listOrders, lstMonth.SelectedIndex + 1, lstYear.SelectedIndex + 2018);
            }
        }
    }
}