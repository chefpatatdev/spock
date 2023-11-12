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
    public class MeasuringScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.measuring_screen);
            // Create your application here
            Button measureButton = FindViewById<Button>(Resource.Id.measure_button);
            //LoginButton.Click += LoginAttempt_Click;


        }
    }
}