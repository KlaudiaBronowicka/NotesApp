using System;
using System.Collections.Generic;
using NotesApp.Models;

namespace NotesApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Note Note { get; set; }

        public bool IsNewNote;

        public string NoteTitle
        {
            get => Note.Title;
            set
            {
                Note.Title = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get => Note.Text;
            set
            {
                Note.Text = value;
                OnPropertyChanged();
            }
        }

        public string NoteCourse
        {
            get => Note.Course;
            set
            {
                Note.Course = value;
                OnPropertyChanged();
            }
        }

        public IList<string> CourseList { get; set; }

        public ItemDetailViewModel(Note note = null)
        {
            if (note == null)
            {
                IsNewNote = true;
                Title = "Create note";
                Note = new Note();
            }
            else
            {
                Title = "Edit note";
                Note = note;
            }

            InitializeCourseList();
        }

        private async void InitializeCourseList()
        {
            CourseList = await DataStore.GetCoursesAsync();
        }
    }
}
