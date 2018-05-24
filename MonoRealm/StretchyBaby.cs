using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoRealm{
    public class StretchyBaby : Baby{
        public StretchyBaby(Game1 game, Texture2D texture, Vector3 position) : base(game, texture, position){ }

        public override void Draw(SpriteBatch spriteBatch){
            _vert[0].Position = _game.Camera.ApplyToVertex(new Vector3(-0.5f, 0.5f, 1));
            _vert[1].Position = _game.Camera.ApplyToVertex(new Vector3(0.5f, 0.5f, 1));
            _vert[2].Position = new Vector3(4, -4, 0);
            _vert[3].Position = new Vector3(-4, -4, 0);
            base.Draw(spriteBatch);
            _vert[0].Position = _game.Camera.ApplyToVertex(new Vector3(-0.5f, -0.5f, 1));
            _vert[1].Position = _game.Camera.ApplyToVertex(new Vector3(-0.5f, 0.5f, 1));
            _vert[2].Position = new Vector3(4, 4, 0);
            _vert[3].Position = new Vector3(4, -4, 0);
            base.Draw(spriteBatch);
        }
    }
}