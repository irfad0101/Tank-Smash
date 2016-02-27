using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankSmashXNA.Entity
{
    class Bullet : GameEntity
    {
        private int direction;
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private GameEntity[,] map;
        private List<Bullet> bulletList;

        public Bullet(int x, int y, int directoin, List<Bullet> bullets, GameEntity[,] map) : base(x, y)
        {
            this.direction = directoin;
            this.bulletList = bullets;
            this.map = map;
        }

        public void updatePosition()
        {
            while (true)
            {
                switch (direction)
                {
                    case 0:
                        Y -= 1;
                        break;
                    case 1:
                        X += 1;
                        break;
                    case 2:
                        Y += 1;
                        break;
                    case 3:
                        X -= 1;
                        break;
                }
                try
                {
                    if (map[X, Y] != null)
                    {
                        if (map[X, Y].GetType() == typeof(Tank) || map[X, Y].GetType() == typeof(Brick) || map[X, Y].GetType() == typeof(Stone)) 
                        {
                            bulletList.Remove(this);    // remove bullet if an obstacle met
                        }
                        else
                        {
                            Thread.Sleep(333);
                        }
                    }
                    else
                    {
                        Thread.Sleep(333);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    bulletList.Remove(this);
                }
            }
        }

    }
}
