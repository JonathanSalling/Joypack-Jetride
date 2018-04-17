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
        Enemy enemy;
        Player player;
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
            player = new Player(TextureLibrary.GetTexture("PlayerTexture"), new Vector2(20, 1), 300, new Vector2(1, 1), Color.White);
            enemy = new Enemy(TextureLibrary.GetTexture("Enemy"), new Vector2(100, 1), 300, new Vector2(1, 1), Color.White);
            position = new Vector2(20, 1);
            speed = 5;
            scale = new Vector2(0.2f, 0.2f);
            offset = (TextureLibrary.GetTexture("PlayerTexture").Bounds.Size.ToVector2() / 2.0f) * scale;
            playerRectangle = new Rectangle((position - offset).ToPoint(), (TextureLibrary.GetTexture("PlayerTexture").Bounds.Size.ToVector2() * scale).ToPoint());
            playerRectangle = TextureLibrary.GetTexture("PlayerTexture").Bounds;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLibrary.LoadTexture(Content, "PlayerTexture");
            TextureLibrary.LoadTexture(Content, "Enemy");

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
            enemy.Update(gameTime, player);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                moveDir.Y = -1f;
            }
            else
            {
                moveDir.Y = 1f;
            }
            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                position += (moveDir * speed);
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
            //enemy.Draw(spriteBatch);
            spriteBatch.Draw(TextureLibrary.GetTexture("PlayerTexture"), position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
