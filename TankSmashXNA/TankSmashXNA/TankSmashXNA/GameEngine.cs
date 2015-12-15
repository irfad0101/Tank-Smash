using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankSmashXNA.Entity;

namespace TankSmashXNA
{
    class GameEngine
    {
        private static GameEngine engine = new GameEngine();
        private const int LINE_FEED_LENGTH = 2;
        
        private List<Tank> tankList;
        public List<Tank> Tanks
        {
            get { return tankList; }
        }

        private List<Brick> brickList;
        public List<Brick> Bricks
        {
            get { return brickList; }
        }

        private List<Stone> stoneList;
        public List<Stone> Stones
        {
            get { return stoneList; }
        }

        private List<WaterPit> waterPitList;
        public List<WaterPit> WaterPits
        {
            get { return waterPitList; }
        }

        private List<CoinPack> coinPackList;
        public List<CoinPack> CoinPacks
        {
            get { return coinPackList; }
        }

        private List<LifePack> lifePackList;
        public List<LifePack> LifePacks
        {
            get { return lifePackList; }
        }

        private List<Bullet> bulletList;
        public List<Bullet> Bullerts
        {
            get { return bulletList; }
        }

        private GameEngine()
        {
            tankList = new List<Tank>();
            brickList = new List<Brick>();
            stoneList = new List<Stone>();
            waterPitList = new List<WaterPit>();
            coinPackList = new List<CoinPack>();
            lifePackList = new List<LifePack>();
            bulletList = new List<Bullet>();
        }

        public static GameEngine GetInstance()
        {
            return engine;
        }

        public void decode(Object message)
        {
            String msg = (String)message;
            if (msg.StartsWith("I:"))
            {
                Initialize(msg.Substring(2, msg.Length - 2 - LINE_FEED_LENGTH));
            }
            else if (msg.StartsWith("S:"))
            {
                InitializeTanks(msg.Substring(2, msg.Length - 2 - LINE_FEED_LENGTH));
            }
            else if (msg.StartsWith("G:"))
            {
                UpdateGameObjects(msg.Substring(2, msg.Length - 2 - LINE_FEED_LENGTH));
            }
        }

        private void Initialize(String message)
        {
            String[] worlddetails = message.Split(':');
            String[] bricks = worlddetails[1].Split(new char[] { ';', ',' });
            for (int i = 0; i < bricks.Length; i += 2)
            {
                Brick brick = new Brick(Int32.Parse(bricks[i]),Int32.Parse(bricks[i+1]),0);
                brickList.Add(brick);
            }
            String[] stones = worlddetails[2].Split(new char[] { ';', ',' });
            for (int i = 0; i < stones.Length; i += 2)
            {
                Stone stone = new Stone(Int32.Parse(stones[i]), Int32.Parse(stones[i + 1]));
                stoneList.Add(stone);
            }
            String[] waterPits = worlddetails[3].Split(new char[] { ';', ',' });
            for (int i = 0; i < waterPits.Length; i += 2)
            {
                WaterPit waterPit = new WaterPit(Int32.Parse(waterPits[i]), Int32.Parse(waterPits[i + 1]));
                waterPitList.Add(waterPit);
            }
        }

        private void InitializeTanks(String message)
        {
            String[] tanks = message.Split(':');
            foreach (String tank in tanks)
            {
                String[] tankDetails = tank.Split(new char[] { ';', ',' });
                Tank t = new Tank(tankDetails[0],Int32.Parse(tankDetails[1]), Int32.Parse(tankDetails[2]), Int32.Parse(tankDetails[3]), 100, 0, 0);
                tankList.Add(t);
            }
        }

        private void UpdateGameObjects(String message)
        {
            String[] details = message.Split(':');
            for (int i = 0; i < details.Length - 1; i++)
            {
                String[] tankDetails = details[i].Split(new char[] { ';', ',' });
                Tank tank = tankList[i];
                tank.X = Int32.Parse(tankDetails[1]);
                tank.Y = Int32.Parse(tankDetails[2]);
                tank.Direction = Int32.Parse(tankDetails[3]);
                tank.Health = Int32.Parse(tankDetails[5]);
                tank.Coins = Int32.Parse(tankDetails[6]);
                tank.Points = Int32.Parse(tankDetails[7]);
            }
            String[] brickDetails = details[details.Length - 1].Split(new char[] { ';', ',' });
            for (int i = 0; i < brickDetails.Length; i += 3)
            {
                Brick brick = brickList[i / 3];
                brick.Damage = Int32.Parse(brickDetails[i + 2]);
            }
        }

    }
}
