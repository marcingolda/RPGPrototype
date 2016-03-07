using GamePrototype.Sceny.Creator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class CharCreator
    {
        Game1 game1;

        SpriteFont font;

        SpriteBatch spriteBatch;

        Button next;
        Button back;

        String imie = "Zbyszek";

        List<Stats> statystyki;

        public CharCreator(SpriteBatch sprite, Game1 game1)
        {
            this.spriteBatch = sprite;
            this.game1 = game1;
            LoadContent();
        }

        public void LoadContent()
        {
            font = game1.Content.Load<SpriteFont>("ChoiceFont");

            Texture2D temp = game1.Content.Load<Texture2D>("CharCreator/next");
            Texture2D tempM = game1.Content.Load<Texture2D>("CharCreator/nextM");
            Texture2D tempP = game1.Content.Load<Texture2D>("CharCreator/nextP");
            next = new Button(temp, tempM, tempP, new Rectangle(1400, 1000, 400, 80));
        }

        public void Update(MouseState mouseState)
        {
            if (next.IsClicked(mouseState))
            {
                game1.hero = new Character(imie, statystyki[0].Wartosc, statystyki[1].Wartosc, statystyki[2].Wartosc, statystyki[3].Wartosc, statystyki[4].Wartosc, statystyki[5].Wartosc, statystyki[6].Wartosc);
                game1.Zmiana(Scena.MainGame);
            }

            next.Update(mouseState);
            foreach(Stats stat in statystyki)
                stat.Update(mouseState);
        }

        public void Draw()
        {
            String tekst = "Ekran tworzenia postaci, narazie zionie pustką otchłani";
            int x = 75;
            int y = 75;
            spriteBatch.DrawString(font, "Imię: " + imie, Game1.Skaluj(new Vector2(x, y)), Color.White, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
            y += 75;

            next.Draw(spriteBatch);
            foreach (Stats stat in statystyki)
                stat.Draw(spriteBatch);
        }

        public void Reload()
        {
            int x = 75;
            int y = 150;
            statystyki = new List<Stats>();
            statystyki.Add(new Stats("Siła", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Kondycja", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Zręczność", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Percepcja", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Inteligencja", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Siła Woli", new Rectangle(x, y, 300, 30), game1, font));
            y += 75;
            statystyki.Add(new Stats("Charyzma", new Rectangle(x, y, 300, 30), game1, font));
        }
    }
}
