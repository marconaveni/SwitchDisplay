// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI;
using System.Runtime.InteropServices;
using WinRT;
using Microsoft.UI.Windowing;
using WinUIEx;
using SwitchDisplayUI.Views;
using SwitchDisplayUI.Helpers;
using SwitchDisplayUI.Models;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SwitchDisplayUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private AppWindow _apw;
        private OverlappedPresenter _presenter;

        public void GetAppWindowAndPresenter()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = AppWindow.GetFromWindowId(myWndId);
            _presenter = _apw.Presenter as OverlappedPresenter;
        }

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            this.SetWindowSize(400, 400);
            this.CenterOnScreen();
         

            Theme theme = new(this);
            theme.TrySetSystemBackdrop();

            //GetAppWindowAndPresenter();
            //IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            //var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            //var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            //appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 480, Height = 800 });

            // _presenter.IsResizable = false;
            //_apw.IsShownInSwitchers = false;
           // _presenter.SetBorderAndTitleBar(true, true);
            //_presenter.IsMaximizable = false;


            //this.
            rootFrame.Navigate(typeof(Main));
        }


    }


}
