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


namespace Prototype
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Scrollen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Scrollen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            int marge = 20;

            if (Character.bounds.X < marge) // Naar links
            {
                for (int i = 0; i < Game1.platforms.Count; i++)
                {
                    Game1.platforms[i].boundingBox = new Rectangle(Game1.platforms[i].boundingBox.X + (marge - Character.bounds.X), Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, Game1.platforms[i].boundingBox.Height);
                    Game1.platforms[i].boundingBoxTop = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, 5);
                }

                Character.bounds.X = marge;
            }

            if ((Character.bounds.X + Character.bounds.Width) > (GraphicsDevice.Viewport.Width - marge)) // Naar rechts
            {
                for (int i = 0; i < Game1.platforms.Count; i++)
                {
                    Game1.platforms[i].boundingBox = new Rectangle(Game1.platforms[i].boundingBox.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Game1.platforms[i].boundingBox.Y,Game1.platforms[i].boundingBox.Width,Game1.platforms[i].boundingBox.Height);
                    Game1.platforms[i].boundingBoxTop = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, 5);
                }

                Character.bounds.X = GraphicsDevice.Viewport.Width - marge - Character.bounds.Width;
            }

            if (Character.bounds.Y < marge) // Naar boven
            {
                for (int i = 0; i < Game1.platforms.Count; i++)
                {
                    Game1.platforms[i].boundingBox = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y + (marge - Character.bounds.Y), Game1.platforms[i].boundingBox.Width, Game1.platforms[i].boundingBox.Height);
                    Game1.platforms[i].boundingBoxTop = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, 5);
                }

                Character.bounds.Y = marge;
            }

            if ((Character.bounds.Y + Character.bounds.Height) > (GraphicsDevice.Viewport.Height - marge)) // Naar beneden
            {
                for (int i = 0; i < Game1.platforms.Count; i++)
                {
                    Game1.platforms[i].boundingBox = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)), Game1.platforms[i].boundingBox.Width, Game1.platforms[i].boundingBox.Height);
                    Game1.platforms[i].boundingBoxTop = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, 5);
                }

                Character.bounds.Y = GraphicsDevice.Viewport.Height - marge - Character.bounds.Height;
            }

            enemies.dimensions = new Rectangle(Game1.platforms[4].boundingBox.X + enemies.distance, Game1.platforms[4].boundingBox.Y - enemies.source.Height, 85, 70);

            base.Update(gameTime);
        }
    }
}
