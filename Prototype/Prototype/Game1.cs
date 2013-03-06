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
        public static Game1 instance; //In andere classes makkelijker gebruik maken van game1
        public static ContentManager contentInstance;

        MainMenu mainMenu;
        OptionsMenu optionsMenu;
        DeathScreen deathScreen;
        public Level1 level1;
        public Level2 level2;
        public Level3 level3;
        LevelMenu levelMenu;
        public ILevel currentLevel;

        public static List<Projectile> projectiles;

        public static Texture2D standardTexture;
        public static Texture2D gunTexture;
        public static Texture2D projectileTexture;
        public static Texture2D character1Texture;
        public static Texture2D character2Texture;
        public static Texture2D character3Texture;
        public static Texture2D enemyTexture1;
        public static Texture2D enemyTexture2;
        public static Texture2D enemyTexture3;
        public static Texture2D backgroundTexture1;
        public static Texture2D backgroundTexture2;
        public static Texture2D grassTexture;
        public static Texture2D baseTexture;
        public static Texture2D cloudTexture;
        public static Texture2D holeTexture;

        public enum GameState //Game status
        {
            mainmenu,
            options,
            levelMenu,
            running,
            dead,
        }
        public GameState gameState = GameState.mainmenu;

        //Sounds;
        public static SoundEffect laserSound;
        public static SoundEffect spiderSound;
        public static SoundEffect runSound1;
        public static SoundEffect runSound2;
        public static SoundEffect starSound;
        public static SoundEffect coinSound;
        public static SoundEffect jumpSound;
        public static SoundEffect runningSound1;
        public static SoundEffect runningSound2;
        public static SoundEffect dragonSound;
        public static SoundEffect hurtSound;
        public static SoundEffect deathSound;

        // Muziek
        public static Song rickSong;
        public static Song littleSong;
        public static Song rapidSong;
        public static Song slagsmalklubbenSong;
        public static Song soundtrackSong;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this; //om game1 te kunnen benaderen vanuit andere classes.
            contentInstance = this.Content;
        }

        protected override void Initialize()
        {
            mainMenu = new MainMenu(this.Content, this);
            optionsMenu = new OptionsMenu(this.Content, this);
            levelMenu = new LevelMenu(this.Content, this);
            deathScreen = new DeathScreen(this.Content, this);

            //Laad textures van character. Dit staat hier en niet in LoadContent, omdat ze al in het menu gebruikt worden, die in Initialize staat.
            character1Texture = this.Content.Load<Texture2D>("character1");
            character2Texture = this.Content.Load<Texture2D>("character2");
            character3Texture = this.Content.Load<Texture2D>("character3");

            level1 = new Level1(this.Content, this, level1);
            level2 = new Level2(this.Content, this);
            level3 = new Level3(this.Content, this);

            level1.Initialize();
            level2.Initialize();
            level3.Initialize();

            projectiles = new List<Projectile>();

            //// Game Components opnemen
            Components.Add(new Scrollen(this, level1));
            Components.Add(new health(this));
            Components.Add(new score(this));
            Components.Add(new coins(this));

            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch); // Zodat je de spriteBatch kan gebruiken in GameComponents

            //achtergrond textures
            backgroundTexture1 = Content.Load<Texture2D>("Background 2");
            backgroundTexture2 = Content.Load<Texture2D>("sky");

            //vijand textures
            enemyTexture1 = Content.Load<Texture2D>("Slime");
            enemyTexture2 = Content.Load<Texture2D>("Dragon");
            enemyTexture3 = Content.Load<Texture2D>("spider");

            //platform textures
            grassTexture = Content.Load<Texture2D>("grassTexture1");
            baseTexture = Content.Load<Texture2D>("Ondergrond");
            cloudTexture = Content.Load<Texture2D>("Cloud");
            standardTexture = Content.Load<Texture2D>("platform");

            //Andere textures
            gunTexture = Content.Load<Texture2D>("star");
            projectileTexture = Content.Load<Texture2D>("laser");
            holeTexture = Content.Load<Texture2D>("hole");

            //Geluidseffecten
            laserSound = Content.Load<SoundEffect>("laserSound");
            spiderSound = Content.Load<SoundEffect>("spiderSound");
            starSound = Content.Load<SoundEffect>("starSound");
            coinSound = Content.Load<SoundEffect>("Yahoo");
            jumpSound = Content.Load<SoundEffect>("jumpSound");
            runningSound1 = Content.Load<SoundEffect>("runningSound1");
            runningSound2 = Content.Load<SoundEffect>("runningSound2");
            dragonSound = Content.Load<SoundEffect>("dragonSound");
            hurtSound = Content.Load<SoundEffect>("hurt");
            deathSound = Content.Load<SoundEffect>("Dundundun");

            // Muziek
            rickSong = Content.Load<Song>("rickroll");
            littleSong = Content.Load<Song>("little");
            rapidSong = Content.Load<Song>("rapid");
            slagsmalklubbenSong = Content.Load<Song>("Slagsmalklubben");
            soundtrackSong = Content.Load<Song>("LF2 Soundtrack");

            MediaPlayer.Stop();
            PlayMusic(soundtrackSong);
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

                if (projectiles != null)
                    UpdateProjectiles();

                if (Character.deathTimer > 2500)
                {
                    gameState = GameState.dead;
                }
            }
            else if (gameState == GameState.dead)
                deathScreen.Update(gameTime, currentKeyboardState, previousKeyboardState);


            if (currentKeyboardState.IsKeyDown(Keys.R))
            {
                level1 = new Level1(this.Content, this, level1);
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


        //private void UpdateCollision()
        //{
        //    // Use the Rectangle's built-in intersect function to
        //    // determine if two objects are overlapping
        //    Rectangle rectangle1;
        //    Rectangle rectangle2;

        //    // Only create the rectangle once for the player
        //    rectangle1 = new Rectangle((int)Character.bounds.X, (int)Character.bounds.Y, Character.bounds.Width, Character.bounds.Height);

        //    // Do the collision between the player and the enemies
        //    for (int i = 0; i < currentlevel.enemies.Count; i++)
        //    {
        //        rectangle2 = new Rectangle((int)enemies[i].Position.X,
        //        (int)enemies[i].Position.Y,
        //        enemies[i].Width,
        //        enemies[i].Height);

        //        // Determine if the two objects collided with each
        //        // other
        //        if (rectangle1.Intersects(rectangle2))
        //        {
        //            // Subtract the health from the player based on
        //            // the enemy damage
        //            player.Health -= enemies[i].Damage;

        //            // Since the enemy collided with the player
        //            // destroy it
        //            enemies[i].Health = 0;

        //            // If the player health is less than zero we died
        //            if (player.Health <= 0)
        //            {
        //                player.Active = false;
        //                alive = false;
        //            }
        //        }
        //    }
        //    // Projectile vs Enemy Collision
        //    for (int i = 0; i < projectiles.Count; i++)
        //    {
        //        for (int j = 0; j < enemies.Count; j++)
        //        {
        //            // Create the rectangles we need to determine if we collided with each other
        //            rectangle1 = new Rectangle((int)projectiles[i].Position.X -
        //            projectiles[i].Width / 2, (int)projectiles[i].Position.Y -
        //            projectiles[i].Height / 2, projectiles[i].Width, projectiles[i].Height);

        //            rectangle2 = new Rectangle((int)enemies[j].Position.X - enemies[j].Width / 2,
        //            (int)enemies[j].Position.Y - enemies[j].Height / 2,
        //            enemies[j].Width, enemies[j].Height);

        //            // Determine if the two objects collided with each other
        //            if (rectangle1.Intersects(rectangle2))
        //            {
        //                enemies[j].Health -= projectiles[i].Damage;
        //                projectiles[i].Active = false;
        //            }
        //        }
        //    }
        //}





        public static void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(projectileTexture, position);

            if (Character.currentFacing == Character.facing.right)
                projectile.projectileMoveSpeed = 6;
            else
                projectile.projectileMoveSpeed = -6;

            projectiles.Add(projectile);

            laserSound.Play();
        }

        public static void UpdateProjectiles()
        {
            // Update the Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update();

                if (projectiles[i].Active == false)
                {
                    projectiles.RemoveAt(i);
                }
                CheckCollisionProjectile(projectiles[i]);
            }
        }

        public static void CheckCollisionProjectile(Projectile projectile)
        {
            List<Enemy> enemies = instance.currentLevel.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].bounds.Intersects(projectile.bounds))
                {
                    
                    enemies[i].health -= 1;
                    projectiles.Remove(projectile);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin();

            //Kijken welke status om te bepalen welk 'scherm' te tonen
            if (gameState == GameState.mainmenu)
            {
                spriteBatch.Draw(backgroundTexture1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                mainMenu.Draw(gameTime, spriteBatch);
            }
            else if (gameState == GameState.options)
            {
                spriteBatch.Draw(backgroundTexture1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                optionsMenu.Draw(gameTime, spriteBatch);
            }
            else if (gameState == GameState.levelMenu)
            {
                spriteBatch.Draw(backgroundTexture1, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                levelMenu.Draw(gameTime, spriteBatch);
            }
            else if (gameState == GameState.running | gameState == GameState.dead)
            {
                currentLevel.Draw(gameTime, spriteBatch);

                //Teken de projectielen
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Draw(spriteBatch);
                }

                if (gameState == GameState.dead)
                {
                    //zorgt voor een donkerdere achtergrond. Deze code is overgenomen uit een ander spel.
                    Color transparant = new Color(0, 0, 0, 100);
                    Texture2D whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
                    whiteTexture.SetData<Color>(new Color[] { transparant });

                    spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, 800, 800), Color.White);

                    deathScreen.Draw(gameTime, spriteBatch);
                }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
