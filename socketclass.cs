using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EncryptionDecryptionUsingSymmetricKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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



        public static void Connect(string host, int port)
        {
            SocketClass.host = host;
            SocketClass.port = port;
            SocketClass.Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketClass.Connection.Connect(host, port);
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
            if (Connection == null || !Connection.Connected)
            {
                return "Socket is not connected.";
            }

            byte[] requestBytes = Encoding.ASCII.GetBytes(AesOperation.EncryptString(message));
            socket.socketObj.Send(requestBytes, 0, requestBytes.Length, SocketFlags.None);
            byte[] responseBytes = new byte[256];
            int bytesReceived = socketObj.Receive(responseBytes);
            string response = AesOperation.DecryptString(Encoding.ASCII.GetString(responseBytes, 0, bytesReceived));
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
                        Console.WriteLine("ok");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine("disconnect");
                        alreadyPinging = false;
                        socketObj.Close();
                        break;
                    }
                    await Task.Delay(1000);
                }
            }
        }
    }
}

