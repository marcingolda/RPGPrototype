using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype.Shared
{
    class Sprite
    {
        Texture2D texture;
        Rectangle rectangle;
        public Sprite(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = Game1.Skaluj(rectangle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
