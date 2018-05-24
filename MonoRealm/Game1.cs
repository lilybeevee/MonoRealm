using System.Collections.Generic;
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
        private List<Baby> _babies = new List<Baby>();

        public Camera Camera;

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

            Camera = new Camera(this);

            base.Initialize();
        }

        protected override void LoadContent(){
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            heart = Content.Load<Texture2D>("heart");
            var floor = Content.Load<Texture2D>("floor");
            var wallTop = Content.Load<Texture2D>("walltop");
            var wallSide = Content.Load<Texture2D>("wallside");

            for(int x = -1; x <= 1; x++){
                for(int y = -1; y <= 1; y++){
                    _babies.Add(new Baby(this, floor, new Vector3(x, y, 0)));
                }
            }
            _babies.Add(new Baby(this, wallTop, new Vector3(-2, 0, 1)));
            _babies.Add(new StretchyBaby(this, wallSide, new Vector3(-2, 0, 0)));
        }

        protected override void Update(GameTime gameTime){
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
               Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetMatrix());
            foreach(var i in _babies)
                i.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}