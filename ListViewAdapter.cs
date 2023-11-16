using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpockApp
{
    class ListViewAdapter : ArrayAdapter<string> 
    {
        string[] list;
        Context context;
        int Position;
        public ListViewAdapter(Context context, string[] items) : base(context, Resource.Layout.list_row, items)
        {
            this.context = context;
            list = items;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                LayoutInflater lf = (LayoutInflater)context.GetSystemService(Activity.LayoutInflaterService);
                convertView = lf.Inflate(Resource.Layout.list_row, null);

                TextView name = convertView.FindViewById<TextView>(Resource.Id.traject_name);
                name.Text = list[position];

                Button modify = convertView.FindViewById<Button>(Resource.Id.modify_traject);
                modify.Click += Modify_Click;

                Button delete = convertView.FindViewById<Button>(Resource.Id.delete_traject);
                delete.Click += Delete_Click;
                Position = position;
            }
            return convertView;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Traject_Editor.RemoveItems(list.ElementAt(Position));//positie klopt niet
        }

        private void Modify_Click(object sender, EventArgs e)
        {
        }
    }
}