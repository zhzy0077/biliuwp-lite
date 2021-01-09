﻿using BiliLite.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace BiliLite.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LocalPlayerPage : Page
    {
        public LocalPlayerPage()
        {
            this.InitializeComponent();
            this.Loaded += LocalPlayerPage_Loaded;
            player.FullScreenEvent += Player_FullScreenEvent;
        }

        private void Player_FullScreenEvent(object sender, bool e)
        {
            if (e)
            {
                this.Margin = new Thickness(0, -40, 0, 0);
              
            }
            else
            {
                this.Margin = new Thickness(0);
              
            }
        }

        private void LocalPlayerPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is MyFrame)
            {
                (this.Parent as MyFrame).ClosedPage -= LocalPlayerPage_ClosedPage;
                (this.Parent as MyFrame).ClosedPage += LocalPlayerPage_ClosedPage;
            }
        }

        private void LocalPlayerPage_ClosedPage(object sender, EventArgs e)
        {
            player?.Dispose();
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode== NavigationMode.New)
            {
                var data = e.Parameter as LocalPlayInfo;
                player.InitializePlayInfo(data.PlayInfos, data.Index);
            }
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            player?.Dispose();
          
            base.OnNavigatingFrom(e);
        }
    }
    public class LocalPlayInfo
    {
        public List<PlayInfo> PlayInfos { get; set; }
        public int Index { get; set; } = 0;
    }
}