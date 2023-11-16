﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using SpockApp.Resources.mipmap_xhdpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpockApp
{
    [Activity(Label = "Traject_Editor")]
    public class Traject_Editor : Activity
    {
        static string[] Trajects { get; set; } = { "Traject 1", "Traject 2", "Traject 3"};
        static ListView list;
        static ListViewAdapter adapter;
        static Context context;
        static EditText input;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_editor);

            Trajects = Intent.GetStringArrayExtra("arrayID");
            context = ApplicationContext;
            adapter = new ListViewAdapter(this, Trajects);

            Button btn = FindViewById<Button>(Resource.Id.add_traject);
            list = (ListView)FindViewById<ListView>(Resource.Id.listview_edit);
            input = FindViewById<EditText>(Resource.Id.traject_input);

            btn.Click += Add_Click;
            list.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(Listview_Click);
            list.Adapter = adapter;
        }
        public override void OnBackPressed()
        {
            //als terug gaat naar traject_driving wordt de veranderde stringarray meegegeven zodat daar gebruikt kan worden
            Intent intent = new Intent(this, typeof(TrajectDriving));
            intent.PutExtra("trajectID", Trajects);
            StartActivity(intent);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            //voegt de ingevoerde string toe aan de projecten en kijkt ook ofdat deze niet leeg is
            string text = input.Text.ToString();
            if (string.IsNullOrEmpty(text))
            {
                Toast.MakeText(this, "Enter Something", ToastLength.Short).Show();
                UpdateListView();
            }
            else {
                AddItems(text);
                input.Text = "";
            }
        }

        private static void UpdateListView()
        {
            //update listview adapter
            adapter = new ListViewAdapter(context, Trajects);
            list.Adapter = adapter;

        }

        private void Listview_Click(object sender, AdapterView.ItemClickEventArgs e)
        {   
            //eventuele click handler als er iets moet gebeuren als er op een item geklikt wordt
            var list = (ListView)sender;
            Toast.MakeText(this, list.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show();
        }

        private void AddItems(string item)
        {
            // item aan trajecten toevoegen door naar een modifiable lijst om te zetten
            var tmplist = new List<string>(Trajects.ToList())
            {
                item
            };
            Trajects = tmplist.ToArray();
            UpdateListView();
        }
        public static void RemoveItems(string item)
        {
            // items van trajecten verwijderen
            Trajects = Trajects.Where(o => o != item).ToArray();
            UpdateListView();
        }
        public static void ModifyItems(string item)
        {   
            // namen van trajecten wijzigen met wat er in het tekstvak is ingetypt
            string text = input.Text.ToString();

            if (string.IsNullOrEmpty(text))
            {
                Toast.MakeText(context, "Enter Something", ToastLength.Short).Show();
            }
            else
            {
                Trajects[Array.FindIndex(Trajects, m => m == item)] = text;
                input.Text = "";
            }
            UpdateListView();
        }
    }
}