using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class Hero
    {
        public static string Imie;
        public static int Sila;
        public static int Kondycja;
        public static int Zrecznosc;
        public static int Percepcja;
        public static int Inteligencja;
        public static int Sila_woli;
        public static int Charyzma;

        public static int Zloto;

        public static void NewHero(string imie, int sila, int kondycja, int zrecznosc, int percpecja, int inteligencja, int sila_woli, int charyzma)
        {
            Imie = imie;
            Sila = sila;
            Kondycja = kondycja;
            Zrecznosc = zrecznosc;
            Percepcja = percpecja;
            Inteligencja = inteligencja;
            Sila_woli = sila_woli;
            Charyzma = charyzma;
            Zloto = 10;
        }
        
    }
}
