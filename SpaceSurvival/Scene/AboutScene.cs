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
    /// This is the scene for the information of creators.
    /// </summary>
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;

        StringComponent title;
        StringComponent creators;
        SpriteFont specialFont;

        private Texture2D menuBackground;
        private Vector2 position = new Vector2(0, 0);
        public AboutScene(Game game,
            SpriteBatch spriteBatch,
            Texture2D menuBackground) : base(game)
        {
            specialFont = game.Content.Load<SpriteFont>("Fonts/SpecialFont");

            title = new StringComponent(game, spriteBatch, specialFont, Vector2.Zero, "", Color.White);
            title.Message = "PROG 2370\nGame Programming with Data Structure\n";
            title.Position = new Vector2(Shared.stage.X / 5, Shared.stage.Y / 4);

            this.Components.Add(title);
            creators = new StringComponent(game, spriteBatch, specialFont, Vector2.Zero, "", Color.White);
            creators.Message = "\nCreated by\n - Ezatullah Rafie \n - Keum Ji Kim";
            creators.Position = new Vector2(Shared.stage.X / 5, Shared.stage.Y * 3 / 5);
            this.Components.Add(creators);

            this.spriteBatch = spriteBatch;
            this.menuBackground = menuBackground;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackground, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
