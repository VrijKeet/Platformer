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


//namespace Prototype
//{
//    /// <summary>
//    /// This is a game component that implements IUpdateable.
//    /// </summary>
//    public class ladder : Microsoft.Xna.Framework.DrawableGameComponent
//    {
//        public static Texture2D ladderTexture;
//        public static Vector2 position;
//        public static int rails;

//        public ladder(Game game)
//            : base(game)
//        {
//            // TODO: Construct any child components here
//        }

//        /// <summary>
//        /// Allows the game component to perform any initialization it needs to before starting
//        /// to run.  This is where it can query for any required services and load content.
//        /// </summary>
//        public override void Initialize()
//        {
//            // TODO: Add your initialization code here
//            rails = 5;

//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            ladderTexture = Game.Content.Load<Texture2D>("ladder");
            
//            base.LoadContent();
//        }

//        /// <summary>
//        /// Allows the game component to update itself.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>
//        public override void Update(GameTime gameTime)
//        {
//            // TODO: Add your update code here
//            position = new Vector2(Game1.platforms[3].boundingBox.X, Game1.platforms[3].boundingBox.Y - ladderTexture.Height);

//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

//            spriteBatch.Begin();
//            for(int i = 0; i < rails; i++)
//                spriteBatch.Draw(ladderTexture, new Vector2(position.X, (position.Y +20) - ladderTexture.Height* i), Color.White);
//            spriteBatch.End();

//            base.Draw(gameTime);
//        }
//    }
//}
