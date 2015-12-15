using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TankSmashXNA.Entity
{
    class LifePack : GameEntity
    {

        private int lifeTime;
        public int LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }

        private List<LifePack> lifePackList;

        public LifePack(int x, int y, int lifeTime,List<LifePack> lifeList) : base(x, y)
        {
            this.LifeTime = lifeTime;
            this.lifePackList = lifeList;
        }

        public void StartTimer()
        {
            Thread.Sleep(LifeTime);
            if (lifePackList.Contains(this))
            {
                lifePackList.Remove(this);
            }
        }

    }
}
