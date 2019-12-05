using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using NotesApp.Models;
using NotesApp.Views;
using System.Linq;

namespace NotesApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadNotesCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "SaveNote", async (obj, note) =>
            {
                Notes.Add(note);
                await DataStore.AddNoteAsync(note);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "UpdateNote", async (obj, note) =>
            {
                //Notes.Remove(Notes.Where(x => x.Id == note.Id).FirstOrDefault());
                //Notes.Add(note);

                await DataStore.UpdateNoteAsync(note);
                await ExecuteLoadNotesCommand();
            });
        }

        async Task ExecuteLoadNotesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await DataStore.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}