using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace belajar_part1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite;
        Texture2D backgroundSprite;
        Texture2D crosshairSprite;
        MouseCursor customCursor;
        SpriteFont gameFont;
        
        Vector2 targetPosition = new Vector2(150, 150);
        const int targetRadius = 45;
        bool mRealesed = true;
        int score = 0;

        MouseState mState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            targetSprite = Content.Load<Texture2D>("target");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");
            crosshairSprite = Content.Load<Texture2D>("crosshairs");

            customCursor = MouseCursor.FromTexture2D(crosshairSprite, crosshairSprite.Width / 2, crosshairSprite.Height / 2);

            Mouse.SetCursor(customCursor);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && mRealesed == true)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                if (mouseTargetDist < targetRadius)
                {
                    score++;

                    if (mouseTargetDist < 15f)
                    {
                        score += 5;
                    }

                    Random rand = new Random();

                    targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
                mRealesed = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mRealesed = true;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(50, 50), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
