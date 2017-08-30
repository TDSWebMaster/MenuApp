using MenusApp.iOS;
using MenusApp.iOS.service;
using MenusApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Xamarin.Forms;

//using MenuAppService = MenusApp.iOS.service.BasicHttpBinding_IMenuAppService;

[assembly: Dependency(typeof(LoadFiles))]
namespace MenusApp.iOS
{
   class LoadFiles : ILoadFiles
   {
      public void LoadFilesFromCMS()
      {
         var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         var library = Path.Combine(documents, "..", "Library");

         try
         {
            using (MenuAppService svc = new MenuAppService())
            {
               string Json = svc.GetSectionJSON(DateTime.Now, true, 1087, true, "APP_Menu", "JourneysEast_Menu");

               File.WriteAllText(Path.Combine(MenuItemDetail.library, "MenuMap.json"), Json);

               ResourceSource[] resources = svc.GetResourceSourcesForSection(DateTime.Now, true, 1087, true, "APP_Menu", "JourneysEast_Menu");

               foreach (ResourceSource resource in resources)
               {
                  string pathName = Path.Combine(library, resource.MediaName);

                  if (!File.Exists(pathName))
                  {
                     File.WriteAllBytes(Path.Combine(library, resource.MediaName), resource.Binary);
                  }
               }
            }
         }
         catch(Exception)
         {
            // Incase something really bad happens (can't load all the images).
            LoadDefaultFiles();
         }
      }

      public void LoadDefaultFiles()
      {
         if (Directory.Exists(MenuItemDetail.library))
         {
            foreach (var file in Directory.EnumerateFiles(MenuItemDetail.library))
            {
               File.Delete(file);
            }
         }

         File.Copy("MenuMap.json", Path.Combine(MenuItemDetail.library, "MenuMap.json"));
         var filename = Path.Combine(MenuItemDetail.library, "MenuMap.json");

         MenuModel modelLoad;

         try
         {
            using (Stream s = File.OpenRead(filename))
            {
               using (StreamReader sr = new StreamReader(s))
               {
                  using (JsonReader reader = new JsonTextReader(sr))
                  {
                     JsonSerializer sir = new JsonSerializer();

                     modelLoad = sir.Deserialize<MenuModel>(reader);
                     modelLoad.SetLanguage();
                  }
               }
            }

            foreach (MenuItemDetail item in modelLoad.MenuItems)
            {
               string ii = Path.Combine(MenuItemDetail.library, item.ItemImage);
               if (!File.Exists(ii))
                  File.Copy("./food/" + item.ItemImage, ii);

               foreach (LanguageGroup languageItem in item.languageImages)
               {
                  string ign = Path.Combine(MenuItemDetail.library, languageItem.ItemGroupName);
                  string di = Path.Combine(MenuItemDetail.library, languageItem.DescriptionImage);

                  if (!File.Exists(ign))
                     File.Copy("./food/" + languageItem.ItemGroupName, ign);

                  if (!File.Exists(di))
                     File.Copy("./food/" + languageItem.DescriptionImage, di);
               }
            }
         }
         catch (Exception)
         {
         }
      }
   }
}
