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

        private Rectangle hitbox = new Rectangle();


        public Vector2 Position
        {

            get { return position; }
            set { position = value; }
        }

        public bool IsVisable
        {

            get { return isVisable; }
            set { isVisable = value; }

        }
        public Rectangle Hitbox
        {

            get { return hitbox; }

        }

        public Bullet(Texture2D newTexture, Vector2 startPos)
        {
            texture = newTexture;
            position = startPos - new Vector2(-5, 50);
            position.X += 75;
            isVisable = true;



            hitbox.Location = position.ToPoint();
            hitbox.Size = new Point(20, 50);

        }

        public void Update()
        {
            position.Y -= speed;

            hitbox.Location = position.ToPoint();

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.Lime);
        }
    }
}
