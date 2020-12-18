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
    public class Rocket : DrawableGameComponent
    {
        private Texture2D rocket;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private const int WIDTH = 257;
        private const int HEIGHT = 332;
        public Rocket(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.rocket = game.Content.Load<Texture2D>("Images/Rocket");
            this.position = new Vector2(Shared.stage.X / 2 - WIDTH / 2, Shared.stage.Y - HEIGHT);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(rocket, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
