using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using HonasGame.Rendering;
using Microsoft.Xna.Framework.Input;

namespace MonoFPS
{
    public class Menu : Entity
    {
        private SpriteFont _font;

        public Menu()
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
        }

        public override void Update(GameTime gameTime)
        {
            if(Input.IsKeyPressed(Keys.Enter))
            {
                if(Scene.GetEntity<Player>(out var player))
                {
                    player.InMenu = false;
                }

                Destroy();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(Camera.Bounds, Color.FromNonPremultiplied(0, 0, 0, 150));
            var str = "Calamity 3D";
            var origin = _font.MeasureString(str) / 2.0f;
            DrawWithOutline(spriteBatch, str, new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 3.0f), origin, 4.0f);

            str = "Move with WASD / Rotate with Arrow Keys";
            origin = _font.MeasureString(str) / 2.0f;
            DrawWithOutline(spriteBatch, str, Camera.CameraSize / 2.0f, origin, 2.0f);

            str = "Shoot with [Space]";
            origin = _font.MeasureString(str) / 2.0f;
            DrawWithOutline(spriteBatch, str, new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 1.75f), origin, 2.0f);

            str = "[Enter] To Start, [ESC] To Quit";
            origin = _font.MeasureString(str) / 2.0f;
            DrawWithOutline(spriteBatch, str, new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 1.25f), origin, 2.0f);

            base.Draw(gameTime, spriteBatch);
        }

        private void DrawWithOutline(SpriteBatch spriteBatch, string str, Vector2 position, Vector2 origin, float scale)
        {
            spriteBatch.DrawString(_font, str, position + Vector2.UnitX, Color.Black, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, str, position - Vector2.UnitX, Color.Black, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, str, position + Vector2.UnitY, Color.Black, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, str, position - Vector2.UnitY, Color.Black, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, str, position, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }

        protected override void Cleanup()
        {

        }
    }
}
