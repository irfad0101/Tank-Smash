using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TankSmashXNA.Entity;

namespace TankSmashXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gridTexture;
        Texture2D brick100Texture;
        Texture2D brick75Texture;
        Texture2D brick50Texture;
        Texture2D brick25Texture;
        Texture2D waterTexture;
        //Texture2D tankTexture;
        Texture2D lifePackTexture;
        Texture2D stoneTexture;
        Texture2D coinPackTexture;
        Texture2D upBulletTexture, downBulletTexture,leftBulletTexture, rightBulletTexture;
        Texture2D groundTexture;
        Texture2D Tank1ETexture, Tank2ETexture, Tank3ETexture, Tank4ETexture;
        Texture2D Tank1WTexture, Tank2WTexture, Tank3WTexture, Tank4WTexture;
        Texture2D Tank1STexture,Tank2STexture, Tank3STexture, Tank4STexture;
        Texture2D Tank1NTexture,Tank2NTexture, Tank3NTexture, Tank4NTexture;
        Texture2D[,] tankTexture;
        GameEngine gameEngine;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 550;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game - Client";
            gameEngine =GameEngine.GetInstance();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gridTexture = Content.Load<Texture2D>("square");
            brick100Texture = Content.Load<Texture2D>("brick100");
            brick75Texture = Content.Load<Texture2D>("brick75");
            brick50Texture = Content.Load<Texture2D>("brick50");
            brick25Texture = Content.Load<Texture2D>("brick25");
            waterTexture = Content.Load<Texture2D>("water");
            lifePackTexture = Content.Load<Texture2D>("LifePack");
            stoneTexture = Content.Load<Texture2D>("stone");
            coinPackTexture = Content.Load<Texture2D>("coins");
            upBulletTexture = Content.Load<Texture2D>("upBullet");
            downBulletTexture = Content.Load<Texture2D>("downBullet");
            leftBulletTexture = Content.Load<Texture2D>("leftBullet");
            rightBulletTexture = Content.Load<Texture2D>("rightBullet");
            groundTexture = Content.Load<Texture2D>("battlefield");
            Tank1ETexture = Content.Load<Texture2D>("Tank1E");
            Tank1WTexture = Content.Load<Texture2D>("Tank1W");
            Tank1STexture = Content.Load<Texture2D>("Tank1S");
            Tank1NTexture = Content.Load<Texture2D>("Tank1N");
            Tank2ETexture = Content.Load<Texture2D>("Tank2E");
            Tank2WTexture = Content.Load<Texture2D>("Tank2W");
            Tank2STexture = Content.Load<Texture2D>("Tank2S");
            Tank2NTexture = Content.Load<Texture2D>("Tank2N");
            Tank3ETexture = Content.Load<Texture2D>("Tank3E");
            Tank3WTexture = Content.Load<Texture2D>("Tank3W");
            Tank3STexture = Content.Load<Texture2D>("Tank3S");
            Tank3NTexture = Content.Load<Texture2D>("Tank3N");
            Tank4ETexture = Content.Load<Texture2D>("Tank4E");
            Tank4WTexture = Content.Load<Texture2D>("Tank4W");
            Tank4STexture = Content.Load<Texture2D>("Tank4S");
            Tank4NTexture = Content.Load<Texture2D>("Tank4N");
            tankTexture = new Texture2D[,] {{ Tank1NTexture, Tank2NTexture, Tank3NTexture, Tank4NTexture }, { Tank1ETexture, Tank2ETexture, Tank3ETexture, Tank4ETexture }, { Tank1STexture, Tank2STexture, Tank3STexture, Tank4STexture }, { Tank1WTexture, Tank2WTexture, Tank3WTexture, Tank4WTexture }  };

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawBackground();
            DrawBrick();
            DrawWater();
           
            DrawLifePack();
            DrawStone();
            DrawCoinPack();
            
            DrawBullet();
            DrawTank();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            Rectangle groundRectangle = new Rectangle(0,0,900,550);
            spriteBatch.Draw(groundTexture, groundRectangle, Color.White);
            for (int i = 0; i < 10; i++)
			{
                for (int j = 0; j < 10; j++)
                {
                    Rectangle screenRectangle = new Rectangle(54*i+5,54*j+5, 50,50);
                    spriteBatch.Draw(gridTexture, screenRectangle, Color.White);
                }
             }
            
        }

        private void DrawBrick()
        {
            
            List<Brick> brickList = gameEngine.Bricks;
            foreach (Brick item in brickList)
            {
                Rectangle brickRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                switch (item.Damage)
                {
                    case 0:
                        spriteBatch.Draw(brick100Texture, brickRectangle, Color.White);break;
                    case 1:
                        spriteBatch.Draw(brick75Texture, brickRectangle, Color.White);break;
                    case 2:
                        spriteBatch.Draw(brick50Texture, brickRectangle, Color.White); break;
                    case 3:
                        spriteBatch.Draw(brick25Texture, brickRectangle, Color.White); break;
                    default:
                        break;
                }
                /*Rectangle brickRectangle = new Rectangle(item.X*54+5, item.Y*54+5, 50, 50);
                spriteBatch.Draw(brick100Texture, brickRectangle, Color.White);*/
                
            }
        }

        private void DrawWater()
        {
            List<WaterPit> waterPitList = gameEngine.WaterPits;
            foreach (WaterPit item in waterPitList)
	        {
                Rectangle waterRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                spriteBatch.Draw(waterTexture, waterRectangle, Color.White);
	        }
        }

        private void DrawTank()
        {
            List<Tank> tankList = gameEngine.Tanks;
            foreach (Tank item in tankList)
            {
                Rectangle tankRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                spriteBatch.Draw(tankTexture[item.Direction,item.getIndex()], tankRectangle, Color.White);
                        
            }
        }

        private void DrawLifePack()
        {
            List<LifePack> lifePackList = gameEngine.LifePacks;
            foreach (LifePack item in lifePackList)
            {
                Rectangle lifePackRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                spriteBatch.Draw(lifePackTexture, lifePackRectangle, Color.White);  
            }
        }

        private void DrawStone()
        {
            List<Stone> stoneList = gameEngine.Stones;
            foreach (Stone item in stoneList)
            {
                Rectangle stoneRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                spriteBatch.Draw(stoneTexture, stoneRectangle, Color.White);   
            }
            
        }

        private void DrawCoinPack()
        {
            List<CoinPack> coinList = gameEngine.CoinPacks;
            foreach (CoinPack coin in coinList)
            {
                Rectangle coinRectangle = new Rectangle(coin.X * 54 + 5, coin.Y * 54 + 5, 50, 50);
                spriteBatch.Draw(coinPackTexture, coinRectangle, Color.White);
            }
        }

        private void DrawBullet()
        {
            List<Bullet> bulletList = gameEngine.Bullerts;
            foreach (Bullet bullet in bulletList)
            {
                Rectangle bulletRectangle = new Rectangle(bullet.X * 54 + 5, bullet.Y * 54 + 5, 50, 50);
                
                switch (bullet.Direction)
                {
                    case 0:
                        spriteBatch.Draw(upBulletTexture, bulletRectangle, Color.White);break;
                    case 1:
                        spriteBatch.Draw(rightBulletTexture, bulletRectangle, Color.White); break;
                    case 2:
                        spriteBatch.Draw(downBulletTexture, bulletRectangle, Color.White); break;
                    case 3:
                        spriteBatch.Draw(leftBulletTexture, bulletRectangle, Color.White); break;
                    default:
                        break;

                }
            }
            /*Rectangle bulletRectangle = new Rectangle(491, 491, 50, 50);
            spriteBatch.Draw(bulletTexture, bulletRectangle, Color.White);*/
        }

    }
}
