using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Windows.Themes;
using YoutubeMusic.Interaction;
using YoutubeMusic.Model;

namespace YoutubeMusic.ViewModel
{
    internal class CheckUsingSystemTrayWindowViewModel
    {
        public CheckUsingSystemTrayWindow ParentWindow { get; set; }
        public Command CommandYes { get; set; }
        public Command CommandNo { get; set; }

        public bool IsChecked { get; set; }

        public CheckUsingSystemTrayWindowViewModel()
        {
            SetDefaultBulletColor(Colors.WhiteSmoke);
            SetCommands();

            IsChecked = false;
        }

        private void SetCommands()
        {
            CommandYes = new Command(CommandYesCallback);
            CommandNo = new Command(CommandNoCallback);
        }

        private void CommandYesCallback()
        {
            ParentWindow.DialogResult = true;
            SetSystemTraySetting();
        }

        private void CommandNoCallback()
        {
            ParentWindow.DialogResult = false;
            SetSystemTraySetting();
        }

        private void SetSystemTraySetting()
        {
            var @checked = IsChecked ? 0xFF : 0x00;
            var @using = ParentWindow.DialogResult != null && ParentWindow.DialogResult.Value ? 0x01 : 0x02;

            MainWindowViewModel.Settings["UseSystemTray"] = @checked & @using;
        }

        private void SetDefaultBulletColor(Color color)
        {
            typeof(BulletChrome).GetRuntimeFields().First(f => f.Name == "_commonCheckMarkFill").SetValue(null, new SolidColorBrush(color));
        }
    }
}
