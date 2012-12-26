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
    class Character
    {
        Game1 game; //Omdat hij moet kijken of character platform raakt, en die code in game1.cs staat

        double elapsedTime;

        public Texture2D playerTexture;
        static int boundsWidth = 80; //Breedte van character in het spel
        static int boundsHeight = 80; //Hoogte van character in het spel
        const int sourceWidth = 80; //Breedte van charactre in player.png
        const int sourceHeight = 80; //Hoogte van character in player.png      
        public static Rectangle bounds = new Rectangle(300, 300, boundsWidth, boundsHeight); //Positie en grootte van character
        Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight); //Bepaalt welk gedeelte van player.png wordt getoond
        Rectangle feetBounds = new Rectangle(bounds.X, bounds.Y + 50, 80, 30);

        enum facing
        {
            left,
            right
        }
        facing currentFacing = facing.right;

        enum state
        {
            standing,
            walkingLeft,
            walkingRight,
            runningLeft,
            runningRight,
            jumping,
            falling
        }
        state currentState = state.standing;

        float speed = 0; //Snelheid waarmee character zich in de horizontale richting voortbeweegt
        float maxWalkingSpeed = 5;
        float maxRunningSpeed = 12;
        int jumpingSpeed = -10;
        Vector2 maxJumpingHeight = new Vector2(0, 10);

        int frameCount = 0; //Telt welke frame op het moment in source is
        const int delay = 4; //Vertraagt de animatie en beweging
        int jumpCount = 0; //Telt hoelang character aan het springen is

        public bool alive = true;

        public Character(Game1 game1) //Constructor
        {
            this.game = game1;
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            feetBounds = new Rectangle(bounds.X + 30, bounds.Y + 70, 20, 10);
            KeyInput(currentKeyboardState, previousKeyboardState);
            Movement();
        }


        private void KeyInput(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if (currentState != state.jumping && currentState != state.falling) //Als de speler niet in de lucht is
            {
                if (!currentKeyboardState.IsKeyDown(Keys.LeftShift)) //Als shift niet is ingedrukt
                {
                    if (currentKeyboardState.IsKeyDown(Keys.D)) //Laat character naar rechts lopen als D is ingedrukt
                    {
                        currentState = state.walkingRight;
                        currentFacing = facing.right;
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.A)) //Laat character naar links lopen als A is ingedrukt
                    {
                        currentState = state.walkingLeft;
                        currentFacing = facing.left;
                    }
                    if (!currentKeyboardState.IsKeyDown(Keys.D) && (currentState == state.walkingRight | currentState == state.runningRight)) //Zorg dat character stopt met lopen of rennen als D wordt losgelaten
                    {
                        currentState = state.standing;
                        currentFacing = facing.right;
                    }
                    if (!currentKeyboardState.IsKeyDown(Keys.A) && (currentState == state.walkingLeft | currentState == state.runningLeft)) //Zorg dat character stopt met lopen of rennen als A wordt losgelaten
                    {
                        currentState = state.standing;
                        currentFacing = facing.left;
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.W)) //Laat character springen als W wordt ingedrukt
                    {
                        currentState = state.jumping;
                    }
                }
                else if (currentKeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    if (!currentKeyboardState.IsKeyDown(Keys.D)) //Zorg dat character stopt met rennen als shift nog steeds is ingedrukt, maar D niet meer
                    {
                        currentState = state.standing;
                    }

                    if (!currentKeyboardState.IsKeyDown(Keys.A)) //Zorg dat character stopt met rennen als shift nog steeds is ingedrukt, maar A niet meer
                    {
                        currentState = state.standing;
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.D)) //Run right when shift and D are pressed
                    {
                        currentState = state.runningRight;
                        currentFacing = facing.right;
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.A)) //Run left when shift and A are pressed
                    {
                        currentState = state.runningLeft;
                        currentFacing = facing.left;
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.W)) //Jump when W is pressed once
                    {
                        currentState = state.jumping;
                    }
                }
            }
            else if (currentState == state.jumping | currentState == state.falling)
            {
                if (!currentKeyboardState.IsKeyDown(Keys.LeftShift)) //When he's not running when jumping
                {
                    if (currentKeyboardState.IsKeyDown(Keys.D))
                    {
                        if (speed <= maxWalkingSpeed)
                            speed += .2f;
                        currentFacing = facing.right;
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.A))
                    {
                        if (speed >= -maxWalkingSpeed)
                            speed -= .2f;
                        currentFacing = facing.left;
                    }
                }
                else if (currentKeyboardState.IsKeyDown(Keys.LeftShift)) //When he's running
                {
                    if (currentKeyboardState.IsKeyDown(Keys.D))
                    {
                        if (speed <= maxRunningSpeed)
                            speed += .2f;
                        currentFacing = facing.right;
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.A))
                    {
                        if (speed >= -maxRunningSpeed)
                            speed -= .2f;
                        currentFacing = facing.left;
                    }
                }
            }


            if (currentKeyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E)) //Character veranderen met E
            {
                if (playerTexture == Game1.character1Texture)
                    playerTexture = Game1.character2Texture;
                else if (playerTexture == Game1.character2Texture)
                    playerTexture = Game1.character1Texture;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q)) //Character veranderen met Q
            {
                if (playerTexture == Game1.character1Texture)
                    playerTexture = Game1.character2Texture;
                else if (playerTexture == Game1.character2Texture)
                    playerTexture = Game1.character1Texture;
            }            
        }


        private void Movement()
        {
            if (frameCount % delay == 0) //Eens in de <delay-waarde> frames
            {
                switch (currentState) //Kijk wat de huidige status is
                {
                    case state.standing:

                        Platform platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform == null) //Als character geen platform raakt.
                        {
                            currentState = state.falling;
                        }
                        else
                        {
                            currentState = state.standing;
                        }

                        bounds.X += 0;
                        speed = 0;

                        if (frameCount / delay >= 4)
                            frameCount = 0;
                        source = new Rectangle(frameCount / delay * 80, 0, sourceWidth, sourceHeight);

                        break;

                    case state.walkingLeft:
                        platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform == null) //Als character geen platform raakt.
                        {
                            currentState = state.falling;
                        }
                        else
                        {
                            if (speed > -maxWalkingSpeed) //gaat steeds sneller lopen tot maximum snelheid
                                speed -= 2;

                            if (speed <= -maxWalkingSpeed)
                                speed = -maxWalkingSpeed;

                            bounds.X += (int)speed;

                            if (frameCount / delay >= 6)
                                frameCount = 0;
                            source = new Rectangle(frameCount / delay * 80, 80, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.walkingRight:
                        platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform == null) //Als character geen platform raakt.
                        {
                            currentState = state.falling;
                        }
                        else
                        {
                            if (speed < maxWalkingSpeed) //gaat steeds sneller lopen tot maximum snelheid
                                speed += 2;

                            if (speed >= maxWalkingSpeed)
                                speed = maxWalkingSpeed;

                            bounds.X += (int)speed;

                            if (frameCount / delay >= 6)
                                frameCount = 0;
                            source = new Rectangle(frameCount / delay * 80, 80, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.runningLeft:
                        platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform == null) //Als character geen platform raakt.
                        {
                            currentState = state.falling;
                        }
                        else
                        {
                            if (speed > -maxRunningSpeed) //gaat steeds sneller rennen tot maximum snelheid
                                speed -= 3;
                            bounds.X += (int)speed;

                            if (frameCount / delay >= 3)
                                frameCount = 0;
                            source = new Rectangle(frameCount / delay * 80, 160, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.runningRight:
                        platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform == null) //Als character geen platform raakt.
                        {
                            currentState = state.falling;
                        }
                        else
                        {
                            if (speed < maxRunningSpeed) //gaat steeds sneller rennen tot maximum snelheid
                                speed += 3;
                            bounds.X += (int)speed;

                            if (frameCount / delay >= 3)
                                frameCount = 0;
                            source = new Rectangle(frameCount / delay * 80, 160, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.jumping:
                        if (speed > -6 && speed < 6) //Als kleine snelheid
                        {
                            if (jumpCount < 2) //Als nog maar pas is begonnen met springen
                            {
                                if (frameCount / delay >= 2)
                                    frameCount = 0;
                            }
                            else if (jumpCount >= 2)
                            {
                                frameCount = 2 * delay;
                            }
                        }
                        else  //Als de speler enige snelheid had
                        {
                            if (jumpCount < 2) //Als nog maar pas is begonnen met springen
                            {
                                if (frameCount / delay >= 2)
                                    frameCount = 0;
                            }
                            else if (jumpCount >= 3)
                            {
                                frameCount = 3 * delay;
                            }
                        }

                        jumpingSpeed += 1;
                        if (jumpingSpeed == 0) //Als hij in de hoogste positie is
                            currentState = state.falling;

                        bounds.X += (int)speed;
                        bounds.Y += jumpingSpeed;
                        jumpCount++;

                        source = new Rectangle(frameCount / delay * sourceWidth, 3 * sourceHeight, sourceWidth, sourceHeight);
                        break;

                    case state.falling:

                        if (jumpingSpeed <= 0) //Als hij nog niet gesprongen heeft, laat hem dan niet eerst springen
                            jumpingSpeed = 0;
                        bounds.Y += jumpingSpeed;
                        bounds.X += (int)speed;
                        jumpingSpeed += 2;

                        if (jumpingSpeed >= 18) //Zorgt dat hij character niet sneller dan 18 pixels/frame naar beneden valt. Sneller dan 18 p/f zorgt ervoor dat character door een platform heen valt.
                            jumpingSpeed = 18;

                        platform = game.GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
                        if (platform != null) //Als character niet geen, dus wél een platform raakt.
                        {
                            bounds.Y = platform.boundingBox.Top - bounds.Height + 1; //Blijf op platform staan
                            jumpCount = 0;
                            jumpingSpeed = -10;
                            currentState = state.standing;
                        }

                        if (speed > -6 && speed < 6) //Als kleine snelheid
                        {
                            frameCount = 2 * delay;
                        }
                        else
                        {
                            frameCount = 3 * delay;
                        }
                        source = new Rectangle(frameCount / delay * sourceWidth, 3 * sourceHeight, sourceWidth, sourceHeight);
                        jumpCount++;
                        break;
                }
            }
            frameCount++;
        }




        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (alive == true)
            {
                if (currentFacing == facing.right)
                    spriteBatch.Draw(playerTexture, bounds, source, Color.White);
                else
                    spriteBatch.Draw(playerTexture, bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
        }


    }
}