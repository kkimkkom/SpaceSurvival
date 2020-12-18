using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSurvival
{
    public class StringComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        public Vector2 Position { get; set; }
        public string Message { get; set; }
        public Color color;
        public bool nameDone;

        private Keys[] lastPressedKeys = new Keys[5];

        public StringComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            string message,
            Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            Position = position;
            Message = message;
            this.color = color;
            nameDone = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, Message, Position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void GetKeys()
        {
            KeyboardState kb = Keyboard.GetState();

            Keys[] pressedKeys = kb.GetPressedKeys();
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    //Key is no longer pressed
                    OnKeyUp(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key) && !nameDone)//&& Message.Length <5
                {
                    OnKeyDown(key);
                }
            }
            lastPressedKeys = pressedKeys;
        }

        public void OnKeyUp(Keys key)
        {
            if (key == Keys.Enter)
            {
                nameDone = true;
            }
        }
        public void OnKeyDown(Keys key)
        {
            if (key.ToString().Length == 1)
            {
                Message += key.ToString();
            }
        }
    }
}
