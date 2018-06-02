using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InicioPage : ContentPage
	{
		public InicioPage ()
		{
			InitializeComponent ();
            BindingContext = new ViewModels.InicioViewModel();
		}
	}
}