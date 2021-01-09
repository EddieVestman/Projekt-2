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
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Enemy(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            randX = random.Next(-11, 1);
            randY = random.Next(5, 8);

            velocity = new Vector2(randX, randY);
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.X <= 0 || position.X >= graphics.Viewport.Width - texture.Width)
                velocity.X = -velocity.X;

            if (position.Y > graphics.Viewport.Height)
                isVisible = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
