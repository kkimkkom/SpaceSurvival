using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SpaceSurvival
{
    /// <summary>
    /// Player can choose level in this scene
    /// </summary>
    public class LevelScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private MenuComponent menu;
        private SoundEffect click;
        private Texture2D background;
        private Vector2 position = new Vector2(0, 0);


        private string[] menuArray = { "Level 1", "Level 2" };
        public MenuComponent Menu { get => menu; set => menu = value; }
        public LevelScene(Game game,
            SpriteBatch spriteBatch,
            SoundEffect click,
            Texture2D background) : base(game)
        {
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game,
                spriteBatch,
                game.Content.Load<SpriteFont>("Fonts/RegularFont"),
                game.Content.Load<SpriteFont>("Fonts/HighlightFont"),
                menuArray, click);
            this.Components.Add(menu);
            this.click = click;
            this.background = background;
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
