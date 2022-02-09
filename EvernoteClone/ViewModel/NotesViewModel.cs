using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NoteBook> Notebooks { get; set; } //using ObservableCollection allow us to bind a list of items to the list view in the xaml

        private NoteBook selectedNotebook;

        public NoteBook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }


        private Visibility isVisible;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }


        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            DeleteCommand = new DeleteCommand(this);

            Notebooks = new ObservableCollection<NoteBook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            //Read the notebooks from database and display inside the list view
            GetNotebooks();
        }

        public async void CreateNoteBook()
        {
            NoteBook newNotebook = new NoteBook()
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            await DatabaseHelper.Insert(newNotebook); //wait for the insert method to return before running next line of code

            GetNotebooks(); // so that the list view is updated with whatever new notebook has just been created.
        }

        public async void CreateNote(string notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            await DatabaseHelper.Insert(newNote); //no need to establish what type of data is passed to the Insert generics
            //Generic method can infer from its caller and set the T according to the data type of the object passed in.

            GetNotes(); //so that the list view is updated with whatever new note has just been created.
        }

        public async void GetNotebooks()
        {
            //Read the database and get the results
            var notebooks = (await DatabaseHelper.Read<NoteBook>()).Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            foreach(var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                //Read the database and get the results by filtering and returning
                //the note's notebookid that matches the selectedNotebook id.
                //The .where() is using Linq.
                var notes = (await DatabaseHelper.Read<Note>()).Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            //Start editing to true
            IsVisible = Visibility.Visible;
        }

        public async void StopEditing(NoteBook notebook)
        {
            //Start editing to true
            IsVisible = Visibility.Collapsed;

            //Save new changes to a notebook
            await DatabaseHelper.Update(notebook);
            GetNotebooks(); //gets the updated database to the list view
        }

        public async void DeleteNotebookOrNote(IHasId deleteEntry) //Since both notebook and note are implementing the IHasId interface
        {
            await DatabaseHelper.Delete(deleteEntry);
            GetNotes();
            GetNotebooks();
        }

    }
}