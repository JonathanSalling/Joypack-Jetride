using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Joypack_Jetride
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Enemy enemy;
        Player player;
        Ground ground;
        Texture2D bulletTexture;
        Texture2D playerTexture;
        Rectangle groundRectangle;
        Texture2D groundTexture;
        Rectangle playerRectangle;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Random random;
        int numEnemys;
        List<Enemy> enemys;
        Dictionary<string, Texture2D> textures;
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
            random = new Random();
            enemys = new List<Enemy>();
            textures = new Dictionary<string, Texture2D>();
            numEnemys = 1;
            // TODO: Add your initialization logic here
            base.Initialize();
            player = new Player(TextureLibrary.GetTexture("PlayerTexture"), new Vector2(20, 1), 5, new Vector2(1, 1), Color.White, 1000, 1);
            enemy = new Enemy(TextureLibrary.GetTexture("Enemy"), new Vector2(100, 1), 5, new Vector2(1, 1), Color.White, 1000, 10000, 1);
            for (int i = 0; i < numEnemys; i++)
            {
                float randomY = random.Next(Window.ClientBounds.Height);
                enemys.Add(new Enemy(TextureLibrary.GetTexture("Enemy"), new Vector2(Window.ClientBounds.Width / 2, randomY), 100, Vector2.One, Color.White, 1000, 5000, 1));
            }

            ground = new Ground(groundTexture, new Vector2(20, 0), new Vector2(20, 1), Color.White);
            position = new Vector2(20, 1);
            scale = new Vector2(0.15f, 0.15f);
            offset = (playerTexture.Bounds.Size.ToVector2() / 2f) * scale;
            playerRectangle = new Rectangle((position - offset).ToPoint(), (playerTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            groundRectangle = groundTexture.Bounds;
            groundRectangle.Location = ground.AccessPosition.ToPoint();
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
            groundTexture = Content.Load<Texture2D>("Ground");
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(0, 0, 800, 485));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Background"), new Rectangle(800, 0, 800, 485));

            TextureLibrary.LoadTexture(Content, "Enemy");
            TextureLibrary.LoadTexture(Content, "PlayerTexture");
            TextureLibrary.LoadTexture(Content, "Bullet");
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
            IsMouseVisible = true;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            }
            if (scrolling2.rectangle.X + scrolling2.rectangle.Width <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling2.rectangle.Width;
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Update(deltaTime, Keyboard.GetState(), Mouse.GetState(), Window.ClientBounds.Size);
            
            KeyboardState keyboardState = Keyboard.GetState();
            
            BulletManager.Update(deltaTime, player, enemys);

            
            scrolling1.Update();
            scrolling2.Update();
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

            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            player.Draw(spriteBatch);
            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].Draw(spriteBatch);
            }
            BulletManager.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
