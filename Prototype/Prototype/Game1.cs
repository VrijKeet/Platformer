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
        public static Game1 instance;

        MainMenu mainMenu;
        OptionsMenu optionsMenu;
        DeathScreen deathScreen;
        public Level1 level1;
        public Level2 level2;
        public Level3 level3;
        LevelMenu levelMenu;
        //public static Gun gun;


        Texture2D standardTexture;
        Texture2D gunTexture1;
        Texture2D gunPickedTexture1;
        public static Texture2D projectileTexture;

        public static Texture2D character1Texture;
        public static Texture2D character2Texture;
        public static Texture2D character3Texture;
        Texture2D backgroundTexture;

        public enum GameState //Game status
        {
            mainmenu,
            options,
            levelMenu,
            running,
            dead,
        }
        public GameState gameState = GameState.mainmenu;

        public ILevel currentLevel;


        public double deathTimer = 0;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Muziek
        public static Song rickSong;
        public static Song littleSong;
        public static Song rapidSong;
        public static Song slagsmalklubbenSong;
        public static Song soundtrackSong;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this; //om game1 te kunnen benaderen vanuit andere classes.
        }


        protected override void Initialize() //Contenmanager, zodat deze aan ieder leve gegeven kan worden, zodat deze het weer aan objecten kunnen geven en daar textures in geladen kunnen worden.
        {
            mainMenu = new MainMenu(this.Content, this);
            optionsMenu = new OptionsMenu(this.Content, this);
            levelMenu = new LevelMenu(this.Content, this);
            deathScreen = new DeathScreen(this.Content, this);

            character1Texture = this.Content.Load<Texture2D>("character1");
            character2Texture = this.Content.Load<Texture2D>("character2");
            character3Texture = this.Content.Load<Texture2D>("character3");



            level1 = new Level1(this.Content, this);
            level2 = new Level2(this.Content, this);
            level3 = new Level3(this.Content, this);
            currentLevel = level1;

            level1.Initialize();
            level2.Initialize();
            level3.Initialize();


            //for (int i = 0; i < platforms.Count; i++)
            //{
            //    platforms[i].boundingBoxTop = new Rectangle(platforms[i].boundingBox.X, platforms[i].boundingBox.Y,
            //        platforms[i].boundingBox.Width, 5);
            //    //Maak voor iedere platform een rechthoek van de top aan.
            //}

            //gun = new Gun();
            //gun.boundingBox = new Rectangle(500, 320, 10, 10);

            //projectiles = new List<Projectile>();

            //// Game Components opnemen
            Components.Add(new Scrollen(this));
            Components.Add(new health(this));
            //Components.Add(new score(this));
            //Components.Add(new ladder(this));



            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch); // Zodat je de spriteBatch kan gebruiken in GameComponents

            backgroundTexture = this.Content.Load<Texture2D>("Background 2");
            standardTexture = Content.Load<Texture2D>("platform");
            gunTexture1 = Content.Load<Texture2D>("gunTexture");
            gunPickedTexture1 = Content.Load<Texture2D>("gloves1");
            //gun.Initialize(gunTexture1);
            projectileTexture = Content.Load<Texture2D>("laser");

            // Muziek
            rickSong = Content.Load<Song>("rickroll");
            littleSong = Content.Load<Song>("little");
            rapidSong = Content.Load<Song>("rapid");
            slagsmalklubbenSong = Content.Load<Song>("Slagsmalklubben");
            soundtrackSong = Content.Load<Song>("LF2 Soundtrack");

            MediaPlayer.Stop();
            //PlayMusic(soundtrackSong);
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
            else if (gameState == GameState.levelMenu)
            {
                levelMenu.Update(gameTime, currentKeyboardState, previousKeyboardState);
            }
            else if (gameState == GameState.running) //Als gebruiker level1 speelt
            {
                if (currentKeyboardState.IsKeyDown(Keys.Escape)) //Ga naar het startscherm als de "escape"-toets wordt ingedrukt
                    gameState = GameState.mainmenu;

                currentLevel.Update(gameTime, currentKeyboardState, previousKeyboardState);
                
                //UpdateProjectiles();

                //if (gun.picked)
                //    gun.gunTexture = gunPickedTexture1;

                //if (health.lifes <= 0)
                //{
                //    deathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                //    //Character.currentState = Character.state.dead;
                //}

                if (deathTimer > 3000)
                    gameState = GameState.dead;
            }
            else if (gameState == GameState.dead)
                deathScreen.Update(gameTime, currentKeyboardState, previousKeyboardState);



            if (currentKeyboardState.IsKeyDown(Keys.R))
            {
                level1 = new Level1(this.Content, this);
            }

            // Status van toetsenbord van vorige doorloop opslaan
            previousKeyboardState = currentKeyboardState;



            base.Update(gameTime);
        }

        public static void PlayMusic(Song song)
        {
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            try
            {
                // Play the music
                MediaPlayer.Play(song);

                // Loop the currently playing song
                MediaPlayer.IsRepeating = true;
            }
            catch { }
        }



        //public static void AddProjectile(Vector2 position)
        //{
        //    Projectile projectile = new Projectile();
        //    projectile.Initialize(projectileTexture, position);

        //    if (Character.currentFacing == Character.facing.right)
        //        projectile.projectileMoveSpeed = 3;
        //    else
        //        projectile.projectileMoveSpeed = -3;

        //    projectiles.Add(projectile);
        //}




        //public static void UpdateProjectiles()
        //{
        //    // Update the Projectiles
        //    for (int i = projectiles.Count - 1; i >= 0; i--)
        //    {
        //        projectiles[i].Update();

        //        //CheckCollisionProjectile(projectiles[i]);

        //        if (projectiles[i].Active == false)
        //        {
        //            projectiles.RemoveAt(i);
        //        }
        //    }
        //}

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






        //public Gun GetIntersectingGun(Rectangle feetBounds)
        //{
        //    if (gun.boundingBox.Intersects(feetBounds))
        //        return gun;

        //    return null;
        //}




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
 
            //Kijken welke status om te bepalen welk 'scherm' te tonen
            if (gameState == GameState.mainmenu)
            {

                mainMenu.Draw(gameTime, spriteBatch);

            }
            else if (gameState == GameState.options)
            {
                optionsMenu.Draw(gameTime, spriteBatch);
            }
            else if (gameState == GameState.levelMenu)
            {
                levelMenu.Draw(gameTime, spriteBatch);
            }
            else if (gameState == GameState.running)
            {
                currentLevel.Draw(gameTime, spriteBatch);

                //gun.Draw(gameTime, spriteBatch);

                //// Draw the Projectiles
                //for (int i = 0; i < projectiles.Count; i++)
                //{
                //    projectiles[i].Draw(spriteBatch);
                //}

            }
            else if (gameState == GameState.dead)
            {
                deathScreen.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
