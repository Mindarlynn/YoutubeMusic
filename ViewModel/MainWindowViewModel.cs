using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using YoutubeMusic.Interaction;
using Drawing = System.Drawing;
using Forms = System.Windows.Forms;

namespace YoutubeMusic.ViewModel
{
    class MainWindowViewModel
    {
        private bool CloseWindow { get; set; }

        public MainWindow ParentWindow { get; set; }

        public MainWindowViewModel()
        {
            LoadSettings();
            InitializeSystemTray();
        }

        #region Settings
        private static JObject DefaultSettings
        {
            get
            {
                var settings = new JObject();

                // Property: UseSystemTray
                // Value: 
                //          0: Unknown
                //          1: Yes
                //          2: No
                settings.Add("UseSystemTray", 0);

                return settings;
            }
        }

        public static JObject Settings { get; set; }

        private const string SettingFileName = "settings.json";

        private void LoadSettings()
        {
            if (File.Exists(SettingFileName))
            {
                using (var reader = new JsonTextReader(File.OpenText(SettingFileName)))
                {
                    Settings = JObject.Load(reader);
                }
            }
            else
            {
                Settings = DefaultSettings;
            }
        }

        private void ExportSettings()
        {
            File.WriteAllText(SettingFileName, Settings.ToString());
        }
        #endregion

        #region System Tray

        private Forms.NotifyIcon _systemTray;

        private void InitializeSystemTray()
        {
            _systemTray = new Forms.NotifyIcon();

            var menu = new Forms.ContextMenu();
            var show = new Forms.MenuItem()
            {
                Index = 0,
                Text = @"Show"
            };
            show.Click += SystemTrayShowItemCallback;
            var close = new Forms.MenuItem()
            {
                Index = 0,
                Text = @"Close"
            };
            close.Click += SystemTrayCloseItemCallback;

            menu.MenuItems.Add(show);
            menu.MenuItems.Add(close);

            _systemTray.Icon = new Drawing.Icon(@"Resource\youtube-music.ico");
            _systemTray.Visible = true;
            _systemTray.DoubleClick += SystemTrayShowItemCallback;
            _systemTray.ContextMenu = menu;
            _systemTray.Text = @"Youtube Music";
        }

        private void SystemTrayShowItemCallback(object sender, EventArgs e)
        {
            ParentWindow.Visibility = Visibility.Visible;
        }

        private void SystemTrayCloseItemCallback(object sender, EventArgs e)
        {
            CloseWindow = true;
            ParentWindow.Close();
        }
        #endregion

        #region Window event
        public void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (CloseWindow)
            {
                _systemTray.Visible = false;
                e.Cancel = false;
                return;
            }

            switch (Settings["UseSystemTray"].ToObject<int>())
            {
                // Unknown
                case 0:
                {
                    var checkWindow = new CheckUsingSystemTrayWindow();
                    if (checkWindow.ShowDialog() == true)
                    {
                        ParentWindow.Visibility = Visibility.Collapsed;
                        e.Cancel = true;
                    }
                    else
                        e.Cancel = false;
                }
                break;
                // Yes
                case 1:
                {
                    e.Cancel = true;
                    ParentWindow.Visibility = Visibility.Collapsed;
                    }
                break;
                // No
                case 2:
                {
                    e.Cancel = false;
                }
                break;
            }
        }

        public void MainWindow_OnClosed(object sender, EventArgs e)
        {
            ExportSettings();
        }
        #endregion
    }
}
