﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewStatistics : ContentPage
	{
        private Display display;
        public ViewStatistics()
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
            display.MonthStats(txtMonTotalCost, txtMonTotalEarnings, txtMonNetProfit, lstMonth.SelectedIndex + 1, lstYear.SelectedIndex + 2018);
            display.YearStats(txtYearTotalCost, txtYearTotalEarnings, txtYearNetProfit, lstYear.SelectedIndex + 2018);

        }

        private void lstMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (display != null)
            {
                display.MonthStats(txtMonTotalCost, txtMonTotalEarnings, txtMonNetProfit, lstMonth.SelectedIndex + 1, lstYear.SelectedIndex + 2018);
            }
        }

        private void lstYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (display != null)
            {
                display.YearStats(txtYearTotalCost, txtYearTotalEarnings, txtYearNetProfit, lstYear.SelectedIndex + 2018);
            }
        }

        private void btnPrevMon_Clicked(object sender, EventArgs e)
        {
            if (lstMonth.SelectedIndex == 0)
            {
                if (lstYear.SelectedIndex > 0)
                {
                    lstYear.SelectedIndex = lstYear.SelectedIndex - 1;
                    lstMonth.SelectedIndex = 11;
                }
            }
            else
            {
                lstMonth.SelectedIndex = lstMonth.SelectedIndex - 1;
            }
        }

        private void btnNextMon_Clicked(object sender, EventArgs e)
        {
            if (lstMonth.SelectedIndex == 11)
            {
                if (lstYear.SelectedIndex < 3)
                {
                    lstYear.SelectedIndex = lstYear.SelectedIndex + 1;
                    lstMonth.SelectedIndex = 0;
                }
            }
            else
            {
                lstMonth.SelectedIndex = lstMonth.SelectedIndex + 1;
            }
        }
    }
}