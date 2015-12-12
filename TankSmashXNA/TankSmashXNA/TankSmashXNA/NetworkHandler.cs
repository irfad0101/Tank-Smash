using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TankSmashXNA
{
    class NetworkHandler
    {

        private static NetworkHandler netHandler = new NetworkHandler();
        private GameEngine gameEngine;
        private TcpClient sender;
        private NetworkStream sendStream;
        private BinaryWriter writer;
        private TcpListener reciever;
        private NetworkStream recieveStream;
        private BinaryReader reader;

        private NetworkHandler(){
            GameEngine.GetInstance();
        }

        public static NetworkHandler getInstance(){
            return netHandler;
        }

        public void Send(String message)
        {
            try
            {
                sender = new TcpClient();
                sender.Connect("127.0.0.1", 6000);
                if (sender.Connected)
                {
                    sendStream = sender.GetStream();
                    writer = new BinaryWriter(sendStream);
                    Byte[] msg = Encoding.ASCII.GetBytes(message);
                    writer.Write(msg);
                    Console.WriteLine("Request Sent:"+message);
                    writer.Close();
                    sendStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public void Recieve()
        {
            Socket socket = null;
            bool error = false;
            try
            {
                reciever = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                reciever.Start();
                while (true)
                {
                    socket = reciever.AcceptSocket();
                    if (socket.Connected)
                    {
                        recieveStream = new NetworkStream(socket);
                        List<Byte> serverMsg = new List<byte>();
                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = recieveStream.ReadByte();
                            serverMsg.Add((Byte)asw);
                        }
                        String message = Encoding.UTF8.GetString(serverMsg.ToArray());
                        Console.WriteLine(message);
                        Thread thread = new Thread(new ParameterizedThreadStart(gameEngine.decode));
                        thread.Start(message);
                        recieveStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (socket != null)
                {
                    if (socket.Connected)
                        socket.Close();
                }
                if (error)
                    Recieve();
            }
        }

    }
}
