using Suda.Else;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AIGS.Common;
using HandyControl.Controls;
using AIGS.Helper;

namespace Suda.Pages
{
    public class SettingsViewModel : Suda.Else.ModelBase
    {
        public Settings Settings { get; set; }
        
        public List<Language.Type> LanguageCombox { get; set; } = Enum.GetValues(typeof(Language.Type)).Cast<Language.Type>().ToList(); 
        public List<Theme.Type> ThemeCombox { get; set; } = Enum.GetValues(typeof(Theme.Type)).Cast<Theme.Type>().ToList(); 

        public void Load()
        {
            Settings = Settings.Read();
        }

        public void Cancel()
        {
            Load();
        }

        public async void Confim()
        {
            if (Settings.Save())
            {
                await Global.VMMain.ChangeSettings(Settings, Global.Settings);
                Global.Settings = Settings.Read();
                Growl.Success(Language.Get("strmsgSaveSuccess"), Global.TOKEN_MAIN);
            }
            else
                Growl.Error(Language.Get("strmsgSaveFailed"), Global.TOKEN_MAIN);
        }

        public void OpenDir()
        {
            System.Diagnostics.Process.Start(Global.PATH_BASE);
        }
    }

    
}
