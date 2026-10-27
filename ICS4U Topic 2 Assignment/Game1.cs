using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;

namespace ICS4U_Topic_2_Assignment
{
    enum Screen
    {
        titleScreen,
        game
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator;
        Rectangle window;
        Texture2D tableBackgroundTexture;
        List<Texture2D> textures;
        List<int> sizes;
        List<int> coins;

        Rectangle buyBoxRect;

        Rectangle playRect;
        Rectangle quitRect;

        Texture2D buyBoxTexture;
        float buyBoxOpacity;

        SpriteFont coinCount;

        List<Texture2D> coinTextures;
        List<Rectangle> coinRects;
        List<double> coinValues;

        int coinAmount;

        string coinNumber;

        MouseState mouseState, prevMouseState;

        double balance;

        bool buyBox = false, refresh = false;

        Screen screen = Screen.titleScreen;
        
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
              
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            textures = new List<Texture2D>();
            coinTextures = new List<Texture2D>();
            coinRects = new List<Rectangle>();
            buyBoxRect = new Rectangle(0, 430, 200, 50);
            coinValues = new List<double>() { 0.01, 0.05, 0.10, 0.25, 1.00, 2.00 };
            coins = new List<int>();
            generator = new Random();
            sizes = new List<int>() { 20, 30, 25, 40, 50, 50 };

            playRect = new Rectangle(300, 200, 200, 50);
            quitRect = new Rectangle(300, 300, 200, 50);

            coinAmount = 50;

            for (int i = 0; i <= coinAmount; i++) // save amounts
            {
                coins.Add(generator.Next(1, 7));
            }

            for (int i = 0; i < coins.Count; i++) // size consistent
            {
                coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[coins[i] - 1]), generator.Next(window.Height - sizes[coins[i] - 1]), sizes[coins[i] - 1], sizes[coins[i] - 1]));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            coinCount = Content.Load<SpriteFont>("fonts/coinCount");

            tableBackgroundTexture = Content.Load<Texture2D>("images/tableBackgroundTexture");

            buyBoxTexture = Content.Load<Texture2D>("images/buyBoxTexture");

            for (int i = 1; i <= 6; i++)
                textures.Add(Content.Load<Texture2D>("images/coin" + (i)));
            for (int i = 0; i < coins.Count; i++) // Textures consistent with size
            {
                coinTextures.Add(textures[coins[i] - 1]);

            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (screen == Screen.titleScreen)
                {
                    if (playRect.Contains(mouseState.Position))
                    {
                        screen = Screen.game;
                    }

                    if (quitRect.Contains(mouseState.Position))
                    {
                        Exit();
                    }
                }
            }

            if (screen == Screen.game)
            {

                coinNumber = Convert.ToString(coinAmount + 1);

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    for (int i = 0; i < coinRects.Count; i++)
                    {
                        if (coinRects[i].Contains(mouseState.Position))
                        {

                            balance = balance + coinValues[coins[i] - 1];

                            coinRects.RemoveAt(i);
                            coins.RemoveAt(i);
                            coinTextures.RemoveAt(i);

                            coinAmount--;

                            i--;
                        }
                    }

                } // Remove Coins

                if (coinAmount + 1 == 0)
                {
                    buyBox = true;
                }

                if (buyBox)
                {
                    buyBoxOpacity = 100f;

                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        if (buyBoxRect.Contains(mouseState.Position))
                        {
                            balance = balance - 10;

                            coinAmount = 50;

                            for (int i = 0; i <= coinAmount; i++) // save amounts
                            {
                                coins.Add(generator.Next(1, 7));
                            }

                            for (int i = 0; i < coins.Count; i++) // size consistent
                            {
                                coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[coins[i] - 1]), generator.Next(window.Height - sizes[coins[i] - 1]), sizes[coins[i] - 1], sizes[coins[i] - 1]));
                            }

                            for (int i = 0; i < coins.Count; i++) // Textures consistent with size
                            {
                                coinTextures.Add(textures[coins[i] - 1]);

                            }

                            refresh = true;

                            buyBox = false;
                            buyBoxOpacity = 0f;


                        }
                    }

                }
            }

            base.Update(gameTime);
            }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if (screen == Screen.titleScreen)
            {
                _spriteBatch.Begin();

                _spriteBatch.Draw(buyBoxTexture, playRect, Color.White);
                _spriteBatch.Draw(buyBoxTexture, quitRect, Color.White);

                _spriteBatch.DrawString(coinCount, "Play", new Vector2(375, 210), Color.Black);
                _spriteBatch.DrawString(coinCount, "Quit", new Vector2(375, 310), Color.Black);
                _spriteBatch.DrawString(coinCount, "Alexander Ronan - Coin Game", new Vector2(225, 100), Color.Black);

                _spriteBatch.End();
            }

            if (screen == Screen.game)
            {
                _spriteBatch.Begin();

                _spriteBatch.Draw(tableBackgroundTexture, window, Color.White);
                _spriteBatch.Draw(buyBoxTexture, buyBoxRect, Color.White * buyBoxOpacity);
                _spriteBatch.DrawString(coinCount, "More Coins: $10", new Vector2(0, 440), Color.Black * buyBoxOpacity);

                for (int i = 0; i < coins.Count; i++)
                    _spriteBatch.Draw(coinTextures[i], coinRects[i], Color.White);

                _spriteBatch.DrawString(coinCount, "Remaining Coins: " + coinNumber, new Vector2(500, 0), Color.Black);
                _spriteBatch.DrawString(coinCount, "Balance: " + $"{balance:F2}", new Vector2(0, 0), Color.Black);

                _spriteBatch.End();

                if (refresh)
                {
                    _spriteBatch.Begin();
                    for (int i = 0; i < coins.Count; i++)
                        _spriteBatch.Draw(coinTextures[i], coinRects[i], Color.White);
                    _spriteBatch.End();
                }
            }
            base.Draw(gameTime);
        }
    }
}
