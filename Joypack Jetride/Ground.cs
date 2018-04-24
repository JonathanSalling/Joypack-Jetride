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
    class Ground
    {
        Texture2D texture;
        Rectangle groundRectangle;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;

        public Vector2 AccessPosition { get => position; set => position = value; }

        public Ground(Texture2D groundTexture, Vector2 groundStartPos, Vector2 groundScale, Color groundColor)
        {
            texture = groundTexture;
            position = groundStartPos;
            scale = groundScale;
            offset = (groundTexture.Bounds.Size.ToVector2() / 2.0f) * scale;
            groundRectangle = new Rectangle((groundStartPos - offset).ToPoint(), (groundTexture.Bounds.Size.ToVector2() * groundScale).ToPoint());
            color = groundColor;
            scale = new Vector2(1f, 1f);
            position = new Vector2(-5, 370);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
        public Rectangle GetRectangle()
        {
            return groundRectangle;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
    }

}
