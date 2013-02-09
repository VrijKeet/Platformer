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
    public class score : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteFont font;
        public static int currentScore; // Ik wilde score gebruiken, maar je mag niet dezelfde naam gebruiken als de class

        public score(Game game)
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
            currentScore = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("score");
            
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + currentScore, new Vector2(health.startLifes * health.width, 0), Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
