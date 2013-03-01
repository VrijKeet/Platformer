//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

//namespace Prototype
//{
//    public class Gun
//    {
//        public Texture2D gunTexture;
//        public Rectangle boundingBox;
//        public bool picked = false;

//        public static Vector2 bounds; //Positie van gun
//        Rectangle source; //Bepaalt welk gedeelte van gun.png wordt getoond        


//        public void Initialize(Texture2D texture)
//        {
//            gunTexture = texture;
//        }

//        public void Update(GameTime gameTime)
//        {
//        }

//        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            if (!picked)
//                spriteBatch.Draw(gunTexture, boundingBox, Color.White);
//            else
//            {
//                source = Character.source;
//                if (Character.currentFacing == Character.facing.right)
//                    spriteBatch.Draw(gunTexture, Character.bounds, source, Color.White);
//                else
//                    spriteBatch.Draw(gunTexture, Character.bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
//            }
//        }
//    }
//}
