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

            Button RRButton = FindViewById<Button>(Resource.Id.rr_button);
            RRButton.Touch += RRButton_Touch;

            Button RLButton = FindViewById<Button>(Resource.Id.rl_button);
            RLButton.Touch += RLButton_Touch;

            ImageView socketIndicator = FindViewById<ImageView>(Resource.Id.socket_indicator);
            SocketClass.socketIndicator_update = socketIndicator;
        }

        private void UpButton_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;

            
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    SocketClass.Sendmessage("d_live,FW");
                    Console.WriteLine("up button pressed");
                    
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
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
                    SocketClass.Sendmessage("d_live,BW");
                    Console.WriteLine("down button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
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
                    SocketClass.Sendmessage("d_live,RD");
                    Console.WriteLine("right button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
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
                    SocketClass.Sendmessage("d_live,LD");
                    Console.WriteLine("left button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
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
                    SocketClass.Sendmessage("d_live,RR");
                    Console.WriteLine("RR button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
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
                    SocketClass.Sendmessage("d_live,RL");
                    Console.WriteLine("RL button pressed");
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_button_unpressed);
                    SocketClass.Sendmessage("d_live,ST");
                    Console.WriteLine("RL button released");
                    break;
                default:
                    Console.WriteLine("default RL");
                    break;
            }
        }

    }
}