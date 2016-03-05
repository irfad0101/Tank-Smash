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
using System.Threading;

namespace TankSmashXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logo;
        Texture2D btnJoin;
        Texture2D messageBox;
        Texture2D backbtn;
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
        Texture2D Tank1ETexture, Tank2ETexture, Tank3ETexture, Tank4ETexture, Tank5ETexture;
        Texture2D Tank1WTexture, Tank2WTexture, Tank3WTexture, Tank4WTexture, Tank5WTexture;
        Texture2D Tank1STexture, Tank2STexture, Tank3STexture, Tank4STexture, Tank5STexture;
        Texture2D Tank1NTexture, Tank2NTexture, Tank3NTexture, Tank4NTexture, Tank5NTexture;
        SpriteFont font;
        SpriteFont menuFont;
        SoundEffect click;

        Texture2D[,] tankTexture;
        GameEngine gameEngine;
        public static GameState currentState = GameState.menu;
        private int counter = 0;
        private String IP = "_";
        public static String message;

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
            this.IsMouseVisible = true;
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
            logo = Content.Load<Texture2D>("logo");
            btnJoin = Content.Load<Texture2D>("btnJoin");
            messageBox = Content.Load<Texture2D>("msgBox");
            backbtn = Content.Load<Texture2D>("back");
            gridTexture = Content.Load<Texture2D>("square");
            brick100Texture = Content.Load<Texture2D>("brick100");
            brick75Texture = Content.Load<Texture2D>("brick75");
            brick50Texture = Content.Load<Texture2D>("brick50");
            brick25Texture = Content.Load<Texture2D>("brick25");
            waterTexture = Content.Load<Texture2D>("water");
            lifePackTexture = Content.Load<Texture2D>("LifePack");
            stoneTexture = Content.Load<Texture2D>("stone");
            coinPackTexture = Content.Load<Texture2D>("coins");
            upBulletTexture = Content.Load<Texture2D>("bullet_up");
            downBulletTexture = Content.Load<Texture2D>("bullet_down");
            leftBulletTexture = Content.Load<Texture2D>("bullet_left");
            rightBulletTexture = Content.Load<Texture2D>("bullet_right");
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
            Tank5ETexture = Content.Load<Texture2D>("Tank5E");
            Tank5WTexture = Content.Load<Texture2D>("Tank5W");
            Tank5STexture = Content.Load<Texture2D>("Tank5S");
            Tank5NTexture = Content.Load<Texture2D>("Tank5N");
            tankTexture = new Texture2D[,] {{ Tank1NTexture, Tank2NTexture, Tank3NTexture, Tank4NTexture, Tank5NTexture }, { Tank1ETexture, Tank2ETexture, Tank3ETexture, Tank4ETexture, Tank5ETexture }, { Tank1STexture, Tank2STexture, Tank3STexture, Tank4STexture, Tank5STexture }, { Tank1WTexture, Tank2WTexture, Tank3WTexture, Tank4WTexture, Tank5WTexture } };
            font = Content.Load<SpriteFont>("myFont");
            menuFont = Content.Load<SpriteFont>("menuFont");
            click = Content.Load<SoundEffect>("Click");
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
            if (currentState == GameState.menu)
            {
                counter++;
                if (counter > 6)
                {
                    if (IP.Length > 0 && IP[IP.Length - 1].Equals('_'))
                    {
                        IP = IP.Substring(0, IP.Length - 1);
                    }
                    else
                    {
                        IP += "_";
                    }
                    counter = 0;
                    processKeyboard();
                    processMouse();
                }
            }
            else if (currentState == GameState.CannotJoin)
            {
                counter++;
                if(counter>6)
                    processMouse();
            }
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
            switch (currentState)
            {
                case GameState.menu:
                    DrawMenu();
                    break;
                case GameState.joining:
                    DrawMenu();
                    DrawMessageBox();
                    break;
                case GameState.CannotJoin:
                    DrawMenu();
                    DrawMessageBox();
                    break;
                case GameState.playing:
                    try
                    {
                        DrawBackground();
                        DrawBrick();
                        DrawWater();
                        DrawLifePack();
                        DrawStone();
                        DrawCoinPack();
                        DrawBullet();
                        DrawTank();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
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
            spriteBatch.Draw(logo, new Rectangle(650,5,200,85), Color.White);
        }

        private void DrawBrick()
        {
            
            List<Brick> brickList = gameEngine.Bricks;
            foreach (Brick item in brickList)
            {
                if (item.Damage < 4)
                {
                    Rectangle brickRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                    switch (item.Damage)
                    {
                        case 0:
                            spriteBatch.Draw(brick100Texture, brickRectangle, Color.White); break;
                        case 1:
                            spriteBatch.Draw(brick75Texture, brickRectangle, Color.White); break;
                        case 2:
                            spriteBatch.Draw(brick50Texture, brickRectangle, Color.White); break;
                        case 3:
                            spriteBatch.Draw(brick25Texture, brickRectangle, Color.White); break;
                        default:
                            break;
                    }
                }
                
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
            int i = 0;
            List<Tank> tankList = gameEngine.Tanks;
            spriteBatch.DrawString(font, "I am Player " +gameEngine.getMyTankIndex(), new Vector2(660, 100), Color.Red);
            foreach (Tank item in tankList)
            {
                if (item.Health > 0)
                {
                    Rectangle tankRectangle = new Rectangle(item.X * 54 + 5, item.Y * 54 + 5, 50, 50);
                    spriteBatch.Draw(tankTexture[item.Direction, item.getIndex()], tankRectangle, Color.White);
                }
                Rectangle tankRect = new Rectangle(600, 85*i+120, 50, 50);
                spriteBatch.Draw(tankTexture[0, item.getIndex()], tankRect, Color.White);
                spriteBatch.DrawString(font, "Player " + item.getIndex(), new Vector2(660, 85 * i + 120), Color.White);
                spriteBatch.DrawString(font, "Coins : " + item.Coins, new Vector2(660, 85 * i + 140), Color.White);
                spriteBatch.DrawString(font, "Points: " + item.Points, new Vector2(660, 85 * i + 160), Color.White);
                drawHealthBar(item.Health, i);
                i++;
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
                spriteBatch.DrawString(font, coin.Amount.ToString(), new Vector2(coin.X * 54 + 10, coin.Y * 54 + 20), Color.Black);
            }
        }

        private void DrawBullet()
        {
            // draw relevent texture of bullet according to direction
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
        }

        private void drawHealthBar(int health,int index)
        {
            Texture2D outerRect = getPlainRectangleTexture(100, 20, Color.White);
            spriteBatch.Draw(outerRect, new Vector2(660, 85*index+180), Color.White);
            if (health > 0)
            {
                if (health <= 20)
                {
                    Texture2D innerRect = getPlainRectangleTexture(health, 20, Color.Red);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
                else if (health <= 40)
                {
                    Texture2D innerRect = getPlainRectangleTexture(health, 20, Color.OrangeRed);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
                else if (health <= 60)
                {
                    Texture2D innerRect = getPlainRectangleTexture(health, 20, Color.Orange);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
                else if (health <= 80)
                {
                    Texture2D innerRect = getPlainRectangleTexture(health, 20, Color.YellowGreen);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
                else if (health <= 100)
                {
                    Texture2D innerRect = getPlainRectangleTexture(health, 20, Color.Green);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
                else
                {
                    Texture2D innerRect = getPlainRectangleTexture(100, 20, Color.Green);
                    spriteBatch.Draw(innerRect, new Vector2(660, 85 * index + 180), Color.White);
                }
            }
            spriteBatch.DrawString(font, health.ToString(), new Vector2(720, 85 * index + 180),Color.Black);
        }

        private void DrawMenu()
        {
            Rectangle rect = new Rectangle(0, 0, 900, 550);
            spriteBatch.Draw(groundTexture, rect, Color.White);
            rect.X = 500;
            rect.Y = 20;
            rect.Width = 300;
            rect.Height = 100;
            spriteBatch.Draw(logo, rect, Color.White);
            spriteBatch.Draw(getPlainRectangleTexture(250, 55, Color.White),new Vector2(135,75),Color.White);
            spriteBatch.DrawString(font, "IP Address :", new Vector2(20, 90), Color.White);
            spriteBatch.DrawString(menuFont, IP, new Vector2(145,85), Color.Black);
            spriteBatch.Draw(btnJoin, new Rectangle(185, 145, 150, 75),Color.White);
        }

        private void DrawMessageBox()
        {
            Rectangle rect = new Rectangle(300,250,300,150);
            spriteBatch.Draw(messageBox,rect,Color.White);
            spriteBatch.DrawString(font, message, new Vector2(320,290), Color.Black);
            if (currentState == GameState.CannotJoin)
            {
                spriteBatch.Draw(backbtn, new Rectangle(425, 340, 50, 50), Color.White);
            }
        }

        private Texture2D getPlainRectangleTexture(int width, int height, Color colour)
        {
            Texture2D texture = new Texture2D(graphics.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = colour;
            texture.SetData(data);
            return texture;
        }

        public enum GameState
        {
            menu,
            playing,
            joining,
            CannotJoin,
            End
        }

        private void processKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            Keys[] pressedkeys = keybState.GetPressedKeys();
            bool isDigit = pressedkeys.Any(key => key >= Keys.D0 && key <= Keys.D9 || key >= Keys.NumPad0 && key <= Keys.NumPad9);
            if (isDigit)
            {
                String pressed = pressedkeys[0].ToString();
                if (IP.Length > 0 && IP[IP.Length - 1].Equals('_'))
                {
                    IP = IP.Insert(IP.Length - 1, pressed[pressed.Length - 1].ToString());
                }
                else
                {
                    IP += pressed[pressed.Length - 1];
                }
            }
            else if (keybState.IsKeyDown(Keys.OemPeriod) || keybState.IsKeyDown(Keys.Decimal))
            {
                if (IP.Length > 0 && IP[IP.Length - 1].Equals('_'))
                {
                    IP = IP.Insert(IP.Length - 1, ".");
                }
                else
                {
                    IP += ".";
                }
            }
            else if (keybState.IsKeyDown(Keys.Back))
            {
                if (IP.Length > 1)
                {
                    if (IP[IP.Length - 1].Equals('_'))
                    {
                        IP = IP.Remove(IP.Length - 2, 1);
                    }
                    else
                    {
                        IP = IP.Substring(0, IP.Length - 1);
                    }
                }
            }
        }

        private void processMouse()
        {
            MouseState mouse = Mouse.GetState();
            switch (currentState)
            {
                case GameState.menu:
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Rectangle pointer = new Rectangle(mouse.X, mouse.Y, 1, 1);
                        if (pointer.Intersects(new Rectangle(185, 145, 150, 75)))
                        {
                            click.Play();
                            NetworkHandler netHandler = NetworkHandler.getInstance();
                            Thread thread = new Thread(new ThreadStart(netHandler.StartListerner));
                            thread.Start();
                            currentState = GameState.joining;
                            netHandler.setIP(IP);
                            message = "Connecting to Server...";
                            netHandler.Send("JOIN#");
                        }
                    }
                    break;
                case GameState.CannotJoin:
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Rectangle pointer = new Rectangle(mouse.X, mouse.Y, 1, 1);
                        if (pointer.Intersects(new Rectangle(425, 340, 50, 50)))
                        {
                            click.Play();
                            currentState = GameState.menu;
                        }
                    }
                    break;
            }
        }

    }
}
