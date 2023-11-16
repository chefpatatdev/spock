using Android.App;
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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_editor);

            Trajects = Intent.GetStringArrayExtra("arrayID");

            Button btn = FindViewById<Button>(Resource.Id.add_traject);

            btn.Click += Add_Click;
            // Create your application here
            InitializeListView();

        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(TrajectDriving));
            intent.PutExtra("trajectID", Trajects);
            StartActivity(intent);
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
            ListView list = (ListView)FindViewById<ListView>(Resource.Id.listview_edit);
            var adapter = new ListViewAdapter(this, Trajects);
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
        public static void RemoveItems(string item)
        {
            Trajects = Trajects.Where(o => o != item).ToArray();
            //InitializeListView();
        }
    }
}