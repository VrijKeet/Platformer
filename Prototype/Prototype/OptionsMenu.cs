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
        
        string selectedSong = "Soundtrack";
        string selectedCharacter = "Bandit";

        Color SelectedColor = new Color(255, 255, 255, 255);
        Color UnselectedColor = new Color(0, 0, 0, 255);
        Color DifficultyColor1 = new Color(255, 255, 255, 255);
        Color DifficultyColor2 = new Color(255, 255, 255, 255);
        Color CharacterColor = new Color(100, 100, 100, 200);
        Color MusicColor = new Color(100, 100, 100, 200);
        Color BackColor = new Color(0, 0, 0, 0);

        enum SelectedDifficulty
        {
            Easy,
            Medium,
            Hard
        }
        SelectedDifficulty selectedDifficulty = SelectedDifficulty.Medium;

        int frameCount = 0; //Telt welke frame op het moment in source is
        const int delay = 4; //Vertraagt de animatie en beweging

        const int sourceWidth = 80; //Breedte van charactre in player.png
        const int sourceHeight = 80; //Hoogte van character in player.png    

        Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight); //Bepaalt welk gedeelte van player.png wordt getoond

        int songCount = 1;
        int characterCount = 1;
        int difficultyCount = 2;
        int selectCount = 1;

        public OptionsMenu(ContentManager content, Game1 game1)
        {
            this.menuFont = content.Load<SpriteFont>("menu");
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            HandleOptions(currentKeyboardState, previousKeyboardState);

            if (frameCount % delay == 0)
            {
                if (frameCount / delay >= 4)
                    frameCount = 0;
                source = new Rectangle(frameCount / delay * 80, 0, sourceWidth, sourceHeight);
            }
            frameCount++;

            if ((currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) | (currentKeyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)))
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
                case 1: //Difficulty
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)))
                    {
                        difficultyCount++;
                        if (difficultyCount >= 4)
                            difficultyCount = 1;
                    }
                    else if ((currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        difficultyCount--;
                        if (difficultyCount <= 0)
                            difficultyCount = 3;
                    }

                    if (difficultyCount == 1)
                    {
                        selectedDifficulty = SelectedDifficulty.Easy;
                        //health.lifes = 5;
                    }
                    else if (difficultyCount == 2)
                    {
                        selectedDifficulty = SelectedDifficulty.Medium;
                        //health.lifes = 3;
                    }
                    else if (difficultyCount == 3)
                    {
                        selectedDifficulty = SelectedDifficulty.Hard;
                        //health.lifes = 1;
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

                case 2: //Medium
                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)))
                    {
                        characterCount++;
                        if (characterCount >= 4)
                            characterCount = 1;
                    }
                    else if ((currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        characterCount--;
                        if (characterCount <= 0)
                            characterCount = 3;
                    }

                    if (characterCount == 1)
                    {
                        Character.playerTexture = Game1.character1Texture;
                        selectedCharacter = "Bandit";
                    }
                    else if (characterCount == 2)
                    {
                        Character.playerTexture = Game1.character2Texture;
                        selectedCharacter = "Jos";
                    }
                    else if (characterCount == 3)
                    {

                        Character.playerTexture = Game1.character3Texture;
                        selectedCharacter = "Maarten";
                    }
                    
                    DifficultyColor1 = UnselectedColor;
                    CharacterColor = SelectedColor;
                    MusicColor = UnselectedColor;
                    BackColor = UnselectedColor;
                    break;

                case 3: //Music

                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)))
                    {
                        songCount++;
                        if (songCount >= 7)
                            songCount = 1;
                    }
                    else if ((currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        songCount--;
                        if (songCount <= 0)
                            songCount = 6;
                    }


                    if ((currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) | (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) | (currentKeyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) | (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)) | (currentKeyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) | (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A)))
                    {
                        //Change music
                         if (songCount == 1)
                        {
                            Game1.PlayMusic(Game1.soundtrackSong);
                            selectedSong = "Soundtrack";
                        }
                        else if (songCount == 2)
                        {
                            MediaPlayer.Stop();
                            selectedSong = "None";
                        }
                        else if (songCount == 3)
                        {
                            Game1.PlayMusic(Game1.rapidSong);
                            selectedSong = "Rapid";
                        }
                        else if (songCount == 4)
                        {
                            Game1.PlayMusic(Game1.rickSong);
                            selectedSong = "Rick Roll";
                        }
                        else if (songCount == 5)
                        {
                            Game1.PlayMusic(Game1.littleSong);
                            selectedSong = "Little";
                        }
                        else if (songCount == 6)
                        {
                            Game1.PlayMusic(Game1.slagsmalklubbenSong);
                            selectedSong = "Slagsmalklubben";
                        }
                        

                    }
                    


                    DifficultyColor1 = UnselectedColor;
                    CharacterColor = UnselectedColor;
                    MusicColor = SelectedColor;
                    BackColor = UnselectedColor;
                    break;

                case 4: //back

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
            spriteBatch.DrawString(menuFont, "Difficulty: ", new Vector2(50, 50), DifficultyColor1);
            spriteBatch.DrawString(menuFont, " " + selectedDifficulty, new Vector2(300, 50), DifficultyColor2);
            spriteBatch.DrawString(menuFont, "Character: " + selectedCharacter, new Vector2(50, 120), CharacterColor);
            spriteBatch.DrawString(menuFont, "Music: " + selectedSong, new Vector2(50, 190), MusicColor);
            spriteBatch.DrawString(menuFont, "Back", new Vector2(50, 390), BackColor);

            spriteBatch.Draw(Character.playerTexture, new Rectangle(500, 100, 320, 320), source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
