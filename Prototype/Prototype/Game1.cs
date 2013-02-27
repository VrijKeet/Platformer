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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MainMenu mainMenu;
        OptionsMenu optionsMenu;
        DeathScreen deathScreen;
        public static Character character;
        public static Gun gun;
            
        public static Texture2D character1Texture;
        public static Texture2D character2Texture;
        public static Texture2D character3Texture;
        Texture2D backgroundTexture;
        Texture2D standardTexture;
        Texture2D grassTexture1;
        Texture2D gunTexture1;
        Texture2D gunPickedTexture1;
        public static Texture2D projectileTexture;

        public static List<Platform> platforms;
        public static Vector2[] startPosPlat;
        public static List<Projectile> projectiles = new List<Projectile>();

        public enum GameState //Game status
        {
            mainmenu,
            options,
            running,
            dead
        }
        public GameState gameState = GameState.mainmenu;

        public double deathTimer = 0;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            mainMenu = new MainMenu(this.Content, this);
            optionsMenu = new OptionsMenu(this.Content, this);
            deathScreen = new DeathScreen(this.Content, this);

            // Maak hard-coded 4 platformen aan:
            platforms = new List<Platform>();
            Platform platform = new Platform();
            Platform platform2 = new Platform();
            Platform platform3 = new Platform();
            Platform platform4 = new Platform();
            Platform platform5 = new Platform();
            Platform platform6 = new Platform();
            Platform platform7 = new Platform();
            Platform platform8 = new Platform();
            Platform platform9 = new Platform();
            Platform platform10 = new Platform();
            platforms.Add(platform);
            platforms.Add(platform2);
            platforms.Add(platform3);
            platforms.Add(platform4);
            platforms.Add(platform5);
            platforms.Add(platform6);
            platforms.Add(platform7);
            platforms.Add(platform8);
            platforms.Add(platform9);
            platforms.Add(platform10);
            startPosPlat = new Vector2[10] { new Vector2(233, 380), new Vector2(150, 290), new Vector2(350, 330), 
                new Vector2(100, 430), new Vector2(-100, 250), new Vector2(600, 350), new Vector2(200, 200), 
                new Vector2(200, 150), new Vector2(200, 100), new Vector2(200, 50) }; // Begin posities voor respawnen
            platform.boundingBox = new Rectangle((int)startPosPlat[0].X, (int)startPosPlat[0].Y, 334, 100);
            platform2.boundingBox = new Rectangle((int)startPosPlat[1].X, (int)startPosPlat[1].Y, 100, 10);
            platform3.boundingBox = new Rectangle((int)startPosPlat[2].X, (int)startPosPlat[2].Y, 300, 15);
            platform4.boundingBox = new Rectangle((int)startPosPlat[3].X, (int)startPosPlat[3].Y, 50, 10);
            platform5.boundingBox = new Rectangle((int)startPosPlat[4].X, (int)startPosPlat[4].Y, 300, 10);
            platform6.boundingBox = new Rectangle((int)startPosPlat[5].X, (int)startPosPlat[5].Y, 300, 10);
            platform7.boundingBox = new Rectangle((int)startPosPlat[6].X, (int)startPosPlat[6].Y, 100, 10);
            platform8.boundingBox = new Rectangle((int)startPosPlat[7].X, (int)startPosPlat[7].Y, 100, 10);
            platform9.boundingBox = new Rectangle((int)startPosPlat[8].X, (int)startPosPlat[8].Y, 100, 10);
            platform10.boundingBox = new Rectangle((int)startPosPlat[9].X, (int)startPosPlat[9].Y, 100, 10);
            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].boundingBoxTop = new Rectangle(platforms[i].boundingBox.X, platforms[i].boundingBox.Y,
                    platforms[i].boundingBox.Width, 5);
                //Maak voor iedere platform een rechthoek van de top aan.
            }

            gun = new Gun();
            gun.boundingBox = new Rectangle(500, 320, 10, 10);

            projectiles = new List<Projectile>();

            // Game Components opnemen
            Components.Add(new Scrollen(this));
            Components.Add(new health(this));
            Components.Add(new Enemy(this));
            Components.Add(new score(this));
            Components.Add(new ladder(this));

            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch); // Zodat je de spriteBatch kan gebruiken in GameComponents

            character = new Character(this); //"this" omdat character een constructor heeft
            character1Texture = this.Content.Load<Texture2D>("character1");
            character2Texture = this.Content.Load<Texture2D>("character2");
            character3Texture = this.Content.Load<Texture2D>("character3");
            character.playerTexture = character1Texture; //Laat de character met character1Texture beginnen

            standardTexture = Content.Load<Texture2D>("platform");
            grassTexture1 = Content.Load<Texture2D>("grassTexture1");
            backgroundTexture = Content.Load<Texture2D>("sky");

            gunTexture1 = Content.Load<Texture2D>("gunTexture");
            gunPickedTexture1 = Content.Load<Texture2D>("gloves1");

            gun.Initialize(gunTexture1);

            projectileTexture = Content.Load<Texture2D>("laser");

            //for (int i = 0; i < platforms.Count; i++)
            //{
            //    platforms[i].Initialize(standardTexture); //Geef iedere platform dezelfde texture
            //}
            platforms[0].Initialize(grassTexture1);
            platforms[1].Initialize(grassTexture1);
            platforms[2].Initialize(grassTexture1);
            platforms[3].Initialize(grassTexture1);
            platforms[4].Initialize(grassTexture1);
            platforms[5].Initialize(grassTexture1);
            platforms[6].Initialize(grassTexture1);
            platforms[7].Initialize(grassTexture1);
            platforms[8].Initialize(grassTexture1);
            platforms[9].Initialize(grassTexture1);

        }


        protected override void UnloadContent()
        {
        }



        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            //Als gebruiker in het hoofdmenu zit
            if (gameState == GameState.mainmenu)
                mainMenu.Update(gameTime, currentKeyboardState, previousKeyboardState);
            else if (gameState == GameState.options) //Als gebruiker in optiemenu zit
            {
                optionsMenu.Update(gameTime, currentKeyboardState, previousKeyboardState);
            }
            else if (gameState == GameState.running) //Als gebruiker het spel speelt
            {
                if (currentKeyboardState.IsKeyDown(Keys.Escape)) //Ga naar het startscherm als de "escape"-toets wordt ingedrukt
                    gameState = GameState.mainmenu;

                character.Update(gameTime, currentKeyboardState, previousKeyboardState);
                UpdateProjectiles();

                if (gun.picked)
                    gun.gunTexture = gunPickedTexture1;

                if (health.lifes <= 0)
                {
                    deathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    Character.currentState = Character.state.dead;
                }
                if (deathTimer > 3000)
                gameState = GameState.dead;
            }
            else if (gameState == GameState.dead)
                deathScreen.Update(gameTime, currentKeyboardState, previousKeyboardState);



            if (currentKeyboardState.IsKeyDown(Keys.R))
            {
                //NIEUWE LEVEL 1
            }

            // Status van toetsenbord van vorige doorloop opslaan
            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }




        public static void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(projectileTexture, position);

            if (Character.currentFacing == Character.facing.right)
                projectile.projectileMoveSpeed = 3;
            else
                projectile.projectileMoveSpeed = -3;

            projectiles.Add(projectile);
        }




        public static void UpdateProjectiles()
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                //CheckCollisionProjectile(projectiles[i]);

                if (projectiles[i].Active == false)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        //public static List<Enemy> enemies;

        //public static void CheckCollisionProjectile(Projectile p)
        //{
        //    for(int i = enemies.Count; i>0; i--){
        //        if (enemies[i].boundingBox.Intersects(p.boundingBox))
        //        {
        //            enemies.RemoveAt(i);
        //            projectiles.Remove(p);
        //        }
        //    }
        //}



        public Platform GetIntersectingPlatform(Rectangle feetBounds) //Kijken of een platform in de lijst in contact komt met character
        {
            for (int i = 0; i < platforms.Count; i++) //Kijk voor iedere platform
            {
                if (platforms[i].boundingBoxTop.Intersects(feetBounds)) //Als een platform in contact is met character
                    return platforms[i]; //Onthoud die informatie dan
            }
            return null; //Als géén platform in de lijst in contact komt met character, onthoud die informatie
        }


        public Gun GetIntersectingGun(Rectangle feetBounds)
        {
            if (gun.boundingBox.Intersects(feetBounds))
                return gun;

            return null;
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Kijken welke status om te bepalen welk 'scherm' te tonen
            if (gameState == GameState.mainmenu)
            {
                spriteBatch.Begin();
                mainMenu.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
            else if (gameState == GameState.options)
            {
                spriteBatch.Begin();
                optionsMenu.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
            else if (gameState == GameState.running)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].Draw(gameTime, spriteBatch); //Teken iedere platform in de lijst
                }
                character.Draw(gameTime, spriteBatch);
                gun.Draw(gameTime, spriteBatch);

                // Draw the Projectiles
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                spriteBatch.End();

                base.Draw(gameTime);
            }
            else if (gameState == GameState.dead)
            {
                spriteBatch.Begin();
                deathScreen.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
        }
    }
}
