using Memorandum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Memorandum.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
        RelayCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            { if (_saveCommand == null) { _saveCommand = new RelayCommand(param => Save(param)); } return _saveCommand; }
        }
        async void Save(object para)
        {            
            string substring = Content.Length >150 ? Content.Remove(150) : Content;
            string alterTitle = substring.Contains(".")?substring.Substring(0, substring.IndexOf('.')) : substring;
            string docName = !string.IsNullOrEmpty(Note.Title) ? Note.Title : alterTitle;
            if (!string.IsNullOrEmpty(Note.FileName) && !Note.FileName.Equals(Path.Combine(ApplicationData.Current.LocalFolder.Path, docName + ".mem")))
            {
                var oldFile = await StorageFile.GetFileFromPathAsync(Note.FileName);
                await oldFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                Note.FileName = Path.Combine(ApplicationData.Current.LocalFolder.Path, docName + ".mem");
            }            
            using (var stream = await(await ApplicationData.Current.LocalFolder.CreateFileAsync(docName.Trim() + ".mem", CreationCollisionOption.ReplaceExisting)).OpenAsync(FileAccessMode.ReadWrite))
            {
                (para as ITextDocument).SaveToStream(TextGetOptions.FormatRtf, stream);
            }
            if (string.IsNullOrEmpty(Note.Title))
                Note.Title = alterTitle;
        }

        DelegateCommand _goHomeCommand;
        public ICommand GoHomeCommand
        {
            get
            { if (_goHomeCommand == null) { _goHomeCommand = new DelegateCommand(GoHome); } return _goHomeCommand; }
        }
        void GoHome()
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Shell));
        }
        public EditViewModel()
        {
            this.PropertyChanged += OnPropertyChanged;
        }
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
                WordCount = CountWords(Content).ToString() + " words";
        }

        string content;
        public string Content
        {
            get { return content; }
            set { Set(ref content, value); }
        }
        string wordCount;
        public string WordCount
        {
            get { return wordCount; }
            set { Set(ref wordCount, value); }
        }
        Note note;
        public Note Note
        {
            get { return note; }
            set { Set(ref note, value); }
        }
        public static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }
    }
}

