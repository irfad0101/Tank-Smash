using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankSmashXNA.Entity
{
    class Tank : GameEntity
    {
        private int direction;
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private int points;
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Tank(int x, int y, int direction, int health, int points) : base(x,y)
        {
            this.Direction = direction;
            this.Health = health;
            this.Points = points;
        }

    }
}
