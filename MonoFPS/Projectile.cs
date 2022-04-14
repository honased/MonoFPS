using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class Projectile : Entity
    {
        private IndexBuffer _ibo;
        private VertexBuffer _vbo;
        private AlphaTestEffect _effect;
        private GraphicsDevice _gd;
        private Mover2D _mover;
        private Transform2D t2D;
        private Vector2 _dir;

        public Projectile(float x, float z, Vector2 direction, GraphicsDevice gd)
        {
            t2D = new Transform2D(this) { Position = new Vector2(x, z) };
            Collider2D collider = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(Globals.WORLD_SCALE / 2.0f, Globals.WORLD_SCALE / 2.0f) { Offset = new Vector2(0.25f, 0.25f) * -Globals.WORLD_SCALE } };
            
            t2D.Position += new Vector2(0.5f, 0.5f) * Globals.WORLD_SCALE;

            _mover = new Mover2D(this);

            QuadConstructor.InitializeQuad(1, 1, gd, out _vbo, out _ibo);
            _gd = gd;
            _dir = direction;

            InitializeEffect();
        }

        private void InitializeEffect()
        {
            _effect = new AlphaTestEffect(_gd);
            _effect.World = Matrix.CreateScale(Globals.WORLD_SCALE / 2.0f) * Matrix.CreateTranslation(new Vector3(t2D.Position.X, Player.HEIGHT / 1.5f, t2D.Position.Y));
            _effect.View = Camera3D.View;
            _effect.Projection = Camera3D.Projection;
            _effect.Texture = AssetLibrary.GetAsset<Texture2D>("projectile");
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CheckDeath(_mover.MoveX(_dir.X * 1300 * t, Globals.TAG_SOLID | Globals.TAG_ENEMY));
            CheckDeath(_mover.MoveY(_dir.Y * 1300 * t, Globals.TAG_SOLID | Globals.TAG_ENEMY));

            base.Update(gameTime);
        }

        private void CheckDeath(Entity e)
        {
            if (e == null) return;

            if(e.GetComponent<Collider2D>(out var collider) && e is Enemy)
            {
                e.Destroy();
            }

            Destroy();
        }

        public override void Draw3D(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _effect.View = Camera3D.View;
            _effect.World = Matrix.CreateScale(Globals.WORLD_SCALE / 2.0f) * Matrix.CreateConstrainedBillboard(new Vector3(t2D.Position.X, Player.HEIGHT / 1.5f, t2D.Position.Y), Camera3D.Position, Vector3.UnitY, Camera3D.Direction, null);
            _effect.CurrentTechnique.Passes[0].Apply();

            _gd.SetVertexBuffer(_vbo);
            _gd.Indices = _ibo;
            _gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);

            base.Draw3D(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {
            
        }
    }
}
