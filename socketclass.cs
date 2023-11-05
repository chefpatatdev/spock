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
    class socketclass
    {
        private Socket socketint;

        public socketclass()
        {
            socketint = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connectsocket(string host, int port)
        {
            socketint.Connect(host, port);
        }

        public string sendmessage(string message = null)
        {
            if (socketint == null || !socketint.Connected)
            {
                return "Socket is not connected.";
            }

            byte[] requestBytes = Encoding.ASCII.GetBytes(message);
            socketint.Send(requestBytes, 0, requestBytes.Length, SocketFlags.None);

            byte[] responseBytes = new byte[256];
            int bytesReceived = socketint.Receive(responseBytes);
            string response = Encoding.ASCII.GetString(responseBytes, 0, bytesReceived);

            return response;
        }
    }
}