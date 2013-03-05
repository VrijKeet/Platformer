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
        public Scrollen(Game1 game)
            : base(game)
        {
        }

        List<Platform> platformList;
        List<Enemy> enemyList;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            int marge = 250;

            Game1 game = (Game1)base.Game;
            platformList = game.currentLevel.GetPlatforms();
            enemyList = game.currentLevel.GetEnemies();

            if (Character.bounds.X < marge) // Naar links
            {
                foreach (Platform platform in platformList)
                {
                    platform.boundingBox = new Rectangle(platform.boundingBox.X + (marge - Character.bounds.X), platform.boundingBox.Y, platform.boundingBox.Width, platform.boundingBox.Height);
                    platform.boundingBoxTop = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y+10, platform.boundingBox.Width, 18);

                }

                foreach (Enemy enemy in enemyList)
                {
                    enemy.bounds = new Rectangle(enemy.bounds.X + (marge - Character.bounds.X), enemy.bounds.Y, enemy.bounds.Width, enemy.bounds.Height);
                }

                if (game.currentLevel == game.level1)
                {
                    Level1.goalPos = new Vector2(Level1.goalPos.X + (marge - Character.bounds.X), Level1.goalPos.Y);
                    for (int i = 0; i < Level1.coins.Count; i++)
                        Level1.coins[i].position = new Vector2(Level1.coins[i].position.X + (marge - Character.bounds.X), Level1.coins[i].position.Y);
                }
                //else if (game.currentLevel == game.level2)
                //    Level2.goalPos = new Vector2(Level2.goalPos.X + (marge - Character.bounds.X), Level2.goalPos.Y);

                //for (int i = 0; i < Game1.platforms.Count; i++)
                //{

                //}
                //Game1.gun.boundingBox = new Rectangle(Game1.gun.boundingBox.X + (marge - Character.bounds.X), Game1.gun.boundingBox.Y, Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height);
                //Game1.projectiles.Position = new Rectangle(Game1.projectiles.Position.X + (marge - Character.bounds.X), Game1.gun.boundingBox.Y, Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height);
                Character.bounds.X = marge;
            }

            if ((Character.bounds.X + Character.bounds.Width) > (GraphicsDevice.Viewport.Width - marge)) // Naar rechts
            {
                foreach (Platform platform in platformList)
                {
                    platform.boundingBox = new Rectangle(platform.boundingBox.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), platform.boundingBox.Y, platform.boundingBox.Width, platform.boundingBox.Height);
                    platform.boundingBoxTop = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y + 10, platform.boundingBox.Width, 18);
                }

                foreach (Enemy enemy in enemyList)
                {
                    enemy.bounds = new Rectangle(enemy.bounds.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), enemy.bounds.Y, enemy.bounds.Width, enemy.bounds.Height);
                }

                if (game.currentLevel == game.level1)
                {
                    Level1.goalPos = new Vector2(Level1.goalPos.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Level1.goalPos.Y);
                    for (int i = 0; i < Level1.coins.Count; i++)
                        Level1.coins[i].position = new Vector2(Level1.coins[i].position.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Level1.coins[i].position.Y);
                }
                //else if(game.currentLevel == game.level2)
                //    Level2.goalPos = new Vector2(Level2.goalPos.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Level2.goalPos.Y);

                //Game1.gun.boundingBox = new Rectangle(Game1.gun.boundingBox.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Game1.gun.boundingBox.Y, Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height);
                //Game1.gun.boundingBox = new Rectangle(Game1.gun.boundingBox.X - ((Character.bounds.X + Character.bounds.Width) - (GraphicsDevice.Viewport.Width - marge)), Game1.gun.boundingBox.Y, Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height); 
                Character.bounds.X = GraphicsDevice.Viewport.Width - marge - Character.bounds.Width;
            }

            if (Character.bounds.Y < marge) // Naar boven
            {
                foreach (Platform platform in platformList)
                {
                    platform.boundingBox = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y + (marge - Character.bounds.Y), platform.boundingBox.Width, platform.boundingBox.Height);
                    platform.boundingBoxTop = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y + 10, platform.boundingBox.Width, 18);
                }

                foreach (Enemy enemy in enemyList)
                {
                    enemy.bounds = new Rectangle(enemy.bounds.X, enemy.bounds.Y + (marge - Character.bounds.Y), enemy.bounds.Width, enemy.bounds.Height);
                }

                if (game.currentLevel == game.level1)
                {
                    Level1.goalPos = new Vector2(Level1.goalPos.X, Level1.goalPos.Y + (marge - Character.bounds.Y));
                    for (int i = 0; i < Level1.coins.Count; i++)
                        Level1.coins[i].position = new Vector2(Level1.coins[i].position.X, Level1.coins[i].position.Y + (marge - Character.bounds.Y));
                }
                //else if( game.currentLevel == game.level2)
                //    Level2.goalPos = new Vector2(Level2.goalPos.X, Level2.goalPos.Y + (marge - Character.bounds.Y));

                //Game1.gun.boundingBox = new Rectangle(Game1.gun.boundingBox.X, Game1.gun.boundingBox.Y + (marge - Character.bounds.Y), Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height);
                Character.bounds.Y = marge;
            }

            if ((Character.bounds.Y + Character.bounds.Height) > (GraphicsDevice.Viewport.Height - marge)) // Naar beneden
            {
                foreach (Platform platform in platformList)
                {
                    platform.boundingBox = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)), platform.boundingBox.Width, platform.boundingBox.Height);
                    platform.boundingBoxTop = new Rectangle(platform.boundingBox.X, platform.boundingBox.Y + 10, platform.boundingBox.Width, 18);
                }

                foreach (Enemy enemy in enemyList)
                {
                    enemy.bounds = new Rectangle(enemy.bounds.X, enemy.bounds.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)), enemy.bounds.Width, enemy.bounds.Height);
                }

                if (game.currentLevel == game.level1)
                {
                    Level1.goalPos = new Vector2(Level1.goalPos.X, Level1.goalPos.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)));
                    for (int i = 0; i < Level1.coins.Count; i++)
                        Level1.coins[i].position = new Vector2(Level1.coins[i].position.X, Level1.coins[i].position.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)));
                }
                //else if(game.currentLevel == game.level2)
                //    Level2.goalPos = new Vector2(Level2.goalPos.X, Level2.goalPos.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)));

                //Game1.gun.boundingBox = new Rectangle(Game1.gun.boundingBox.X, Game1.gun.boundingBox.Y - ((Character.bounds.Y + Character.bounds.Height) - (GraphicsDevice.Viewport.Height - marge)), Game1.gun.boundingBox.Width, Game1.gun.boundingBox.Height);
                Character.bounds.Y = GraphicsDevice.Viewport.Height - marge - Character.bounds.Height;
            }

            //Enemy.boundingBox = new Rectangle(Game1.platforms[4].boundingBox.X + Enemy.distance, Game1.platforms[4].boundingBox.Y - Enemy.source.Height, 85, 70);

            base.Update(gameTime);
        }
    }
}
