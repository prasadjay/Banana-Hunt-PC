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
using System.Diagnostics;
using System.ComponentModel;

/*

W.A.P.Jayashanka
prasadjayashanka@gmail.com
http://www.jaynode.com/

*/

namespace Banana_Hunt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        KeyboardState lastKeyboardState = new KeyboardState();
        KeyboardState currentKeyboardState = new KeyboardState();

        #region Monkey

        Texture2D monkey;
        public Rectangle monkeyRect;
        public Vector2 monkeyPosition = Vector2.Zero;
        float speed = 5f;

        //Left Walk

        Texture2D leftWalk;
        Point leftWalk_frameSize = new Point(143, 172);
        Point leftWalk_currentFrame = new Point(0, 0);
        Point leftWalk_sheetSize = new Point(2, 1);
        int leftWalk_timeSinceLastFrame = 0;
        int leftWalk_millisecondsPerFrame = 300;

        //Right Walk

        Texture2D rightWalk;
        Point rightWalk_frameSize = new Point(143, 172);
        Point rightWalk_currentFrame = new Point(0, 0);
        Point rightWalk_sheetSize = new Point(2, 1);
        int rightWalk_timeSinceLastFrame = 0;
        int rightWalk_millisecondsPerFrame = 300;
        
        //Left Jump

        //Right Jump

        #endregion

        Texture2D background;
        Texture2D Info;
        Texture2D credits;
        Texture2D Pause;
        Texture2D win;

        GameSprite[] bananas = new GameSprite[50];
        bool[] bananaStat = new bool[50];
        Random rnd = new Random();

        

        #region Game_Screens

        Texture2D Screen1;
        GameSprite[] gameScreen = new GameSprite[15];
        GameSprite[] groundPanel = new GameSprite[15];
        int width = -570;

        #endregion

        enum ScreenState
        {
            MainMenu,
            info,
            Pause,
            credits,
            Game,
            Congrats
        };

        enum monkeyState
        {
            leftWalk,
            rightWalk,
            leftJump,
            rightJump,
            normal
        }

        monkeyState heroact;

        ScreenState currentState;


        //Monkey m;

        GameSprite pillar;
        GameSprite pillarEnd;

        //marks

        int marks = 0;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;

            for (int x = 0; x < 50; x++)
                bananaStat[x] = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            monkey = Content.Load<Texture2D>(@"textures\Monkey\nomalStand");
            leftWalk = Content.Load<Texture2D>(@"textures\Monkey\leftWalk");
            rightWalk = Content.Load<Texture2D>(@"textures\Monkey\rightWalk");


            background = Content.Load<Texture2D>(@"textures\startScreenFull");
            Info = Content.Load<Texture2D>(@"textures\infoScreen");
            credits = Content.Load<Texture2D>(@"textures\credits");
            Pause = Content.Load<Texture2D>(@"textures\pauseScreen");
            win = Content.Load<Texture2D>(@"textures\Congrats");

            pillar = new GameSprite(Content.Load<Texture2D>(@"textures\pillar"), new Rectangle(), new Vector2(-200, 400), 5f);
            pillarEnd = new GameSprite(Content.Load<Texture2D>(@"textures\pillar"), new Rectangle(), new Vector2(17500, 400), 5f);

            font = Content.Load<SpriteFont>(@"Fonts\SpriteFont1");

            #region gameScreens

            Screen1 = Content.Load<Texture2D>(@"textures\background");
            

            for (int x = 0; x < 15; x++ )
            {
                gameScreen[x] = new GameSprite(Screen1, new Rectangle(), new Vector2(width, 0), 5f);
                groundPanel[x] = new GameSprite(Content.Load<Texture2D>(@"textures\ground"), new Rectangle(), new Vector2(width, 660), 5f);
                width += 1280;
            }

            for (int x = 0; x < 50; x++)
            {
                bananas[x] = new GameSprite(Content.Load<Texture2D>(@"textures\banana"), new Rectangle(), new Vector2((rnd.Next(-100, 17500)), (rnd.Next(200,500))), 5f);
            }

            #endregion

                currentState = ScreenState.MainMenu;
                heroact = monkeyState.normal;

            //m = new Monkey(monkey, new Rectangle(), new Vector2(500, 500), 5f);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            monkeyRect = new Rectangle((int)monkeyPosition.X, (int)monkeyPosition.Y, (int)monkey.Width, (int)monkey.Height);

            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            


            //m.Update();

            switch (currentState)
            {
                case ScreenState.MainMenu:
                    {
                        if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            currentState = ScreenState.info;

                        if ((currentKeyboardState.IsKeyDown(Keys.F1) && lastKeyboardState.IsKeyUp(Keys.F1)))
                            currentState = ScreenState.credits;

                        if (currentKeyboardState.IsKeyDown(Keys.F2))
                            this.Exit();

                        break;
                    }

                case ScreenState.credits:
                    {
                        if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            currentState = ScreenState.Game;

                        if ((currentKeyboardState.IsKeyDown(Keys.F1) && lastKeyboardState.IsKeyUp(Keys.F1)))
                            Process.Start("IExplore.exe", "www.facebook.com/prasad.jayashanka");

                        if ((currentKeyboardState.IsKeyDown(Keys.F2) && lastKeyboardState.IsKeyUp(Keys.F2)))
                            Process.Start("IExplore.exe", "www.vickiwenderlich.com");

                        if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                            currentState = ScreenState.MainMenu;

                        break;
                    }

                case ScreenState.info:
                    {
                        if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            currentState = ScreenState.Game;

                        if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                            currentState = ScreenState.MainMenu;
                        break;
                    }

                case ScreenState.Pause:
                    {
                        if ((currentKeyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter)))
                            currentState = ScreenState.Game;

                        if (currentKeyboardState.IsKeyDown(Keys.F3))
                            this.Exit();

                        break;
                    }

                case ScreenState.Game:
                    {
                        #region update_Textures

                        //Walk Left

                        leftWalk_timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                        if (leftWalk_timeSinceLastFrame > leftWalk_millisecondsPerFrame)
                        { 
                            leftWalk_timeSinceLastFrame -= leftWalk_millisecondsPerFrame;
                            ++leftWalk_currentFrame.X;
                            if (leftWalk_currentFrame.X >= leftWalk_sheetSize.X)
                            {
                                leftWalk_currentFrame.X = 0;
                                ++leftWalk_currentFrame.Y;
                                if (leftWalk_currentFrame.Y >= leftWalk_sheetSize.Y)
                                    leftWalk_currentFrame.Y = 0;
                            }
                        }

                        //walk right

                       rightWalk_timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                       if (rightWalk_timeSinceLastFrame > rightWalk_millisecondsPerFrame)
                        {
                            rightWalk_timeSinceLastFrame -= rightWalk_millisecondsPerFrame;
                            ++rightWalk_currentFrame.X;
                            if (rightWalk_currentFrame.X >= rightWalk_sheetSize.X)
                            {
                                rightWalk_currentFrame.X = 0;
                                ++rightWalk_currentFrame.Y;
                                if (rightWalk_currentFrame.Y >= rightWalk_sheetSize.Y)
                                    rightWalk_currentFrame.Y = 0;
                            }
                        }

                        #endregion

                        if ((currentKeyboardState.IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape)))
                            currentState = ScreenState.Pause;

                        if (currentKeyboardState.IsKeyDown(Keys.D) && (currentKeyboardState.IsKeyUp(Keys.Space)))
                        {
                            monkeyPosition.X += speed;
                            heroact = monkeyState.rightWalk;
                        }

                        else if (currentKeyboardState.IsKeyDown(Keys.A) && (currentKeyboardState.IsKeyUp(Keys.Space)))
                        {
                            monkeyPosition.X -= speed;
                            heroact = monkeyState.leftWalk;
                        }

                        else if ((currentKeyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space)) && touchGround())
                        {
                            monkeyPosition.Y -= 150;
                            //heroact = monkeyState.normal;
                        }

                        else if ((currentKeyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space)) && touchGround() && currentKeyboardState.IsKeyDown(Keys.D))
                        {
                            monkeyPosition.X += speed;
                            heroact = monkeyState.rightJump;
                        }

                        else if ((currentKeyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space)) && touchGround() && currentKeyboardState.IsKeyDown(Keys.A))
                        {
                            heroact = monkeyState.leftJump;
                        }

                        else
                        {
                            //heroact = monkeyState.normal;
                        }

                        if (!touchGround())
                            monkeyPosition.Y += 5;
                        else
                            monkeyPosition.Y += 0;


                        for (int x = 0; x < 15; x++)
                        {
                            groundPanel[x].Update();
                        }

                        for (int x = 0; x < 50; x++)
                        {
                            bananas[x].Update();
                        }

                        pillar.Update();
                        pillarEnd.Update();
                        touchPillar();
                        touchPillarEnd();


                            break;
                    }

                case ScreenState.Congrats:
                    {
                        
                        break;
                    }


                default:
                    {


                        break;
                    }
            }

            camera.update(gameTime, this);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            switch (currentState)
            {
                case ScreenState.MainMenu:
                    {
                        spriteBatch.Draw(background, new Vector2(-570, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        break;
                    }

                case ScreenState.credits:
                    {
                        spriteBatch.Draw(credits, new Vector2(-570, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        break;
                    }

                    case ScreenState.info:
                    {
                        spriteBatch.Draw(Info, new Vector2(-570, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        break;
                    }

                    case ScreenState.Pause:
                    {
                        spriteBatch.Draw(Pause, new Vector2(-570, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        break;
                    }

                    case ScreenState.Game:
                    {
                        spriteBatch.DrawString(font, "Score : " + marks.ToString(), new Vector2(monkeyPosition.X, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 20);
                        touchBanana();

                        for (int x = 0; x < 15; x++)
                        {
                            gameScreen[x].Draw(spriteBatch);
                            groundPanel[x].DrawObject(spriteBatch);
                        }

                        for (int x = 0; x < 50; x++)
                        {
                            if(bananaStat[x])
                                bananas[x].DrawObject(spriteBatch);
                        }

                        pillarEnd.DrawObject(spriteBatch);
                        pillar.DrawObject(spriteBatch);

                        switch(heroact)
                        {
                            case monkeyState.normal:
                                {
                                    spriteBatch.Draw(monkey, monkeyPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 15);
                                    break;
                                }

                            case monkeyState.leftWalk:
                                {
                                    spriteBatch.Draw(leftWalk, monkeyPosition, new Rectangle(leftWalk_currentFrame.X * leftWalk_frameSize.X, leftWalk_currentFrame.Y * leftWalk_frameSize.Y, leftWalk_frameSize.X, leftWalk_frameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 20);

                                    break;
                                }

                            case monkeyState.rightWalk:
                                {
                                    spriteBatch.Draw(rightWalk, monkeyPosition, new Rectangle(rightWalk_currentFrame.X * rightWalk_frameSize.X, rightWalk_currentFrame.Y * rightWalk_frameSize.Y, rightWalk_frameSize.X, rightWalk_frameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 20);

                                    break;
                                }

                            case monkeyState.leftJump:
                                {
                                    break;
                                }

                            case monkeyState.rightJump:
                                {
                                    break;
                                }
                        }


                        break;
                    }

                    case ScreenState.Congrats:
                    {
                        spriteBatch.Draw(win, new Vector2(-570, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                        break;
                    }


                default:
                    {
                        

                        break;
                    }
            }


            //m.Draw(spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }


        public bool touchGround()
        {

            if (monkeyRect.Intersects(groundPanel[0].getRectangle()) || monkeyRect.Intersects(groundPanel[1].getRectangle()) || monkeyRect.Intersects(groundPanel[2].getRectangle()) || monkeyRect.Intersects(groundPanel[3].getRectangle()) || monkeyRect.Intersects(groundPanel[4].getRectangle()) || monkeyRect.Intersects(groundPanel[5].getRectangle()) || monkeyRect.Intersects(groundPanel[6].getRectangle()) || monkeyRect.Intersects(groundPanel[7].getRectangle()) || monkeyRect.Intersects(groundPanel[8].getRectangle()) || monkeyRect.Intersects(groundPanel[9].getRectangle()) || monkeyRect.Intersects(groundPanel[10].getRectangle()) || monkeyRect.Intersects(groundPanel[11].getRectangle()) || monkeyRect.Intersects(groundPanel[12].getRectangle()) || monkeyRect.Intersects(groundPanel[13].getRectangle()) || monkeyRect.Intersects(groundPanel[14].getRectangle()))
                return true;
            else
                return false;

        }

        public void touchBanana()
        {
            for (int x = 0; x < 50; x++)
            {
                if (monkeyRect.Intersects(bananas[x].getRectangle()))
                {
                    bananaStat[x] = false;
                    marks += 10;
                }
            }
        }

        public void touchPillar()
        {
            if (monkeyRect.Intersects(pillar.getRectangle()))
                monkeyPosition.X += 10;
        }

        public void touchPillarEnd()
        {
            if (monkeyRect.Intersects(pillarEnd.getRectangle()))
                currentState = ScreenState.Congrats;
        }

    }
}
