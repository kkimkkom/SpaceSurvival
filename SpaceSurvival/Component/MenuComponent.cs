using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SpaceSurvival
{
    public class MenuComponent : DrawableGameComponent
    {
        // Declare global variables
        private SpriteBatch spriteBatch;

        private SpriteFont regularFont, highlightFont;
        private Color regularColor = Color.White;
        private Color highlightColor = Color.Firebrick;

        private string[] menuArray;
        private int selectedIndex = 0;
        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        private Vector2 position;
        private KeyboardState oldState;
        private SoundEffect click;

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont highlightFont,
            string[] menu,
            SoundEffect click) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuArray = menu;
            this.click = click;
            position = new Vector2(Shared.stage.X * 3 / 4, Shared.stage.Y / 2);
        }



        public override void Draw(GameTime gameTime)
        {
            Vector2 temp = position;
            spriteBatch.Begin();

            for (int i = 0; i < menuArray.Length; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuArray[i], temp, highlightColor);
                    temp.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuArray[i], temp, regularColor);
                    temp.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuArray.Length)
                {
                    selectedIndex = 0;
                }
                click.Play();
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuArray.Length - 1;
                }
                click.Play();
            }

            oldState = ks;
            base.Update(gameTime);
        }
    }
}
