using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace SpaceSurvival
{
    /// <summary>
    /// Main action scene for playing game
    /// </summary>
    public class PlayScene : GameScene
    {
        #region global variables
        private int level;

        private SpriteBatch spriteBatch;
        private Song song;
        private Texture2D background;
        private Texture2D gameOver;
        private bool isOver = false;
        public bool hitFireball;
        private Vector2 backgroundPosition = new Vector2(0, -400);

        //Alien
        private Alien alien;

        //Multiple aliens
        Alien[] aliens;
        Random random = new Random();
        private int maxAlien = 8;
        Rectangle alienRect;

        //Fireball
        private Fireball fireball;
        Rectangle fireballRect;

        private int minPositionX = 0;
        private int maxPositionX = 880;

        private int minSpeed = 3;
        private int maxSpeed = 5;

        //Shooting
        private SoundEffect alienDying;
        Rectangle mouseRect;

        //Astronaut
        private Astronaut astronaut;
        private Rectangle astronautRect;
        private Rectangle overlapDie;

        // add string components
        StringComponent overMsg;
        StringComponent scoreInfo;
        StringComponent playerInput;
        StringComponent extraMsg;
        SpriteFont fontReg;
        SpriteFont fontHigh;
        private int score = 0;
        public int Score { get => score; set => score = value; }
        public bool IsOver { get => isOver; set => isOver = value; }

        //score
        private ScoreManager scoreManager;


        // time gap between creating next alien
        private int delay = 70;

        //Mouse click explosion
        private Explosion explosion;
        private MouseState oldState;
        private SoundEffect shootingSound;
        private Texture2D explosionSprite;
        Rectangle explosionRect;

        //Rocket
        private Rocket rocket;

        //Game over
        private GameOver gameOverExplosion;
        private Texture2D bigExplosion;
        private SoundEffect gameOverSound;
        private bool isSaved = false;
        #endregion



        #region constructor
        public PlayScene(Game game,
            SpriteBatch spriteBatch,
            Song song,
            Texture2D background,
            int level) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.song = song;
            MediaPlayer.IsRepeating = true;
            this.background = background;
            this.level = level;
            isOver = false;
            hitFireball = false;

            //Game over explosion
            bigExplosion = game.Content.Load<Texture2D>("Images/SpaceShip Explosion");
            gameOverSound = game.Content.Load<SoundEffect>("Sounds/GameOver");
            gameOverExplosion = new GameOver(game, spriteBatch, bigExplosion, new Vector2(10, 10), gameOverSound, 3);
            this.Components.Add(gameOverExplosion);

            // Rocket
            rocket = new Rocket(game, spriteBatch);
            this.Components.Add(rocket);

            //Alien
            alien = new Alien(game, spriteBatch, game.Content.Load<Texture2D>("Images/Alien"), 3);

            //aliens
            aliens = new Alien[maxAlien];
            for (int i = 0; i < maxAlien; i++)
            {
                aliens[i] = new Alien(game, spriteBatch, game.Content.Load<Texture2D>("Images/Alien"), 3);
                this.Components.Add(aliens[i]);
            }

            //fireball
            if (level == 2)
            {
                fireball = new Fireball(game, spriteBatch, game.Content.Load<Texture2D>("Images/Fireball2"), 3);
                this.Components.Add(fireball);
            }


            //Astronaut
            astronaut = new Astronaut(game, spriteBatch, game.Content.Load<Texture2D>("Images/Astronaut"), 3);
            this.Components.Add(astronaut);

            //Explosion
            explosionSprite = game.Content.Load<Texture2D>("Images/explosion");
            shootingSound = game.Content.Load<SoundEffect>("Sounds/Explosion");
            explosion = new Explosion(game, spriteBatch, explosionSprite, Vector2.Zero, shootingSound, 3);
            this.Components.Add(explosion);


            //Shooting
            alienDying = game.Content.Load<SoundEffect>("Sounds/alienDying");


            // Point
            score = 0;
            fontReg = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            scoreInfo = new StringComponent(game, spriteBatch, fontReg, Vector2.Zero, "", Color.AliceBlue);
            this.Components.Add(scoreInfo);

            // Game Over message
            fontHigh = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            overMsg = new StringComponent(game, spriteBatch, fontHigh, Vector2.Zero, "", Color.Yellow);
            this.Components.Add(overMsg);
            playerInput = new StringComponent(game, spriteBatch, fontReg, Vector2.Zero, "", Color.Red);
            extraMsg = new StringComponent(game, spriteBatch, fontHigh, Vector2.Zero, "", Color.Yellow);

            gameOver = game.Content.Load<Texture2D>("Images/BigExplosion");

            //Score
            scoreManager = new ScoreManager();
        }
        #endregion

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundPosition, Color.White);
            if (isOver && hitFireball)
            {
                spriteBatch.Draw(gameOver, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        // variables to make time gaps between creating aliens, creating fireballs
        int i = 0;
        int j = 0;
        int k = 0;

        public override void Update(GameTime gameTime)
        {
            if (!isOver)
            {
                // Creating Aliens
                if ((i > delay && j < aliens.Length) || (i == 0))
                {
                    Vector2 randAlienPosition = new Vector2(random.Next(minPositionX, maxPositionX), 0);
                    Vector2 randAlienSpeed = new Vector2(random.Next(minSpeed, maxSpeed), random.Next(minSpeed, maxSpeed));
                    aliens[j].Position = randAlienPosition;
                    aliens[j].Speed = randAlienSpeed;
                    aliens[j].IsAlive = true;

                    // Why don't they go left when they created?
                    Vector2 direction = astronaut.Position - aliens[j].Position;
                    direction.Normalize();
                    aliens[j].Position += direction * aliens[j].Speed;
                    aliens[j].start();
                    j++;
                    i = 0;
                    if (j == aliens.Length)
                    {
                        j = 0;
                    }
                }
                i++;

                // Creating Fireballs
                if (k > delay * 5 && level == 2)
                {
                    Vector2 randPosition = new Vector2(random.Next(minPositionX, maxPositionX), 0);
                    Vector2 randSpeed = new Vector2(random.Next(minSpeed, maxSpeed + 2), random.Next(minSpeed, maxSpeed + 2));
                    fireball.Position = randPosition;
                    fireball.Speed = randSpeed;
                    fireball.start();
                    k = 0;
                }
                k++;

                //mouseClick explosion
                MouseState ms = Mouse.GetState();
                if (ms.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    explosion.Position = new Vector2(ms.X - 55, ms.Y - 55);
                    explosion.start();

                    shootingSound.Play();
                    explosionRect = explosion.getBound();
                    mouseRect = new Rectangle(ms.X - 5, ms.Y - 5, 10, 10);

                    if (level == 2)
                    {
                        fireballRect = fireball.GetBound();
                        if (mouseRect.Intersects(fireballRect))
                        {
                            isOver = true;
                            gameOverSound.Play();
                            hitFireball = true;
                            //string storeScore = $"Ali {score}";
                            //scoreManager.Add(new Score()
                            //    {
                            //        PlayerName = "Ali",
                            //        Value = score,
                            //    }
                            //);
                            //ScoreManager.Save(storeScore);
                            //int result;
                            //result = Convert.ToInt32(MessageBox.Show("Game Over", $"You died! Score : {score}", new[] { "New Game", "Main Page" }));
                            //if (result == 0)
                            //{
                            //    Vector2 dimension = font.MeasureString("New Game");
                            //    Vector2 strPos = new Vector2(Shared.stage.X - dimension.X,
                            //        Shared.stage.Y - dimension.Y);
                            //    info.Position = strPos;
                            //}
                            //this.Enabled = false;
                        }
                    }
                }
                oldState = ms;

                astronautRect = astronaut.GetBound();
                foreach (Alien alien in aliens)
                {
                    alienRect = alien.GetBound();
                    overlapDie = new Rectangle();
                    overlapDie = Rectangle.Intersect(alienRect, astronautRect);
                    int overlapDieDim = overlapDie.Width * overlapDie.Height;

                    // used mouseRect instead of explosionRect
                    // for better targeting
                    if (mouseRect.Intersects(alienRect))
                    {
                        //alien.Speed += alien.Speed * 10;
                        alien.hide();
                        alienDying.Play();
                        alienRect = Rectangle.Empty;
                        explosionRect = Rectangle.Empty;
                        alien.IsAlive = false;
                        score += 100;
                        break;
                    }
                    if (overlapDieDim > 1500 && alien.IsAlive)
                    {
                        isOver = true;
                        gameOverExplosion.Position = new Vector2(Shared.stage.X / 2 - 302 / 2, Shared.stage.Y - 302);
                        gameOverExplosion.start();
                        gameOverSound.Play();
                    }
                }
                if (level == 2)
                {
                    fireballRect = fireball.GetBound();
                    if (astronautRect.Intersects(fireballRect))
                    {
                        fireball.hide();
                        isOver = true;
                        gameOverSound.Play();
                        hitFireball = true;
                    }
                }
            }
            else
            {
                // remove components
                this.Components.Remove(fireball);
                this.Components.Remove(rocket);
                this.Components.Remove(scoreInfo);
                foreach (var alien in aliens)
                {
                    this.Components.Remove(alien);
                }
                MediaPlayer.Pause();

                // display GAME OVER message
                overMsg.Message = $" GAME OVER \n Your total Score is : {score} \n - Enter your name (< 5 characters)\n and press [Enter]\n" +
                    $" - Or press [ESC] key \n to go back to menu.";
                Vector2 dimension = fontHigh.MeasureString(overMsg.Message);

                // get player name
                playerInput.GetKeys();
                Vector2 strPos = new Vector2(10, dimension.Y + 10);
                playerInput.Position = strPos;
                this.Components.Add(playerInput);
                if (playerInput.nameDone)
                {
                    extraMsg.Message = $"Name {playerInput.Message} Entered!";
                    extraMsg.Position = new Vector2(10, dimension.Y + 50);
                    this.Components.Add(extraMsg);


                    if (!isSaved)
                    {
                        string storeScore = $"{playerInput.Message} {score}";

                        ScoreManager.Save(storeScore);
                        isSaved = true;
                    }
                    //scoreManager.Add(new Score()
                    //    {
                    //        PlayerName = "Ali",
                    //        Value = score,
                    //    }
                    //);
                    //Shared.playerArr[5] = playerInput.Message;
                    //Shared.scoreArr[5] = score;
                }
            }

            // Update score info
            scoreInfo.Message = $"SCORE : {score}";

            //Initialize it
            mouseRect = new Rectangle(0, 0, 0, 0);

            // Make it faster~
            // ! WHY??
            if (score >= 2000)
            {
                maxAlien = 20;
                delay = 30;
                minSpeed = 10;
                maxSpeed = 12;
            }
            else if (score >= 1000)
            {
                delay = 50;
                minSpeed = 8;
                maxSpeed = 11;
            }
            else if (score >= 500)
            {
                minSpeed = 7;
                maxSpeed = 10;
            }
            else if (score >= 300)
            {
                minSpeed = 5;
                maxSpeed = 8;
            }


            base.Update(gameTime);
        }
    }
}
