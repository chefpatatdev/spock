using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Telecom;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using EncryptionDecryptionUsingSymmetricKey;
using Google.Android.Material.Snackbar;
using SpockApp.Resources.mipmap_xhdpi;
using SpockApp.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


namespace SpockApp
{


    [Activity(Label = "Spock", MainLauncher = true)]
    public class LoginScreen : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);
            // Create your application here
            Button LoginButton = FindViewById<Button>(Resource.Id.login_button);
            LoginButton.Touch += LoginBtn_Touch;
        }
        private void LoginBtn_Touch(object sender, View.TouchEventArgs e)
        {
            Button btn = (Button)sender;
            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    break;
                case MotionEventActions.Up:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_unpressed);
                    LoginHandler(sender);
                    break;
                default:
                    btn.SetBackgroundResource(Resource.Drawable.live_upbutton_pressed);
                    break;
            }
        }
        private void LoginHandler(object sender)
        {
            /*
            Intent intent = new Intent(this, typeof(HomeScreen));
            StartActivity(intent);*/
            // Code to be executed when the button is clicked

            EditText usernameField = FindViewById<EditText>(Resource.Id.user_name_field);
            string usernameRaw = usernameField.Text;

            EditText passwordField = FindViewById<EditText>(Resource.Id.password_field);
            string passwordRaw = passwordField.Text;

            //hide keyboard to see the popup incase of wrong password and/or username
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(passwordField.WindowToken, 0);

            
            EditText ipField = FindViewById<EditText>(Resource.Id.ip_field);
            string IPAddr = ipField.Text;

            EditText portField = FindViewById<EditText>(Resource.Id.port_field);
            int port = Int32.Parse(portField.Text);

            bool connectionStatus = false;
            
            if (!SocketClass.IsConnected()){
                Console.WriteLine("Not connected, Trying to connect..");
                connectionStatus = SocketClass.Connect(IPAddr, port);
                if (!connectionStatus)
                {  
                    //popup at the bottom of the screen
                    View view = (View)sender;
                    Snackbar.Make(view, "Could not connect to that IP and port!", Snackbar.LengthLong)
                        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                }
                else
                {

                    Console.WriteLine("Connected!");


                    string allowdEntrance = SocketClass.Sendmessage("r_login," + usernameRaw + "," + passwordRaw);
                    //SocketClass.Pinging();

                    if (allowdEntrance == "ok")
                    {
                        //change screen to homescreen
                        Intent intent = new Intent(this, typeof(HomeScreen));
                        intent.PutExtra("screen", "login");
                        StartActivity(intent);

                    }
                    else
                    {
                        //popup at the bottom of the screen
                        View view = (View)sender;
                        Snackbar.Make(view, "Wrong password and/or username!", Snackbar.LengthLong)
                            .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                        SocketClass.Disconnect();
                    }
                }

            }

        }
    }
}