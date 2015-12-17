using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TankSmashXNA.Entity;

namespace TankSmashXNA
{
    class AI
    {

        private static AI ai = new AI();
        Random random;
        private GameEntity[,] map;
        private String[] commands;
        Tank mytank;
        private NetworkHandler netHandler;

        private AI()
        {
            random = new Random();
            commands = new String[] { "UP#", "RIGHT#", "DOWN#", "LEFT#", "SHOOT#" };
            netHandler = NetworkHandler.getInstance();
        }

        public static AI getInstance()
        {
            return ai;
        }

        public void setParameters(Tank mytank,GameEntity[,] map)
        {
            this.mytank = mytank;
            this.map = map;
        }

        public void getNextMove()
        {
            while (true)
            {
                int next = random.Next() % 5;
                bool send = false;
                switch (next)
                {
                    case 0:
                        if (mytank.Y - 1 < 0 || map[mytank.X, mytank.Y - 1] != null)
                            send = false;
                        else
                            send = true;
                        break;
                    case 1:
                        if (mytank.X + 1 > 9 || map[mytank.X + 1, mytank.Y] != null)
                            send = false;
                        else
                            send = true;
                        break;
                    case 2:
                        if (mytank.Y + 1 > 9 || map[mytank.X, mytank.Y + 1] != null)
                            send = false;
                        else
                            send = true;
                        break;
                    case 3:
                        if (mytank.X - 1 < 0 || map[mytank.X - 1, mytank.Y] != null)
                            send = false;
                        else
                            send = true;
                        break;
                    case 4:
                        send = true;
                        break;
                }
                if (send)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(netHandler.Send));
                    thread.Start(commands[next]);
                    Thread.Sleep(1100);
                }
            }
        }

    }
}
