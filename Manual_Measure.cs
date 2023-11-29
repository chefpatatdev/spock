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
    public class Manual_Measure : Activity
    {
        static EditText input;
        static ListView list;
        static ListViewAdapterMeasure adapter;
        static Context context;
        static string[] Filter { get; set; } = { "value", "Scalar", "Sensor", "datum" };
        static string[,] SensorData { get; set; } = { { "4cm", "100cm", "53cm", "13cm", "1" }, { "afstand1", "afstand2", "afst3", "afst4", "1" }, { "sens1", "sens2", "sens3", "sens4", "1" }, { "1 januari", "2januari", "3 febr", "6 december", "16 decemberrrrrrr" } };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.manual_measurement_screen);

            string[] listlength = new string[SensorData.GetLength(1)];
            InitializeStringArray(listlength);
            adapter = new ListViewAdapterMeasure(this, SensorData, listlength);
            context = ApplicationContext;


            InitializeSpinner(Filter);
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


        private void Btn_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    break;
                default:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    break;
            }
        }

        private void InitializeSpinner(string[] traject_array)
        {
            //Maken van drop down menu om traject te slecteren
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            //custom strings in de drop down menu zetten
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, traject_array);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
           
        }

    }
}