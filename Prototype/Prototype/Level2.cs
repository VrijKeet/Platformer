﻿using System;
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
    public class Level2 : ILevel
    {
        public Game1 game;
        public Texture2D backgroundTexture;
        public Texture2D grassTexture;
        public Texture2D standard;

        public Texture2D enemyTexture2;

        public Character character;
        public List<Platform> platforms;
        public Vector2[] startPosPlat;
        public List<Projectile> projectiles = new List<Projectile>();
        public static List<Enemy> enemies = new List<Enemy>();
        public Gun gun;



        public Level2(ContentManager content, Game1 game1)
        {
            this.game = game1;
            character = new Character(game1); //"game1" omdat character een constructor heeft
        }

        public void Initialize()
        {
            backgroundTexture = Game1.backgroundTexture2;
            grassTexture = Game1.grassTexture;

            gun = new Gun();


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

            platforms[0].Initialize(grassTexture);
            platforms[1].Initialize(grassTexture);
            platforms[2].Initialize(grassTexture);
            platforms[3].Initialize(grassTexture);
            platforms[4].Initialize(grassTexture);
            platforms[5].Initialize(grassTexture);
            platforms[6].Initialize(grassTexture);
            platforms[7].Initialize(grassTexture);
            platforms[8].Initialize(grassTexture);
            platforms[9].Initialize(grassTexture);


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

            Character.bounds = new Rectangle(300, 280, 80, 80);
        }


        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            character.Update(gameTime, currentKeyboardState, previousKeyboardState);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

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
