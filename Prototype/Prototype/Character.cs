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
    public class Character
    {
        Game1 game; //Omdat hij moet kijken of character platform raakt, en die code in game1.cs staat

        double elapsedTime;
        float shootingDurance;
        float shootingTimer;

        public static Texture2D playerTexture;
        public static int boundsWidth = 80; //Breedte van character in het spel
        public static int boundsHeight = 80; //Hoogte van character in het spel
        const int sourceWidth = 80; //Breedte van charactre in player.png
        const int sourceHeight = 80; //Hoogte van character in player.png

        ////////public static Vector2 startPos = new Vector2(300, 300);
        ////////public static Rectangle characterBounds = new Rectangle((int)startPos.X, (int)startPos.Y, Character.boundsWidth, Character.boundsHeight); //Positie en grootte van character
        
        //public static Rectangle bounds = Level1.characterBounds; //Positie en grootte van character
        public static Rectangle bounds = new Rectangle(300, 280, boundsHeight, boundsWidth);
        public static Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight); //Bepaalt welk gedeelte van player.png wordt getoond
        public static Rectangle feetBounds = new Rectangle(bounds.X, bounds.Y + 50, 80, 30);

        public enum facing
        {
            left,
            right
        }
        public static facing currentFacing = facing.right;

        public enum state
        {
            standing,
            walkingLeft,
            walkingRight,
            runningLeft,
            runningRight,
            jumping,
            falling,
            picking,
            shooting,
            dead
        }
        public static state currentState = state.standing;

        bool carryingGun = false;
        bool isShooting = false;

        float speed = 0; //Snelheid waarmee character zich in de horizontale richting voortbeweegt
        float maxWalkingSpeed = 5;
        float maxRunningSpeed = 12;
        public static int jumpingSpeed = -10;
        Vector2 maxJumpingHeight = new Vector2(0, 10);

        int frameCount = 0; //Telt welke frame op het moment in source is
        const int delay = 4; //Vertraagt de animatie en beweging
        public static int jumpCount = 0; //Telt hoelang character aan het springen is

        public bool alive = true;

        // Ladder
        bool onLadder = false;

        public Character(Game1 game1) //Constructor
        {
            this.game = game1;
            playerTexture = Game1.character1Texture; //Laat de character met character1Texture beginnen
        }

        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            feetBounds = new Rectangle(bounds.X + 30, bounds.Y + 70, 20, 10);
            KeyInput(currentKeyboardState, previousKeyboardState);
            //if (!onLadder)
                Movement();

            ////////////if (Character.bounds.X + Character.bounds.Width > ladder.position.X && Character.bounds.X < ladder.position.X + ladder.ladderTexture.Width && Character.bounds.Y < ladder.position.Y + ladder.ladderTexture.Height && Character.bounds.Y + Character.bounds.Height > ladder.position.Y - (ladder.rails - 1) * ladder.ladderTexture.Height)
            ////////////    onLadder = true;
            ////////////else
            ////////////    onLadder = false;

            ////////////if ((bounds.Y - (Level1.platforms[4].boundingBox.Y + Level1.platforms[4].boundingBox.Height)) > 200) // Als character meer dan 200 px onder platform 4 (onderste) komt respawnt hij
            ////////////    respawn();
        }

        //public static void respawn()
        //{
        //    for (int i = 0; i < Game1.platforms.Count; i++) // Platformen op begin positie plaatsen
        //    {
        //        Game1.platforms[i].boundingBox = new Rectangle((int)Game1.startPosPlat[i].X, (int)Game1.startPosPlat[i].Y, Game1.platforms[i].boundingBox.Width, Game1.platforms[i].boundingBox.Height);
        //        Game1.platforms[i].boundingBoxTop = new Rectangle(Game1.platforms[i].boundingBox.X, Game1.platforms[i].boundingBox.Y, Game1.platforms[i].boundingBox.Width, 5);
        //        //Game1.gun.boundingBox = new Rectangle((int)Game1.gun.startPosition.X, (int)Game1.startPosPlat[i].Y, Game1.platforms[i].boundingBox.Width, Game1.platforms[i].boundingBox.Height);
        //    }

        //    bounds.X = (int)startPos.X;
        //    bounds.Y = (int)startPos.Y + 1;
        //    currentState = state.standing;
        //    currentFacing = facing.right;
        //    jumpingSpeed = -10;
        //    jumpCount = 0;
        //    health.lifes = health.startLifes;
        //}

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
                    if (currentKeyboardState.IsKeyDown(Keys.S)) //Laat character iets oppakken als Spatie wordt ingedrukt
                    {
                        currentState = state.picking;
                    }
                    if (currentState == state.picking && !currentKeyboardState.IsKeyDown(Keys.S))
                    {
                        currentState = state.standing;
                    }
                    //Later misschien ook toevoegen dat je ook in de lucht kan schieten, maar daar ik vond de oplossing op de bug niet
                    if (carryingGun == true)
                    {
                        if (shootingTimer > 50)
                        {
                            if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                            {
                                //if (currentFacing == facing.left)
                                //    Game1.AddProjectile(new Vector2(bounds.X, bounds.Y + sourceHeight / 2));
                                //else
                                //    Game1.AddProjectile(new Vector2(bounds.X + source.Width, bounds.Y + sourceHeight / 2));

                                //currentState = state.shooting;
                                shootingTimer = 0;
                                isShooting = true;
                            }
                        }
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
                    if (currentKeyboardState.IsKeyDown(Keys.S)) //Laat character iets oppakken als Spatie wordt ingedrukt
                    {
                        currentState = state.picking;
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



            // Ladder beklimmen
            if (onLadder)
            {
                if (currentKeyboardState.IsKeyDown(Keys.W))
                    bounds.Y -= 1;
                if (currentKeyboardState.IsKeyDown(Keys.S))
                    bounds.Y += 1;
                if (currentKeyboardState.IsKeyDown(Keys.A))
                    bounds.X -= 1;
                if (currentKeyboardState.IsKeyDown(Keys.D))
                    bounds.X += 1;
            }

            if (isShooting == true)
            {
                shoot();
            }
            shootingTimer++;
        }











        private void shoot()
        {
            if (frameCount % delay == 0) //Eens in de <delay-waarde> frames
            {
                frameCount = 1 * delay;
                source = new Rectangle(frameCount / delay * 80, 4 * 80, sourceWidth, sourceHeight);

                frameCount++;
                shootingDurance++;

                if (shootingDurance > 5)
                {
                    shootingDurance = 0;
                    isShooting = false;
                }
            }

        }





        private void Movement()
        {
            if (frameCount % delay == 0) //Eens in de <delay-waarde> frames
            {
                switch (currentState) //Kijk wat de huidige status is
                {
                    case state.standing:

                        Platform platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                        if (isShooting == false)
                        {
                            if (frameCount / delay >= 4)
                                frameCount = 0;
                            source = new Rectangle(frameCount / delay * 80, 0, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.walkingLeft:
                        platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                            if (isShooting == false)
                            {
                                source = new Rectangle(frameCount / delay * 80, 80, sourceWidth, sourceHeight);
                            }
                        }
                        break;

                    case state.walkingRight:
                        platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                            if (isShooting == false)
                            {
                                source = new Rectangle(frameCount / delay * 80, 80, sourceWidth, sourceHeight);
                            }
                        }
                        break;

                    case state.runningLeft:
                        platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                            if (isShooting == false)
                            {
                                source = new Rectangle(frameCount / delay * 80, 160, sourceWidth, sourceHeight);
                            }
                        }
                        break;

                    case state.runningRight:
                        platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                            if (isShooting == false)
                            {
                                source = new Rectangle(frameCount / delay * 80, 160, sourceWidth, sourceHeight);
                            }
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

                        if (isShooting == false)
                        {
                            source = new Rectangle(frameCount / delay * sourceWidth, 3 * sourceHeight, sourceWidth, sourceHeight);
                        }
                        break;

                    case state.falling:

                        if (jumpingSpeed <= 0) //Als hij nog niet gesprongen heeft, laat hem dan niet eerst springen
                            jumpingSpeed = 0;
                        bounds.Y += jumpingSpeed;
                        bounds.X += (int)speed;
                        jumpingSpeed += 2;

                        if (jumpingSpeed >= 18) //Zorgt dat hij character niet sneller dan 18 pixels/frame naar beneden valt. Sneller dan 18 p/f zorgt ervoor dat character door een platform heen valt.
                            jumpingSpeed = 18;

                        platform = GetIntersectingPlatform(feetBounds); //Kijk in game1.cs of de character in contact is met een platform is de lijst
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

                        if (isShooting == false)
                        {
                            source = new Rectangle(frameCount / delay * sourceWidth, 3 * sourceHeight, sourceWidth, sourceHeight);
                        }
                        jumpCount++;
                        break;

                    case state.picking:
                        //Gun gun = game.GetIntersectingGun(feetBounds);

                        speed = 0;
                        frameCount = 6 * delay;

                        if (isShooting == false)
                        {
                            source = new Rectangle(frameCount / delay * 80, 6 * 80, sourceWidth, sourceHeight);
                        }

                        //if (gun != null) //als character bij een gun staat
                        //{
                        //    carryingGun = true;
                        //    gun.picked = true;
                        //}

                        break;

                    case state.dead:
                        //platform = game.GetIntersectingPlatform(feetBounds);

                        speed = 0;

                        if (frameCount / delay >= 5)
                            frameCount = 5 * delay;

                        source = new Rectangle(frameCount / delay * 80, 6 * 80, sourceWidth, sourceHeight);
                        break;

                    //case state.shooting:
                    //    shootingDurance += 1;

                    //    platform = game.GetIntersectingPlatform(feetBounds);
                    //    if (platform != null) //Als character niet geen, dus wél een platform raakt.
                    //    {
                    //        bounds.Y = platform.boundingBox.Top - bounds.Height + 1; //Blijf op platform staan
                    //        jumpCount = 0;
                    //        jumpingSpeed = -10;
                    //        if (shootingDurance > 10)
                    //        {
                    //            shootingDurance = 0;
                    //            currentState = state.standing;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (shootingDurance > 10)
                    //        {
                    //            shootingDurance = 0;
                    //            currentState = state.falling;
                    //        }

                    //        bounds.Y += jumpingSpeed;

                    //        jumpingSpeed += 2;
                    //        jumpCount++;
                    //        if (jumpingSpeed >= 18) //Zorgt dat hij character niet sneller dan 18 pixels/frame naar beneden valt. Sneller dan 18 p/f zorgt ervoor dat character door een platform heen valt.
                    //            jumpingSpeed = 18;
                    //    }
                    //    bounds.X += (int)speed;

                    //    frameCount = 1 * delay;
                    //    source = new Rectangle(frameCount / delay * 80, 4 * 80, sourceWidth, sourceHeight);
                    //    break;

                }
            }
            frameCount++;
        }



        public Platform GetIntersectingPlatform(Rectangle feetBounds) //Kijken of een platform in de lijst in contact komt met character
        {
            for (int i = 0; i < Level1.platforms.Count; i++) //Kijk voor iedere platform
            {
                if (Level1.platforms[i].boundingBox.Intersects(feetBounds)) //Als een platform in contact is met character
                    return Level1.platforms[i]; //Onthoud die informatie dan
            }
            return null; //Als géén platform in de lijst in contact komt met character, onthoud die informatie
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (playerTexture != null)
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
}
