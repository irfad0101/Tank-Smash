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
        public String msg;
        
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

        private GameEngine()
        {
            tankList = new List<Tank>();
            brickList = new List<Brick>();
            stoneList = new List<Stone>();
            waterPitList = new List<WaterPit>();
        }

        public static GameEngine GetInstance()
        {
            return engine;
        }

        public void decode()
        {
            if (msg.StartsWith("I"))
                Initialize(msg.Substring(2, msg.Length - 2));
            else
                Console.WriteLine(msg);
        }

        public void Initialize(String message)
        {
            String[] worlddetails = message.Split(':');
            String[] bricks = worlddetails[1].Split(new char[] { ';', ',' });
            for (int i = 0; i < bricks.Length; i += 2)
            {
                Brick brick = new Brick(Int32.Parse(bricks[i]),Int32.Parse(bricks[i+1]),0);
                brickList.Add(brick);
            }
            String[] stones = worlddetails[2].Split(new char[] { ';', ',' });
            for (int i = 0; i < bricks.Length; i += 2)
            {
                Stone stone = new Stone(Int32.Parse(bricks[i]), Int32.Parse(bricks[i + 1]));
                stoneList.Add(stone);
            }
            String[] waterPits = worlddetails[3].Split(new char[] { ';', ',' });
            for (int i = 0; i < bricks.Length; i += 2)
            {
                WaterPit waterPit = new WaterPit(Int32.Parse(bricks[i]), Int32.Parse(bricks[i + 1]));
                waterPitList.Add(waterPit);
            }
        }

    }
}
