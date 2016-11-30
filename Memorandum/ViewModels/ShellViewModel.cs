using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Memorandum.Model;
using Windows.Storage.Search;
using System.IO;
using Windows.UI.Text;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Memorandum.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        DelegateCommand _newNoteCommand;
        public ICommand NewNoteCommand
        {
            get
            { if (_newNoteCommand == null) { _newNoteCommand = new DelegateCommand(NewNote); } return _newNoteCommand; }
        }
        void NewNote()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(EditView), new Note());
        }
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
                using (var stream = new StreamReader(await (await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(note.FileName)).OpenStreamForReadAsync()))
                {
                    string content = await stream.ReadToEndAsync();
                    note.Content = content;
                }
                note.Title = file.DisplayName;
                note.Date = file.DateCreated.ToString(@"dd/MM/yy hh:mm:ss");
                NoteCollection.Add(note);
            }
        }
    }
}
