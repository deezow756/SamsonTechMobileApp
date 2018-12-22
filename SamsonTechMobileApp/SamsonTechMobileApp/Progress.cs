using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SamsonTechMobileApp
{
    public class Progress
    {
        public static void Show()
        {
            DependencyService.Get<IProgressInterface>().Show();
        }

        public static void Hide()
        {
            DependencyService.Get<IProgressInterface>().Hide();
        }
    }
}
