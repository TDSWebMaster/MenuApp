using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MenusApp.Models
{
   [JsonObject]
   public class MenuItemDetail : INotifyPropertyChanged
   {
      public static string library = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library") ;
      public static MenuLanguage activeLanguage { get; private set; }

      public LanguageGroup ActiveLanguageObject { get { return languageImages[(int)activeLanguage]; } }

      public string ActiveItemImage { get { return Path.Combine(library, ItemImage); } }

      public string ActiveGroupName
      {
         get
         {
            return Path.Combine(library, ActiveLanguageObject.ItemGroupName);
         }
      }

      public string ActiveDescription
      {
         get
         {
            return Path.Combine(library, ActiveLanguageObject.DescriptionImage);
         }
      }

      [JsonProperty]
      public string ItemImage { get; set; }
      
      [JsonProperty]
      public List<LanguageGroup> languageImages { get; set; }

      [JsonProperty]
      public string ItemGroupName { get; private set; }

      public event PropertyChangedEventHandler PropertyChanged;

      public static void SetActiveLanguage(MenuLanguage language)
      {
         activeLanguage = language;
      }

      /// <summary>
      /// this needs to be done seperately, because the language is static, and the change is static, the propery changed event must be called seperately.
      /// </summary>
      public void SendNotification()
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveDescription"));
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveGroupName"));
      }
   }

   [JsonObject]
   public class LanguageGroup
   {
      [JsonProperty]
      public string ItemGroupName { get; set; }

      [JsonProperty]
      public string DescriptionImage { get; set; }
   }
}
