using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    public class PipesTop : MovableObject
    {
        private Texture2D tex;

        private TimeSpan lastShot;

        private TimeSpan lastTime;
        public MovableObject circleSpeed;

        private const float maxY = 512;
        private const float minY = 0;


        
        public float height;
        private int h1;
        private int h2;
        private Texture2D debugtex;

        public PipesTop(MovableObjectProperties mop,float height)
            : base(mop)
        {
            this.gop.offscreen = true;
            this.gop.mass = 10;
            this.gop.causescollision = true;
            this.gop.velocity = new Vector2(0, 0);
            this.tex = mop.world.content.Load<Texture2D>("Flappy-Pipes");
            this.debugtex = this.gop.world.content.Load<Texture2D>("pixel");
            //health parameters
            this.height = height;
            this.h1 = (int) (height - 40);
            this.h2 = (int) (height + 40);
            
        }



        public override void Update(GameTime gametime)
        {

            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            KeyboardState keys = Keyboard.GetState();


            this.gop.pos.X -= (this.gop.world as WorldFlappyBird).horSpeed*dt;

      



            base.Update(gametime);

        }

        public override Rectangle getBoundingRect()
        {
            return new Rectangle((int)this.gop.pos.X, 0, 52,h1);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {

            VectorA tmp = new VectorA(this.gop.velocity);


            spriteBatch.Draw(tex, new Rectangle((int)this.gop.pos.X, 0, 52, h1), new Rectangle(112, 320-h1, 52, h1), Color.White,0,new Vector2(1,1), SpriteEffects.None, this.gop.zindex);
            //spriteBatch.Draw(tex, new Rectangle((int)this.gop.pos.X, h2, 52, this.gop.world.height - h2), new Rectangle(168, 0, 52, this.gop.world.height - h2), Color.White, 0, new Vector2(1,1), SpriteEffects.None, this.gop.zindex);


            //spriteBatch.Draw(tex, this.gop.pos, new Rectangle(168,0,52,h1), Color.White, tmp.Angle.Value, new Vector2(this.gop.radius, this.gop.radius), new Vector2(1, 1), SpriteEffects.None, this.gop.zindex);
            //spriteBatch.Draw(tex, this.gop.pos, new Rectangle(112, 0, 52,h2 ), Color.White, tmp.Angle.Value, new Vector2(this.gop.radius, this.gop.radius), new Vector2(1, 1), SpriteEffects.None, this.gop.zindex);

            //if (this.gop.world.diagnostics)
            //{
            //    spriteBatch.DrawString(this.gop.world.fontCourier, String.Format("[X: {0} / Y: {1}] [Velocity: {2}]", this.gop.pos.X.ToString("#.00"), this.gop.pos.Y.ToString("#.00"), tmp.ToString(), this), new Vector2(0, 0), Color.Yellow);

            //}

            if (this.gop.world.diagnostics)
            {
                spriteBatch.DrawString(this.gop.world.fontCourier, "T " + this.getBoundingRect().ToString(), new Vector2(0, h1), Color.Yellow);
                spriteBatch.Draw(debugtex, this.getBoundingRect(), Color.Yellow);
            }


            base.Draw(spriteBatch, gametime);
        }



        public override string ToString()
        {
            return String.Format("PipesTop : {0} / BoundingRect: {1}", base.ToString(), getBoundingRect().ToString());
        }
    }
}
