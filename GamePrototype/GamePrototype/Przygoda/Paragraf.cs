using GamePrototype.Sceny.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GamePrototype.Przygoda
{
    class Paragraf
    {
        static TextManager textManager;

        protected static Cel actual;
        protected static String text;
        protected static List<Choice> wybory;
        protected static String image; //bez rozszerzenia!!!

        public static void SetParagraf(TextManager tekstManager)
        {
            textManager = tekstManager;
        }
        
        internal static void Update(bool isDirect = false)
        {
            if(!isDirect)
                text = GetText(text);
            textManager.GetParagraph(text, image, wybory, actual);
        }
        internal static String GetText(String number)
        {
            Console.WriteLine(number);
            String temp;
            using (StreamReader sr = new StreamReader(string.Format("Content/Text/{0}.txt", number)))
            {
                temp = sr.ReadToEnd().Normalize();
            }
            return temp;
        }
    }
}
