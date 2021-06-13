using System;
using NAudio.Wave;
using System.Threading;
using System.Windows.Forms;
namespace Boulder_Dash_Project
{
    class Music
    {
        static Random RandomNumber = new Random();

        public static Mp3FileReader reader = new Mp3FileReader("music1.mp3");
        public static WaveOut waveOut = new WaveOut();
        public static TimeSpan totalTime;

        public static void SelectMusic(int i)
        {
            reader = new Mp3FileReader("music" + i + ".mp3");
            totalTime = reader.TotalTime;
            waveOut.Init(reader);
            waveOut.Play();
        }

        public static void MusicFunction()
        {
            int i;

            while (true)
            {
                i = RandomNumber.Next() % 23 + 1;
                SelectMusic(i);
                Thread.Sleep(totalTime);
            }

        }
    }
}
