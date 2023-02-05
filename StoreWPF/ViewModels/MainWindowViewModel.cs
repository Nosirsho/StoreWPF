using MathCore.ViewModels;
using MathCore.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /*=====================================================================================================*/
        #region Title : string - Загаловок
        /// <summary>Загаловок</summary>
        private string _Title = "Главное окно программы";

        /// <summary>Загаловок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }
        #endregion
        /*=====================================================================================================*/

        /*=====================================================================================================*/
        #region Command ShowBooksViewCommand - Отобразить представление книг
        /// <summary>Отобразить представление книг</summary>
        private ICommand _ShowBooksViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowBooksViewCommand => _ShowBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление книг</summary>
        private bool CanShowBooksViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление книг</summary>
        private void OnShowBooksViewCommandExecuted()
        {
            
        }
        #endregion
        /*=====================================================================================================*/
    }
}
