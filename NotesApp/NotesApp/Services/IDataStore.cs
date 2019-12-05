using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface IDataStore
    {
        Task<string> AddNoteAsync(Note courseNote);
        Task<bool> UpdateNoteAsync(Note courseNote);
        Task<Note> GetNoteAsync(string id);
        Task<IList<Note>> GetNotesAsync();
        Task<IList<string>> GetCoursesAsync();

    }
}
