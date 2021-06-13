using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
namespace Boulder_Dash_Project
{
    class GameField : Field
    {
        public static int score = 0;
        public static int maxpoint = 0;
        public static System.DateTime Time = DateTime.Now;
        public static bool TechnicalLevel = false;
        public static bool GameStatus = false;
        public static bool win = false;

        public static void Win()
        {
            player.SoundLocation = "win.wav";
            player.Play();

            Process.Start(new ProcessStartInfo(@"win.mp4") { UseShellExecute = true });

            Output.Clear();
            Output.Message("Win!");

            Thread.Sleep(5000);
            player.Stop();
            score = maxpoint;
            Output.Clear();
            win = true;
        }

        public static void Defeat()
        {
            player.SoundLocation = "loose.wav";
            player.Play();
            Process.Start(new ProcessStartInfo(@"defeat.mp4") { UseShellExecute = true });

            Output.Clear();
            Output.Message("Defeat!");
            Thread.Sleep(3000);
            player.Stop();

            Output.Clear();

            win = false;
        }

        public static void AddScores()
        {
            GameField.score += 100;
            if (GameField.score >= GameField.maxpoint && TechnicalLevel == false) GameField.Win();
        }
    }
}