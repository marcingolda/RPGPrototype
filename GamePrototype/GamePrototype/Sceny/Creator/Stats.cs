using GamePrototype.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GamePrototype.Sceny.Creator
{
    class Stats
    {
        SpriteFont font;

        const int maks = 20;
        const int min = 3;

        String nazwa;
        int wartość;
        public int Wartosc
        {
            set { wartość = value; }
            get { return wartość; }
        }

        static bool napisane = false; // czy zostało napisane ile zostało napisane i ramka na objaśnienie
        static int zostalo = 20;
        static Sprite objasnienieRamka;
        static Rectangle objasnienieRectangle;

        bool draw_objasnie = false; //czy statystyka ma wypisać swoje objaśnienie
        String objasnienie;

        Button plus;
        Button minus;

        Rectangle obramowanie;

        public Stats(String name, Rectangle obramowanie, Game1 game, SpriteFont font)
        {
            zostalo = 20;
            wartość = 10;
            objasnienieRectangle = new Rectangle(1520, 0, 400, 400);
            objasnienieRamka = new Sprite(game.Content.Load<Texture2D>("CharCreator/Ramka"), objasnienieRectangle);
            objasnienieRectangle = Game1.Skaluj(objasnienieRectangle);
            nazwa = name;
            this.obramowanie = Game1.Skaluj(obramowanie);
            this.font = font;
            using (StreamReader sr = new StreamReader(string.Format("Content/Text/stats/{0}.txt", name)))
            {
                // Read the stream to a string
                objasnienie = sr.ReadToEnd().Normalize();
            }
            objasnienie = WrapText(objasnienie, 380F);
            plus = new Button(game.Content.Load<Texture2D>("CharCreator/Plus"), new Rectangle(obramowanie.X + obramowanie.Width - obramowanie.Height, obramowanie.Y, obramowanie.Height, obramowanie.Height));
            minus = new Button(game.Content.Load<Texture2D>("CharCreator/Minus"), new Rectangle(obramowanie.X + obramowanie.Width - obramowanie.Height * 2, obramowanie.Y, obramowanie.Height, obramowanie.Height));
        }

        public void Update(MouseState state)
        {
            napisane = false;
            if (plus.IsClicked(state) && zostalo>0 && wartość<maks)
            {
                zostalo--;
                wartość++;
            }
            if (minus.IsClicked(state) && wartość > min)
            {
                zostalo++;
                wartość--;
            }
            plus.Update(state);
            minus.Update(state);
            Point point = new Point(state.X, state.Y);
            if (this.obramowanie.Contains(point))
                draw_objasnie = true;
            else
                draw_objasnie = false;
        }

        public void Draw (SpriteBatch sprite)
        {
            plus.Draw(sprite);
            minus.Draw(sprite);
            if (!napisane)
            {
                objasnienieRamka.Draw(sprite);
                sprite.DrawString(font, "Punkty rozwoju: " + zostalo.ToString(), Game1.Skaluj(new Vector2(500, 75)), Color.White, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
                napisane = true;
            }
            if (draw_objasnie)
                sprite.DrawString(font, objasnienie, new Vector2(objasnienieRectangle.X, objasnienieRectangle.Y), Color.Black, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
            sprite.DrawString(font, nazwa + "   " + wartość, new Vector2(obramowanie.X, obramowanie.Y), Color.White, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
        }

        string WrapText(String text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);
                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += (size.X + spaceWidth) * Game1.Skala.X;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = (size.X + spaceWidth) * Game1.Skala.X;
                }
            }
            return sb.ToString();
        }
    }
}
