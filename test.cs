using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpockApp
{
    [Activity(Label = "test")]
    public class test : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pickertest);

            // Create your application here
            NumberPicker picker = FindViewById<NumberPicker>(Resource.Id.numberPicker);
            picker.MinValue = 0;
            picker.MaxValue = 10;
            //picker.ValueChanged += numberPicker;
        }
    }
}