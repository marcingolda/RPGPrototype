﻿using GamePrototype.Sceny.Game;
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
            wybory.Add(new Choice("gospoda/kosci", actual));
            wybory.Add(new Choice("gospoda/plotki", actual));
            wybory.Add(new Choice("gospoda/wyjdz", actual));
            Update();
        }
    }
}