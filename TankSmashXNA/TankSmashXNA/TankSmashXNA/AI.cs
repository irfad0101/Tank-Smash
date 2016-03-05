using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TankSmashXNA.Entity;

namespace TankSmashXNA
{
    class AI
    {

        private static AI ai = new AI();
        private GameEntity[,] map;
        private GameEntity[,] collectabilies;
        private String[] commands;
        Tank mytank;
        private NetworkHandler netHandler;
        private bool[,] discovered;
        private Node[,] graph = new Node[10, 10];
        private int startX, startY;
        private int nextX, nextY;

        private AI()
        {
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
                    graph[i, j].parentX = -1;
                    graph[i, j].parentY = -1;
                }
            }
        }

        public static AI getInstance()
        {
            return ai;
        }

        public void setParameters(Tank mytank,GameEntity[,] map, GameEntity[,] collectabilities)
        {
            this.mytank = mytank;
            this.map = map;
            this.startX = mytank.X;
            this.startY = mytank.Y;
            this.collectabilies = collectabilities;
        }

        public void getNextMove()
        {
            while (Game1.currentState==Game1.GameState.playing && mytank.Health>0)    // loop for thrad
            {
                nextX = -1;
                nextY = -1;
                bool commandSend = false;
                if (mytank.Health < 100)    // if health is not full search for life pack
                {
                    Console.WriteLine("Searching for life pack...");
                    SearchFor(typeof(LifePack));
                }
                int tankInRangeDir = GetTankInRangeDir();
                Console.WriteLine("Tank in range: " + tankInRangeDir);
                if (tankInRangeDir > 0)
                {
                    if (tankInRangeDir == mytank.Direction)
                    {
                        Thread thrd = new Thread(new ThreadStart(destroyTank));
                        thrd.Start();
                        commandSend = true;
                    }
                    else
                    {
                        Thread thrd = new Thread(new ParameterizedThreadStart(netHandler.Send));
                        thrd.Start(commands[tankInRangeDir]);
                        commandSend = true;
                    }
                }
                else
                {
                    Console.WriteLine("Searching for Coin pack...");
                    SearchFor(typeof(CoinPack));        // search for coin pack
                }
                if (!commandSend)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(netHandler.Send));
                    thread.Start(GetNextCommand());
                }
                Thread.Sleep(1200);
            }
        }

        public void SearchFor(Type searchType)
        {
            // use BFS search to find nearest coin packs or life packs
            List<Node> queue = new List<Node>();
            discovered = new bool[10, 10];
            resetGraph();
            Node node = graph[mytank.X,mytank.Y];
            queue.Add(node);
            discovered[mytank.X, mytank.Y] = true;
            bool found = false;
            while (queue.Count!=0)
            {
                node = queue.ElementAt(0);      // head of queue
                queue.RemoveAt(0);
                for (int i = 0; i < 4; i++)     // loop to visit child nodes
                {
                    Node child = gotoChild(node,i);
                    if (child != null && !discovered[child.currentX,child.currentY])  // process child if not already discovered and not null
                    {
                        if (map[child.currentX, child.currentY] == null)
                        {
                            child.parentX = node.currentX;
                            child.parentY = node.currentY;
                            queue.Add(child);
                            discovered[child.currentX, child.currentY] = true;
                            if (collectabilies[child.currentX, child.currentY] != null)
                            {
                                if (collectabilies[child.currentX, child.currentY].GetType() == searchType)     // found a coin pack or life pack
                                {
                                    Console.WriteLine("Collectable at (" + child.currentX + "," + child.currentY + ")");
                                    found = true;
                                    TraceBack(child);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (found) break;
            }
        }

        private Node gotoChild(Node current, int direction)
        {
            // given current node and direction of next child return the child node if it is valid
            int nextX = current.currentX;
            int nextY = current.currentY;
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
            if (nextX >= 0 && nextX < 10 && nextY >= 0 && nextY < 10)       // check validity of child node
            {
                return graph[nextX, nextY];
            }
            return null;
        }

        private void TraceBack(Node start)
        {
            // called when a collectible has found. Trace back the path and set the cordinates for next move
            int currentX = start.currentX;
            int currentY = start.currentY;
            //Console.WriteLine("My tank: ("+mytank.X+","+mytank.Y+")");
            Console.Write("Trace: (" + currentX + "," + currentY + ") P:(" + graph[currentX, currentY].parentX + "," + graph[currentX, currentY].parentY + ") -> ");
            try
            {
                while ((graph[currentX, currentY].parentX != mytank.X) || (graph[currentX, currentY].parentY != mytank.Y))
                {
                    int tempX = currentX;
                    currentX = graph[currentX, currentY].parentX;
                    currentY = graph[tempX, currentY].parentY;
                    Console.Write("(" + currentX + "," + currentY + ") P:(" + graph[currentX, currentY].parentX + "," + graph[currentX, currentY].parentY + ")-> ");
                }
            }
            catch (IndexOutOfRangeException)
            {
                currentX = -1;
                currentY = -1;
            }
            //Console.WriteLine("(" + graph[currentX, currentY].parentX + "," + graph[currentX, currentY].parentY + ")");
            nextX = currentX;
            nextY = currentY;
        }

        private String GetNextCommand()
        {
            // according to the coordinates of next move, select the command.
            if (nextX < 0 || nextY < 0)
            {
                return commands[4];
            }
            if (mytank.X < nextX)
            {
                return commands[1];
            }
            else if(mytank.X > nextX)
            {
                return commands[3];
            }
            else if (mytank.Y < nextY)
            {
                return commands[2];
            }
            else if (mytank.Y > nextY)
            {
                return commands[0];
            }
            return commands[4];
        }

        private int GetTankInRangeDir()
        {
            // find a tank that has no obstacle between mytank so that it can be directly shot. search in all for direction.
            bool[] obstacle = new bool[4];
            for (int i = 1; i < 6; i++ )    // look upto 5 boxes in each direction
            {
                if (mytank.Y - i >= 0 && !obstacle[0])  // up direction
                {
                    if (map[mytank.X, mytank.Y - i] != null ){
                        if (map[mytank.X, mytank.Y - i].GetType() == typeof(Tank))
                        {
                            Console.WriteLine("tank at (" + mytank.X + "," + (mytank.Y - i)+")");
                            return 0; 
                        }
                        else obstacle[0] = true;
                    }
                }
                if (mytank.X + i < 10 && !obstacle[1])  // rigth direction
                {
                    if (map[mytank.X+i, mytank.Y] != null)
                    {
                        if (map[mytank.X + i, mytank.Y].GetType() == typeof(Tank))
                        {
                            Console.WriteLine("tank at (" + (mytank.X+i) + "," + (mytank.Y) + ")");
                            return 1;
                        }
                        else obstacle[1] = true;
                    }
                }
                if (mytank.Y + i < 10 && !obstacle[2])  // down direction
                {
                    if (map[mytank.X, mytank.Y + i] != null)
                    {
                        if (map[mytank.X, mytank.Y + i].GetType() == typeof(Tank))
                        {
                            Console.WriteLine("tank at (" + (mytank.X) + "," + (mytank.Y+i) + ")");
                            return 2;
                        }
                        else obstacle[2] = true;
                    }
                }
                if (mytank.X - i >= 0 && !obstacle[3])  // left direction
                {
                    if (map[mytank.X - i, mytank.Y] != null)
                    {
                        if (map[mytank.X - i, mytank.Y].GetType() == typeof(Tank))
                        {
                            Console.WriteLine("tank at (" + (mytank.X - i) + "," + (mytank.Y) + ")");
                            return 3;
                        }
                        else obstacle[3] = true;
                    }
                }
            }
            return -1;
        }

        public void destroyTank()
        {
            /* if a bullet can reach an enemy tank without any obstacles in any 4 directions (up,right,down,left) this method is called to shoot 
             * many times to destroy the tank. */
            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(netHandler.Send));
                thread.Start(commands[4]);
                Thread.Sleep(300);
            }
        }

        private void resetGraph()
        {
            // assign minus values for parents of each node to avoide infinite loop in traceback method
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    graph[i, j].parentX = -1;
                    graph[i, j].parentY = -1;
                }
            }
        }

    }

    class Node
    {
        public int currentX, currentY;
        public int parentX, parentY;
    }
}
