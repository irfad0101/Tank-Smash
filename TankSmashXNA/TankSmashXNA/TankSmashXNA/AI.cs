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
        private bool[,] discovered = new bool[10,10];
        private Node[,] graph = new Node[10, 10];
        private int startX, startY;

        private AI()
        {
            random = new Random();
            commands = new String[] { "UP#", "RIGHT#", "DOWN#", "LEFT#", "SHOOT#" };
            netHandler = NetworkHandler.getInstance();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Node node = new Node();
                    node.currentX = i;
                    node.currentY = j;
                    graph[i, j] = node;
                }
            }
        }

        public static AI getInstance()
        {
            return ai;
        }

        public void setParameters(Tank mytank,GameEntity[,] map)
        {
            this.mytank = mytank;
            this.map = map;
            this.startX = mytank.X;
            this.startY = mytank.Y;
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

        public void SearchFor(int searchType)
        {
            // search type 0 for coin packs and 1 for health packs
            List<Node> queue = new List<Node>();
            Node node = graph[mytank.X,mytank.Y];
            node.parentX = -1;
            node.parentY = -1;
            queue.Add(node);
            discovered[mytank.X, mytank.Y] = true;
            bool found = false;
            while (queue.Count!=0)
            {
                node = queue[0];
                queue.RemoveAt(0);
                int direction = mytank.Direction;
                for (int i = 0; i < 4; i++)
                {
                    Node child = gotoChild(direction + i);
                    if (child != null && !discovered[child.currentX,child.currentY])
                    {
                        if (map[child.currentX, child.currentY] == null)
                        {
                            child.parentX = node.currentX;
                            child.parentY = node.currentY;
                            queue.Add(child);
                            discovered[mytank.X, mytank.Y] = true;
                        }
                        else if (map[child.currentX, child.currentY].GetType() == typeof(CoinPack) && searchType==0)
                        {
                            found = true;
                            break;
                        }
                        else if (map[child.currentX, child.currentY].GetType() == typeof(LifePack) && searchType == 1)
                        {
                            found = true;
                            break;
                        }
                    }
                }

            }
        }

        private Node gotoChild(int direction)
        {
            if (direction > 3)
            {
                direction = 4 - direction;
            }
            int nextX = mytank.X;
            int nextY = mytank.Y;
            switch (direction)
            {
                case 0:
                    nextY--; break;
                case 1:
                    nextX++; break;
                case 2:
                    nextY++; break;
                case 3:
                    nextX--; break;
                default:
                    return null;

            }
            if (nextX > 0 && nextX < 10 && nextY > 0 && nextY < 9)
            {
                return graph[nextX, nextY];
            }
            return null;
        }

        //private void back

    }

    class Node
    {
        public int currentX, currentY;
        public int parentX, parentY;
    }
}
