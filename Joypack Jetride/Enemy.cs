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
        
        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, Color enemyColor)
        {
            texture = enemyTexture;
            position = enemyStartPos;

            speed = enemySpeed;
            moveDir = Vector2.Zero;
            scale = enemyScale;
            enemyRectangle = texture.Bounds;
            color = enemyColor;
            scale = new Vector2(0.2f, 0.2f);
            position = new Vector2(400, 1);
        }
        public void Update(GameTime gameTime, Player player)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float pixelsToMove = speed * deltaTime;
            moveDir.Y = 1;
            moveDir.Normalize();
            position += moveDir * pixelsToMove;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
        public Rectangle GetRectangle()
        {
            return enemyRectangle;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
