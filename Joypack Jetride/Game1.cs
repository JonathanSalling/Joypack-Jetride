using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Joypack_Jetride
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Camera camera;
        Bullet bullet;
        Enemy enemy;
        Player player;
        Ground ground;
        Texture2D bulletTexture;
        Rectangle bulletRectangle;
        Texture2D playerTexture;
        Rectangle groundRectangle;
        Texture2D enemyTexture;
        Rectangle enemyRectangle;
        Texture2D groundTexture;
        Rectangle playerRectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        float speed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            player = new Player(playerTexture, new Vector2(20, 1), 5, new Vector2(1, 1), Color.White);
            enemy = new Enemy(enemyTexture, new Vector2(100, 1), 60, new Vector2(1, 1), Color.White);
            ground = new Ground(groundTexture, new Vector2(20, 0), new Vector2(20, 1), Color.White);
            position = new Vector2(20, 1);
            speed = 5;
            scale = new Vector2(0.2f, 0.2f);
            offset = (playerTexture.Bounds.Size.ToVector2() / 2.0f) * scale;
            playerRectangle = new Rectangle((position - offset).ToPoint(), (playerTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            //playerRectangle = playerTexture.Bounds;
            groundRectangle = groundTexture.Bounds;
            groundRectangle.Location = ground.AccessPosition.ToPoint();
            enemyRectangle.Location = ground.AccessPosition.ToPoint();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bulletTexture = Content.Load<Texture2D>("Bullet");
            playerTexture = Content.Load<Texture2D>("PlayerTexture");
            enemyTexture = Content.Load<Texture2D>("Enemy");
            groundTexture = Content.Load<Texture2D>("Ground");

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float pixelsToMove = speed * deltaTime;
            enemy.Update(gameTime, player);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                speed = -5f;
            }
            else
            {
                moveDir.Y = 5f;
            }
            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                position += (moveDir * speed);
                playerRectangle.Location = position.ToPoint();
            }
            if (playerRectangle.Intersects(groundRectangle))
            {
                speed = 0;   
            }
            if (enemyRectangle.Intersects(groundRectangle))
            {
                
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                speed = 5;
            }
            if (enemyTexture.Bounds.Location.Y != playerTexture.Bounds.Location.Y)
            {
                
            }

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            enemy.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            bullet.Draw(spriteBatch);
            //player.Draw(spriteBatch);
            //spriteBatch.Draw(groundTexture, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(playerTexture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
