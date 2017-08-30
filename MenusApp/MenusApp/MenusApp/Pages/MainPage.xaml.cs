using MenusApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using UIKit;

namespace MenusApp.Pages
{
   public partial class MainPage: ContentPage
   {
      public MainPage()
      {
         UIApplication.SharedApplication.StatusBarHidden = true;
         InitializeComponent();

         // run initial load of files. if files are not present, generate or something...
         // the files should be the json file and the images.

         ChineseButton.Image = "Chinese_Idle.jpg";
         EnglishButton.Image = "English_Idle.jpg";

         DependencyService.Get<ILoadFiles>().LoadDefaultFiles();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         NavigationPage.SetHasNavigationBar(this, false);
      }

      private void EnglishButton_Clicked(object sender, EventArgs e)
      {
         Navigation.PushAsync(new MenuPage(MenuLanguage.ENGLISH), false);
      }

      private void ChineseButton_Clicked(object sender, EventArgs e)
      {
         Navigation.PushAsync(new MenuPage(MenuLanguage.CHINESE), false);
      }

      private void LoadMenu_Clicked(object sender, EventArgs e)
      {
         DependencyService.Get<ILoadFiles>().LoadFilesFromCMS();
      }
   }
}
