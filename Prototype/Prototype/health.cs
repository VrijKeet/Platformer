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
    public class health : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Game1 game;

        Texture2D texture;
        public static int lifes;
        public static int startLifes;
        public static int width; // Nodig om score juist te positioneren

        public health(Game1 game1)
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
            startLifes = 3;
            lifes = startLifes;
            width = 20;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("health");

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (game.currentLevel == game.level3)
            {
                if (Character.bounds.X < Enemy.boundingBox.X + Enemy.boundingBox.Width && Character.bounds.Y + Character.bounds.Height > Enemy.boundingBox.Y + 10 && Character.bounds.X + Character.bounds.Width > Enemy.boundingBox.X && Character.bounds.Y < Enemy.boundingBox.Y + Enemy.boundingBox.Height)
                {
                    lifes -= 1;
                    if ((Character.bounds.X + Character.bounds.Width) - (Enemy.boundingBox.X + Enemy.boundingBox.Width) > 0)
                        Character.bounds.X += 50;
                    else
                        Character.bounds.X -= 50;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();
            if (game.gameState == Game1.GameState.running)
            {
                for (int i = 0; i < lifes; i++)
                    spriteBatch.Draw(texture, new Rectangle(width * i, 0, width, 20), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
