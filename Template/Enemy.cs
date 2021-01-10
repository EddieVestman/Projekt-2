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
    class Enemy
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;

        public bool isVisible = true;

        private Rectangle hitbox = new Rectangle();

        Random random = new Random();
        int randX, randY;

        public Rectangle Hitbox
        {

            get { return hitbox; }

        }

        public Enemy(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
                 
            randX = random.Next(-1, 1);
            randY = random.Next(5, 8);

            velocity = new Vector2(randX, randY);

            hitbox.Location = position.ToPoint();
            hitbox.Size = new Point(200, 200);
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.X <= 0 || position.X >= graphics.Viewport.Width - texture.Width)
                velocity.X = -velocity.X;

            if (position.Y > graphics.Viewport.Height)
                isVisible = false;

            hitbox.Location = position.ToPoint();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }

    }
}
