using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    class Flappy : MovableObject
    {
        private Texture2D tex;

        private TimeSpan lastShot;

        private TimeSpan lastTime;
        public MovableObject circleSpeed;

        private const float maxY = 512;
        private const float minY = 0;
    

        private Texture2D debugtex;

        public Flappy(MovableObjectProperties mop)
            : base(mop)
        {
            this.gop.offscreen = false;
            this.gop.mass = 10;
            this.gop.causescollision = true;
            this.gop.velocity = new Vector2(0, 0);
            this.gop.radius = 20;
            this.gop.zindex = 0.1f;
            this.tex = mop.world.content.Load<Texture2D>("flappy-bird-50x50");
            this.debugtex = this.gop.world.content.Load<Texture2D>("pixel");
            //health parameters
            this.gop.maxhealth = 100;
            this.gop.currenthealth = 100;
            this.gop.stephealth = 20;

        }


        public void Stop()
        {
            (this.gop.world as WorldFlappyBird).fallSpeed = 0;
            (this.gop.world as WorldFlappyBird).vertSpeed = 0;
        }

        public override void Update(GameTime gametime)
        {

            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            KeyboardState keys = Keyboard.GetState();



           //if (keys.IsKeyDown(Keys.Down))
           //{
           //    if (this.gop.pos.Y < gop.world.height)
           //    {
           //        this.gop.pos.Y += (this.gop.world as WorldFlappyBird).vertSpeed * dt;
           //    }
           //}

            if (keys.IsKeyDown(Keys.Up))
            {
                if (this.gop.pos.Y > 0)
                {
                    this.gop.pos.Y -= (this.gop.world as WorldFlappyBird).vertSpeed * dt;
                }
            }

      

            if (this.gop.pos.Y < gop.world.height)
            {
                this.gop.pos.Y += (this.gop.world as WorldFlappyBird).fallSpeed * dt;
            }




            base.Update(gametime);

        }

        public override Rectangle getBoundingRect()
        {
            int x = (int)this.gop.pos.X - 12;
            int y = (int)this.gop.pos.Y - 12;
            return new Rectangle(x,y,24,24);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {

            VectorA tmp = new VectorA(this.gop.velocity);

            spriteBatch.Draw(tex, this.gop.pos, new Rectangle(0, 0, 50, 50), Color.White, tmp.Angle.Value, new Vector2(this.gop.radius, this.gop.radius), new Vector2(1, 1), SpriteEffects.None, this.gop.zindex);

            if (this.gop.world.diagnostics)
            {
                spriteBatch.DrawString(this.gop.world.fontCourier, String.Format("[X: {0} / Y: {1}] [Velocity: {2}]", this.gop.pos.X.ToString("#.00"), this.gop.pos.Y.ToString("#.00"), tmp.ToString(), this), new Vector2(0, 0), Color.Yellow);
                spriteBatch.Draw(debugtex, this.getBoundingRect(), Color.Red);
            }

            spriteBatch.DrawString(this.gop.world.fontCourier, string.Format("Score: {0}",(this.gop.world as WorldFlappyBird).score),new Vector2(20,200),Color.White);

            base.Draw(spriteBatch, gametime);
        }



        public override string ToString()
        {
            return String.Format("Flappy : {0}", base.ToString());
        }
    }
}
