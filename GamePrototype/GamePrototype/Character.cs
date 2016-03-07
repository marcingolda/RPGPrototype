using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    public class Character
    {
        string imie;
        int sila;
        int zrecznosc;
        int kondycja;
        int percepcja;
        int inteligencja;
        int sila_woli;
        int charyzma;

        public Character(string imie, int sila, int zrecznosc, int kondycja, int percepcja, int inteligencja, int sila_woli, int charyzma)
        {
            this.imie = imie;
            this.sila = sila;
            this.zrecznosc = zrecznosc;
            this.kondycja = kondycja;
            this.percepcja = percepcja;
            this.inteligencja = inteligencja;
            this.sila_woli = sila_woli;
            this.charyzma = charyzma;
        }
    }
}
