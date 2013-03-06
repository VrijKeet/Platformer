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
    class DeathScreen
    {
        Game1 game;

        SpriteFont menuFont;
        SpriteFont deathFont;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(255, 0 , 0);
        Color RestartColor;
        Color MainMenuColor;
        Color ExitColor;

        int selectCount = 1;
        public DeathScreen(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.deathFont = content.Load<SpriteFont>("death");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            HandleOptions(currentKeyboardState, previousKeyboardState);
        }
        


        private void HandleOptions(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
            {
                selectCount++;
                if (selectCount >= 5)
                    selectCount = 1;
            }
            else if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
            {
                selectCount--;
                if (selectCount <= 0)
                    selectCount = 4;
            }   



            switch (selectCount)
            {
                case 1: //Restart
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        health.lifes = health.startLifes;
                        Character.deathTimer = 0;
                        Character.currentState = Character.state.standing;
                        game.gameState = Prototype.Game1.GameState.running; //Game spelen wanneer Spatie ingedrukt is geweest                         
                        Character.respawn(game);
                    }
                    RestartColor = SelectedColor;
                    MainMenuColor = UnselectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case 2: // Main Menu
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.gameState = Prototype.Game1.GameState.mainmenu;

                    }
                    RestartColor = UnselectedColor;
                    MainMenuColor = SelectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case 3: //Exit
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.Exit();
                    }
                    RestartColor = UnselectedColor;
                    MainMenuColor = UnselectedColor;
                    ExitColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 length1 = menuFont.MeasureString("YOU DIED") / 2;
            Vector2 length2 = deathFont.MeasureString("Restart") / 2;
            Vector2 length3 = deathFont.MeasureString("Go to the main menu") / 2;
            Vector2 length4 = deathFont.MeasureString("Exit") / 2;
            spriteBatch.DrawString(menuFont, "YOU DIED", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length1.X, 50), new Color(255, 0, 0));
            spriteBatch.DrawString(deathFont, "Restart", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length2.X, 190), RestartColor);
            spriteBatch.DrawString(deathFont, "Go to the main menu", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length3.X, 240), MainMenuColor);
            spriteBatch.DrawString(deathFont, "Exit", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length4.X, 330), ExitColor);
        }
    }
}
