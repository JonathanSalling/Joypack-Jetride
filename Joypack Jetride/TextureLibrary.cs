﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joypack_Jetride
{
    static class TextureLibrary
    {
        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static void LoadTexture(ContentManager content, string textureName)
        {
            textures.Add(textureName, content.Load<Texture2D>(textureName));
        }

        public static Texture2D GetTexture(string textureName)
        {
            return textures[textureName];
        }
    }
}
