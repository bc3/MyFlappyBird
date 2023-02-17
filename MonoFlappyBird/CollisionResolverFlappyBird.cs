using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FlappyBird
{
    class CollisionResolverFlappyBird : CollisionResolver
    {


        public CollisionResolverFlappyBird(World w)
            : base(w)
        { }

        public override void ResolveCollision(GameTime gametime, GameObject a, GameObject other)
        {
            if (a is Flappy && other is PipesBottom)
            {
                a.gop.world.gameover = true;
            }

            if (a is Flappy && other is PipesTop)
            {
                a.gop.world.gameover = true;
            }

            if (a is Flappy && other is Ground)
            {
                a.gop.world.gameover = true;
            }

        }

        //protected void MakeExplosion(Vector2 pos, float scale)
        //{
        //    AnimationObjectProperties aop = new AnimationObjectProperties() { world = w, pos = pos, scale = scale };
        //    w.AddObject(new Explosion(aop));
        //}



    }
}
