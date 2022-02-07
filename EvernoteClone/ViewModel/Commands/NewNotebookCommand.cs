using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        //Need an instance of the view model that requires the command
        public NotesViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesViewModel vm) //instance passed through the constructor
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO: Create new notebook
            VM.CreateNoteBook();
        }
    }
}
