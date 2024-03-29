﻿using Android.App;
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
        public ListViewAdapter(Context context, string[] items) : base(context, Resource.Layout.list_row, items)
        {
            this.context = context;
            list = items;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            if (convertView == null)
            {
                LayoutInflater lf = (LayoutInflater)context.GetSystemService(Activity.LayoutInflaterService);
                convertView = lf.Inflate(Resource.Layout.list_row, null);

                TextView name = convertView.FindViewById<TextView>(Resource.Id.traject_name);
                name.Text = list[position];

                Button modify = convertView.FindViewById<Button>(Resource.Id.modify_traject);
                modify.Click += (sender, eventArgs) =>
                {
                    Traject_Editor.ModifyItems(list[position]);
                };

                Button delete = convertView.FindViewById<Button>(Resource.Id.delete_traject);
                delete.Click += (sender, eventArgs) =>
                {
                    Traject_Editor.RemoveItems(list[position]);  
                };
            }
            return convertView;
        }
    }
}