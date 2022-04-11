using HonasGame;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using HonasGame.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class Player : Entity
    {
        Transform2D t2D;
        Vector2 _privateVel;
        Velocity2D _velocity;
        float y;
        Mover2D _mover;
        float rotation;

        private const float HEIGHT = Globals.WORLD_SCALE * 1.5f;
        public Player(float x, float z)
        {
            t2D = new Transform2D(this) { Position = new Vector2(x * Globals.WORLD_SCALE, z * Globals.WORLD_SCALE) };
            y = HEIGHT;
            var c2D = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(Globals.WORLD_SCALE, Globals.WORLD_SCALE) { Offset = new Vector2(-Globals.WORLD_SCALE / 2.0f, -Globals.WORLD_SCALE/2.0f) } };
            _mover = new Mover2D(this);
            rotation = 0.0f;
            _privateVel = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Camera3D.Position = new Vector3(t2D.Position.X, y, t2D.Position.Y);
            int vx, vy;
            vx = ((Input.IsKeyDown(Keys.A)) ? 1 : 0) - ((Input.IsKeyDown(Keys.D)) ? 1 : 0);
            vy = ((Input.IsKeyDown(Keys.S)) ? 1 : 0) - ((Input.IsKeyDown(Keys.W)) ? 1 : 0);

            float rx = ((Input.IsKeyDown(Keys.Left)) ? 1 : 0) - ((Input.IsKeyDown(Keys.Right)) ? 1 : 0);

            rotation = ((rotation + (rx * (float)gameTime.ElapsedGameTime.TotalSeconds * MathHelper.Pi)) + MathHelper.TwoPi) % MathHelper.TwoPi;
            Camera3D.Direction = Vector3.Transform(Vector3.Forward, Matrix.CreateRotationY(rotation));

            _privateVel.X = HonasMathHelper.LerpDelta(_privateVel.X, vx * 500.0f, 0.2f, gameTime);
            _privateVel.Y = HonasMathHelper.LerpDelta(_privateVel.Y, vy * 300.0f, 0.2f, gameTime);

            var forwardMovement = Camera3D.Direction * -_privateVel.Y;
            var sideMovement = Vector3.Cross(Vector3.Up, Camera3D.Direction) * _privateVel.X;
            var combined = forwardMovement + sideMovement;

            if(combined.Length() > 500)
            {
                combined.Normalize();
                combined *= 500;
            }

            _velocity.X = (int)combined.X;
            _velocity.Y = (int)combined.Z;

            Vector2 actualVel = _velocity.CalculateVelocity(gameTime);

            _mover.MoveX(actualVel.X, Globals.TAG_SOLID);
            _mover.MoveY(actualVel.Y, Globals.TAG_SOLID);

            if(vx != 0 || vy != 0)
            {
                y = HonasMathHelper.LerpDelta(y, MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 7.0f) * 10 + HEIGHT, 0.2f, gameTime);
            }
            else
            {
                y = HonasMathHelper.LerpDelta(y, HEIGHT, 0.2f, gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Cleanup()
        {
        }
    }
}
