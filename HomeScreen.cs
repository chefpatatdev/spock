using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpockApp.Resources.mipmap_xhdpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpockApp.src
{
    [Activity(Label = "HomeScreen")]
    public class HomeScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_screen);

            Button LiveDrivingNav = FindViewById<Button>(Resource.Id.live_driving_nav);
            LiveDrivingNav.Click += LiveDrivingNav_Click;
        }

        private void LiveDrivingNav_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LiveDriving));
            StartActivity(intent);
        }

    }
}