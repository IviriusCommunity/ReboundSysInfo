using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Win32;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ReboundSysInfo.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SystemInformationPage : Page
{
    public SystemInformationPage()
    {
        this.InitializeComponent();
        var manufacturer = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS").GetValue("SystemManufacturer");
        var model = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS").GetValue("SystemProductName");
        wallBox.UriSource = new Uri(GetCurrentWallpaper());
        deviceName.Text = (string)model;
        manufacturerName.Text = (string)manufacturer;
    }

    private string GetCurrentWallpaper()

    {

          // The current wallpaper path is stored in the registry at HKEY_CURRENT_USER\\Control Panel\\Desktop\\WallPaper

          RegistryKey rkWallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);

          string WallpaperPath = rkWallPaper.GetValue("WallPaper").ToString();

         rkWallPaper.Close();

          // Return the current wallpaper path

          return WallpaperPath;

    }
}
