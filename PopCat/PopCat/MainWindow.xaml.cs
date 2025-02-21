﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PopCat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t;
        bool sound = true;

        public MainWindow()
        {
            InitializeComponent();
            t = new Thread(new ThreadStart(Play));
            t.Start();
            
            MouseDown += MainWindow_MouseDown;

            var menu = new System.Windows.Forms.ContextMenu();

            var noti = new NotifyIcon
            {
                Icon = new Icon("PopCat-Icon.ico"),
                Visible = true,
                Text = "PopCat",
                ContextMenu = menu,
            };
            var shutdown = new System.Windows.Forms.MenuItem
            {
                Index = 2,
                Text = "Exit",

            };
            var bgm = new System.Windows.Forms.MenuItem
            {
                Index = 0,
                Text = "Sound",
            }; bgm.Checked = true;
            var license = new System.Windows.Forms.MenuItem
            {
                Index = 1,
                Text = "License"
            };

            shutdown.Click += (object o, EventArgs e) =>
            {
                System.Windows.Application.Current.Shutdown();
            };
            bgm.Click += (object o, EventArgs e) =>
            {
                bgm.Checked = !bgm.Checked;
                if (bgm.Checked == true)
                    sound = true;
                else
                    sound = false;
            };
            license.Click += (object o, EventArgs e) =>
            {
                System.Windows.Forms.MessageBox.Show("MIT License\r\n\r\nCopyright (c) 2022 Kyumin Nam\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy\r\nof this software and associated documentation files (the \"Software\"), to deal\r\nin the Software without restriction, including without limitation the rights\r\nto use, copy, modify, merge, publish, distribute, sublicense, and/or sell\r\ncopies of the Software, and to permit persons to whom the Software is\r\nfurnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all\r\ncopies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\nAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\nOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\r\nSOFTWARE.", "Licensing information");
            };

            menu.MenuItems.Add(bgm);
            menu.MenuItems.Add(license);
            menu.MenuItems.Add(shutdown);
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Play()
        {
            while (true)
            {
                if (sound == true)
                {
                    MediaPlayer mediaPlayer = new MediaPlayer();
                    mediaPlayer.Open(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "PopCat-Sound.wav")));
                    mediaPlayer.Play();
                    Thread.Sleep(500);
                }
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            t.Abort();
        }
    }
}
