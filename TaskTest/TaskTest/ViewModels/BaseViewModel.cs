using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskTest.ViewModels
{

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public static CultureInfo culture = new CultureInfo("pt-BR");

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public async Task DisplayAlert(string title, string message, string cancel = "Ok")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }


        #region Navegação
        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var page = Locator<TViewModel>(args);

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
        Page Locator<TViewModel>(object[] args) where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel);
            var viewModelTypeName = viewModelType.Name;
            var viewModelWordLength = "ViewModel".Length;

            var name = typeof(BaseViewModel).AssemblyQualifiedName.Split('.')[0];

            var viewTypeName = $"{name}.Views.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
            var viewType = Type.GetType(viewTypeName);

            var page = Activator.CreateInstance(viewType) as Page;

            var viewModel = Activator.CreateInstance(viewModelType, args);
            if (page != null)
                page.BindingContext = viewModel;
            return page;
        }

        public async Task PopAsync() => await Application.Current.MainPage.Navigation.PopAsync();

        public async Task PopToRootAsync() => await Application.Current.MainPage.Navigation.PopToRootAsync();

        public async Task PushModalAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var page = Locator<TViewModel>(args);

            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync() => await Application.Current.MainPage.Navigation.PopModalAsync();
        #endregion
    }
}
