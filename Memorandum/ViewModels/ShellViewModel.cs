using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Memorandum.Model;
using Windows.Storage.Search;

namespace Memorandum.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        ThreadSafeObservableCollection<Note> noteCollection = new ThreadSafeObservableCollection<Note>();
        public ThreadSafeObservableCollection<Note> NoteCollection
        {
            get { return noteCollection; }
            set { Set(ref noteCollection, value); }
        }
        public ShellViewModel()
        {
            FillCollection();
        }
        public async void FillCollection()
        {
            var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new List<string> { ".rtf", ".txt" });
            var fileCol = await ApplicationData.Current.LocalFolder.CreateFileQueryWithOptions(queryOptions).GetFilesAsync();
            foreach(var file in fileCol)
            {
                Note note = new Note();
                note.FileName = file.DisplayName + file.FileType;
                note.Title = file.DisplayName;
                note.Date = file.DateCreated.ToString();
                NoteCollection.Add(note);
            }
        }
    }
}
