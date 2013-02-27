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
        enum Selected
        {
            restart,
            MainMenu,
            exit
        }
        Selected selected = Selected.restart;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color RestartColor = new Color(255, 255, 255, 255);
        Color MainMenuColor = new Color(100, 100, 100, 200);
        Color ExitColor = new Color(100, 100, 100, 200);

        public DeathScreen(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.deathFont = content.Load<SpriteFont>("death");
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
            if (selected == Selected.restart)
            {
                {
                    if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                        selected = Selected.MainMenu;
                    if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                        selected = Selected.exit;
                }
            }
            else if (selected == Selected.MainMenu)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.exit;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.restart;
            }
            else if (selected == Selected.exit)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.restart;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.MainMenu;
            }
        }






        private void DoSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            switch (selected)
            {
                case Selected.restart:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        health.lifes = 3;
                        game.deathTimer = 0;
                        Character.currentState = Character.state.standing;
                        game.gameState = Prototype.Game1.GameState.running; //Game spelen wanneer Spatie ingedrukt is geweest                         
                        Character.respawn();
                    }
                    RestartColor = SelectedColor;
                    MainMenuColor = UnselectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case Selected.MainMenu:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.gameState = Prototype.Game1.GameState.mainmenu;

                    }
                    RestartColor = UnselectedColor;
                    MainMenuColor = SelectedColor;
                    ExitColor = UnselectedColor;
                    break;

                case Selected.exit:
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
            spriteBatch.DrawString(menuFont, "YOU DIED", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length1.X, 50), new Color(0, 0, 100));
            spriteBatch.DrawString(deathFont, "Restart", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length2.X, 190), RestartColor);
            spriteBatch.DrawString(deathFont, "Go to the main menu", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length3.X, 240), MainMenuColor);
            spriteBatch.DrawString(deathFont, "Exit", new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width / 2 - length4.X, 330), ExitColor);
        }



    }
}
