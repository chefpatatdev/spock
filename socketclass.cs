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

    static class socket
    {
        private static Socket socketObj;
        public static String host;
        public static int port;
        public static bool alreadyPinging;



        public static void Connect(string host, int port)
        {
            socket.host = host;
            socket.port = port;
            socket.socketObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.socketObj.Connect(host, port);
        }

        public static void Disconnect()
        {
            socket.socketObj.Close();
        }
        public static bool IsConnected()
        {
            if (socketObj == null)
            {
                return false;
            }
            else
            {
                return socketObj.Connected;
            }
        }
        public static string Sendmessage(string message = null)
        {
            if (socketObj == null || !socketObj.Connected)
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
                reconnecting();
            }
        }
        public static async void reconnecting()
        {

            while (!IsConnected())
            {
                try
                {
                    Console.WriteLine("trying to connect");
                    socket.socketObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.socketObj.Connect(host, port);
                }
                catch (Exception error)
                {
                    Console.WriteLine("failed");
                    break;
                }
                await Task.Delay(1000);
            }
            Pinging();
        }
    }
}

