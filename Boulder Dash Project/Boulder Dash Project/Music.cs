using System;
using NAudio.Wave;
using System.Threading;

namespace Boulder_Dash_Project
{
    class Music
    {
        static Random RandomNumber = new Random();

        public static Mp3FileReader reader = new Mp3FileReader("music1.mp3");
        public static WaveOut waveOut = new WaveOut();

        public static void MusicFunction()
        {
            int i, time;
            TimeSpan totalTime;
            //while (true)
            {
                i = RandomNumber.Next() % 23 + 1;
                
                switch (i)
                {
                    case 1:
                        reader = new Mp3FileReader("music1.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 2:
                        reader = new Mp3FileReader("music2.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 3:
                        reader = new Mp3FileReader("music3.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 4:
                        reader = new Mp3FileReader("music4.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 5:
                        reader = new Mp3FileReader("music5.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 6:
                        reader = new Mp3FileReader("music6.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 7:
                        reader = new Mp3FileReader("music7.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 8:
                        reader = new Mp3FileReader("music8.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 9:
                        reader = new Mp3FileReader("music9.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 10:
                        reader = new Mp3FileReader("music10.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 11:
                        reader = new Mp3FileReader("music11.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 12:
                        reader = new Mp3FileReader("music12.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 13:
                        reader = new Mp3FileReader("music13.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 14:
                        reader = new Mp3FileReader("music14.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 15:
                        reader = new Mp3FileReader("music15.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 16:
                        reader = new Mp3FileReader("music16.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 17:
                        reader = new Mp3FileReader("music17.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 18:
                        reader = new Mp3FileReader("music18.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 19:
                        reader = new Mp3FileReader("music19.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 20:
                        reader = new Mp3FileReader("music20.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 21:
                        reader = new Mp3FileReader("music21.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 22:
                        reader = new Mp3FileReader("music22.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                    case 23:
                        reader = new Mp3FileReader("music23.mp3");
                        totalTime = reader.TotalTime;
                        waveOut.Init(reader);
                        waveOut.Play();
                        Thread.Sleep(totalTime);
                        break;
                }
            }
            
        }
    }
}
