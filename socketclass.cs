using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony.Euicc;
using Android.Views;
using Android.Widget;
using EncryptionDecryptionUsingSymmetricKey;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpockApp
{

    static class SocketClass
    {
        private static Socket Connection;
        public static String host;
        public static int port;
        public static bool alreadyPinging;
        public static ImageView socketIndicator_update;

        public static bool Connect(string host, int port)
        {

                SocketClass.host = host;
                SocketClass.port = port;
                SocketClass.Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            
            // Set a timeout for the Connect operation (in milliseconds)
            int timeout = 2000; // 2 seconds
            SocketClass.Connection.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
            SocketClass.Connection.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);

            try
            {
                SocketClass.Connection.Connect(host, port); //we zitten in 1 thread en dit houd alles tegen als hij geen connectie vind -> app reageerd niet meer en crashed 
                SocketClass.Pinging();
                return true;
            }catch (SocketException e)
            {
                return false;
            }


        }

        public static void Disconnect()
        {
            SocketClass.Connection.Close();
        }
        public static bool IsConnected()
        {
            if (Connection == null)
            {
                return false;
            }
            else
            {
                return Connection.Connected;
            }
        }
        public static string Sendmessage(string message = null)
        {
            if (!IsConnected())
            {
                return null;//"Socket is not connected.";
            }

            byte[] requestBytes = Encoding.ASCII.GetBytes(AesOperation.EncryptString(message));
            SocketClass.Connection.Send(requestBytes, 0, requestBytes.Length, SocketFlags.None);
            byte[] responseBytes = new byte[256];
            int bytesReceived = 0;
            string response = "";
            try
            {
                bytesReceived = SocketClass.Connection.Receive(responseBytes);
                response = AesOperation.DecryptString(Encoding.ASCII.GetString(responseBytes, 0, bytesReceived));
            }
            catch(SocketException e)
            {
                response = null;
            }
            
            
            return response;
        }
        public static async void Pinging()
        {
            if (!alreadyPinging)
            {
                alreadyPinging = true;
                while (alreadyPinging)
                {
                    try
                    {
                        Sendmessage("ping");

                        if(socketIndicator_update != null)
                        {
                            socketIndicator_update.SetBackgroundResource(Resource.Drawable.online_indicator);

                        }
                        //Console.WriteLine("ok");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("disconnect");
                        if (socketIndicator_update != null)
                        {
                            socketIndicator_update.SetBackgroundResource(Resource.Drawable.offline_indicator);
                        }
                        alreadyPinging = false;
                        Connection.Close();
                        break;
                    }
                    await Task.Delay(1000);
                }
            }
        }
    }
}

