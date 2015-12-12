using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankSmashXNA.Entity
{
    class LifePack : GameEntity
    {

        private long lifeTime;
        public long LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }

        public LifePack(int x, int y, long lifeTime) : base(x, y)
        {
            this.LifeTime = lifeTime;
        }

    }
}
