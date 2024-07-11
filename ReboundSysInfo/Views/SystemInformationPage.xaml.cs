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
using System.Management;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Reflection.Metadata.Ecma335;

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
        DeviceName.Text = (string)model + $" ({Environment.MachineName})";
        cpu.Text = GetCPUName();
        ram.Text = GetRAMAmount();
        WindowsVersion.Text = Environment.OSVersion.ToString();
    }

    public static string GetCPUName() {
        var cpu =
    new ManagementObjectSearcher("select * from Win32_Processor")
    .Get()
    .Cast<ManagementObject>()
    .First();
        var ProcessorName = (string)cpu["Name"];

        ProcessorName =
           ProcessorName
           .Replace("(TM)", "™")
           .Replace("(tm)", "™")
           .Replace("(R)", "®")
           .Replace("(r)", "®")
           .Replace("(C)", "©")
           .Replace("(c)", "©")
           .Replace("    ", " ")
           .Replace("  ", " ");
        return ProcessorName;
    }

    public string GetRAMAmount()
    {

        ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
        ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
        var amount = "";
        foreach (ManagementObject managementObject in managementObjectCollection)
        {
            amount = $"{managementObject["TotalVisibleMemorySize"]}MB RAM";
        }
        return amount;
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
