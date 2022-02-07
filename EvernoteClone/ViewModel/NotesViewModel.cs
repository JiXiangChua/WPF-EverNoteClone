using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace EvernoteClone.ViewModel
{
    public class NotesViewModel
    {
        public ObservableCollection<NoteBook> NoteBooks { get; set; } //using ObservableCollection allow us to bind a list of items to the list view in the xaml

        private NoteBook selectedNotebook;

        public NoteBook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //TODO: get notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

    }
}