using Gym.ViewModels;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gym.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
            InitializeComponent();

            this.BindingContext = TinyIoCContainer.Current.Resolve<SettingsViewModel>();
		}
	}
}