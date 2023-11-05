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
    [Activity(Label = "LiveDriving")]
    public class LiveDriving : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.live_driving);

            // Create your application here
            Button upButton = FindViewById<Button>(Resource.Id.up_button);
            upButton.Touch += UpButton_Touch;

            Button downButton = FindViewById<Button>(Resource.Id.down_button);
            downButton.Touch += DownButton_Touch;

            Button rightButton = FindViewById<Button>(Resource.Id.right_button);
            rightButton.Touch += RightButton_Touch;

            Button leftButton = FindViewById<Button>(Resource.Id.left_button);
            leftButton.Touch += LeftButton_Touch;

        }

        private void UpButton_Touch(object sender, View.TouchEventArgs e)
        { 
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move://treated same as down action
                    Console.WriteLine("up button pressed");
                    break;
                case MotionEventActions.Up:
                    Console.WriteLine("up button released");
                    break;
                default:
                    Console.WriteLine("default up");
                    break;
            }
        }

        private void DownButton_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move:
                    Console.WriteLine("down button pressed");
                    break;
                case MotionEventActions.Up:
                    Console.WriteLine("down button released");
                    break;
                default:
                    Console.WriteLine("default down");
                    break;
            }
        }

        private void RightButton_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move: 
                    Console.WriteLine("right button pressed");
                    break;
                case MotionEventActions.Up:
                    Console.WriteLine("right button released");
                    break;
                default:
                    Console.WriteLine("default right");
                    break;
            }
        }

        private void LeftButton_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move:
                    Console.WriteLine("left button pressed");
                    break;
                case MotionEventActions.Up:
                    Console.WriteLine("left button released");
                    break;
                default:
                    Console.WriteLine("default left");
                    break;
            }
        }

    }
}