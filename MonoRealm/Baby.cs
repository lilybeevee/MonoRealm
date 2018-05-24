using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoRealm{
    public class Baby{
        protected BasicEffect _basicEffect;
        protected VertexPositionTexture[] _vert;
        protected short[] _ind;
        protected Game1 _game;
        protected Texture2D _texture;
        protected Vector3 _position;

        public Baby(Game1 game, Texture2D texture, Vector3 position){
            _game = game;
            _texture = texture;
            _position = position;

            _basicEffect = new BasicEffect(_game.GraphicsDevice);
            _basicEffect.View = Matrix.Identity;
            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height, 0, 0, 1);
            _basicEffect.World = Matrix.Identity;
            _basicEffect.Texture = _texture;
            _basicEffect.TextureEnabled = true;

            _vert = new VertexPositionTexture[4];

            _vert[0].Position = new Vector3(4f, 4f, 0);
            _vert[1].Position = new Vector3(-4f, 4f, 0);
            _vert[2].Position = new Vector3(4f, -4f, 0);
            _vert[3].Position = new Vector3(-4f, -4f, 0);

            _vert[0].TextureCoordinate = new Vector2(0, 0);
            _vert[1].TextureCoordinate = new Vector2(1, 0);
            _vert[2].TextureCoordinate = new Vector2(0, 1);
            _vert[3].TextureCoordinate = new Vector2(1, 1);

            _ind = new short[6];
            _ind[0] = 0;
            _ind[1] = 2;
            _ind[2] = 1;
            _ind[3] = 1;
            _ind[4] = 2;
            _ind[5] = 3;
        }

        public virtual void Draw(SpriteBatch spriteBatch){
            _basicEffect.World = Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(new Vector3(_game.Camera.ApplyTo(_position), 0)) * _game.Camera.GetMatrix();
            foreach(var effectPass in _basicEffect.CurrentTechnique.Passes){
                effectPass.Apply();
                _game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList, _vert, 0, _vert.Length, _ind, 0, _ind.Length / 3);
            }
        }
    }
}