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
    public class Enemy : Entity
    {
        private IndexBuffer _ibo;
        private VertexBuffer _vbo;
        private AlphaTestEffect _effect;
        private GraphicsDevice _gd;
        private Vector3 _position;
        private Vector3 _scale;

        public Enemy(Vector3 position, float width, float height, GraphicsDevice gd)
        {
            QuadConstructor.InitializeQuad(width, height, gd, out _vbo, out _ibo);
            _gd = gd;
            _position = position * Globals.WORLD_SCALE + (new Vector3(0.5f, 0, 0.5f) * Globals.WORLD_SCALE);
            _scale = new Vector3(width * Globals.WORLD_SCALE, 2 * Globals.WORLD_SCALE, 1);

            Transform2D t2D = new Transform2D(this) { Position = new Vector2(_position.X, _position.Z) };
            Collider2D collider = new Collider2D(this) { Shape = new BoundingRectangle(Globals.WORLD_SCALE * 0.5f, Globals.WORLD_SCALE * 0.5f), Transform = t2D, Tag = Globals.TAG_ENEMY };

            InitializeEffect();
        }

        private void InitializeEffect()
        {
            _effect = new AlphaTestEffect(_gd);
            _effect.World = Matrix.CreateScale(_scale) * Matrix.CreateTranslation(_position);
            _effect.View = Camera3D.View;
            _effect.Projection = Camera3D.Projection;
            _effect.Texture = AssetLibrary.GetAsset<Texture2D>("enemy");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw3D(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _effect.View = Camera3D.View;
            _effect.World = Matrix.CreateScale(_scale) * Matrix.CreateConstrainedBillboard(_position, Camera3D.Position, Vector3.UnitY, Camera3D.Direction, null);
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
