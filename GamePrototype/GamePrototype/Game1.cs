using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GamePrototype
{
    public enum Scena { Title, Creator, MainGame }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AudioManager audio;

        KeyboardState keyState;
        MouseState mouseState; //pozycja myszy. STRUKTURA! ZASTANOWIC SIE NAD WYDAJNOSCIA!!!

        static Rectangle ekran; //Rozdzielczość ekranu. UWAGA>>>> ZROBIC Z TEGO MOŻLIWOŚ ZMIANY W OPCJACH!!!!!!!!!! DODAC TRYB 4:3
        static Vector2 skala;
        public static Vector2 Skala
        {
            get { return skala; }
        }

        Scena scena;

        //Tryby - Da sie to jakos uproscić?!
        TitleScreen title;
        CharCreator charCreator;
        MainScreen mainScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            //fullscreen
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;

            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            ekran = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            //ekran = new Rectangle(0, 0, 1366, 768);
            float scalaX = (float)ekran.Width / 1920;
            float scalaY = (float)ekran.Height / 1080;
            skala = new Vector2(scalaX, scalaY);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            audio = new AudioManager(this);

            title = new TitleScreen(spriteBatch, this);
            charCreator = new CharCreator(spriteBatch, this); //Wszystko incijalizuje się na pocżatku. Pinknie
            mainScreen = new MainScreen(spriteBatch, this);

            scena = Scena.Title;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            title.LoadContent();
            audio.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Pozwala wyjśc esc w dowolnym momencie. Wywalić potem
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            mouseState = Mouse.GetState();

            switch (scena)
            {
                case Scena.Title:
                    title.Update(mouseState);
                    break;
                case Scena.Creator:
                    charCreator.Update(mouseState);
                    break;
                case Scena.MainGame:
                    mainScreen.Update(mouseState);
                    break;
            }

            audio.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue); -- to było domyslne
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (scena)
            {
                case Scena.Title:
                    title.Draw();
                    break;
                case Scena.Creator:
                    charCreator.Draw();
                    break;
                case Scena.MainGame:
                    mainScreen.Draw();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Skalowanie do ekranu + przesunięcie dla innych rozdizelczości(SPRAWDZI TO KONEICZNE NA NATYWNYM EKRANIE!!!)
        public static Rectangle Skaluj(Rectangle prostokat)
        {
            float przesunięcie = (ekran.Height-(ekran.Width * 9 / 16))/2;
            //return new Rectangle((int)(prostokat.X * skala.X), (int)(prostokat.Y * skala.Y), (int)(prostokat.Width * skala.X), (int)(prostokat.Height * skala.y));// rozciągnięcie
            return new Rectangle((int)(prostokat.X * skala.X), (int)(prostokat.Y * skala.X + przesunięcie), (int)(prostokat.Width * skala.X), (int)(prostokat.Height * skala.X));
        }

        public static Vector2 Skaluj(Vector2 vector)
        {
            float przesunięcie = (ekran.Height - (ekran.Width * 9 / 16)) / 2;
            //return new Vector2((int)(vector.X * skala.X), (int)(vector.Y * skala.X));//Rozciągniecie
            return new Vector2((int)(vector.X * skala.X), (int)(vector.Y * skala.X));
        }

        public void Zmiana(Scena scena)
        {
            /*
            //Czyszczenie. Garbage Collector powinien (?????!!!!) zwolnić pamięć
            switch (this.scena)
            {
                case Scena.Title:
                    title = null; //title.UnloadContent(); -- narazie ni ma!!!!!
                    break;
                case Scena.Creator:
                    charCreator = null;
                    break;
            }

            //Utworzenie sceny
            switch (scena)
            {
                case Scena.Creator:
                    charCreator = new CharCreator(spriteBatch, this);
                    break;
                case Scena.MainGame:
                    mainScreen = new MainScreen(spriteBatch, this);
                    break;
            }
             */

            //Własciwa zmiana sceny
            if (scena == Scena.Creator)
                charCreator.Reload();
            this.scena = scena;
        }

    }
}
