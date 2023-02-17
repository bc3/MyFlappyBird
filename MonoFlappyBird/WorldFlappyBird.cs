using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    class WorldFlappyBird : World
    {
        //world params
        public float horSpeed ;
        public float vertSpeed;
        public float fallSpeed;
        public int score;

        KeyboardState oldState = Keyboard.GetState();


        public WorldFlappyBird(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content, Game game)
            : base(graphics, spriteBatch, content, game)
        {
            //set the collision detector object
            cr = new CollisionResolverFlappyBird(this);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            oldState = newState;

            if (!this.gameover)
            {

                CleanUpObjects();

                if (objects.Count(x => x is PipesBottom) == 2)
                {
                    var lastPipe = this.objects.Where(x => x is PipesBottom).OrderByDescending(x => x.gop.pos.X).First();

                    var last = (lastPipe as PipesBottom).height;

                    var minl = last - 150;
                    var maxl = last + 150;

                    DrawAPipe((int)lastPipe.gop.pos.X + 150, RandomBetween(minl < 50 ? 50 : minl, maxl > 412 ? 412 : maxl));
                    score++;
                }

            }
            else
            {

                CleanUpAllPipes();
                ground.Stop();
                flappy.Stop();

                if (newState.IsKeyDown(Keys.F1))
                {
                    Initialize();
                }

            }


            base.Update(gameTime);

        }
        private void CleanUpAllPipes()
        {


            var pipes = this.objects.Where(x => x is PipesBottom || x is PipesTop).ToList();
            for (int i = pipes.Count() - 1; i >= 0; i--)
            {
              
                    this.objects.Remove(pipes[i]);
              
            }
        }

        private void CleanUpObjects()
        {


            var pipes = this.objects.Where(x => x is PipesBottom || x is PipesTop).ToList();
            for (int i = pipes.Count() - 1; i >= 0; i--)
            {
                if (pipes[i].gop.pos.X < -50)
                {
                    this.objects.Remove(pipes[i]);
                }
            }
        }

        private Ground ground;
        private Flappy flappy;

        public override void Initialize()
        {
            base.Initialize();

            score = 0;
            horSpeed = 50;
            vertSpeed = 300;
            fallSpeed = 180;
            GameObjectProperties gop;

            gop = new GameObjectProperties(this, new Vector2(0, 0), 0, false);

            var flappyMop = new MovableObjectProperties() { pos = new Vector2(100, 200), world = this, zindex = 0.5f };
            flappy = new Flappy(flappyMop);
            this.AddObject(flappy);

            DrawAPipe(200, RandomBetween(120, 292));
            DrawAPipe(350, RandomBetween(120, 292));
            DrawAPipe(500, RandomBetween(120, 292));


            var groundMop = new MovableObjectProperties() { pos = new Vector2(0, this.height - 100), world = this, zindex = 0.1f };
            ground = new Ground(groundMop);
            this.AddObject(ground);

        }

        private void DrawAPipe(int x, float rnd)
        {
            var _pipeMop1 = new MovableObjectProperties() { pos = new Vector2(x, 0), world = this, zindex = 0.3f };
            var _pipes1 = new PipesBottom(_pipeMop1, rnd);
            this.AddObject(_pipes1);

            var _pipeMop2 = new MovableObjectProperties() { pos = new Vector2(x, 0), world = this, zindex = 0.3f };
            var _pipes2 = new PipesTop(_pipeMop1, rnd);
            this.AddObject(_pipes2);
        }
    }
}
