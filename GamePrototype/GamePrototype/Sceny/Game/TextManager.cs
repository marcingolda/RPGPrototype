using GamePrototype.Przygoda;
using GamePrototype.Sceny.Game;
using GamePrototype.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class TextManager
    {
        Game1 game;

        String aktualny; //aktualny paragraf. ZMienić na publiczny chyba. Albo do klasy która bedzie zapisywać zapis

        Sprite notes; //Tło tekstu
        Rectangle obszar;//obszar pisania

        String tekst; //Wypisywany tekst
        Vector2 startPos;//startowa pozycja tekstu (na dobrą sprawe obszar.x, obszar.Y
        SpriteFont font;

        Vector2 lastLine; //pozycja ostatniej linii

        Char inital; //Inicjał
        SpriteFont initialFont;

        Rectangle obszarObrazka; //Obszar rysowania obrazków W celu unikniecia podójbnego skalowania w przeciwieństiwe do pozostałych prostokątków nie jest wyskalowany
        Dictionary<String, String> slownik; //przypisane obrazki do danego paragrafu
        bool isImage;
        Sprite image; //obrazek

        List<Choice> wybory;

        Cel actual; //aktualna metoda/paragraf

        public TextManager(Game1 game)
        {
            this.game = game;

            //Ładowanie contentu
            this.font = game.Content.Load<SpriteFont>("MainFont");
            this.initialFont = game.Content.Load<SpriteFont>("Florana");
            Choice.Font = game.Content.Load<SpriteFont>("ChoiceFont");
            Texture2D background = game.Content.Load<Texture2D>("MainScreen/notes");

            Rectangle notesR = new Rectangle(1920-40 -background.Width, (1080 - background.Height) / 2, background.Width, background.Height);
            notes = new Sprite(background, notesR);

            //wyliczanie obszaru pisania tekstu
            int marginesL = 100;
            int marginesG = 80;
            int marginesP = 200;
            int marginesD = marginesG;
            Rectangle temp = new Rectangle(notesR.X + marginesL, notesR.Y + marginesG, notesR.Width/2 - marginesP, notesR.Height - marginesD);
            this.obszar = Game1.Skaluj(temp);
            startPos = new Vector2(obszar.X, obszar.Y);

            //wyliczanie obszaru rysowania obrazka
            marginesL = 50;
            marginesG = 60;
            marginesP = 120;
            marginesD = 120; ;

            obszarObrazka = new Rectangle(notesR.X+notesR.Width/2+marginesL, notesR.Y+marginesG, notesR.Width/2 - marginesP, notesR.Height - marginesD);
           // this.obszarObrazka = Game1.Skaluj(temp); //Wywoływało podówjne skalowanmie samego obrazka
            isImage = false;

            Paragraf.SetParagraf(this);
            Start.Wstep1();
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            notes.Draw(spriteBatch);
            spriteBatch.DrawString(font, tekst,startPos, Color.Black, 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
            spriteBatch.DrawString(initialFont, inital.ToString(), startPos+Game1.Skaluj(new Vector2(-5,-5)), new Color(77,9,9), 0F, Vector2.Zero, Game1.Skala.X, SpriteEffects.None, 0F);
            foreach (Choice wybor in wybory)
                wybor.Draw(spriteBatch);

            if (this.isImage)
                image.Draw(spriteBatch);
        }

        public void Update(MouseState state)
        {
            foreach (Choice wybor in wybory)
            {
                if (wybor.IsClicked(state))
                    wybor.cel();
                wybor.Update(state);
            }
        }

        //Zawija tekst + wyłapuje initiala
        string WrapInitialText(String text)
        {
            float maxLineWidth = obszar.Width;
            inital = text[0];
            text = text.Substring(1);
            int line=1;

            string[] words = text.Split(' ');

            words[0] = words[0].Insert(0, "                ");
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);
                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");

                    if (word.Contains("\n"))
                        lineWidth = (size.X + spaceWidth) * Game1.Skala.X;
                    else
                        lineWidth += (size.X + spaceWidth) * Game1.Skala.X;
                }
                else
                {
                    line++;
                    if (line < 4)
                    {
                        sb.Append("\n" + "                " + word + " ");
                        lineWidth = (size.X + 17*spaceWidth) * Game1.Skala.X;
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = (size.X + spaceWidth) * Game1.Skala.X;
                    }
                }
            }
            lastLine.Y += font.MeasureString(sb.ToString()).Y*Game1.Skala.X;
            lastLine.Y += font.MeasureString("1").Y*2 * Game1.Skala.X;
            return sb.ToString();
        }

        //Tylko zawija
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
            lastLine.Y += font.MeasureString(sb.ToString()).Y * Game1.Skala.X;
            lastLine.Y += font.MeasureString("1").Y * 2 * Game1.Skala.X;
            return sb.ToString();
        }

        //Pobieranie obrazka
        void GetImage(String image)
        {
            isImage = true;
            Texture2D temp = game.Content.Load<Texture2D>(string.Format("Images/{0}", image));

            Rectangle rect = new Rectangle(obszarObrazka.X + (obszarObrazka.Width - temp.Width) / 2, obszarObrazka.Y, temp.Width, temp.Height); ;
            if (rect.Width > obszarObrazka.Width || rect.Height >obszarObrazka.Height)
                rect = new Rectangle(obszarObrazka.X + (obszarObrazka.Width - rect.Width), obszarObrazka.Y, obszarObrazka.Width, (int)(rect.Height * ((float)obszarObrazka.Width / temp.Width)));

            this.image = new Sprite(temp, rect);
        }

        public void GetParagraph(String text, String image, List<Choice> choices, Cel actual)
        {
            this.actual = actual;
            lastLine = startPos;
            this.tekst = WrapInitialText(text);
            if (image != "")
                GetImage(image);
            else
                isImage = false;
            float space = Choice.Font.MeasureString("I").Y * 1.5F * Game1.Skala.X;
            this.wybory = new List<Choice>();
            foreach (Choice wybor in choices)
            {
                wybory.Add(new Choice(wybor, wybory.Count() + 1, lastLine));
                lastLine.Y += space;
            }
        }

        /*

        //Nowy paragraf - wypierdolić?
        public void GetParagraph(String number)
        {
            //number = number.Insert(0, lokacja + "/");
            Console.WriteLine("Paragraf: " + number);
            switch (number) //Tu jakieś ewentualne specjały dla pobieranego tekstu. Testy?
            {
                default:
                    tekst = GetText(number);
                    break;
            }
            if (slownik.ContainsKey(number))
                GetImage(slownik[number]);
            else
                isImage = false;

            aktualny = number;

            //Clue funkcji, czyli wybory. Nie wiem jak to inaczej załatwić
            wybory = new List<Choice>();
            float space = Choice.Font.MeasureString("I").Y * 1.5F * Game1.Skala.X;
            switch (number)
            {
                case "00/01":
                    wybory.Add(new Choice(number+"a", wybory.Count()+1, lastLine,"00/02")); //Start
                    lastLine.Y += space;
                    break;
                case "00/02":
                    wybory.Add(new Choice(number + "a", wybory.Count() + 1, lastLine,"00/04")); //Wejście
                    lastLine.Y += space;
                    wybory.Add(new Choice(number + "b", wybory.Count() + 1, lastLine, "00/03")); //Przed gospodą
                    break;
                case "00/03":
                    wybory.Add(new Choice(number + "a", wybory.Count() + 1, lastLine, "00/04"));//Wejście
                    lastLine.Y += space;
                    wybory.Add(new Choice(number + "b", wybory.Count() + 1, lastLine, "00/05"));//Stajnia
                    break;
                case "00/04":
                    wybory.Add(new Choice(number + "b", wybory.Count() + 1, lastLine, "00/07"));// Zupa
                    lastLine.Y += space;
                    wybory.Add(new Choice(number + "a", wybory.Count() + 1, lastLine, "00/06"));// Test skradania się za oberżystą
                    break;
            }
            */
        
    }
}
