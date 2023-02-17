﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    public class World
    {
        public List<GameObject> objects;
        //public List<GameObject> collisionobjects;
        public CollisionResolver cr;

        private List<GameObject> removeobjects;

        internal int width;
        internal int height;
        internal SpriteBatch spriteBatch;
        internal ContentManager content;
        internal static Random random = new Random();
        public Game game;
        public bool diagnostics;
        public bool gameover;
        private float totalSeconds;
        private KeyboardState oldState;
        public SpriteFont fontCourier;
        private Texture2D texbackground;

        public World(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content, Game game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.diagnostics = false;
            this.fontCourier = content.Load<SpriteFont>("Courier");
            this.texbackground = content.Load<Texture2D>("Flappy-Backgrounds");


            if (graphics.IsFullScreen)
            {
                width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                width = graphics.PreferredBackBufferWidth;
                height = graphics.PreferredBackBufferHeight;
            }


        }


        public virtual void Initialize()
        {
            objects = new List<GameObject>();
            //collisionobjects = new List<GameObject>();
            gameover = false;
            totalSeconds = 0;


        }

        public static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }


        public virtual void Update(GameTime gameTime)
        {
            if (!gameover) totalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.D) & !oldState.IsKeyDown(Keys.D))
            {
                this.diagnostics = !diagnostics;

            }

            removeobjects = new List<GameObject>();



            if (newState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(-1);
            }



            if (!gameover)
            {
                int count = objects.Count();
                for (int i = 0; i < count; i++)
                {
                    objects[i].Update(gameTime);
                }

                DetectCollission(gameTime);
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.F1))
                {
                    Initialize();
                }
            }

            //if (removeobjects.Count > 0)
            //{
            //    var count = removeobjects.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        objects.Remove(removeobjects[i]);
            //        if (collisionobjects.Contains(removeobjects[i]))
            //            collisionobjects.Remove(removeobjects[i]);
            //    }
            //}



            oldState = newState;


        }

        public void Draw(GameTime gametime)
        {

            spriteBatch.Begin(SpriteSortMode.BackToFront, null); //Between Begin() and End() we can draw.

            spriteBatch.Draw(texbackground, new Rectangle(0, 0, this.width, this.height), new Rectangle(0, 0, this.width, this.height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);


            int count = objects.Count();
            for (int i = 0; i < count; i++)
            {
                objects[i].Draw(spriteBatch, gametime);
            }
            //if (gameover)
            //{
            //    Vector2 loc;

            //    String line1 = String.Format("Game over , you survided {0} seconde", ((int)totalSeconds).ToString());
            //    String line2 = String.Format("Press F1 to continue");

            //    loc = fontCourier.MeasureString(line1) / 2;
            //    spriteBatch.DrawString(fontCourier, line1, new Vector2(this.width / 2, this.height / 2), Color.GreenYellow, 0, loc, 3, SpriteEffects.None, 0f);
            //    loc = fontCourier.MeasureString(line2) / 2;
            //    spriteBatch.DrawString(fontCourier, line2, new Vector2(this.width / 2, this.height / 2 + 40), Color.GreenYellow, 0, loc, 3, SpriteEffects.None, 0f);
            //}
            spriteBatch.End();
        }


        internal void AddObject(GameObject go)
        {
            if (go.gop.world == null)
                go.gop.world = this;

            go.Initialize();
            objects.Add(go);

        }

        internal void RemoveObject(GameObject go)
        {
            removeobjects.Add(go);
        }

        public void DetectCollission(GameTime gameTime)
        {
            int count = objects.Count();
            for (int i = 0; i < count; i++)
            {
                if ((gameTime.TotalGameTime - objects[i].gop.lastCollision) > new TimeSpan(0, 0, 0, 0, 100))
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        if (objects[i] != objects[j])
                        {
                            if (cr.DetectCollision(gameTime, objects[i], objects[j]))
                            {
                                //collisionobjects[i].ResolveCollision(gameTime, collisionobjects[j]);

                                cr.ResolveCollision(gameTime, objects[i], objects[j]);
                            }
                        }
                    }
                }
            }
        }

    
    }
}

