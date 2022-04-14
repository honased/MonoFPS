using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class Fall : Entity
    {
        public Fall(float x, float z, float w, float h)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, z) * Globals.WORLD_SCALE };
            new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(w * Globals.WORLD_SCALE, h * Globals.WORLD_SCALE), Tag = Globals.TAG_FALL };
        }

        protected override void Cleanup()
        {

        }
    }
}
