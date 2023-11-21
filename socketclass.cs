using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SpockApp
{

    static class SocketClass
    {
        private static Socket Connection;
        public static String host;
        public static int port;

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

            byte[] requestBytes = Encoding.ASCII.GetBytes(message);
            SocketClass.Connection.Send(requestBytes, 0, requestBytes.Length, SocketFlags.None);
            byte[] responseBytes = new byte[256];
            int bytesReceived = Connection.Receive(responseBytes);
            string response = Encoding.ASCII.GetString(responseBytes, 0, bytesReceived);
            return response;
        }
    }
}