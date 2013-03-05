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
        public Rectangle bounds;
        public bool picked = false;

        Rectangle source; //Bepaalt welk gedeelte van gun.png wordt getoond        



        public Gun(ContentManager content)
        {
            texture = content.Load<Texture2D>("star");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
