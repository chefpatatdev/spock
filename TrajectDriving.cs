using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio.Channels;
using SpockApp.src;
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
        string traject_name;
        int Number_picker_value { get; set; }
        string[,] traject = new string[2, 50];
        string Command { get; set; } = "";
        int Array_index { get; set; } = 0;
        string[] ta = { "Traject 1", "Traject 2", "Traject 3", "Test" };

        ScrollView scroll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_driving);
            if(Intent.GetStringArrayExtra("trajectID") != null)
            {
                ta = Intent.GetStringArrayExtra("trajectID");
            }
            InitializeStringMatrix(traject);
            InitializeSpinner(ta);
            InitializePicker();
            InitializeButtons();
            scroll = FindViewById<ScrollView>(Resource.Id.scrollView);

        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(HomeScreen));
            StartActivity(intent);
        }

        //initalizers
        private void InitializeButtons()
        {
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

            Button removeButton = FindViewById<Button>(Resource.Id.remove);
            removeButton.Touch += RemoveButton_Touch;

            Button addButton = FindViewById<Button>(Resource.Id.add);
            addButton.Touch += AddButton_Touch;

            Button editButton = FindViewById<Button>(Resource.Id.edit_traject);
            editButton.Click += EditTraject_Click;

            Button driveButton = FindViewById<Button>(Resource.Id.drive);
            driveButton.Click += DriveTraject_Click;

        }
        private void InitializeStringMatrix(string[,] matrix)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    matrix[i, j] = "";
                }
            }
        }
        private void InitializePicker()
        {
            NumberPicker picker = FindViewById<NumberPicker>(Resource.Id.numberPicker);
            picker.MinValue = 0;
            picker.MaxValue = 10;
            picker.ValueChanged += NumberPicker;

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

        //Update Traject
        private void AddCommand()
        {
            traject[0, Array_index] = Command;
            traject[1, Array_index] = Number_picker_value.ToString();
            Array_index++;
            UpdateTrajectText();

        }
        private void RemoveCommand()
        {
            if (Array_index != 0)
            {
                Array_index--;
                traject[0, Array_index] = "";
                traject[1, Array_index] = "";
            }
            UpdateTrajectText();

        }
        private void UpdateTrajectText()
        {
            string toast = "";
            for (int i = 0; i < Array_index; i++)
            {
                toast += traject[1, i] + "s naar " + traject[0, i] + "\n";
            }
            TextView text = FindViewById<TextView>(Resource.Id.traject_text);
            text.Text = toast;
            scroll.FullScroll(FocusSearchDirection.Down);
        }
        private void UpdateTrajectMatrix()
        {

        }

        //Event Handlers
        private void NumberPicker(object sender, System.EventArgs e)
        {
            NumberPicker picker = (NumberPicker)sender;
            Number_picker_value = picker.Value;
        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //Traject opslaan om door te sturen naar database
            traject_name = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //pop up dat zegt welk traject gekozen is
            string toast = string.Format("The traject is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();

            Array_index = 0;
        }

            //Button Handlers
        private void UpButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    this.Command = "U";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    break;
                default:
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
                    this.Command = "D";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
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
                    this.Command = "R";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
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
                    this.Command = "L";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
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
                    this.Command = "RR";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
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
                    this.Command = "RL";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
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
                    this.Command = "Wait";
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    break;
                default:
                    break;
            }
        }
        private void RemoveButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    RemoveCommand();
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    break;
                default:
                    break;
            }
        }
        private void AddButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    AddCommand();
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    break;
                default:
                    break;
            }
        }
        private void DriveTraject_Click(object sender, System.EventArgs e)
        {
            string toast = "Traject " + traject_name + " is running";
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        private void EditTraject_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Traject_Editor));
            intent.PutExtra("arrayID", ta);
            StartActivity(intent);
        }
    }
}