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

namespace TimersAndTweens
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TimerController timerController = new TimerController();
        TweenController tweenController = new TweenController();
        Random rand = new Random();

        List<Vector2> squares = new List<Vector2>(); 

        private Texture2D texSquare;

        private Color _bgColor = Color.CornflowerBlue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texSquare = Content.Load<Texture2D>("square");

            // Change background colour every second
            timerController.Create("bgcolor", () => { _bgColor = new Color(rand.Next(256), rand.Next(256), rand.Next(256)); }, 1000, true);

            for(int i=0;i<5;i++)
                squares.Add(new Vector2(0, 100 + (i*60)));

            tweenController.Create("linear", TweenFuncs.Linear, (tween) => { squares[0] = new Vector2((GraphicsDevice.Viewport.Width - 50) * tween.Value, squares[0].Y); }, 1000, false, true, TweenDirection.Forward);
            tweenController.Create("quadraticin", TweenFuncs.QuadraticEaseIn, (tween) => { squares[1] = new Vector2((GraphicsDevice.Viewport.Width - 50) * tween.Value, squares[1].Y); }, 1000, true, true, TweenDirection.Forward);
            tweenController.Create("quadraticout", TweenFuncs.QuadraticEaseOut, (tween) => { squares[2] = new Vector2((GraphicsDevice.Viewport.Width - 50) * tween.Value, squares[2].Y); }, 1000, true, true, TweenDirection.Forward);
            tweenController.Create("quadraticinout", TweenFuncs.QuadraticEaseInOut, (tween) => { squares[3] = new Vector2((GraphicsDevice.Viewport.Width - 50) * tween.Value, squares[3].Y); }, 1000, true, true, TweenDirection.Forward);
            tweenController.Create("bounce", TweenFuncs.Bounce, (tween) => { squares[4] = new Vector2((GraphicsDevice.Viewport.Width - 50) * tween.Value, squares[4].Y); }, 1000, true, true, TweenDirection.Forward);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            timerController.Update(gameTime);
            tweenController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_bgColor);

            spriteBatch.Begin();
            foreach(Vector2 pos in squares)
                spriteBatch.Draw(texSquare, pos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
