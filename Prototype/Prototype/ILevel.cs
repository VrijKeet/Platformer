using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Prototype
{
    public interface ILevel
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void Initialize();
        void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState);
        List<Platform> GetPlatforms();
        List<Enemy> GetEnemies();
    }
}
