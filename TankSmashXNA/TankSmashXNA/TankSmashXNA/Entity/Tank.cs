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

        private int coins;
        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public int getIndex(){
            return Int32.Parse(name.Substring(1));
        }

        public Tank(String name, int x, int y, int direction, int health, int points, int coins) : base(x,y)
        {
            this.Name = name;
            this.Direction = direction;
            this.Health = health;
            this.Points = points;
            this.Coins = coins;
        }

    }
}
