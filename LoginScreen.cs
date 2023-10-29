using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using EncryptionDecryptionUsingSymmetricKey;
using Google.Android.Material.Snackbar;
using SpockApp.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SpockApp.Resources
{
    [Activity(Label = "LoginScreen", MainLauncher = true)]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);
            // Create your application here
            Button myButton = FindViewById<Button>(Resource.Id.login_button);
            myButton.Click += LoginAttempt_Click;


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

            Console.WriteLine(usernameRaw);
            Console.WriteLine(passwordRaw);

            bool allowedEntrance = false;

            //temporary for development reasons
            if(usernameRaw.Equals("admin") && passwordRaw.Equals("admin"))
            {
                allowedEntrance = true;
            }


            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            var encryptedUsername = AesOperation.EncryptString(key, usernameRaw);
            var encryptedPassword = AesOperation.EncryptString(key, passwordRaw);
            Console.WriteLine(encryptedUsername);
            Console.WriteLine(encryptedPassword);
            //encrypt password before sending message thru socket

            //allowedEntrance = response from socket

            if (allowedEntrance)
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