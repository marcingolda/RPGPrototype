using GamePrototype.Sceny.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype.Przygoda
{
    class Gospoda : Paragraf
    {
        internal static void Wejscie()
        {
            actual = Wejscie;
            image = "";
            text = "gospoda/opis-1";
            wybory = new List<Choice>();
            wybory.Add(new Choice("gospoda/karczmarz", actual));
            wybory.Add(new Choice("gospoda/gora", actual));
            wybory.Add(new Choice("gospoda/kosci", Kosci));
            wybory.Add(new Choice("gospoda/plotki", actual));
            wybory.Add(new Choice("gospoda/wyjdz", actual));
            Update();
        }

        internal static void Kosci()
        {
            actual = Kosci;
            image = "";
            text = "gospoda/kosci1";
            wybory = new List<Choice>();
            wybory.Add(new Choice("gospoda/kosci-zagraj", KosciGra));
            wybory.Add(new Choice("gospoda/kosci-odejdz", actual));
            Update();
        }

        internal static void KosciGra()
        {
            actual = KosciGra;
            image = "";
            if (Zmienne.rozegraneGry < 3)
            {
                Random rnd = new Random();
                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 100);
                Zmienne.rozegraneGry++;
                if (x < y)
                {
                    text = "gospoda/przegrana";
                    Hero.Zloto--;
                }
                else
                {
                    Hero.Zloto++;
                    text = "gospoda/wygrana";
                }
            }
            else
                text = "gospoda/przegrana";
            Update();
        }
    }
}
