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
    class ListViewAdapterMeasure : ArrayAdapter<string>
    {
        string[] list;
        Context context;
        public ListViewAdapterMeasure(Context context, string[] items) : base(context, Resource.Layout.list_row_measure, items)
        {
            this.context = context;
            list = items;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                LayoutInflater lf = (LayoutInflater)context.GetSystemService(Activity.LayoutInflaterService);
                convertView = lf.Inflate(Resource.Layout.list_row_measure, null);

                TextView value = convertView.FindViewById<TextView>(Resource.Id.textView1);
                value.Text = list[position];

                TextView scalar = convertView.FindViewById<TextView>(Resource.Id.textView2);
                scalar.Text = list[position];

                TextView sensor = convertView.FindViewById<TextView>(Resource.Id.textView3);
                sensor.Text = list[position];

                TextView timestamp = convertView.FindViewById<TextView>(Resource.Id.textView4);
                timestamp.Text = list[position];

            }
            return base.GetView(position, convertView, parent);
        }

    }
}