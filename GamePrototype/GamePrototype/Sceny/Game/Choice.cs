using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GamePrototype.Sceny.Game
{
    public delegate void Cel();

    class Choice
    {

        String text;
        Rectangle rectangle;
        Vector2 position;
        static SpriteFont font;
        public static SpriteFont Font
        {
           set { font = value; }
           get { return font; }
        }

        bool enable;
        public Cel cel { get; set; }
        delegate bool warunek();

        Stan stan = Stan.normal;
        enum Stan { normal, marked, pressed }

        //Mniejsza wersja
        public Choice(String paragraf, Cel destination, bool enable = true, bool directText = false)
        {
            this.enable = enable;
            cel = destination;
            if (!directText)
                text = GetText(paragraf);
            else
                text = paragraf;
        }

        //Transformacja
        public Choice (Choice wzorzec, int number, Vector2 position)
        {
            this.enable = wzorzec.enable;
            this.cel = wzorzec.cel;
            this.text = number.ToString() + ". ";
            text += wzorzec.text;
            this.position = position;
            Vector2 size = font.MeasureString(text);
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y * 3 / 4);
        }


        public Choice(String paragraf, int number, Vector2 position, Cel destination)
        {
            cel = destination;
            this.text = number.ToString() + ". ";
            text += GetText(paragraf);
            this.position = position;
            Vector2 size = font.MeasureString(text);
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y*3/4);
        }

        public void Update(MouseState mouseState)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            if (rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                stan = Stan.pressed;
            else if (rectangle.Contains(mousePosition))
                stan = Stan.marked;
            else
                stan = Stan.normal;
        }

        public bool IsClicked(MouseState mouseState)
        {
            if (enable)
            {
                Point mousePosition = new Point(mouseState.X, mouseState.Y);
                if (stan == Stan.pressed && rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Released)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enable)
            {
                switch (stan)
                {
                    case Stan.normal:
                        spriteBatch.DrawString(font, text, position, Color.Black, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
                        break;
                    case Stan.marked:
                    case Stan.pressed:
                        spriteBatch.DrawString(font, text, position, new Color(77, 9, 9), 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F); ;
                        break;
                }
            }
            else
                spriteBatch.DrawString(font, text, position, new Color(60, 60, 60), 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F); ;
        }

        String GetText(String number)
        {
            String tekst;
            using (StreamReader sr = new StreamReader(string.Format("Content/Text/{0}.txt", number)))
            {
                // Read the stream to a string
                tekst = sr.ReadToEnd().Normalize();
            }
            return tekst;
        }
    }
}
