using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype
{
    class SceneNode
    {
        private Texture2D texture;
        private Vector2 worldPosition;

        public Vector2 Position
        {
            get { return worldPosition; }
            set { worldPosition = value; }
        }

        public SceneNode(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.worldPosition = position;
        }

        public void Draw(SpriteBatch renderer, Vector2 drawPosition)
        {
            renderer.Draw(texture, drawPosition, Color.White);
        }
    }
}
