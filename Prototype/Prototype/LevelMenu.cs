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

        enum Selected
        {
            level1,
            level2,
            boss
        }
        Selected selected = Selected.level1;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color Level1Color = new Color(255, 255, 255, 255);
        Color Level2Color = new Color(100, 100, 100, 200);
        Color BossColor = new Color(100, 100, 100, 200);

        public LevelMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            CheckSelectedOption(currentKeyboardState, previousKeyboardState);
            DoSelectedOption(currentKeyboardState, previousKeyboardState);

            if (currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape))               
            game.gameState = Prototype.Game1.GameState.mainmenu;
        }




        private void CheckSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            //Zorgen dat je andere menuopties kunt selecteren. Kan ook met bijvoorbeeld een integer SelectedIndex, maar ik ben hier maar niet voor gegaan, aangezien je dan ook nog moet definiëren wat "omhoog" en "omlaag" is.
            if (selected == Selected.level1)
            {
                {
                    if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                        selected = Selected.level2;
                    if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                        selected = Selected.boss;
                }
            }
            else if (selected == Selected.level2)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.boss;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.level1;
            }
            else if (selected == Selected.boss)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.level1;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.level2;
            }
        }






        private void DoSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            switch (selected)
            {
                case Selected.level1:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level1;
                        game.currentLevel.Initialize();
                    }
                    Level1Color = SelectedColor;
                    Level2Color = UnselectedColor;
                    BossColor = UnselectedColor;
                    break;

                case Selected.level2:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level2;
                        game.currentLevel.Initialize();
                    }
                    Level1Color = UnselectedColor;
                    Level2Color = SelectedColor;
                    BossColor = UnselectedColor;
                    break;

                case Selected.boss:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.currentLevel = game.level3;
                        game.currentLevel.Initialize();
                    }
                    Level1Color = UnselectedColor;
                    Level2Color = UnselectedColor;
                    BossColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(menuFont, "Level 1", new Vector2(50, 50), Level1Color);
            spriteBatch.DrawString(menuFont, "Level2", new Vector2(50, 120), Level2Color);
            spriteBatch.DrawString(menuFont, "Boss", new Vector2(50, 190), BossColor);
        }



    }
}
