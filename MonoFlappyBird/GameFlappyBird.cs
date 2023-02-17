using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlappyBird
{
   /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameFlappyBird : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World world;

        public GameFlappyBird()
        {
            graphics = new GraphicsDeviceManager(this);


            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = 1600;
            //graphics.PreferredBackBufferHeight = 1200;

            graphics.PreferredBackBufferWidth = 270;
            graphics.PreferredBackBufferHeight = 512;

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
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            world = new WorldFlappyBird(graphics, spriteBatch, Content, this);
            world.Initialize();
            Window.Title = "MyFlappyBird in 30 minutes!";
            base.Initialize();


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

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

            world.Update(gameTime);
            
          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}

