using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

        private int lifeTime;
        public int LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }

        private List<CoinPack> coinPackList;

        public CoinPack(int x, int y,int Amount,int lifeTime,List<CoinPack> coinList) : base(x, y)
        {
            this.Amount = amount;
            this.LifeTime = lifeTime;
            this.coinPackList = coinList;
        }

        public void StartTimer()
        {
            Thread.Sleep(LifeTime);
            coinPackList.Remove(this);
        }

    }
}
