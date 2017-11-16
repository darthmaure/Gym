using Gym.ViewModels;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gym.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CounterPage : ContentPage
	{
		public CounterPage ()
		{
            InitializeComponent();
            BindingContext = TinyIoCContainer.Current.Resolve<CounterViewModel>();
		}
	}
}