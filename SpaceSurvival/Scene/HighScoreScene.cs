using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SpaceSurvival
{
    /// <summary>
    /// To display TOP 5 high Score record
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        StringComponent[] highScore = new StringComponent[5];
        SpriteFont fontHigh;
        string temp = "Shared.player";

        Score scoreTest = new Score();

        //Score
        private SpriteFont scoreFont;
        private ScoreManager scoreManager;
        private string loadScore;


        private Texture2D menuBackground;

        public HighScoreScene(Game game, SpriteBatch spriteBatch, Texture2D menuBackground) : base(game)
        {
            this.spriteBatch = spriteBatch;
            fontHigh = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            for (int i = 0; i < 5; i++)
            {
                temp += (i + 1).ToString();
                highScore[i] = new StringComponent(game, spriteBatch, fontHigh, new Vector2(100, 200 + 100 * i), "", Color.Yellow);
                //highScore[i].Message = $"{i+1} - {Shared.playerArr[i]} : {Shared.scoreArr[i]} \n";
                this.Components.Add(highScore[i]);
            }

            this.menuBackground = menuBackground;

            loadScore = ScoreManager.Load();
            this.scoreFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            //playScene = new PlayScene(game, spriteBatch, song, background, 1);
            //if (playScene.IsOver == true)
            //{
            //    this.score = playScene.Score;
            //}
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.DrawString(scoreFont, "Highscores: \n" + string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(50, 50), Color.Red);
            spriteBatch.Draw(menuBackground, Vector2.Zero, Color.White);
            spriteBatch.DrawString(scoreFont, "Highscores: \n" + loadScore, new Vector2(50, 50), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
