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
        Game1 game; //Omdat hij moet kijken of character platform raakt, en die code in game1.cs wordt opgeslagen

        public Texture2D texture;
        Texture2D enemyTexture1;
        Texture2D enemyTexture2;
        Texture2D enemyTexture3;
        
        public Rectangle bounds;
        public Rectangle source;
        public Rectangle feetBounds;
        public int distance;
        int richting;
        public bool alive;
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

        public void Initialize(Texture2D Texture, Rectangle Bounds)
        {
            texture = Texture;
            bounds = Bounds;
            richting = 1;
            alive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                Platform platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst

                if (platform == null)//Als enemy geen platform raakt.
                {
                    bounds.X += richting;
                    bounds.Y++;

                    if (turningTimer == 0)
                    {
                        if (richting == -1) //naar links
                        {
                            richting = 1;
                            currentFacing = facing.right;
                            bounds.Y--;
                        }
                        else if (richting == 1)
                        {
                            richting = -1;
                            currentFacing = facing.left;
                            bounds.Y--;
                        }
                    }
                }
                else
                {
                    bounds.X += richting;
                    if (turningTimer >= 1)
                    turningTimer++;
                    if (turningTimer >= 10)
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

                        source = new Rectangle(frameCount / delay * 96, 2*96, 96, 96);
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

            //if (alive && Character.bounds.X < bounds.X + bounds.Width && Character.feetBounds.Y + Character.feetBounds.Height > bounds.Y && Character.feetBounds.X + Character.feetBounds.Width > bounds.X && Character.feetBounds.Y < (bounds.Y + bounds.Height) - 60)
            //{
            //    score.currentScore += 1;
            //    alive = false;
            //}
        }



        public Platform GetIntersectingPlatform(Rectangle feetBounds) //Kijken of een platform in de lijst in contact komt met enemy
        {
            List<Platform> platforms = Game1.instance.currentLevel.GetPlatforms();
            for (int i = 0; i < platforms.Count; i++) //Kijk voor iedere platform
            {
                if (platforms[i].boundingBox.Intersects(feetBounds)) //Als een platform in contact is met character
                    return platforms[i]; //Onthoud die informatie dan
            }
            return null; //Als g��n platform in de lijst in contact komt met character, onthoud die informatie
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
