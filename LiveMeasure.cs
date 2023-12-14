using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpockApp.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace SpockApp.Resources


{
    [Activity(Label = "MeasuringScreen")]
    public class LiveMeasure : Activity
    {
        static string[] Sensornames { get; set; } = { "USsensor", "Temp", "Sensor", "all" };
        string SensorSelected { get; set; } = "USSensor";
        bool Switch { get; set; } = false;
        TextView editText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.measuring_screen);
            // Create your application here
            //Button measureButton = FindViewById<Button>(Resource.Id.measure_button);
            //LoginButton.Click += LoginAttempt_Click;
            //Maken van drop down menu om traject te slecteren
            Spinner spinners = FindViewById<Spinner>(Resource.Id.spinnersensor);
            spinners.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Ssensors_ItemSelected);
            //custom strings in de drop down menu zetten
            var adapter2 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, Sensornames);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinners.Adapter = adapter2;

            editText = FindViewById<TextView>(Resource.Id.display);
            editText.Text = "__cm";
            Switch ptr = FindViewById<Switch>(Resource.Id.measuring_switch);
            ptr.CheckedChange += (sender, e) =>
            {
                Switch = e.IsChecked;
                LiveMeasureRequested();
            };
        }
        private void Ssensors_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
            SensorSelected = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }
        public void ErrorHandling()
        {
            Intent intent = new Intent(this, typeof(HomeScreen));
            intent.PutExtra("context", "error");

            StartActivity(intent);
        }
        private void LiveMeasureRequested()
        {
            while(Switch)
            {
                string value = SocketClass.Sendmessage("r_measure," + "live," + SensorSelected);
                if(value == null)
                {
                    ErrorHandling();
                }
                else
                {
                    editText.Text = value;
                }
            }

        }
    }
}   