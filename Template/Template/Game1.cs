using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D xwing;
        Texture2D background;
        Texture2D meteorite;
        Vector2 xwingPos = new Vector2(100, 300);
        Vector2 xwingPos2 = new Vector2(200, 300);
        List<Vector2> xwingBulletPos = new List<Vector2> ();
        List<Vector2> meteoritePos = new List<Vector2>();
        Random meteoriteRandom = new Random();
        Random meteoriteSpawn = new Random();
        int meteoriteSpawnPos = 0;

        KeyboardState kNewState;
        int time = 0;
        int shooter = 0;
        //Komentar
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
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
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
            xwing = Content.Load<Texture2D>("xwing");
            background = Content.Load<Texture2D>("Space");
            meteorite = Content.Load<Texture2D>("meteorite");

            // TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            meteoriteSpawnPos = meteoriteSpawn.Next(0, GraphicsDevice.DisplayMode.Width);
            time++;
            meteoriteRandom.Next(50, GraphicsDevice.DisplayMode.Width-49);
            MouseState mstate = Mouse.GetState();
            kNewState = Keyboard.GetState();
            if (kNewState.IsKeyDown(Keys.D))
                xwingPos.X+= 20;
            if (kNewState.IsKeyDown(Keys.A))
                xwingPos.X-= 20;
            if (kNewState.IsKeyDown(Keys.S))
                xwingPos.Y+= 20;
            if (kNewState.IsKeyDown(Keys.W))
                xwingPos.Y-= 20;
            if (kNewState.IsKeyDown(Keys.D))
                xwingPos2.X += 20;
            if (kNewState.IsKeyDown(Keys.A))
                xwingPos2.X -= 20;
            if (kNewState.IsKeyDown(Keys.S))
                xwingPos2.Y += 20;
            if (kNewState.IsKeyDown(Keys.W))
                xwingPos2.Y -= 20;
            if (meteoriteRandom.Next(0, 4) == 0)
            {
                meteoritePos.Add(new Vector2(meteoriteSpawnPos, 0));
            }
            if (mstate.LeftButton == ButtonState.Pressed && time >= 15 && shooter == 0)
            {
                xwingBulletPos.Add(xwingPos);
                time = 0;
                shooter = 1;
            }
            if (mstate.LeftButton == ButtonState.Pressed && time >= 15 && shooter == 1)
            {
                xwingBulletPos.Add(xwingPos2);
                time = 0;
                shooter = 0;
            }
            for (int i = 0; i < xwingBulletPos.Count; i++)
            {
                xwingBulletPos[i] = xwingBulletPos[i] - new Vector2(0, 15);
            }
            for (int i = 0; i < meteoritePos.Count; i++)
            {
                meteoritePos[i] = meteoritePos[i] - new Vector2(0, -2);
            }
            RemoveObjects();
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
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.Draw(xwing, xwingPos, Color.White);
            foreach (Vector2 bulletPos in xwingBulletPos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = bulletPos.ToPoint();
                rec.Size = new Point(10, 100);
                spriteBatch.Draw(xwing, rec, Color.FloralWhite);
            }
            foreach (Vector2 meteoritePos in meteoritePos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = meteoritePos.ToPoint();
                rec.Size = new Point(50, 50);
                spriteBatch.Draw(meteorite, rec, Color.White);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here.

            base.Draw(gameTime);
        }
        void RemoveObjects()
        {
            List<Vector2> temp = new List<Vector2>();
            foreach (var item in xwingBulletPos)
            {
                if (item.Y >= -10)
                {
                    temp.Add(item);
                }
            }
            xwingBulletPos = temp;
        }
    }
}
