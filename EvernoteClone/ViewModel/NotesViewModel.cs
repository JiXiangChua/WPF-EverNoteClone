﻿using EvernoteClone.Model;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<NoteBook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            //Read the notebooks from database and display inside the list view
            GetNotebooks();
        }

        public void CreateNoteBook()
        {
            NoteBook newNotebook = new NoteBook()
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            DatabaseHelper.Insert(newNotebook);

            GetNotebooks(); // so that the list view is updated with whatever new notebook has just been created.
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

            GetNotes(); //so that the list view is updated with whatever new note has just been created.
        }

        public void GetNotebooks()
        {
            //Read the database and get the results
            var notebooks = DatabaseHelper.Read<NoteBook>().Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            foreach(var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                //Read the database and get the results by filtering and returning
                //the note's notebookid that matches the selectedNotebook id.
                //The .where() is using Linq.
                var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

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

        public void StopEditing(NoteBook notebook)
        {
            //Start editing to true
            IsVisible = Visibility.Collapsed;

            //Save new changes to a notebook
            DatabaseHelper.Update(notebook);
            GetNotebooks(); //gets the updated database to the list view
        }
    }
}