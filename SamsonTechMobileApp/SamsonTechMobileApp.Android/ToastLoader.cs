using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SamsonTechMobileApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FloatLoader))]
namespace SamsonTechMobileApp.Droid
{
    public class FloatLoader : IToastInterface
    {
        public void Show(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}