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
   class TitleScreen
    {
        Game1 game1;
        SpriteBatch spriteBatch;

        Sprite background;

        Button continueGame;
        Button newGame;
        Button load;
        Button options;
        Button credits;
        Button exit;

        public TitleScreen(SpriteBatch sprite, Game1 game)
        {
            this.spriteBatch = sprite;
            this.game1 = game;
        }

        public void LoadContent()
        {
            background = new Sprite(game1.Content.Load<Texture2D>("background"), new Rectangle(0, 0, 1920, 1080));

            int ile = 6; //Ile buttonów
            Texture2D tempT = game1.Content.Load<Texture2D>("TitleScreen/continueDisabled");

            int yHeight = tempT.Height; //Wysokosc spirta -- 
            int ySpace = ((1080-(ile*yHeight))/(ile+1)); //Odstępy -- 
            int y = ySpace;

            int xWidth = tempT.Width; //Szerokość spirta -- 
            int x = 100; //Pozycja X -- 

            Rectangle tempR = new Rectangle(x, y, xWidth, yHeight);
            continueGame = new Button(tempT, tempR);

            tempT = game1.Content.Load<Texture2D>("TitleScreen/newgame");
            Texture2D tempM = game1.Content.Load<Texture2D>("TitleScreen/newgameM");
            Texture2D tempP = game1.Content.Load<Texture2D>("TitleScreen/newgameP");
            y += yHeight + ySpace;
            tempR = new Rectangle(x, y, xWidth, yHeight);
            newGame = new Button(tempT, tempM, tempP, tempR);

            tempT = game1.Content.Load<Texture2D>("TitleScreen/load");
            tempM = game1.Content.Load<Texture2D>("TitleScreen/loadM");
            tempP = game1.Content.Load<Texture2D>("TitleScreen/loadP");
            y += yHeight + ySpace;
            tempR = new Rectangle(x, y, xWidth, yHeight);
            load = new Button(tempT, tempM, tempP, tempR);

            tempT = game1.Content.Load<Texture2D>("TitleScreen/options");
            tempM = game1.Content.Load<Texture2D>("TitleScreen/optionsM");
            tempP = game1.Content.Load<Texture2D>("TitleScreen/optionsP");
            y += yHeight + ySpace;
            tempR = tempR = new Rectangle(x, y, xWidth, yHeight);
            options = new Button(tempT, tempM, tempP, tempR);

            tempT = game1.Content.Load<Texture2D>("TitleScreen/credits");
            tempM = game1.Content.Load<Texture2D>("TitleScreen/creditsM");
            tempP = game1.Content.Load<Texture2D>("TitleScreen/creditsP");
            y += yHeight + ySpace;
            tempR = new Rectangle(x, y, xWidth, yHeight);
            credits = new Button(tempT, tempM, tempP, tempR);

            tempT = game1.Content.Load<Texture2D>("TitleScreen/exit");
            tempM = game1.Content.Load<Texture2D>("TitleScreen/exitM");
            tempP = game1.Content.Load<Texture2D>("TitleScreen/exitP");
            y += yHeight + ySpace;
            tempR = tempR = new Rectangle(x, y, xWidth, yHeight);
            exit = new Button(tempT, tempM, tempP, tempR);
        }

        public void Update(MouseState mouseState)
        {
            if (exit.IsClicked(mouseState))
                game1.Exit();
            else if (newGame.IsClicked(mouseState))
                game1.Zmiana(Scena.Creator);

            continueGame.Update(mouseState);
            newGame.Update(mouseState);
            load.Update(mouseState);
            options.Update(mouseState);
            credits.Update(mouseState);
            exit.Update(mouseState);
        }

        public void Draw()
        {

            background.Draw(spriteBatch);

            continueGame.Draw(spriteBatch, Color.White);
            newGame.Draw(spriteBatch, Color.White);
            load.Draw(spriteBatch, Color.White);
            options.Draw(spriteBatch, Color.White);
            credits.Draw(spriteBatch, Color.White);
            exit.Draw(spriteBatch, Color.White);
        }
    }
}
