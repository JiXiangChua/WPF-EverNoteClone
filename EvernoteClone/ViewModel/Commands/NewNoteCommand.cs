using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        //Need an instance of the view model that requires the command
        public NotesViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            //add and remove the event handlers from the value
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NewNoteCommand(NotesViewModel vm) //instance passed through the constructor
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            NoteBook selectedNotebook = parameter as NoteBook; //parameter is binded through the view xaml
            
            //Evaluate if the selectedNotebook is null / not selected:
            if (selectedNotebook != null)
                return true;

            return false;
        }

        public void Execute(object parameter)
        {
            NoteBook selectedNotebook = parameter as NoteBook;

            //TODO: Create new note
            //Calling the method from the view model, thats why we need a view model property here
            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
