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

        SpriteFont coinCount;

        List<Texture2D> coinTextures;
        List<Rectangle> coinRects;
        List<double> coinValues;

        int coinAmount;

        string coinNumber;


        
        

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
            coinValues = new List<double>();
            coins = new List<int>();
            generator = new Random();
            sizes = new List<int>() { 20, 30, 25, 40, 50, 50 };

            coinAmount = 50;

            for (int i = 0; i <= coinAmount; i++) // save amounts
            {
                coins.Add(generator.Next(1, 7));
            }

            for (int i = 0; i <= coinAmount; i++) // size consistent
            {
                if (coins[i] == 1)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[0]), generator.Next(window.Height - sizes[0]), sizes[0], sizes[0]));
                else if (coins[i] == 2)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[1]), generator.Next(window.Height - sizes[1]), sizes[1], sizes[1]));
                else if (coins[i] == 3)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[2]), generator.Next(window.Height - sizes[2]), sizes[2], sizes[2]));
                else if (coins[i] == 4)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[3]), generator.Next(window.Height - sizes[3]), sizes[3], sizes[3]));
                else if (coins[i] == 5)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[4]), generator.Next(window.Height - sizes[4]), sizes[4], sizes[4]));
                else if (coins[i] == 6)
                    coinRects.Add(new Rectangle(generator.Next(window.Width - sizes[5]), generator.Next(window.Height - sizes[5]), sizes[5], sizes[5]));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            coinCount = Content.Load<SpriteFont>("fonts/coinCount");

            tableBackgroundTexture = Content.Load<Texture2D>("images/tableBackgroundTexture");

            for (int i = 1; i <= 6; i++)
                textures.Add(Content.Load<Texture2D>("images/coin" + (i)));
            for (int i = 0; i < coins.Count; i++) // Textures consistent with size
            {
                if (coins[i] == 1)
                    coinTextures.Add(textures[0]);
                else if (coins[i] == 2)
                    coinTextures.Add(textures[1]);
                else if (coins[i] == 3)
                    coinTextures.Add(textures[2]);
                else if (coins[i] == 4)
                    coinTextures.Add(textures[3]);
                else if (coins[i] == 5)
                    coinTextures.Add(textures[4]);
                else if (coins[i] == 6)
                    coinTextures.Add(textures[5]);

            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            _spriteBatch.Begin();

            _spriteBatch.Draw(tableBackgroundTexture, window, Color.White);

            for (int i = 0; i < coins.Count; i++)
                _spriteBatch.Draw(coinTextures[i], coinRects[i], Color.White);

            _spriteBatch.DrawString(coinCount, Convert.ToString(coinAmount), new Vector2(800, 800), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
