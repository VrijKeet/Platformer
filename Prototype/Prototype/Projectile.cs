using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Prototype
{
    public class Projectile
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;
        public int Damage; //Schade aan vijand
        public float projectileMoveSpeed;
        public Rectangle bounds;

        // Get the width of the projectile ship
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the projectile ship
        public int Height
        {
            get { return Texture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Active = true;
            Damage = 2;
            bounds = new Rectangle((int)position.X, (int)position.Y, Game1.projectileTexture.Width, Game1.projectileTexture.Height);
        }
        public void Update()
        {
            bounds.X += (int)projectileMoveSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, bounds, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White);
        }
    }
}
