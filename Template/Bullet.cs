using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    class Bullet : Basklass
    {

        private float speed = 7.5f;
        private Vector2 origin;

        private bool isVisable;

        public Vector2 Position
        {

            get { return position; }
            set { position = value; }
        }

        public bool IsVisable
        {

            get { return isVisable; }

        }

        public Bullet(Texture2D newTexture, Vector2 startPos)
        {
            texture = newTexture;
            position = startPos;
            position.X += 75;
            isVisable = true;

        }

        public void Update()
        {
            position.Y -= speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)-Math.PI / 2, origin, .1f, SpriteEffects.None, 0);
        }
    }
}
