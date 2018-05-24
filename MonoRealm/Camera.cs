using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoRealm{
    public class Camera{
        private Game1 _game;
        public float Rotation;
        public float Scale = 5f;

        public Camera(Game1 game){
            _game = game;
        }

        public void Update(){
            if(Keyboard.GetState().IsKeyDown(Keys.Q))
                Rotation += 2f;
            if(Keyboard.GetState().IsKeyDown(Keys.E))
                Rotation -= 2f;
        }

        public Matrix GetMatrix(){
            return Matrix.CreateRotationZ(Rotation * (float)(Math.PI / 180))
                   * Matrix.CreateScale(Scale, Scale, 1f)
                   * Matrix.CreateTranslation(_game.GraphicsDevice.Viewport.Width / 2f,
                                              _game.GraphicsDevice.Viewport.Height / 2f, 0);
        }

        public Vector2 ApplyTo(Vector3 pos){
            var newX = (pos.X + -((float)Math.Cos((Rotation - 90) * (Math.PI / 180)) * pos.Z)) * 8;
            var newY = (pos.Y + ((float)Math.Sin((Rotation - 90) * (Math.PI / 180)) * pos.Z)) * 8;
            return new Vector2(newX, newY);
        }

        public Vector3 ApplyToVertex(Vector3 vert) => ApplyToVertex(vert.X, vert.Y, vert.Z);

        public Vector3 ApplyToVertex(float x, float y, float z){
            var newX = (x + ((float)Math.Cos((Rotation - 90) * (Math.PI / 180)) * z)) * 8;
            var newY = (y + ((float)Math.Sin((Rotation - 90) * (Math.PI / 180)) * z)) * 8;
            return new Vector3(-newX, -newY, 0);
        }
    }
}