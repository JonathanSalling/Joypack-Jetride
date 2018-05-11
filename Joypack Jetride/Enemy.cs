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
    class Enemy
    {
        Rectangle enemyRectangle;
        Texture2D texture;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Color color;
        float speed;
        float health;
        bool alive;
        float attackSpeed;
        float attackTimer;
        float attackRange;


        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, Color enemyColor, float enemyHealth, float enemyAttackRange, float enemyAttackSpeed)
        {
            texture = enemyTexture;
            speed = enemySpeed;
            moveDir.Y = 1;
            color = enemyColor;
            scale = new Vector2(0.2f, 0.2f);
            position = new Vector2(400, 0);
            enemyRectangle = new Rectangle(position.ToPoint(), (texture.Bounds.Size.ToVector2() * scale).ToPoint());
            health = enemyHealth;
            alive = true;
            attackRange = enemyAttackRange;
            attackSpeed = enemyAttackSpeed;
            attackTimer = 0;
        }
        
        public void Update(float deltaTime, Player player, int windowHeight)
        {
            if (alive == true)
            {
                attackTimer += deltaTime;
                if (attackTimer <= attackSpeed)
                {
                    attackTimer += deltaTime;
                }

                if (Vector2.Distance(position, new Vector2(1, 0)) <= attackRange && attackTimer >= attackSpeed)
                {
                    BulletManager.AddBullet(TextureLibrary.GetTexture("Bullet"), position, new Vector2(-1, 0) - position, 400, new Vector2(0.05f, 0.05f), Bullet.Owner.Enemy, color);
                    attackTimer = 0;
                }
            }
        }
        public void Update(GameTime gameTime, Player player)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float pixelsToMove = speed * deltaTime;
            
            if (position.Y <= 0)
            {
                moveDir.Y = 1;
            }
            else if (position.Y <= 363)
            {
                moveDir.Y = -1;
            }

            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                position += (moveDir * speed);
                enemyRectangle.Location = position.ToPoint();
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive == true)
            {
                spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }

        public void ChangeHealth(float healthMod)
        {
            health += healthMod;
            if (health <= 0)
            {
                alive = false;
                color = Color.Black;
            }
        }
        public Rectangle GetRectangle()
        {
            return enemyRectangle;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public bool GetIsAlive()
        {
            return alive;
        }
    }
}
