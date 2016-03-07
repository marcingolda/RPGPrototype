using GamePrototype.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class MainScreen
    {

        Game1 game1;
        SpriteBatch spriteBatch;

        Sprite wooden;

        Button game;
        Button map;
        Button equipment;
        Button character;
        Button journey;
        Button menu;

        TextManager tekst;

        public MainScreen(SpriteBatch sprite, Game1 game1)
        {
            this.spriteBatch = sprite;
            this.game1 = game1;
            LoadContent();
        }

        public void LoadContent()
        {
            Texture2D temp = game1.Content.Load<Texture2D>("MainScreen/wooden");
            Rectangle tempR = new Rectangle(0, 0, 1920, 1080);
            wooden = new Sprite(temp, tempR);

            tekst = new TextManager(game1);

            String source = "MainScreen/Buttons/";
            int ile = 6; //ile buttonów
            temp = game1.Content.Load<Texture2D>(source + "game");
            int ywys = temp.Height;
            int space = (1080-(ile*ywys))/(ile+1);
            int y = space;
            int xszer = temp.Width;
            int x = ((1920 / 4) - xszer)/2;
            Texture2D tempM = game1.Content.Load<Texture2D>(source + "gameM");
            Texture2D tempP = game1.Content.Load<Texture2D>(source + "gameP");
            tempR = new Rectangle(x, y, xszer, ywys);
            game = new Button(temp, tempM, tempP, tempR);

            y += ywys + space;
            temp = game1.Content.Load<Texture2D>(source + "map");
            tempM = game1.Content.Load<Texture2D>(source + "mapM");
            tempP = game1.Content.Load<Texture2D>(source + "mapP");
            tempR = new Rectangle(x, y, xszer, ywys);
            map = new Button(temp, tempM, tempP, tempR);

            y += ywys + space;
            temp = game1.Content.Load<Texture2D>(source + "equip");
            tempM = game1.Content.Load<Texture2D>(source + "equipM");
            tempP = game1.Content.Load<Texture2D>(source + "equipP");
            tempR = new Rectangle(x, y, xszer, ywys);
            equipment = new Button(temp, tempM, tempP, tempR);

            y += ywys + space;
            temp = game1.Content.Load<Texture2D>(source + "cart");
            tempM = game1.Content.Load<Texture2D>(source + "cartM");
            tempP = game1.Content.Load<Texture2D>(source + "cartP");
            tempR = new Rectangle(x, y, xszer, ywys);
            character = new Button(temp, tempM, tempP, tempR);

            y += ywys + space;
            temp = game1.Content.Load<Texture2D>(source + "journ");
            tempM = game1.Content.Load<Texture2D>(source + "journM");
            tempP = game1.Content.Load<Texture2D>(source + "journP");
            tempR = new Rectangle(x, y, xszer, ywys);
            journey= new Button(temp, tempM, tempP, tempR);

            y += ywys + space;
            temp = game1.Content.Load<Texture2D>(source + "menu");
            tempM = game1.Content.Load<Texture2D>(source + "menuM");
            tempP = game1.Content.Load<Texture2D>(source + "menuP");
            tempR = new Rectangle(x, y, xszer, ywys);
            menu = new Button(temp, tempM, tempP, tempR);
        }

        public void Update(MouseState state)
        {
            if (menu.IsClicked(state))
                game1.Zmiana(Scena.Title);

            //Menu
            game.Update(state);
            map.Update(state);
            equipment.Update(state);
            character.Update(state);
            journey.Update(state);
            menu.Update(state);

            //Tekst manager
            tekst.Update(state); 
        }

        public void Draw()
        {
            //Tło
            wooden.Draw(spriteBatch, new Color(200, 200, 200));

            //Elementy stałe
            game.Draw(spriteBatch);
            map.Draw(spriteBatch);
            equipment.Draw(spriteBatch);
            character.Draw(spriteBatch);
            journey.Draw(spriteBatch);
            menu.Draw(spriteBatch);

            //Text manager
            tekst.Draw(spriteBatch);
        }
    }
}
