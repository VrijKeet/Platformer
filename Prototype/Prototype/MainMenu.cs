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

        //List<string> MenuOptions = new List<string>();        
        //List<Vector2> MenuOptionsPositions = new List<Vector2>();

        enum Selected
        {
            start,
            options,
            exit
        }
        Selected selected = Selected.start;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color StartColor = new Color(255, 255, 255, 255);
        Color OptionsColor = new Color(100, 100, 100, 200);
        Color ExitColor = new Color(100, 100, 100, 200);

        public MainMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            CheckSelectedOption(currentKeyboardState, previousKeyboardState);
            DoSelectedOption(currentKeyboardState, previousKeyboardState);
        }

        


        private void CheckSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            //Zorgen dat je andere menuopties kunt selecteren. Kan ook met bijvoorbeeld een integer SelectedIndex, maar ik ben hier maar niet voor gegaan, aangezien je dan ook nog moet definiëren wat "omhoog" en "omlaag" is.
            if (selected == Selected.start)
            {
                {
                    if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                        selected = Selected.options;
                    if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                        selected = Selected.exit;
                }
            }
            else if (selected == Selected.options)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.exit;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.start;
            }
            else if (selected == Selected.exit)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.start;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.options;
            }
        }






        private void DoSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            switch (selected)
            {
                case Selected.start:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.gameState = Prototype.Game1.GameState.running; //Game spelen wanneer Spatie ingedrukt is geweest            
                    }
                    StartColor = SelectedColor;
                    OptionsColor = UnselectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case Selected.options:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {                        
                        game.gameState = Prototype.Game1.GameState.options;
                        
                    }
                    StartColor = UnselectedColor;
                    OptionsColor = SelectedColor;
                    ExitColor = UnselectedColor;                    
                    break;

                case Selected.exit:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.Exit();
                    }
                    StartColor = UnselectedColor;
                    OptionsColor = UnselectedColor;
                    ExitColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            spriteBatch.DrawString(menuFont, "Start", new Vector2(50, 50), StartColor);
            spriteBatch.DrawString(menuFont, "Options", new Vector2(50, 120), OptionsColor);
            spriteBatch.DrawString(menuFont, "Exit", new Vector2(50, 190), ExitColor);
        }



    }
}
