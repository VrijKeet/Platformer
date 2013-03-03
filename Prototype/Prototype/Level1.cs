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
    public class Level1 : ILevel
    {
        public Game1 game;
        public Texture2D backgroundTexture;
        public Texture2D grassTexture1;


        public Character character;

        public List<Platform> platforms;
        public List<Rectangle> startDimPlat;
        public List<Projectile> projectiles = new List<Projectile>();



        public Level1(ContentManager content, Game1 game1)
        {
            this.game = game1;
            backgroundTexture = content.Load<Texture2D>("sky");
            grassTexture1 = content.Load<Texture2D>("grassTexture1");
            platforms = new List<Platform>();
            character = new Character(game1); //"game1" omdat character een constructor heeft
        }

        public void Initialize()
        {
            //testPlatform = new Platform();
            //testPlatform.Initialize(grassTexture1);
            //testPlatform.boundingBox = new Rectangle(100, 100, 300, 100);
            platforms.Clear();

            // Lijst met posities van platformen
            startDimPlat = new List<Rectangle>();

            startDimPlat.Add(new Rectangle(0, 350, 2000, 350)); // Huge platform
            startDimPlat.Add(new Rectangle(600, 250, 100, 100));
            startDimPlat.Add(new Rectangle(1200, 250, 100, 100));
            startDimPlat.Add(new Rectangle(1900, 250, 100, 100));
            startDimPlat.Add(new Rectangle(750, 150, 400, 100)); // Groot platform onder
            startDimPlat.Add(new Rectangle(800, 50, 90, 60));
            startDimPlat.Add(new Rectangle(905, 50, 90, 60));
            startDimPlat.Add(new Rectangle(1020, 50, 90, 60));
            startDimPlat.Add(new Rectangle(700, -50, 500, 100)); // Groot platform boven
            startDimPlat.Add(new Rectangle(600, -150, 200, 100)); // Wolk begin
            startDimPlat.Add(new Rectangle(300, -250, 200, 100));
            startDimPlat.Add(new Rectangle(0, -350, 200, 100));
            startDimPlat.Add(new Rectangle(-300, -450, 200, 100)); // Wolk einde
            startDimPlat.Add(new Rectangle(2100, 350, 500, 100));
            startDimPlat.Add(new Rectangle(2800, 450, 300, 100));
            startDimPlat.Add(new Rectangle(3300, 550, 300, 100));
            startDimPlat.Add(new Rectangle(3850, 550, 1000, 350));
            startDimPlat.Add(new Rectangle(4950, 550, 1000, 350));

            // Platformen aanmaken
            for (int i = 0; i < startDimPlat.Count; i++)
            {
                Platform platform = new Platform();
                platforms.Add(platform);
            }

            // Platformen texture geven
            for (int i = 0; i < platforms.Count; i++)
                platforms[i].Initialize(grassTexture1);

            // Platformen posities geven
            for (int i = 0; i < platforms.Count; i++)
                platforms[i].boundingBox = startDimPlat[i];
            
            Character.bounds = new Rectangle(300, platforms[0].boundingBox.Y - Game1.character1Texture.Height, 80, 80);
        }


        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            character.Update(gameTime, currentKeyboardState, previousKeyboardState);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

            // Maak hard-coded 4 platformen aan:

          

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(gameTime, spriteBatch); //Teken iedere platform in de lijst
            }
            character.Draw(gameTime, spriteBatch);
        }

        public List<Platform> GetPlatforms()
        {
            return platforms;
        }
    }
}
