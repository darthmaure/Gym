using System;
using Gym.Views;
using Xamarin.Forms;

namespace Gym
{
    public partial class App : Application
	{
		public App ()
		{
            try
            {
                InitializeComponent();

                new Bootstrap().Init();

                var page = new TabbedPage() { Title = "Chin-ups counter" };
                page.Children.Add(new CounterPage() { Title = "Counter" });
                page.Children.Add(new SettingsPage() { Title = "Settings" });

                MainPage = page;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
	}
}
