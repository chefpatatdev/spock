using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
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
        string[] Trajects { get; set; } = { "Traject 1", "Traject 2", "Traject 3", "Test" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_editor);

            Button btn = FindViewById<Button>(Resource.Id.add_traject);

            btn.Click += Add_Click;
            // Create your application here
            InitializeListView();


        }

        private void Add_Click(object sender, EventArgs e)
        {
            EditText input = FindViewById<EditText>(Resource.Id.traject_input);
            string text = input.Text.ToString();
            if (string.IsNullOrEmpty(text))
            {
                Toast.MakeText(this, "Enter Something", ToastLength.Short).Show();
            }
            AddItems(text);
            input.Text = "";
        }

        private void InitializeListView()
        {
            //Maken van drop down menu om traject te slecteren
            ListView list = FindViewById<ListView>(Resource.Id.listview_edit);
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, Trajects);
            list.Adapter = adapter;
            list.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(Listview_Click);

        }

        private void Listview_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            var list = (ListView)sender;
            Toast.MakeText(this, list.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show();
        }

        private void AddItems(string item)
        {
            var list = new List<string>();
            list = Trajects.ToList();
            list.Add(item);
            Trajects = list.ToArray();
            InitializeListView();
        }
        private void RemoveItems(string item)
        {
            Trajects = Trajects.Where(o => o != item).ToArray();
            InitializeListView();
        }
    }
}