using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace SpockApp.Resources


{
    [Activity(Label = "MeasuringScreen")]
    public class LiveMeasure : Activity
    {
        static string[] Sensornames { get; set; } = { "US sensor", "Temp", "Sensor", "all" };
        string SensorSelected { get; set; } = "US Sensor";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.measuring_screen);
            // Create your application here
            Button measureButton = FindViewById<Button>(Resource.Id.measure_button);
            //LoginButton.Click += LoginAttempt_Click;

            
        }
        private void LiveMeasureRequested()
        {
            string traject_names = SocketClass.Sendmessage("r_measure,"  + "live," + SensorSelected);

        }
    }
}   