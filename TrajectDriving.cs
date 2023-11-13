﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.InputMethodServices.Keyboard;

namespace SpockApp.Resources.mipmap_xhdpi
{
    [Activity(Label = "TrajectDriving")]
    public class TrajectDriving : Activity
    {
        string traject_index;
        int number_picker_value;
        string[,] traject_1 = new string[2,50];
        string[,] traject_2 = new string[2, 50];
        string[,] traject_3 = new string[2, 50];
        int array_index = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_driving);

            // Create your application here
            Button upButton = FindViewById<Button>(Resource.Id.up_button);
            upButton.Touch += UpButton_Touch;

            Button downButton = FindViewById<Button>(Resource.Id.down_button);
            downButton.Touch += DownButton_Touch;

            Button rightButton = FindViewById<Button>(Resource.Id.right_button);
            rightButton.Touch += RightButton_Touch;

            Button leftButton = FindViewById<Button>(Resource.Id.left_button);
            leftButton.Touch += LeftButton_Touch;

            Button RRButton = FindViewById<Button>(Resource.Id.rr_button);
            RRButton.Touch += RRButton_Touch;

            Button RLButton = FindViewById<Button>(Resource.Id.rl_button);
            RLButton.Touch += RLButton_Touch;

            Button centerButton = FindViewById<Button>(Resource.Id.center_button);
            centerButton.Touch += CenterButton_Touch;


            initializeStringMatrix(traject_1);
            initializeStringMatrix(traject_2);
            initializeStringMatrix(traject_3);
            initializeSpinner();
            initializePicker();
        }
        private void initializeStringMatrix(string[,] matrix)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 50 ; j++)
                {
                    matrix[i, j] = "";
                }
            }
        }
        private void initializePicker()
        {
            NumberPicker picker = FindViewById<NumberPicker>(Resource.Id.numberPicker);
            picker.MinValue = 0;
            picker.MaxValue = 10;
            picker.ValueChanged += numberPicker;
            
        }

        private void initializeSpinner()
        {
            //Maken van drop down menu om traject te slecteren
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            //custom stings in de drop down menu zetten
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.traject_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }
        private void numberPicker(object sender, System.EventArgs e)
        {
            NumberPicker picker = (NumberPicker)sender;
            number_picker_value = picker.Value;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
            traject_index = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //pop up dat zegt welk traject gekozen is
            string toast = string.Format("The traject is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            array_index = 0;
        }

        private void TrajectUpdater(string button)
        {
            switch (traject_index) {
                case "Traject 1":
                    traject_1[0, array_index] = button;
                    traject_1[1, array_index] = number_picker_value.ToString();
                    array_index++;
                    break;
                case "Traject 2":
                    traject_2[0, array_index] = button;
                    traject_2[1, array_index] = number_picker_value.ToString();
                    array_index++;
                    break;
                case "Traject 3":
                    traject_3[0, array_index] = button;
                    traject_3[1, array_index] = number_picker_value.ToString();
                    array_index++;
                    break;
                default:
                    array_index = 0;
                    break;
            }
        }
        private void UpButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    TrajectUpdater("U");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    //Console.WriteLine("up button released");
                    break;
                default:
                    TrajectUpdater("U");
                    break;
            }
        }

        private void DownButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("D");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("D");
                    break;
            }
        }

        private void RightButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("R");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("R");
                    break;
            }
        }

        private void LeftButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("L");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("L");
                    break;
            }
        }

        private void RRButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("RR");

                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("RR");
                    break;
            }
        }

        private void RLButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("RL");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("RL");
                    break;
            }
        }

        private void CenterButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_pressed);
                    TrajectUpdater("Stop");
                    array_index  = 0;
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    TrajectUpdater("Stop");
                    array_index = 0;
                    break;
            }
        }

    }
}