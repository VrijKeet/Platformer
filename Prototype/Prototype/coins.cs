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
    public class coins : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D coinTexture;
        public static Vector2 position;
        bool taken;

        public coins(Game game)
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
            taken = false;
            position = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            coinTexture = Game.Content.Load<Texture2D>("coin");
            
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (!taken && Character.bounds.Y < position.Y + coinTexture.Height && Character.bounds.X < position.X + coinTexture.Width && Character.bounds.Y + Character.bounds.Height > position.Y && Character.bounds.X + Character.bounds.Width > position.X)
            {
                score.currentScore += 1;
                taken = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();
            if (!taken)
            {
                spriteBatch.Draw(coinTexture, position, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
