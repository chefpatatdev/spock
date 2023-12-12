using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Renderscripts.ScriptGroup;

namespace SpockApp
{
    [Activity(Label = "Manual_Driving")]
    public class ManualMeasure : Activity
    {
        static EditText input;
        static ListView list;
        static ListViewAdapterMeasure adapter;
        static Context context;
        static string[] Filter { get; set; } = { "scalar", "sensor", "date", "time before", "time after", "value bigger", "value smaller", "all" };
        static string[] Sensornames { get; set; } = { "US sensor", "Temp", "Sensor", "all" };
        static string[,] SensorData { get; set; } = { { "4cm", "100cm", "53cm", "13cm", "1" }, { "afstand1", "afstand2", "afst3", "afst4", "1" }, { "sens1", "sens2", "sens3", "sens4", "1" }, { "1 januari", "2januari", "3 febr", "6 december", "16 decemberrrrrrr" } };

        string SensorSelected { get; set; } 
        string FilterSelected { get; set; } 
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.manual_measurement_screen);

            string[] listlength = new string[SensorData.GetLength(1)];
            InitializeStringArray(listlength);
            adapter = new ListViewAdapterMeasure(this, SensorData, listlength);
            context = ApplicationContext;


            InitializeSpinners(Filter, Sensornames);
            input = FindViewById<EditText>(Resource.Id.traject_input);
            Button btn = FindViewById<Button>(Resource.Id.manual_measure_button);
            btn.Touch += Btn_Touch;

            list = (ListView)FindViewById<ListView>(Resource.Id.listview_edit);
            list.Adapter = adapter;

        }
        private void InitializeStringArray(string[] array)
        {
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = "";
                }
            
        }

        private void RequestMeasurements()
        {
            string measurements = SocketClass.Sendmessage("r_filter," + FilterSelected + "," + input.Text.ToString());
            string[] measurements_list = measurements.Split(",");
            for (int i = 0; i < SensorData.GetLength(1); i++)
            {
                for (int j = 0; j < SensorData.GetLength(0); j++)
                {
                    if (measurements_list[SensorData.GetLength(0) * i + j] == "Stop") break;
                    SensorData[j, i] = measurements_list[SensorData.GetLength(0) * i + j];
                    if (i == 2) Sensornames[j] = SensorData[j, i];
                }
            }

        }
        private void Btn_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    RequestManualMeasure();
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    break;
                default:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    break;
            }
        }
        private void RequestManualMeasure()
        {
            string traject_names = SocketClass.Sendmessage("r_measure," + SensorSelected + ",one" );
        }

        private void InitializeSpinners(string[] filter_array, string[] naam_array )
        {
            //Maken van drop down menu om traject te slecteren
            Spinner spinnerf = FindViewById<Spinner>(Resource.Id.spinnerfilter);
            spinnerf.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Sfilter_ItemSelected);
            //custom strings in de drop down menu zetten
            var adapter1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, filter_array);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerf.Adapter = adapter1;

            //Maken van drop down menu om traject te slecteren
            Spinner spinners = FindViewById<Spinner>(Resource.Id.spinnersensor);
            spinners.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Ssensors_ItemSelected);
            //custom strings in de drop down menu zetten
            var adapter2 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, naam_array);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinners.Adapter = adapter2;

        }
        private void Sfilter_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
            FilterSelected = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        }
        private void Ssensors_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
            SensorSelected = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

    }
}