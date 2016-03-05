using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePrototype
{
    class AudioManager
    {
        Game1 game;
        List<Song> music;
        public AudioManager(Game1 game)
        {
            this.game = game;
            //LoadContent();
        }

        public void LoadContent()
        {
            Button.Click = game.Content.Load<SoundEffect>("Sounds/Click");

            music = new List<Song>();
            Song song = game.Content.Load<Song>("Music/Alpha");
            music.Add(song);
        }

        public void Update()
        {
            if (MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Play(music[0]);
        }
    }
}
