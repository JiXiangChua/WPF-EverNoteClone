using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public DeleteCommand(NotesViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IHasId deleteEntry = parameter as IHasId; //Since both notebook and note are implementing the IHasId interface

            ViewModel.DeleteNotebookOrNote(deleteEntry);
        }
    }
}
