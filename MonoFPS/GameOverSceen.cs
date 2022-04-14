using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class GameOverSceen : Entity
    {
        private SpriteFont _font;
        private string _message;

        public GameOverSceen()
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            _message = "";

            Coroutine routine = new Coroutine(this, Routine());
            routine.Start();
        }

        private IEnumerator<double> Routine()
        {
            yield return 3.0;

            _message = "Calamity 3D";

            yield return 2.5;

            _message = "";

            yield return 2.0;
            _message = "A Game By Eric Honas";

            yield return 4.0;
            _message = "";

            yield return 7.0;
            AssetLibrary.GetAsset<TiledMap>("room_0_0").Goto();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var origin = _font.MeasureString(_message) / 2.0f;

            spriteBatch.DrawString(_font, _message, Camera.CameraSize / 2.0f, Color.White, 0.0f, origin, 3.0f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
