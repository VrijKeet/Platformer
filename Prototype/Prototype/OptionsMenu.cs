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
    class OptionsMenu
    {
        Game1 game;
        SpriteFont menuFont;

        enum Selected
        {
            difficulty,
            character,
            music,
            back
        }
        Selected selected = Selected.difficulty;

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color DifficultyColor1 = new Color(255, 255, 255, 255);
        Color DifficultyColor2 = new Color(255, 255, 255, 255);
        Color CharacterColor = new Color(100, 100, 100, 200);
        Color MusicColor = new Color(100, 100, 100, 200);
        Color BackColor = new Color(0, 0, 0, 0);
        
        enum SelectedCharacter
        {
            Bandit,
            Jos,
            Maarten
        }
        SelectedCharacter selectedCharacter = SelectedCharacter.Bandit;
        SelectedCharacter nextSelectedCharacter;
        SelectedCharacter previousSelectedCharacter;

        enum SelectedDifficulty
        {
            Easy,
            Medium,
            Hard
        }
        SelectedDifficulty selectedDifficulty = SelectedDifficulty.Medium;
        SelectedDifficulty nextSelectedDifficulty;
        SelectedDifficulty previousSelectedDifficulty;

        int frameCount = 0; //Telt welke frame op het moment in source is
        const int delay = 4; //Vertraagt de animatie en beweging

        const int sourceWidth = 80; //Breedte van charactre in player.png
        const int sourceHeight = 80; //Hoogte van character in player.png    

        Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight); //Bepaalt welk gedeelte van player.png wordt getoond

        public OptionsMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            CheckSelectedOption(currentKeyboardState, previousKeyboardState);
            DoSelectedOption(currentKeyboardState, previousKeyboardState);
            CharacterSelection(currentKeyboardState, previousKeyboardState);
            DifficultySelection(currentKeyboardState, previousKeyboardState);

            if (frameCount % delay == 0)
            {
                if (frameCount / delay >= 4)
                    frameCount = 0;
                source = new Rectangle(frameCount / delay * 80, 0, sourceWidth, sourceHeight);
            }
            frameCount++;
        }

        private void CharacterSelection(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if (selectedCharacter == SelectedCharacter.Bandit)
            {
                nextSelectedCharacter = SelectedCharacter.Jos;
                previousSelectedCharacter = SelectedCharacter.Maarten;
                Game1.character.playerTexture = Game1.character1Texture;
            }
            else if (selectedCharacter == SelectedCharacter.Jos)
            {
                nextSelectedCharacter = SelectedCharacter.Maarten;
                previousSelectedCharacter = SelectedCharacter.Bandit;
                Game1.character.playerTexture = Game1.character2Texture;
            }
            else if (selectedCharacter == SelectedCharacter.Maarten)
            {
                nextSelectedCharacter = SelectedCharacter.Bandit;
                previousSelectedCharacter = SelectedCharacter.Jos;
                Game1.character.playerTexture = Game1.character3Texture;
            }
        }


        private void DifficultySelection(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if (selectedDifficulty == SelectedDifficulty.Easy)
            {
                nextSelectedDifficulty = SelectedDifficulty.Medium;
                previousSelectedDifficulty = SelectedDifficulty.Hard;
                health.lifes = 5;
            }
            else if (selectedDifficulty == SelectedDifficulty.Medium)
            {
                nextSelectedDifficulty = SelectedDifficulty.Hard;
                previousSelectedDifficulty = SelectedDifficulty.Easy;
                health.lifes = 3;
            }
            else if (selectedDifficulty == SelectedDifficulty.Hard)
            {
                nextSelectedDifficulty = SelectedDifficulty.Easy;
                previousSelectedDifficulty = SelectedDifficulty.Medium;
                health.lifes = 1;
            }
        }

        private void CheckSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) | (currentKeyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)))
                game.gameState = Prototype.Game1.GameState.mainmenu;

            if (selected == Selected.difficulty)
            {
                {
                    if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                        selected = Selected.character;
                    if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                        selected = Selected.music;
                }
            }
            else if (selected == Selected.character)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.music;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.difficulty;
            }
            else if (selected == Selected.music)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.back;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.character;
            }
            else if (selected == Selected.back)
            {
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) | (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)))
                    selected = Selected.difficulty;
                if ((currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) | (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)))
                    selected = Selected.music;
            }
        }





        private void DoSelectedOption(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            switch (selected)
            {
                case Selected.difficulty:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)))
                    {
                        selectedDifficulty = nextSelectedDifficulty;
                    }
                    else if ((currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        selectedDifficulty = previousSelectedDifficulty;
                    }

                    if (selectedDifficulty == SelectedDifficulty.Easy)
                        DifficultyColor2 = new Color(124, 252, 0);
                    else if (selectedDifficulty == SelectedDifficulty.Medium)
                        DifficultyColor2 = new Color(255, 165, 0);
                    else if (selectedDifficulty == SelectedDifficulty.Hard)
                        DifficultyColor2 = new Color(255, 0, 0);

                    DifficultyColor1 = SelectedColor;
                    CharacterColor = UnselectedColor;
                    MusicColor = UnselectedColor;
                    BackColor = UnselectedColor;
                    break;

                case Selected.character:
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)))
                    {
                        selectedCharacter = nextSelectedCharacter;
                    }
                    else if ((currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        selectedCharacter = previousSelectedCharacter;
                    }

                    DifficultyColor1 = UnselectedColor;
                    CharacterColor = SelectedColor;
                    MusicColor = UnselectedColor;
                    BackColor = UnselectedColor;
                    break;

                case Selected.music:

                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)))
                    {
                        //Change music
                    }

                    DifficultyColor1 = UnselectedColor;
                    CharacterColor = UnselectedColor;
                    MusicColor = SelectedColor;
                    BackColor = UnselectedColor;
                    break;

                case Selected.back:

                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)))
                    {
                        game.gameState = Prototype.Game1.GameState.mainmenu;
                    }

                    DifficultyColor1 = UnselectedColor;
                    CharacterColor = UnselectedColor;
                    MusicColor = UnselectedColor;
                    BackColor = SelectedColor;
                    break;
            }
        }





        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(menuFont, "Difficulty:", new Vector2(50, 50), DifficultyColor1);
            spriteBatch.DrawString(menuFont, " " +selectedDifficulty, new Vector2(300, 50), DifficultyColor2);
            spriteBatch.DrawString(menuFont, "Character: " + selectedCharacter, new Vector2(50, 120), CharacterColor);
            spriteBatch.DrawString(menuFont, "Music", new Vector2(50, 190), MusicColor);
            spriteBatch.DrawString(menuFont, "Back", new Vector2(50, 330), BackColor);

            spriteBatch.Draw(Game1.character.playerTexture, new Rectangle(500, 100, 320, 320), source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
