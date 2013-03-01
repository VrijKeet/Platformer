using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prototype
{
    interface ILevel
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void Initialize();
        void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState);
    }
}
