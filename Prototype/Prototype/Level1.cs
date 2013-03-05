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
        public Texture2D grassTexture;
        public Texture2D baseTexture;
        public Texture2D cloudTexture;

        public Texture2D enemyTexture2;
        public Texture2D enemyTexture3;
        public Character character;        
        public Gun gun;

        public List<Platform> platforms;
        public List<Rectangle> startDimPlat;
        public List<Projectile> projectiles = new List<Projectile>();
        public static List<Enemy> enemies;

        public static Vector2 goalPos;


        public Level1(ContentManager content, Game1 game1)
        {
            this.game = game1;
            backgroundTexture = content.Load<Texture2D>("sky");
            grassTexture = content.Load<Texture2D>("grassTexture1");
            baseTexture = content.Load<Texture2D>("Ondergrond");
            cloudTexture = content.Load<Texture2D>("Cloud");
            enemyTexture3 = content.Load<Texture2D>("Mario");
            platforms = new List<Platform>();

            character = new Character(game1); //"game1" omdat character een constructor heeft
            
            enemies = new List<Enemy>();
            enemies.Clear();

            Enemy enemy1 = new Enemy(content);
            Enemy enemy2 = new Enemy(content);
            Enemy enemy3 = new Enemy(content);

            enemies.Add(enemy1);
            enemies.Add(enemy2);
            enemies.Add(enemy3);

            enemies[0].Initialize(enemyTexture3, new Rectangle(350, 350 - Game1.character1Texture.Height, 22, 33)); //Texture en (positie, grootte)
            enemies[1].Initialize(enemyTexture3, new Rectangle(-150, 80, 170, 140));
            enemies[2].Initialize(enemyTexture3, new Rectangle(380, 260, 22, 33));
        }

        public void Initialize()
        {
            //testPlatform = new Platform();
            //testPlatform.Initialize(grassTexture1);
            //testPlatform.boundingBox = new Rectangle(100, 100, 300, 100);
            platforms.Clear();

            // Lijst met posities van platformen
            startDimPlat = new List<Rectangle>();

            startDimPlat.Add(new Rectangle(-300, 350, 2000, 350)); // Huge platform //0
            startDimPlat.Add(new Rectangle(600, 250, 100, 60)); //1
            startDimPlat.Add(new Rectangle(1200, 250, 100, 60)); //2
            startDimPlat.Add(new Rectangle(1800, 340, 100, 60)); //3
            startDimPlat.Add(new Rectangle(750, 150, 400, 100)); // Groot platform onder //4
            startDimPlat.Add(new Rectangle(800, 50, 90, 40)); //5
            startDimPlat.Add(new Rectangle(905, 50, 90, 40)); //6
            startDimPlat.Add(new Rectangle(1020, 50, 90, 40)); //7
            startDimPlat.Add(new Rectangle(700, -50, 500, 50)); // Groot platform boven //8
            startDimPlat.Add(new Rectangle(450, -130, 100, 20)); // Wolk begin //9
            startDimPlat.Add(new Rectangle(180, -140, 100, 20)); //10
            startDimPlat.Add(new Rectangle(-50, -150, 100, 20)); //11
            startDimPlat.Add(new Rectangle(-330, -160, 100, 20)); // Wolk einde //12
            startDimPlat.Add(new Rectangle(2100, 350, 500, 100)); //13
            startDimPlat.Add(new Rectangle(2800, 450, 300, 100)); //14
            startDimPlat.Add(new Rectangle(3300, 550, 300, 100)); //15
            startDimPlat.Add(new Rectangle(3800, 550, 1000, 350)); //16
            startDimPlat.Add(new Rectangle(4950, 550, 1000, 350)); //17

            // Platformen aanmaken
            for (int i = 0; i < startDimPlat.Count; i++)
            {
                Platform platform = new Platform();
                platforms.Add(platform);
            }

            // Platformen texture geven
            for (int i = 0; i < platforms.Count; i++)
                platforms[i].Initialize(grassTexture);

            platforms[0].Initialize(baseTexture);
            platforms[9].Initialize(cloudTexture);
            platforms[10].Initialize(cloudTexture);
            platforms[11].Initialize(cloudTexture);
            platforms[12].Initialize(cloudTexture);

            // Platformen posities geven
            for (int i = 0; i < platforms.Count; i++)
                platforms[i].boundingBox = startDimPlat[i];

            goalPos = new Vector2(5851, 453);
        }


        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            character.Update(gameTime, currentKeyboardState, previousKeyboardState);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime);
            }

            if (Character.boundingBox.X < goalPos.X + Game1.holeTexture.Width && Character.boundingBox.X + Character.boundingBox.Width > goalPos.X && Character.boundingBox.Y < goalPos.Y + Game1.holeTexture.Height && Character.boundingBox.Y + Character.boundingBox.Height > goalPos.Y)
            {
                game.currentLevel = game.level2;
                game.currentLevel.Initialize();
                health.lifes = health.startLifes;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(gameTime, spriteBatch); //Teken iedere platform in de lijst
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(gameTime, spriteBatch); //Teken iedere enemy in de lijst
            }

            spriteBatch.Draw(Game1.holeTexture, goalPos, Color.White);

            character.Draw(gameTime, spriteBatch);

        }

        public List<Platform> GetPlatforms()
        {
            return platforms;
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
        public Gun GetGun()
        {
            return gun;
        }

    }
}
