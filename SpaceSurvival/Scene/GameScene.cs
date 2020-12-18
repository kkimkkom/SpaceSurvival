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
    /// Common characteristics for all scenes
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        /// <summary>
        /// Make the selected scene enabled & visible
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Make the scene disabled & invisible
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        protected GameScene(Game game) : base(game)
        {
            // When child instance is created it will be hidden by default
            components = new List<GameComponent>();
            Hide();
        }


        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        // If the scene is DrawableGameComponent and selected to be enabled then draw it
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    // If the scene is selected to be enabled then update it
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
