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
    class LevelMenu
    {
        Game1 game;
        SpriteFont menuFont;

        //List<string> MenuLevel2 = new List<string>();        
        //List<Vector2> MenuLevel2Positions = new List<Vector2>();


        int selectCount = 1;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color Level1Color = new Color(255, 255, 255, 255);
        Color Level2Color = new Color(100, 100, 100, 200);
        Color BossColor = new Color(100, 100, 100, 200);
        Color BackColor = new Color(100, 100, 100, 200);

        public LevelMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            HandleOptions(currentKeyboardState, previousKeyboardState);

            if (currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape))               
            game.gameState = Prototype.Game1.GameState.mainmenu;
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
                case 1: //Level1
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level1;
                        game.currentLevel.Initialize();
                        game.gameState = Prototype.Game1.GameState.running;
                    }
                    Level1Color = SelectedColor;
                    Level2Color = UnselectedColor;
                    BossColor = UnselectedColor;
                    BackColor = UnselectedColor;
                    break;

                case 2: //Level2
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level2;
                        game.currentLevel.Initialize();
                        game.gameState = Prototype.Game1.GameState.running;
                    }
                    Level1Color = UnselectedColor;
                    Level2Color = SelectedColor;
                    BossColor = UnselectedColor;
                    BackColor = UnselectedColor;
                    break;

                case 3: //Boss
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level3;
                        game.currentLevel.Initialize();
                        game.gameState = Prototype.Game1.GameState.running;
                    }
                    Level1Color = UnselectedColor;
                    Level2Color = UnselectedColor;
                    BossColor = SelectedColor;
                    BackColor = UnselectedColor;
                    break;

                case 4: //Back
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    {                        
                        game.gameState = Prototype.Game1.GameState.mainmenu;
                    }
                    Level1Color = UnselectedColor;
                    Level2Color = UnselectedColor;
                    BossColor = UnselectedColor;
                    BackColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(menuFont, "Level 1", new Vector2(50, 50), Level1Color);
            spriteBatch.DrawString(menuFont, "Level2", new Vector2(50, 120), Level2Color);
            spriteBatch.DrawString(menuFont, "Boss", new Vector2(50, 190), BossColor);
            spriteBatch.DrawString(menuFont, "Back", new Vector2(50, 390), BackColor);
        }



    }
}
