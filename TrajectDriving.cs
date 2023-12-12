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
using static Android.Graphics.ColorSpace;
using static Android.InputMethodServices.Keyboard;

namespace SpockApp.Resources.mipmap_xhdpi
{
    [Activity(Label = "TrajectDriving")]
    public class TrajectDriving : Activity
    {
        string traject_name;
        int Number_picker_value { get; set; } = 1;
        string[,] traject = new string[2, 50];
        string Command { get; set; } = "";
        int Array_index { get; set; } = 0;
        string[] t_names = { "Traject 1", "Traject 2", "Traject 3", "Test" };
        bool Switch = false;

        ScrollView scroll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.traject_driving);
            if (Intent.GetStringArrayExtra("trajectID") != null)
            {
                t_names = Intent.GetStringArrayExtra("trajectID");
            }
            InitializeStringMatrix(traject);
            InitializeSpinner(t_names);
            InitializePicker();
            InitializeButtons();

            RecieveTrajectNames();
            RecieveSavedTraject();



            scroll = FindViewById<ScrollView>(Resource.Id.scrollView);

            ImageView socketIndicator = FindViewById<ImageView>(Resource.Id.socket_indicator);
            SocketClass.socketIndicator_update = socketIndicator;

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

            Switch ptr = FindViewById<Switch>(Resource.Id.switch1);
            ptr.CheckedChange += (sender, e) =>
            {
                Switch = e.IsChecked;
            };

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
            picker.MinValue = 1;
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
        private void RecieveSavedTraject()
        {
            string saved_traject = SocketClass.Sendmessage("r_traject," + traject_name);
            string[] s_traject = saved_traject.Split(",");

            for (int i = 0; i < traject.GetLength(0); i++)
            {
                for (int j = 0; j < traject.GetLength(1); j++)
                {
                    if (s_traject[j] == "Stop") break;
                    traject[i, j] = s_traject[j];
                }
            }
           
        }
        private void RecieveTrajectNames()
        {
            string traject_names = SocketClass.Sendmessage("r_names,");
            t_names = traject_names.Split(",");

        }

        //Update Traject
        private void AddCommand()
        {
            if (Command == "")
            {
                Toast.MakeText(this, "Pick a direction", ToastLength.Short).Show();
            }
            else
            {

                for (int i = traject.GetLength(1) - 1 ; i > Array_index; i--)
                {
                    traject[0, i] = traject[0, i - 1];
                    traject[1, i] = traject[1, i - 1];
                }
                traject[0, Array_index] = Command;
                traject[1, Array_index] = (Number_picker_value -1).ToString();
                UpdateTrajectText();
                if(traject[0, Array_index+1] == "") Array_index++;

            }

        }
        private void RemoveCommand()
        {
            if (traject[0, Array_index+1] != "")
            {
                for (int i = Array_index; i < traject.GetLength(1)-1; i++)
                {
                    traject[0, i] = traject[0, i + 1];
                    traject[1, i] = traject[1, i + 1];
                }
                traject[0, traject.GetLength(1) - 1] = "";
                traject[1, traject.GetLength(1) - 1] = "";
            }
            else if (Array_index >= 0)
            {
               Array_index = Math.Max(Array_index - 1, 0);
               traject[0, Array_index] = "";
               traject[1, Array_index] = "";
            }

            Array_index = Math.Max(Array_index - 1, 0);
            UpdateTrajectText();
            if (traject[0, Array_index + 1] == "" && Array_index != 0) Array_index++;
        }
        private void UpdateTrajectText()
        {
            string toast = "";
            if (!(Array_index <= 0 && traject[0, 0] == ""))
            {
                for (int i = 0; i < traject.GetLength(1); i++)
                {
                    if (i == Array_index) toast += "→";
                    toast += "  " + (int.Parse(traject[1, i]) + 1).ToString() + "s " + traject[0, i] + "\n";
                    if (traject[0, i + 1] == "") break;
                }
            }
            TextView text = FindViewById<TextView>(Resource.Id.traject_text);
            text.Text = toast;
            if (traject[0, Array_index + 1] == "") scroll.FullScroll(FocusSearchDirection.Down);
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
        private void DriveTraject(string trajectname)
        {
            SocketClass.Sendmessage("d_traject," + trajectname);

        }
        private void SendTraject(string trajectname)
        {
            string send = "s_traject," + trajectname + ",";
            for(int i = 0; i < traject.GetLength(1); i++)
            {
                send += traject[0,i] + traject[1, i] + ",";
                if (traject[0, i] == "") break;

            }
            SocketClass.Sendmessage(send);
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
                    if (!Switch)
                    {
                        RemoveCommand();
                    }
                    else
                    {
                        if (Array_index > 0)
                        {
                            if (traject[0 , Array_index+1] == "") Array_index--;
                            Array_index--;
                            UpdateTrajectText();
                        }
                        else
                        {
                            Toast.MakeText(this, "Can't move pointer down anymore", ToastLength.Short).Show();
                        }
                    }
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
                    if (!Switch)
                    {
                        AddCommand();
                    }
                    else
                    {
                        if (traject[0, Array_index + 1] != "")
                        {
                            Array_index++;
                            UpdateTrajectText();
                            if (traject[0, Array_index + 1] == "") Array_index++;
                        }
                        else
                        {
                            Toast.MakeText(this, "Can't move pointer up anymore", ToastLength.Short).Show();
                        }
                    }
                    
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
            DriveTraject(traject_name);
            string toast = "Traject " + traject_name + " is running";
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        private void EditTraject_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Traject_Editor));
            intent.PutExtra("arrayID", t_names);
            StartActivity(intent);
        }
    }
}