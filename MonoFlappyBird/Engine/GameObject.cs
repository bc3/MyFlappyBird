using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird
{
    public class GameObject : DrawableGameComponent, ICloneable
    {
        public GameObjectProperties gop;


        public GameObject(GameObjectProperties gop)
            : base(gop.world.game)
        {
            this.gop = gop;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (gop.world.diagnostics) spriteBatch.DrawString(gop.world.fontCourier, this.ToString(), this.gop.pos, Color.Red);


        }

        public override string ToString()
        {
            return this.getBoundingRect().ToString();
        }

        public virtual Rectangle getBoundingRect()
        {
            int ballHeight = (int)(gop.radius * 2 * 0.80f);
            int ballWidth = (int)(gop.radius * 2 * 0.80f);
            int x = (int)gop.pos.X - ballWidth / 2;
            int y = (int)gop.pos.Y - ballHeight / 2;

            return new Rectangle(x, y, ballHeight, ballWidth);
        }

        public object Clone()
        {
            return ((object)this).CloneObject();
        }
    }
}
