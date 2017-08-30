using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MenusApp.Models
{
   [JsonObject]
   public class MenuModel : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [JsonProperty]
      public List<MenuItemDetail> MenuItems { get; private set; }
      
      // Constructor
      [JsonConstructor]
      public MenuModel(MenuLanguage language = MenuLanguage.ENGLISH)
      {
         MenuItems = new List<MenuItemDetail>();
         SetLanguage(language);
      }

      public void SetLanguage(MenuLanguage language = MenuLanguage.ENGLISH)
      {
         MenuItemDetail.SetActiveLanguage(language);

         foreach (var item in MenuItems)
         {
            item.SendNotification();
         }

         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MenuItems"));
      }
   }
}
