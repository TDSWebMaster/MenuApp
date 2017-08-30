using MenusApp.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using UIKit;

namespace MenusApp.Pages
{
   public partial class MenuPage : ContentPage
   {
      private MenuModel Menu { get; set; }

      private void EnableEnglishButtonHighlight(bool enabled)
      {
         EnglishButton.Image = enabled ? "English_Active.jpg" : "English_Idle.jpg";
      }

      private void EnableChineseButtonHighlight(bool enabled)
      {
         ChineseButton.Image = enabled ? "Chinese_Active.jpg" : "Chinese_Idle.jpg";
      }

      private void LightButtons(MenuLanguage language)
      {
         EnableEnglishButtonHighlight(language == MenuLanguage.ENGLISH);
         EnableChineseButtonHighlight(language == MenuLanguage.CHINESE);
      }

      public MenuPage(MenuLanguage language)
      {
         UIApplication.SharedApplication.StatusBarHidden = true;
         InitializeComponent();

         LoadMenu(language);

         BindingContext = Menu;
         LightButtons(language);
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         NavigationPage.SetHasNavigationBar(this, false);
      }

      private void EnglishButton_Clicked(object sender, System.EventArgs e)
      {
         LightButtons(MenuLanguage.ENGLISH);
         Menu.SetLanguage(MenuLanguage.ENGLISH);
      }

      private void ChineseButton_Clicked(object sender, System.EventArgs e)
      {
         LightButtons(MenuLanguage.CHINESE);
         Menu.SetLanguage(MenuLanguage.CHINESE);
      }

      private void HomeButton_Clicked(object sender, System.EventArgs e)
      {
         Navigation.PopAsync();
      }

      private void MoveLeftButton_Clicked(object sender, EventArgs e)
      {
         if(MenuCarousel != null && Menu != null && Menu.MenuItems.Count > 1)
         {
            MenuCarousel.Position = (MenuCarousel.Position + Menu.MenuItems.Count - 1) % Menu.MenuItems.Count;
         }
      }

      private void MoveRightButton_Clicked(object sender, EventArgs e)
      {
         if (MenuCarousel != null && Menu != null && Menu.MenuItems.Count > 1)
         {
            MenuCarousel.Position = (MenuCarousel.Position + 1) % Menu.MenuItems.Count;
         }
      }
      public void LoadMenu(MenuLanguage language)
      {
         var filename = Path.Combine(MenuItemDetail.library, "MenuMap.json");

         try
         {
            using (Stream s = File.OpenRead(filename))
            {
               using (StreamReader sr = new StreamReader(s))
               {
                  using (JsonReader reader = new JsonTextReader(sr))
                  {
                     JsonSerializer sir = new JsonSerializer();

                     Menu = sir.Deserialize<MenuModel>(reader);
                     Menu.SetLanguage(language);
                  }
               }
            }
         }
         catch (Exception)
         {
            Menu = new MenuModel(language);
         }
      }
   }
}
