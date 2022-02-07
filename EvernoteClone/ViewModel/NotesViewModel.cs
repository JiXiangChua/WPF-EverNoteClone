using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
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

        public void CreateNoteBook()
        {
            NoteBook newNotebook = new NoteBook()
            {
                Name = "New notebook",
            };

            DatabaseHelper.Insert(newNotebook);
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            DatabaseHelper.Insert(newNote); //no need to establish what type of data is passed to the Insert generics
            //Generic method can infer from its caller and set the T according to the data type of the object passed in.

        }


    }
}