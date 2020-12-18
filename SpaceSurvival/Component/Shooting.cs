/*
 *  2020-DEC-16
 *  Trying to organize shooting actions here
 *   - When shooting nothing
 *          SoundEffect, Explosion
 *   - When shooting Alien
 *          SoundEffect, Explosion, AlienDying, Score
 *   - When shooting Fireball
 *          SoundEffect, Explosion, BigExplosion, GameOver
 * 
 * 
 */


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSurvival
{
    public class Shooting : GameComponent
    {
        private Alien alien;
        private Fireball fireball;
        private Rectangle mouseRect;
        private Rectangle targetRect;
        int target = 0; // 0:nothing, 1: alien, 2: fireball
        MouseState ms;

        private SoundEffect shootingSound;
        private Explosion explosion;

        public Shooting(Game game,
            DrawableGameComponent compInput,
            SoundEffect shootingSound,
            Explosion explosion
            ) : base(game)
        {
            if (compInput is Alien)
            {
                alien = (Alien)compInput;
                target = 1;
            }
            else if (compInput is Fireball)
            {
                fireball = (Fireball)compInput;
                target = 2;
            }
            this.shootingSound = shootingSound;
            this.explosion = explosion;
        }

        public override void Update(GameTime gameTime)
        {
            this.mouseRect = new Rectangle(ms.X - 5, ms.Y - 5, 10, 10);
            if (target == 1)
            {
                targetRect = alien.GetBound();
            }
            else if (target == 2)
            {
                targetRect = fireball.GetBound();
            }


            if (mouseRect.Intersects(targetRect))
            {

            }




            shootingSound.Play();

            explosion.Position = new Vector2(ms.X - 55, ms.Y - 55);
            explosion.start();



            base.Update(gameTime);
        }
    }
}
