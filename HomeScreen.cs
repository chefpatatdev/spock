using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpockApp.Resources;
using SpockApp.Resources.mipmap_xhdpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using static Xamarin.Essentials.Platform;
using Context = Android.Content.Context;
using Intent = Android.Content.Intent;

namespace SpockApp.src
{
    [Activity(Label = "HomeScreen")]
    public class HomeScreen : Activity
    {
        string login;
        static Context context;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_screen);
            context = ApplicationContext;
            Button LiveDrivingNav = FindViewById<Button>(Resource.Id.live_driving_nav);
            LiveDrivingNav.Click += LiveDrivingNav_Click;

            Button TrajectDrivingNav = FindViewById<Button>(Resource.Id.traject_driving_nav);
            TrajectDrivingNav.Click += TrajectDrivingNav_Click;

            Button measure = FindViewById<Button>(Resource.Id.measuring_screen);
            measure.Click += MeasuringScreen_click;

            Button measure_live = FindViewById<Button>(Resource.Id.sockets);
            measure_live.Click += MeasuringLive_click;

            ImageView socketIndicator = FindViewById<ImageView>(Resource.Id.socket_indicator);
            SocketClass.socketIndicator_update = socketIndicator;
            string error;
            if (Intent.GetStringExtra("context") != null)
            {
                error = Intent.GetStringExtra("context");

                if (error == "error") ErrorHandling();
            }
            
            if (Intent.GetStringExtra("screen") != null)
            {
                login = Intent.GetStringExtra("screen");
            }
        }
        public override void OnBackPressed()
        {  
            if (login == "login") SocketClass.Disconnect(); 
            base.OnBackPressed();
        }

        public void ErrorHandling()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Connection Lost");
            alert.SetMessage("Try again or go back to login page to try and connect again.");
            alert.SetPositiveButton("Retry", (c, ev) =>
            {
               bool connectionstatus =  SocketClass.Connect(SocketClass.host, SocketClass.port);
               if (!connectionstatus)
               {
                    ErrorHandling();
               }

            });
            alert.SetNegativeButton("Login", (c, ev) =>
            {
                Intent intent = new Intent(this, typeof(LoginScreen));
                StartActivity(intent);
            });
            alert.Show();
        }
        private void LiveDrivingNav_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LiveDriving));
            StartActivity(intent);
        }
        private void MeasuringScreen_click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ManualMeasure));
            StartActivity(intent);
        }

        private void TrajectDrivingNav_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TrajectDriving));
            StartActivity(intent);
        }

        private void MeasuringLive_click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LiveMeasure));
            StartActivity(intent);
        }

    }
}