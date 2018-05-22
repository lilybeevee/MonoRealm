using LilyInjection.Framework.API;
using LilyInjection.Framework.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoRealm{
    public class Game1 : Game{
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D heart;
        private IContext context;

        public Game1(){
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize(){
            // TODO: Add your initialization logic here
            context = new Context(new Injector())
                      .Extend<TestExtension>()
                      .Config<TestConfig>();

            base.Initialize();
        }

        protected override void LoadContent(){
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            heart = Content.Load<Texture2D>("heart");
        }

        protected override void Update(GameTime gameTime){
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
               Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var drawpos = new Vector2(
                GraphicsDevice.Viewport.Width / 2f,
                GraphicsDevice.Viewport.Height / 2f);

            spriteBatch.Begin(SpriteSortMode.Immediate);
            spriteBatch.Draw(heart, drawpos, null, Color.White, 0f, new Vector2(heart.Width / 2f, heart.Height / 2f),
                             0.1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}