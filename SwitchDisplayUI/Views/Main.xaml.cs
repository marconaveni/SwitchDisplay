// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SwitchDisplayUI.Helpers;
using SwitchDisplayUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SwitchDisplayUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        public Main()
        {
            this.InitializeComponent();
            ListMonitors();
        }

        private void ListMonitors()
        {

            List<Monitor> monitores = MonitorChanger.GetMonitorList();

            for (int i = 0; i < monitores.Count; i++)
            {
                cmbDisplays.Items.Add("MONITOR: " + ((int)monitores[i].id+1) + " " + monitores[i].name);
            }
            cmbDisplays.SelectedIndex = (int)MonitorChanger.GetMonitorPrimary();
            getMonitor();
        }

        private void getMonitor() 
        {
            if (cmbDisplays.SelectedIndex == (int)MonitorChanger.GetMonitorPrimary())
            {
                BtnApply.IsEnabled = false;
                return;
            }
            BtnApply.IsEnabled = true;
        }


        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            MonitorChanger.SetMonitorPrimary((uint)cmbDisplays.SelectedIndex);
            getMonitor();
        }

        private void cmbDisplays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getMonitor();
        }
    }
}
