using System;
using System.Threading;

namespace TankSmashXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            NetworkHandler netHandler = NetworkHandler.getInstance();
            Thread thread = new Thread(new ThreadStart(netHandler.Recieve));
            thread.Start();
            netHandler.Send("JOIN#");
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

