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
    class Enemy : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Platform mooiStatischPlatform;

        Texture2D enemyTexture;
        public static Rectangle boundingBox;
        public static Rectangle source;
        public static int distance;
        int richting;
        public static bool alive;

        enum facing
        {
            left,
            right
        }
        facing currentFacing = facing.right;

        public Enemy(Game game)
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
            source = new Rectangle(0, 70, 85, 70);


            richting = 1;

            alive = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            List<Platform> platforms = ((Game1)Game).currentLevel.GetPlatforms();
            //mooiStatischPlatform = platforms[4];

            enemyTexture = Game.Content.Load<Texture2D>("Slime");
            //boundingBox = new Rectangle(mooiStatischPlatform.boundingBox.X, mooiStatischPlatform.boundingBox.Y - source.Height, 85, 70);
            boundingBox = new Rectangle(boundingBox.X, boundingBox.Y, enemyTexture.Width, enemyTexture.Height);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (alive)
            {
                if (distance > 200)
                {
                    richting = -1;
                    currentFacing = facing.left;
                }
                else if (distance <= 0)
                {
                    richting = 1;
                    currentFacing = facing.right;
                }
                distance += richting;
                //boundingBox = new Rectangle(mooiStatischPlatform.boundingBox.X + distance, mooiStatischPlatform.boundingBox.Y - source.Height, 85, 70);
            }

            //if (alive && Character.bounds.X < boundingBox.X + boundingBox.Width && Character.feetBounds.Y + Character.feetBounds.Height > boundingBox.Y && Character.feetBounds.X + Character.feetBounds.Width > boundingBox.X && Character.feetBounds.Y < (boundingBox.Y + boundingBox.Height) - 60)
            {
                score.currentScore += 1;
                alive = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();

            if (alive)
            {
                if (currentFacing == facing.left)
                    spriteBatch.Draw(enemyTexture, boundingBox, source, Color.White);
                else
                    spriteBatch.Draw(enemyTexture, boundingBox, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
