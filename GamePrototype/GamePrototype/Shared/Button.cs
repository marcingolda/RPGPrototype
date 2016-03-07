using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class Button
    {
        Stan status = Stan.normal;
        public Stan Status
        {
            get { return status; }
        }
       
        public enum Stan { normal, marked, pressed}

        Texture2D image;
        Texture2D marked;
        Texture2D pressed;
        Rectangle rectangle;

        static SoundEffect click; 
        public static SoundEffect Click //Wczytanie Clicka w klasie Game1
        {
            set {click = value;}
        }

        public Button(Texture2D image, Rectangle rectangle)
        {
            this.image = image;
            this.marked = image;
            this.pressed = image;
            this.rectangle = Game1.Skaluj(rectangle);
        }

        public Button(Texture2D image, Texture2D marked, Texture2D pressed, Rectangle rectangle)
        {
            this.image = image;
            this.marked = marked;
            this.pressed = pressed;
            this.rectangle = Game1.Skaluj(rectangle);
        }

        public void Update(MouseState mouseState)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            if (rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                status = Stan.pressed;
            else if (rectangle.Contains(mousePosition))
                status = Stan.marked;
            else
                status = Stan.normal;
        }

        public bool IsClicked(MouseState mouseState)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);
            if (status == Stan.pressed && rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Released)
            {
                click.Play();
                return true;
            }
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch, Color? kolor = null)
        {
            Color color;
            if (kolor == null)
                color = new Color(190, 190, 190);
            else
                color = (Color)kolor;
            switch (status)
            {
                case Stan.normal:
                    spriteBatch.Draw(image, rectangle, color);
                    break;
                case Stan.marked:
                    spriteBatch.Draw(marked, rectangle, color);
                    break;
                case Stan.pressed:
                    spriteBatch.Draw(pressed, rectangle, color);
                    break;
            }

        }
    }
}
