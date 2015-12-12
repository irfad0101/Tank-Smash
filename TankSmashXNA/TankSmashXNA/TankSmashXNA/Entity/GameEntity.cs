using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankSmashXNA.Entity
{
    class GameEntity
    {
        int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public GameEntity(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
