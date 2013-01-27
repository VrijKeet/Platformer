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
        Texture2D texture;
        public static int lifes;
        public static int startLifes;

        public health(Game game)
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
            startLifes = 3;
            lifes = startLifes;

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
            if (Character.bounds.X < enemies.dimensions.X + enemies.dimensions.Width && Character.bounds.Y + Character.bounds.Height > enemies.dimensions.Y && Character.bounds.X + Character.bounds.Width > enemies.dimensions.X && Character.bounds.Y < enemies.dimensions.Y + enemies.dimensions.Height)
            {
                lifes -= 1;
                if ((Character.bounds.X + Character.bounds.Width) - (enemies.dimensions.X + enemies.dimensions.Width) > 0)
                    Character.bounds.X += 50;
                else
                    Character.bounds.X -= 50;
            }

            if (lifes <= 0)
                Character.respawn();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

            spriteBatch.Begin();
            for (int i = 0; i < lifes; i++)
                spriteBatch.Draw(texture, new Rectangle(20 * i, 0, 20, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
