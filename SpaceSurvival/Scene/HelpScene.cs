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
    /// <summary>
    /// Help Scene for describing what game is this & how to play
    /// </summary>
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpPage;
        private Vector2 position = new Vector2(0, 0);

        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            helpPage = game.Content.Load<Texture2D>("Images/About");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(helpPage, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
