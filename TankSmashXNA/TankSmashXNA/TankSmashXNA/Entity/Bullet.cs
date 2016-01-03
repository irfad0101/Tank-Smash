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

        public Bullet(int x, int y, int directoin) : base(x, y)
        {
            this.Direction = direction;
        }

    }
}
