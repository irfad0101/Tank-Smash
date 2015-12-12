using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankSmashXNA.Entity
{
    class Brick : GameEntity
    {

        private int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public Brick(int x, int y, int damage) : base(x,y)
        {
            this.Damage = damage;
        }

    }
}
