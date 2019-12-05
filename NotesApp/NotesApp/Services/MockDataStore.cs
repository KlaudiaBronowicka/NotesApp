using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Models;

namespace NotesApp.Services
{
    public class MockDataStore : IDataStore
    {
        readonly List<Note> _notes;
        readonly List<string> _courses;

        public MockDataStore()
        {
            _courses = new List<string>()
            {
                "Introduction to Xamarin.Forms",
                "Android Apps with Kotlin: First App",
                "Managing Android App Data with SQLite"
            };

            _notes = new List<Note>()
            {
                new Note { Id = Guid.NewGuid().ToString(), Title = "First item", Text="This is an item description.", Course = _courses[0] },
                new Note { Id = Guid.NewGuid().ToString(), Title = "Second item", Text="This is an item description.", Course = _courses[2] },
                new Note { Id = Guid.NewGuid().ToString(), Title = "Third item", Text="This is an item description.", Course = _courses[1] },
                new Note { Id = Guid.NewGuid().ToString(), Title = "Fourth item", Text="This is an item description.", Course = _courses[2] }
            };
        }

        public async Task<string> AddNoteAsync(Note note)
        {
            _notes.Add(note);

            return await Task.FromResult(note.Title);
        }

        public async Task<bool> UpdateNoteAsync(Note note)
        {
            var noteIndex = _notes.FindIndex((Note arg) => arg.Id == note.Id);
            var noteFound = noteIndex != -1;
            if (noteFound)
            {
                _notes[noteIndex].Title = note.Title;
                _notes[noteIndex].Text = note.Text;
                _notes[noteIndex].Course = note.Course;
            }
            return await Task.FromResult(noteFound);
        }

        public async Task<Note> GetNoteAsync(string id)
        {
            return await Task.FromResult(_notes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IList<Note>> GetNotesAsync()
        {
            // Make a copy of the notes to simulate reading from an external datastore
            var returnNotes = new List<Note>();
            foreach (var note in _notes)
                returnNotes.Add(CopyNote(note));
            return await Task.FromResult(returnNotes);
        }

        public async Task<IList<string>> GetCoursesAsync()
        {
            return await Task.FromResult(_courses);
        }

        private static Note CopyNote(Note note)
        {
            return new Note { Id = note.Id, Title = note.Title, Text = note.Text, Course = note.Course };
        }
    }
}