using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using HonasGame.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Collider2D _collider;
        float rotation;
        float shootAnimation;
        GraphicsDevice _gd;
        private bool falling;
        public bool InMenu { get; set; }

        public const float HEIGHT = Globals.WORLD_SCALE * 1.5f;
        public Player(float x, float z, GraphicsDevice gd)
        {
            t2D = new Transform2D(this) { Position = new Vector2(x * Globals.WORLD_SCALE, z * Globals.WORLD_SCALE) };
            y = HEIGHT;
            _collider = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(Globals.WORLD_SCALE, Globals.WORLD_SCALE) };
            _mover = new Mover2D(this);
            rotation = 0.0f;
            _privateVel = Vector2.Zero;
            shootAnimation = 0.0f;
            _gd = gd;
            falling = false;
            InMenu = true;
        }

        public override void Update(GameTime gameTime)
        {
            if(!InMenu)
            {
                Camera3D.Position = new Vector3(t2D.Position.X + (Globals.WORLD_SCALE / 2.0f), y, t2D.Position.Y + (Globals.WORLD_SCALE / 2.0f));
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

                if (combined.Length() > 500)
                {
                    combined.Normalize();
                    combined *= 500;
                }

                _velocity.X = (int)combined.X;
                _velocity.Y = (int)combined.Z;

                Vector2 actualVel = _velocity.CalculateVelocity(gameTime);

                if (!falling)
                {
                    _mover.MoveX(actualVel.X, Globals.TAG_SOLID);
                    _mover.MoveY(actualVel.Y, Globals.TAG_SOLID);

                    if (vx != 0 || vy != 0)
                    {
                        y = HonasMathHelper.LerpDelta(y, MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 7.0f) * 10 + HEIGHT, 0.2f, gameTime);
                    }
                    else
                    {
                        y = HonasMathHelper.LerpDelta(y, HEIGHT, 0.2f, gameTime);
                    }
                }

                if (Input.IsKeyPressed(Keys.Space))
                {
                    shootAnimation = 30.0f;
                    var dir = new Vector2(Camera3D.Direction.X, Camera3D.Direction.Z);
                    if(!falling) Scene.AddEntity(new Projectile(t2D.Position.X + dir.X * 50.0f, t2D.Position.Y + dir.Y * 50.0f, dir, _gd));
                }

                shootAnimation = HonasMathHelper.LerpDelta(shootAnimation, 0.0f, 0.2f, gameTime);

                if (!falling && _collider.CollidesWith(Globals.TAG_FALL))
                {
                    falling = true;
                    Scene.AddEntity(new GameOverSceen());
                }

                if (falling)
                {
                    y += 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                rotation = ((rotation + (0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds * MathHelper.PiOver4)) + MathHelper.TwoPi) % MathHelper.TwoPi;
                y = HonasMathHelper.LerpDelta(y, MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) * 10 + HEIGHT, 0.2f, gameTime);

                Camera3D.Direction = Vector3.Transform(Vector3.Forward, Matrix.CreateRotationY(rotation));
                Camera3D.Position = new Vector3(t2D.Position.X + (Globals.WORLD_SCALE / 2.0f), y, t2D.Position.Y + (Globals.WORLD_SCALE / 2.0f));
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!InMenu)
            {
                var spriteBounds = AssetLibrary.GetAsset<Texture2D>("hand").Bounds;
                float yOffset = (MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 5.0f) + 1) / 2.0f * 15.0f;
                spriteBatch.Draw(AssetLibrary.GetAsset<Texture2D>("hand"), new Vector2(shootAnimation, Camera.CameraSize.Y + yOffset), null, Color.White, 0.0f, new Vector2(0, spriteBounds.Height), 1.0f, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(AssetLibrary.GetAsset<Texture2D>("hand"), new Vector2(Camera.CameraSize.X - shootAnimation, Camera.CameraSize.Y + yOffset), null, Color.White, 0.0f, new Vector2(spriteBounds.Width, spriteBounds.Height), 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {
        }
    }
}
