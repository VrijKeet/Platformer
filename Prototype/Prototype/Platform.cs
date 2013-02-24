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
    public class Platform
    {
        public Texture2D platformTexture;
        public Rectangle boundingBox;
        public Rectangle boundingBoxTop;


        public Platform()
        {
            //this.platformTexture = platformTexture;
        }

        public void Initialize(Texture2D texture)
        {
            platformTexture = texture; //texture haalt hij van game1.cs
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (platformTexture != null) //Load wordt later uitgevoerd dan Initialize. Zonder deze voorwaarde loopt hij vast. 
                //DIT IS ALLEEN als je de manier via directe Initialize gebruikt, dus niet via LoadContent Initialize.

                spriteBatch.Draw(platformTexture, boundingBox, Color.White);
        }
    }
}
