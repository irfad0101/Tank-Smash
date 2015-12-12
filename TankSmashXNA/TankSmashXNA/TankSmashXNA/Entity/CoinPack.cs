using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankSmashXNA.Entity
{
    class CoinPack : GameEntity
    {

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private long lifeTime;
        public long LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }

        public CoinPack(int x, int y,int Amount,long lifeTime) : base(x, y)
        {
            this.Amount = amount;
            this.LifeTime = lifeTime;
        }

    }
}
