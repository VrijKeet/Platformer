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
    class MainMenu
    {
        Game1 game;
        SpriteFont menuFont;
        SpriteFont titelFont;        

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(255, 165, 0, 255);
        Color StartColor = new Color(255, 255, 255, 255);
        Color NewGameColor = new Color(255, 255, 255);
        Color OptionsColor = new Color(100, 100, 100, 200);
        Color ExitColor = new Color(100, 100, 100, 200);

        int selectCount = 1;

        public MainMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.titelFont = content.Load<SpriteFont>("titel");
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
                case 1: //Resume
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.gameState = Prototype.Game1.GameState.running; //Game spelen wanneer Spatie ingedrukt is geweest
                    }
                    StartColor = SelectedColor;
                    NewGameColor = UnselectedColor;
                    OptionsColor = UnselectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case 2: //New Game
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.gameState = Prototype.Game1.GameState.levelMenu;

                    }
                    StartColor = UnselectedColor;
                    NewGameColor = SelectedColor;
                    OptionsColor = UnselectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case 3: //Options
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)))
                    {                        
                        game.gameState = Prototype.Game1.GameState.options;
                        
                    }
                    StartColor = UnselectedColor;
                    NewGameColor = UnselectedColor;
                    OptionsColor = SelectedColor;
                    ExitColor = UnselectedColor;                    
                    break;
                                    

                case 4: //Exit
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.Exit();
                    }
                    StartColor = UnselectedColor;
                    NewGameColor = UnselectedColor;
                    OptionsColor = UnselectedColor;
                    ExitColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            Vector2 length1 = titelFont.MeasureString("Little Platformer") / 2;
            spriteBatch.DrawString(titelFont, "Little Platformer", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length1.X, 10), new Color(0, 0, 100));
            
            spriteBatch.DrawString(menuFont, "Resume", new Vector2(50, 140), StartColor);
            spriteBatch.DrawString(menuFont, "New game", new Vector2(50, 230), NewGameColor);
            spriteBatch.DrawString(menuFont, "Options", new Vector2(50, 290), OptionsColor);
            spriteBatch.DrawString(menuFont, "Exit", new Vector2(50, 390), ExitColor);
        }



    }
}
