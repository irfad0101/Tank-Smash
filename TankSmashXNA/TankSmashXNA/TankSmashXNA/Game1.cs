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
        Texture2D brickTexture;
        Texture2D waterTexture;
        Texture2D tankTexture;
        Texture2D lifePackTexture;
        Texture2D stoneTexture;


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
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game - Client";
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
            gridTexture = Content.Load<Texture2D>("grid");
            brickTexture = Content.Load<Texture2D>("brick");
            waterTexture = Content.Load<Texture2D>("water");
            tankTexture = Content.Load<Texture2D>("Tank");
            lifePackTexture = Content.Load<Texture2D>("LifePack");
            stoneTexture = Content.Load<Texture2D>("stone");

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
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawBackground();
            DrawBrick();
            DrawWater();
            DrawTank();
            DrawLifePack();
            DrawStone();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            Rectangle screenRectangle = new Rectangle(0,0,500,500);
            spriteBatch.Draw(gridTexture, screenRectangle, Color.White);
            /*for (int i = 0; i < 10; i++)
			{
                for (int j = 0; j < 10; j++)
                {
                    Rectangle screenRectangle = new Rectangle(50*i,50*j, 55,55);
                    spriteBatch.Draw(gridTexture, screenRectangle, Color.White);
                }
             }*/
            
        }

        private void DrawBrick()
        {
            Rectangle brickRectangle = new Rectangle(51, 1, 50, 50);
            spriteBatch.Draw(brickTexture, brickRectangle, Color.White);
        }

        private void DrawWater()
        {
            Rectangle waterRectangle = new Rectangle(51, 101, 50, 50);
            spriteBatch.Draw(waterTexture, waterRectangle, Color.White);
        }

        private void DrawTank()
        {
            Rectangle tankRectangle = new Rectangle(101, 101, 50, 50);
            spriteBatch.Draw(tankTexture, tankRectangle, Color.White);
        }
        private void DrawLifePack()
        {
            Rectangle lifePackRectangle = new Rectangle(201, 201, 50, 50);
            spriteBatch.Draw(lifePackTexture, lifePackRectangle, Color.White);
        }

        private void DrawStone()
        {
            Rectangle stoneRectangle = new Rectangle(250, 250, 50, 50);
            spriteBatch.Draw(stoneTexture, stoneRectangle, Color.White);
        }
    }
}
