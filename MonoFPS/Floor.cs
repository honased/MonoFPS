using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class Floor : Entity
    {
        private IndexBuffer _ibo;
        private VertexBuffer _vbo;
        private BasicEffect _effect;
        private GraphicsDevice _gd;
        private Vector3 _position;
        private Vector3 _scale;

        public Floor(Vector3 position, float width, float length, GraphicsDevice gd)
        {
            FloorConstructor.InitializeFloor(width, length, gd, out _vbo, out _ibo);
            _gd = gd;
            _position = position * Globals.WORLD_SCALE;
            _scale = new Vector3(width * Globals.WORLD_SCALE, 2 * Globals.WORLD_SCALE, length * Globals.WORLD_SCALE);

            InitializeEffect();
        }

        private void InitializeEffect()
        {
            _effect = new BasicEffect(_gd);
            _effect.World = Matrix.CreateScale(_scale) * Matrix.CreateTranslation(_position);
            _effect.View = Camera3D.View;
            _effect.Projection = Camera3D.Projection;
            _effect.TextureEnabled = true;
            _effect.Texture = AssetLibrary.GetAsset<Texture2D>("floor");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _effect.View = Camera3D.View;
            _effect.CurrentTechnique.Passes[0].Apply();

            _gd.SetVertexBuffer(_vbo);
            _gd.Indices = _ibo;
            _gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
