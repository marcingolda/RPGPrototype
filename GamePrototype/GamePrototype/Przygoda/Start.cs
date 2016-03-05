using GamePrototype.Sceny.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype.Przygoda
{
    class Start : Paragraf
    {
        internal static void Wstep1()
        {
            actual = Wstep1;
            image = "";
            text = "Start/start";
            wybory = new List<Choice>();
            wybory.Add(new Choice("Start/Dalej", Wstep2));
            Update();
        }

        internal static void Wstep2()
        {
            actual = Wstep2;
            image = "gospoda";
            text = "Start/poczatek";
            wybory = new List<Choice>();
            wybory.Add(new Choice("Start/poczatek-wejdz", Gospoda.Wejscie));
            Update();
        }
    }
}
