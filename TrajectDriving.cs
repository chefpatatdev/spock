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

namespace SpockApp.Resources.mipmap_xhdpi
{
    [Activity(Label = "TrajectDriving")]
    public class TrajectDriving : Activity
    {
        string traject_index;
        int number_picker_value;
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



            initializeSpinner();
            initializePicker();
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
        }

        private void UpButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    Console.WriteLine("up button pressed");
                    
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    Console.WriteLine("up button released");
                    break;
                default:
                    Console.WriteLine("default up");
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
                    Console.WriteLine("down button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    Console.WriteLine("down button released");
                    break;
                default:
                    Console.WriteLine("default down");
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
                    Console.WriteLine("right button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    Console.WriteLine("right button released");
                    break;
                default:
                    Console.WriteLine("default right");
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
                    Console.WriteLine("left button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    Console.WriteLine("left button released");
                    break;
                default:
                    Console.WriteLine("default left");
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
                    Console.WriteLine("RR button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    Console.WriteLine("RR button released");
                    break;
                default:
                    Console.WriteLine("default RR");
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
                    Console.WriteLine("RL button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    Console.WriteLine("RL button released");
                    break;
                default:
                    Console.WriteLine("default RL");
                    break;
            }
        }

    }
}