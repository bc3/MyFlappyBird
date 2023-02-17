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
    public class Ground : MovableObject
    {
        private Texture2D tex;

        private TimeSpan lastShot;

        private TimeSpan lastTime;
        public MovableObject circleSpeed;

        private float horSpeed = 50;

        public Ground(MovableObjectProperties mop)
            : base(mop)
        {
            this.gop.offscreen = false;
            this.gop.mass = 10;
            this.gop.causescollision = true;
            this.gop.velocity = new Vector2(0, 0);
            this.tex = mop.world.content.Load<Texture2D>("Flappy-Ground");
     
            
        }

        public void Stop()
        {
            horSpeed = 0;
        }

        public override Rectangle getBoundingRect()
        {
            return new Rectangle(0, 412, 270,100);
        }

        public override void Update(GameTime gametime)
        {

            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            KeyboardState keys = Keyboard.GetState();


            this.gop.pos.X -= horSpeed*dt;

      



            base.Update(gametime);

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {

            VectorA tmp = new VectorA(this.gop.velocity);

            var secondSprite = new Vector2(this.gop.pos.X - 336, this.gop.pos.Y);

            var rect = new Rectangle(0, 0, 336, 100);
            spriteBatch.Draw(tex,this.gop.pos,rect,Color.White,0,new Vector2(1,1),new Vector2(1,1),SpriteEffects.None,this.gop.zindex  );
            spriteBatch.Draw(tex, secondSprite, rect, Color.White,0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffects.None, this.gop.zindex);

           if (this.gop.world.diagnostics)
           {
               spriteBatch.DrawString(this.gop.world.fontCourier, String.Format("[Ground: {0}]", this.gop.pos.ToString()), new Vector2(0, 20), Color.Yellow);

           }

            base.Draw(spriteBatch, gametime);
        }



        public override string ToString()
        {
            return String.Format("Ground : {0} / BoundingRect: {1}", base.ToString(),getBoundingRect().ToString());
        }
    }
}
