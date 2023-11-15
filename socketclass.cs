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

    static class socket
    {
        private static Socket socketObj;
        public static String host;
        public static int port;

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
        public static string sendmessage(string message = null)
        {
            if (socketObj == null || !socketObj.Connected)
            {
                return "Socket is not connected.";
            }

            byte[] requestBytes = Encoding.ASCII.GetBytes(message);
            socket.socketObj.Send(requestBytes, 0, requestBytes.Length, SocketFlags.None);
            byte[] responseBytes = new byte[256];
            int bytesReceived = socketObj.Receive(responseBytes);
            string response = Encoding.ASCII.GetString(responseBytes, 0, bytesReceived);
            return response;
        }
    }
}