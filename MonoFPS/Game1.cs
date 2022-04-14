using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.JSON;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoFPS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            Camera.CameraSize = new Vector2(640, 360);
            Camera.Bounds = new Rectangle(0, 0, 640, 360);
            Camera.Position = Vector2.Zero;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            AssetLibrary.AddAsset("rifle", Content.Load<Model>("models/Rifle1"));
            AssetLibrary.AddAsset("crate0", Content.Load<Texture2D>("textures/crate0"));
            AssetLibrary.AddAsset("floor", Content.Load<Texture2D>("textures/floor"));
            AssetLibrary.AddAsset("ceiling", Content.Load<Texture2D>("textures/ceiling"));
            AssetLibrary.AddAsset("enemy", Content.Load<Texture2D>("textures/enemy"));
            AssetLibrary.AddAsset("hand", Content.Load<Texture2D>("textures/hand"));
            AssetLibrary.AddAsset("projectile", Content.Load<Texture2D>("textures/projectile"));
            AssetLibrary.AddAsset("room_0_0", new TiledMap(JSON.FromFile("Content/rooms/room_0_0.json") as JObject));

            AssetLibrary.AddAsset("fntText", Content.Load<SpriteFont>("fonts/fntText"));

            TiledManager.AddSpawnerDefinition("Wall", (obj) => new Wall(new Vector3(obj.X / 32.0f, 0.0f, obj.Y / 32.0f), obj.Width / 32.0f, obj.Height / 32.0f, GraphicsDevice));
            TiledManager.AddSpawnerDefinition("Floor", (obj) => new Floor(new Vector3(obj.X / 32.0f, 0.0f, obj.Y / 32.0f), obj.Width / 32.0f, obj.Height / 32.0f, GraphicsDevice));
            TiledManager.AddSpawnerDefinition("Ceiling", (obj) => new Ceiling(new Vector3(obj.X / 32.0f, 3.0f, obj.Y / 32.0f), obj.Width / 32.0f, obj.Height / 32.0f, GraphicsDevice));
            TiledManager.AddSpawnerDefinition("Player", (obj) => new Player(obj.X / 32.0f, obj.Y / 32.0f, GraphicsDevice));
            TiledManager.AddSpawnerDefinition("Enemy", (obj) => new Enemy(new Vector3(obj.X / 32.0f, 0, obj.Y / 32.0f), obj.Width / 32.0f, obj.Height / 32.0f, GraphicsDevice));
            TiledManager.AddSpawnerDefinition("Fall", (obj) => new Fall(obj.X / 32.0f, obj.Y / 32.0f, obj.Width / 32.0f, obj.Height / 32.0f));
            TiledManager.AddSpawnerDefinition("Menu", (obj) => new Menu());

            AssetLibrary.GetAsset<TiledMap>("room_0_0").Goto();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Scene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Scene.Draw(gameTime, _spriteBatch, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));

            base.Draw(gameTime);
        }
    }
}
