using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SpockApp.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static Android.Renderscripts.ScriptGroup;

namespace SpockApp.Resources


{
    [Activity(Label = "MeasuringScreen")]
    public class LiveMeasure : Activity
    {
        static string[] Sensornames { get; set; } = { "" };
        string SensorSelected { get; set; } 
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

            RequestSensorNames();
            SensorSelected = Sensornames[0];
            Spinner spinners = FindViewById<Spinner>(Resource.Id.spinnersensorlive);
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
                if (Switch)  LiveMeasureRequested(); 
            };
            ImageView socketIndicator = FindViewById<ImageView>(Resource.Id.socket_indicator);
            SocketClass.socketIndicator_update = socketIndicator;

        }

        private void RequestSensorNames()
        {

            string measurements = SocketClass.Sendmessage("r_filter,all,");
            string[,] tmp = new string[4, 100];
            if (measurements == null)
            {
                  ErrorHandling();
            }
            else
            {
                string[] measurements_list = measurements.Split(",");
                for (int i = 0; i < tmp.GetLength(1); i++)
                {
                    for (int j = 0; j < tmp.GetLength(0); j++)
                    {
                        if (measurements_list[tmp.GetLength(0) * i + j] == ";") break;
                        tmp[j, i] = measurements_list[tmp.GetLength(0) * i + j];
                        if (j == 2 ) Sensornames[i] = tmp[j, i];
                    }
                   if (measurements_list[tmp.GetLength(0) * i + 1] == " ") break;
                }
            }
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
        private async void LiveMeasureRequested()
        {
            while (Switch)
            {
                string value = SocketClass.Sendmessage("r_measure," + "live," + SensorSelected);
                if (value == null)
                {
                    ErrorHandling();
                }
                else
                {
                    editText.Text = value;
                    await Task.Delay(1000);
                }
            }
            

        }
    }
}   