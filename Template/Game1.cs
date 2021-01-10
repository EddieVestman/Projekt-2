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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum GameState
        {
            MainMenu,
            Game,
            Ending
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int Height;
        public static int Width;
        //player
        Texture2D plane;
        Rectangle playerPos;
       
        //background
        Texture2D background;
        Vector2 backgroundpos;
        
        //bullets
        List<Bullet> bullets = new List<Bullet>();
        Texture2D bulletTexture;
        Texture2D enemyTexture;
        Rectangle bulletRectangle;

        //enemies
        List<Enemy> enemies = new List<Enemy>();
        Random random = new Random();
        Rectangle enemyRectangle;

        


        KeyboardState koldstate;

        //Komentar
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Height = graphics.PreferredBackBufferHeight = 1080;
            Width = graphics.PreferredBackBufferWidth = 1920;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            playerPos = new Rectangle(new Point(0, 0), new Point(180, 135));
            playerPos.Y = graphics.PreferredBackBufferHeight - playerPos.Height;
            playerPos.X = graphics.PreferredBackBufferWidth / 2 - playerPos.Width / 2;

            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           background = Content.Load<Texture2D>("spacebild");
            plane = Content.Load<Texture2D>("lasercannon");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            bulletRectangle = new Rectangle(300, 300, bulletTexture.Width, bulletTexture.Height);
            enemyTexture = Content.Load<Texture2D>("Enemy1");
            enemyRectangle = new Rectangle(350, 550, enemyTexture.Width, enemyTexture.Height);
            // TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        public void Updatebullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].IsVisable)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot()
        {
            Bullet newBullet = new Bullet(bulletTexture, playerPos.Location.ToVector2());

            bullets.Add(newBullet);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState knewstate = Keyboard.GetState();
            if (knewstate.IsKeyDown(Keys.D) && playerPos.X < graphics.PreferredBackBufferWidth - playerPos.Width)
                playerPos.X += 20;
            if (knewstate.IsKeyDown(Keys.A) && playerPos.X > 0)
                playerPos.X -= 20;

            Updatebullets();

            if (knewstate.IsKeyDown(Keys.Space) && !koldstate.IsKeyDown(Keys.Space))
            {
                Shoot();
            }

            spawn += 2;
            foreach (Enemy enemy in enemies)
                enemy.Update(graphics.GraphicsDevice);

            base.Update(gameTime);

            Collision();

            LoadEnemies();
            koldstate = knewstate;
        }

        public void LoadEnemies()
        {
            int randX = random.Next(0, GraphicsDevice.Viewport.Width - enemyTexture.Width);
            if (spawn >= 1)
            {
                spawn = 0;
                if (enemies.Count() < 4)
                    enemies.Add(new Enemy(enemyTexture, new Vector2(randX, -300)));

            }
            for(int i = 0; i < enemies.Count; i++)
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           

            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            Rectangle backgroundRec = new Rectangle();
            backgroundRec.Location = backgroundpos.ToPoint();
            backgroundRec.Size = new Point(Width, Height);
            spriteBatch.Draw(background, new Rectangle(0, 0, Width, Height), Color.White);
            spriteBatch.Draw(plane, playerPos, Color.White);
           
            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
            spriteBatch.End();
           

            base.Draw(gameTime);
        }

        public void Collision()
        {
            foreach (Enemy enemy in enemies)
            {
                foreach (Bullet bullet in bullets)
                {
                    if (bullet.Hitbox.Intersects(enemy.Hitbox))
                    {
                        enemy.isVisible = false;
                        bullet.IsVisable = false;
                    }

                }
                if (enemy.Hitbox.Intersects(playerPos))
                {
                    Exit();
                }

            }
        }
    }
}
