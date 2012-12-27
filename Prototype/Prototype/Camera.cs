using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype
{
    class Camera
    {
        private SpriteBatch spriteRenderer;
        private Vector2 cameraPosition;

        public Vector2 Position
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        public Camera(SpriteBatch renderer)
        {
            spriteRenderer = renderer;
            cameraPosition = new Vector2(0, 0);
        }

        public void DrawNode(SceneNode node)
        {
            //scherm positie van de node krijgen
            Vector2 drawPosition = ApplyTransformations(node.Position);
            node.Draw(spriteRenderer, drawPosition);
        }

        private Vector2 ApplyTransformations(Vector2 nodePosition)
        {
            //Translatie toepassen
            Vector2 finalPosition = nodePosition - cameraPosition;
            return finalPosition;
        }

        public void Translate(Vector2 moveVector)
        {
            cameraPosition += moveVector;
        }
    }
}
