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
    class Bullet
    {
        public enum Owner { Player, Enemy };
        Owner owner;
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float speed;
        float rotation;
        float damage;
        bool alive;

        public Bullet(Texture2D bulletTexture, Vector2 bulletStartPos, Vector2 bulletDir, float bulletSpeed, Vector2 bulletScale, Owner bulletOwner, Color bulletColor)
        {
            texture = bulletTexture;
            position = bulletStartPos;
            speed = bulletSpeed;
            moveDir = bulletDir;
            moveDir.Normalize();
            scale = bulletScale;
            offset = bulletTexture.Bounds.Size.ToVector2() * 0.5f;
            rectangle = new Rectangle((bulletStartPos - offset * scale).ToPoint(), (bulletTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            rotation = (float)Math.Atan2(moveDir.Y, moveDir.X);
            color = bulletColor;
            damage = 100;
            alive = true;
            owner = bulletOwner;
        }
        public void Update(float deltaTime)
        {
            position += moveDir * speed * deltaTime;
            rectangle.Location = (position - offset * scale).ToPoint();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, offset, scale, SpriteEffects.None, 0);
        }
        public float Damage(Rectangle otherRectangle)
        {
            float damageToDeal = 0;

            if (rectangle.Intersects(otherRectangle))
            {
                damageToDeal = damage;
                alive = false;
            }
            return damageToDeal;
        }
        public bool GetIsAlive()
        {
            return alive;
        }
        public Owner GetOwner()
        {
            return owner;
        }
    }

}
