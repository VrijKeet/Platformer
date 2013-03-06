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
    public class Gun
    {
        public Texture2D texture;
        public Texture2D gunTexture;
        public Rectangle bounds;
        public bool picked = false;

        Rectangle source = new Rectangle(0, 0, 20, 20); //Bepaalt welk gedeelte van gun.png wordt getoond        

        public Gun()
        {
            gunTexture = Game1.gunTexture;
        }

        public void Initialize(Texture2D Texture, Rectangle bounds)
        {
            texture = Texture;
            this.bounds = bounds;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                if (!picked)
                    spriteBatch.Draw(texture, bounds, Color.White);
                else
                {
                    if (Character.currentFacing == Character.facing.right)
                        spriteBatch.Draw(texture, Character.bounds, source, Color.White);
                    else
                        spriteBatch.Draw(texture, Character.bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
            }
        }
    }
}
