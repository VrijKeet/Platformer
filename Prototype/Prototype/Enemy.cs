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


namespace Prototype
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Enemy
    {
        public Texture2D texture;
        Texture2D enemyTexture1;
        Texture2D enemyTexture2;
        Texture2D enemyTexture3;

        public int health;
        public Rectangle bounds;
        public Rectangle source;
        public Rectangle feetBounds;
        public int distance;
        int richting;
        public bool alive;
        public bool justDied;
        double turningTimer = 10;
        enum facing
        {
            left,
            right
        }
        facing currentFacing = facing.right;

        
        int frameCount = 0;
        int delay = 10;

        public Enemy()
        {
            enemyTexture1 = Game1.enemyTexture1;
            enemyTexture2 = Game1.enemyTexture2;
            enemyTexture3 = Game1.enemyTexture3;
        }

        public void Initialize(Texture2D Texture, Rectangle Bounds, int health)
        {
            texture = Texture;
            bounds = Bounds;
            richting = 1;
            alive = true;
            this.health = health;
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                //if (richting == -1) //naar links
                //{
                //    richting = 1;
                //    currentFacing = facing.right;
                //    distance--;
                //}
                //else if (richting == 1)
                //{
                //    richting = -1;
                //    currentFacing = facing.left;
                //    distance++;
                //}

                //if (distance >= 200)
                //    richting = -1;
                //else if (distance <= 200)
                //    richting = 1;

                //bounds.X += richting;


                Platform platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst

                if (platform == null)//Als enemy geen platform raakt.
                {
                    bounds.X += richting;

                    if (turningTimer == 0)
                    {
                        if (richting == -1) //naar links
                        {
                            richting = 1;
                            bounds.X += 2;
                            currentFacing = facing.right;
                            bounds.Y--;
                        }
                        else if (richting == 1)
                        {
                            richting = -1;

                            bounds.X -= 2;
                            currentFacing = facing.left;
                            bounds.Y--;
                        }
                    }

                    bounds.Y++;
                }
                else
                {
                    bounds.Y = platform.boundingBoxTop.Y - bounds.Height + 1;
                    bounds.X += richting;
                    if (turningTimer >= 1)
                        turningTimer++;
                    if (turningTimer >= 30)
                        turningTimer = 0;
                }

                feetBounds = new Rectangle(bounds.X + (bounds.Width / 4), (bounds.Y + bounds.Height) - 5, (bounds.Width / 4), 5);

                if (frameCount % delay == 0) //Eens in de <delay-waarde> frames
                {
                    if (texture == enemyTexture1) //Slime
                    {
                        if (frameCount / delay >= 3)
                            frameCount = 0;

                        source = new Rectangle(frameCount / delay * 0, 0, 75, 55);
                    }
                    else if (texture == enemyTexture2) //Draak
                    {
                        if (frameCount / delay >= 3)
                            frameCount = 0;

                        source = new Rectangle(frameCount / delay * 96, 2 * 96, 96, 96);
                    }
                    else if (texture == enemyTexture3) //Spider
                    {
                        if (frameCount / delay >= 4)
                            frameCount = 0;

                        source = new Rectangle(frameCount / delay * 93, 3, 89, 65);
                    }
                }
                frameCount++;
            }

            if (health <= 0)
            {
                alive = false;
                bounds = new Rectangle(-1000, -1000, 0, 0);
                giveScore();
            }

            
        }

        private void giveScore()
        {            
                if (texture == enemyTexture1) //Slime
                    score.currentScore += 1;

                else if (texture == enemyTexture3) //Spin
                {
                    score.currentScore += 5;
                    Game1.spiderSound.Play();
                }
                else if (texture == enemyTexture2) //Draak
                {
                    score.currentScore += 30;
                    Game1.dragonSound.Play();
                }
                health = 1;
        }


        public Platform GetIntersectingPlatform(Rectangle feetBounds) //Kijken of een platform in de lijst in contact komt met enemy
        {
            List<Platform> platforms = Game1.instance.currentLevel.GetPlatforms();
            for (int i = 0; i < platforms.Count; i++) //Kijk voor iedere platform
            {
                if (platforms[i].boundingBox.Intersects(feetBounds)) //Als een platform in contact is met character
                    return platforms[i]; //Onthoud die informatie dan
            }
            return null; //Als géén platform in de lijst in contact komt met character, onthoud die informatie
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (alive)
            {
                if (texture == enemyTexture1) //Slime
                {
                    if (currentFacing == facing.left)
                        spriteBatch.Draw(texture, bounds, source, Color.White);
                    else
                        spriteBatch.Draw(texture, bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (texture == enemyTexture2) //Dragon
                {
                    if (currentFacing == facing.right)
                        spriteBatch.Draw(texture, bounds, source, Color.White);
                    else
                        spriteBatch.Draw(texture, bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
                else if (texture == enemyTexture3) //Spider
                {
                    if (currentFacing == facing.right)
                        spriteBatch.Draw(texture, bounds, source, Color.White);
                    else
                        spriteBatch.Draw(texture, bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                }
            }
        }
    }
}
