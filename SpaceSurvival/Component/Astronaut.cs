using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSurvival
{
    /// <summary>
    /// Astronaut component
    /// </summary>
    public class Astronaut : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int WIDTH = 128;
        private const int HEIGHT = 128;

        public Vector2 Position { get => position; set => position = value; }

        public Astronaut(Game game,
                SpriteBatch spriteBatch,
                Texture2D tex,
                int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(Shared.stage.X / 2 - WIDTH / 2, Shared.stage.Y - HEIGHT);
            speed = new Vector2(5, 0);
            this.delay = delay;

            createFrames();
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public void start()
        {
            this.Enabled = true;
            this.Visible = true;
            frameIndex = -1;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < tex.Width / WIDTH; j++)
                {
                    int x = j * WIDTH;
                    int y = i * HEIGHT;
                    Rectangle r = new Rectangle(x, y, WIDTH, HEIGHT);
                    frames.Add(r);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, Position, frames[frameIndex], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > 3 * tex.Width / WIDTH - 1)
                {
                    frameIndex = -1;
                }
                delayCounter = 0;
            }

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right))
            {
                position += speed;
                if (position.X > Shared.stage.X - WIDTH)
                {
                    position.X = Shared.stage.X - WIDTH;
                }
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                position -= speed;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }

            base.Update(gameTime);
        }

        public Rectangle GetBound()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, tex.Width / 5 - 20, tex.Height / 5 - 20);
        }

    }
}
