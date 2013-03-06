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
        Game1 game;

        Texture2D coinTexture;
        public Vector2 position;
        public static List<bool> taken;

        public coins(Game1 game1)
            : base(game1)
        {
            // TODO: Construct any child components here
            this.game = game1;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            taken = new List<bool>();

            for (int i = 0; i < Level1.coins.Count; i++)
            {
                taken.Add(new bool());
                taken[i] = false;
            }

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
            for (int i = 0; i < Level1.coins.Count; i++)
            {
                if (!taken[i] && Character.boundingBox.Y < Level1.coins[i].position.Y + coinTexture.Height && Character.boundingBox.X < Level1.coins[i].position.X + coinTexture.Width && Character.boundingBox.Y + Character.boundingBox.Height > Level1.coins[i].position.Y && Character.boundingBox.X + Character.boundingBox.Width > Level1.coins[i].position.X)
                {
                    score.currentScore += 1;
                    taken[i] = true;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();

            for (int i = 0; i < Level1.coins.Count; i++)
            {
                if (game.gameState == Game1.GameState.running && !taken[i])
                {
                    spriteBatch.Draw(coinTexture, Level1.coins[i].position, Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
