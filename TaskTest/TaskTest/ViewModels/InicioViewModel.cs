using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskTest.Models;
using TaskTest.Servicos;
using Xamarin.Forms;

namespace TaskTest.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        #region Propriedades

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (SetProperty(ref isBusy, value))
                {
                    CarregarCommand.ChangeCanExecute();
                }
            }
        }

        private string tempo;

        public string Tempo
        {
            get { return tempo; }
            set { SetProperty(ref tempo, value); }
        }


        public Command CarregarCommand                        { get; }
        public ObservableCollection<Palestra> Palestras       { get; }
        public ObservableCollection<Palestrante> Palestrantes { get; }

        #endregion

        public InicioViewModel()
        {
            Palestras       = new ObservableCollection<Palestra>();
            Palestrantes    = new ObservableCollection<Palestrante>();
            CarregarCommand = new Command(async () => await ExecuteCarregarCommand(),()=>!IsBusy);
        }

        void CarregarDados<T>(IEnumerable<T> colecao) where T: class
        {
            var tipo = typeof(T);

            if (tipo == typeof(Palestra))
                foreach (var item in colecao)
                {
                    Palestras.Add(item as Palestra);
                }
            else
                foreach (var item in colecao as IEnumerable<Palestrante>)
                {
                    Palestrantes.Add(item);
                }
        }

        async Task ExecuteCarregarCommand()
        {
            var timer = new Stopwatch();
            if (!IsBusy)
            {
                try
                {
                    Palestrantes.Clear();
                    Palestras.Clear();
                    IsBusy          = true;
                    timer.Start();
                    var taskpalestra    = ApiServico.ObterPalestras();
                    var taskpalestrante = ApiServico.ObterPalestrantes();

                    //var palestrante = await taskpalestrante;
                    //var palestra    = await taskpalestra;

                    CarregarDados(await taskpalestrante);
                    CarregarDados(await taskpalestra);
                    timer.Stop();

                    Tempo = timer.ElapsedMilliseconds.ToString();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Erro:{ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return;
        }
    }
}


