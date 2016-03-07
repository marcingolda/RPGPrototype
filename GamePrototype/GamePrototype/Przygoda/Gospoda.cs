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
            text = "gospoda/kosci1";
            if (Zmienne.rozegraneGry < 3)
            {
                int x = 2;
                int y = 3;
                if (x < y) 
                    Update("Przejebałeś");
            }
        }
    }
}
