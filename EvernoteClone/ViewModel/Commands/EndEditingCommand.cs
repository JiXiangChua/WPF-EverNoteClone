using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel ViewModel { get; set; }

        public EndEditingCommand(NotesViewModel vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NoteBook notebook = parameter as NoteBook;
            if (notebook != null)
                ViewModel.StopEditing(notebook);
        }
    }
}
