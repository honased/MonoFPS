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
    public class Wall : Entity
    {
        private IndexBuffer _ibo;
        private VertexBuffer _vbo;
        private BasicEffect _effect;
        private GraphicsDevice _gd;
        private Vector3 _position;
        private Vector3 _scale;

        public Wall(Vector3 position, float width, float length, GraphicsDevice gd)
        {
            CubeConstructor.InitializeCube(width, 2, length, gd, out _vbo, out _ibo);
            _gd = gd;
            _position = position * Globals.WORLD_SCALE;
            _scale = new Vector3(width * Globals.WORLD_SCALE, 2 * Globals.WORLD_SCALE, length * Globals.WORLD_SCALE);

            var t2D = new Transform2D(this) { Position = new Vector2(_position.X, _position.Z) };
            new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(width * Globals.WORLD_SCALE, length * Globals.WORLD_SCALE), Tag = Globals.TAG_SOLID };

            InitializeEffect();
        }

        private void InitializeEffect()
        {
            _effect = new BasicEffect(_gd);
            _effect.World = Matrix.CreateScale(_scale) * Matrix.CreateTranslation(_position);
            _effect.View = Camera3D.View;
            _effect.Projection = Camera3D.Projection;
            _effect.TextureEnabled = true;
            _effect.Texture = AssetLibrary.GetAsset<Texture2D>("crate0");
            _effect.LightingEnabled = true;
            _effect.DirectionalLight0.Enabled = true;
            _effect.DirectionalLight0.Direction = new Vector3(1.0f, 1.0f, 1.0f);
            _effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            _effect.DirectionalLight0.SpecularColor = new Vector3(0.0f, 0.0f, 0.25f);

            _effect.DirectionalLight1.Enabled = true;
            _effect.DirectionalLight1.Direction = new Vector3(-1.0f, 1.0f, 1.0f);
            _effect.DirectionalLight1.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            _effect.DirectionalLight1.SpecularColor = new Vector3(0.0f, 0.0f, 0.25f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _effect.View = Camera3D.View;
            _effect.CurrentTechnique.Passes[0].Apply();

            _gd.SetVertexBuffer(_vbo);
            _gd.Indices = _ibo;
            _gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {
            
        }
    }
}
