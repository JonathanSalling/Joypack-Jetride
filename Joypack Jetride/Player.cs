using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Joypack_Jetride
{
    class Player
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float speed;
        float health;
        bool alive = true;
        float attackSpeed;
        float attackTimer;

        public Player(Texture2D playerTexture, Vector2 playerStartPos, float playerSpeed, Vector2 playerScale, Color playerColor, float playerHealth, float playerAttackSpeed)
        {
            texture = playerTexture;
            position = playerStartPos;
            speed = playerSpeed;
            moveDir = Vector2.Zero;
            scale = playerScale;
            offset = (playerTexture.Bounds.Size.ToVector2() / 2.0f) * scale;
            rectangle = new Rectangle((playerStartPos - offset).ToPoint(), (playerTexture.Bounds.Size.ToVector2() * playerScale).ToPoint());
            color = playerColor;
            attackSpeed = playerAttackSpeed;
            attackTimer = 0;
        }
        public void Update(float deltaTime, KeyboardState keyboardState, MouseState mouseState, Point windowSize)
        {
            if (alive)
            {
                //spelar rörelse

                attackTimer += deltaTime;
                if (attackTimer <= attackSpeed)
                {
                    attackTimer += deltaTime;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && attackTimer >= attackSpeed)
                {
                    Vector2 bulletDir = mouseState.Position.ToVector2() - position;
                    BulletManager.AddBullet(TextureLibrary.GetTexture("Bullet"), position, bulletDir, 0.1f, new Vector2(0.05f, 0.05f), Bullet.Owner.Player, color);
                    attackTimer = 0;
                }
                else
                {
                    color = Color.Black;
                }
            }
        }
        public void Update(GameTime gameTime, Player player)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float pixelsToMove = speed * deltaTime;
            moveDir.Y = 1;
            moveDir.Normalize();
            position += moveDir * pixelsToMove;
            rectangle.Location = (position - offset).ToPoint();
        }
        public void ChangeHealth(float healthMod)
        {
            health += healthMod;
            if(health <= 0)
            {
                alive = false;
            }
        }
        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        //}
        public Rectangle GetRectangle()
        {
            return rectangle;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
