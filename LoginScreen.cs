using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
            LoginButton.Click += LoginAttempt_Click;


        }

        private void LoginAttempt_Click(object sender, System.EventArgs e)
        {

            // Code to be executed when the button is clicked

            EditText usernameField = FindViewById<EditText>(Resource.Id.user_name_field);
            string usernameRaw = usernameField.Text;

            EditText passwordField = FindViewById<EditText>(Resource.Id.password_field);
            string passwordRaw = passwordField.Text;

            //hide keyboard to see the popup incase of wrong password and/or username
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(passwordField.WindowToken, 0);

            
            EditText ipField = FindViewById<EditText>(Resource.Id.ip_field);
            EditText portField = FindViewById<EditText>(Resource.Id.port_field);
            
            if (!SocketClass.IsConnected()){
                SocketClass.Connect(ipField.Text, Int32.Parse(portField.Text));
            }

            String allowdEntrance= SocketClass.Sendmessage("r_login," + usernameRaw + "," + passwordRaw);
            SocketClass.Pinging();

            if (allowdEntrance == "ok")
            {
                //swicth to homescreen
                Intent intent = new Intent(this, typeof(HomeScreen));
                StartActivity(intent);

            }
            else
            {
                //popup at the bottom of the screen
                View view = (View)sender;
                Snackbar.Make(view, "Wrong password and/or username!", Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            }
            


        }
    }
}