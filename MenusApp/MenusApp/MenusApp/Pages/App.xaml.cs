
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace MenusApp
{
   public partial class App : Application
	{
		public App ()
		{
         InitializeComponent();
         MainPage = new NavigationPage(new MenusApp.Pages.MainPage());
      }

		protected override void OnStart ()
		{
			// Handle when your app starts
			MobileCenter.Start("ios=d6fe7440-7a70-4cc6-b410-49cb5f309ac5;", typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
