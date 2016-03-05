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
        private String IP;
        private bool isListening = false;

        private NetworkHandler(){
            gameEngine = GameEngine.GetInstance();
        }

        public static NetworkHandler getInstance(){
            return netHandler;
        }

        public void Send(Object mesg)
        {
            String message = (String)mesg;
            try
            {
                sender = new TcpClient();
                sender.Connect(IP, 6000);
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
                if (message.Equals("JOIN#"))
                {
                    Game1.currentState = Game1.GameState.CannotJoin;
                    Game1.message = "Network Error!";
                }
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
                while (Game1.currentState==Game1.GameState.playing || Game1.currentState==Game1.GameState.joining)
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
                if (error && (Game1.currentState == Game1.GameState.playing || Game1.currentState == Game1.GameState.joining))
                    Recieve();
            }
            Console.WriteLine("Thread Finished");
        }

        public void StartListerner()
        {
            Console.WriteLine("Listener Thread Started");
            if (!isListening)
            {
                try
                {
                    reciever = new TcpListener(IPAddress.Any, 7000);
                    reciever.Start();
                    isListening = true;
                    Recieve();
                    reciever.Stop();
                    isListening = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    isListening = false;
                }
            }
            Console.WriteLine("Listener Thread Ended");
        }

        public void setIP(String IPaddress)
        {
            if (IPaddress.Length > 0 && IPaddress[IPaddress.Length - 1].Equals("_"))
                IPaddress = IPaddress.Substring(0, IPaddress.Length - 1);
            Console.WriteLine(IPaddress);
            this.IP = IPaddress;
        }

    }
}
