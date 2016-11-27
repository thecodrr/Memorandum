using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Model
{
    public class Note : ViewModelBase
    {
        string title;
        string fileName;
        string date;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
        public string FileName
        {
            get { return fileName; }
            set { Set(ref fileName, value); }
        }
        public string Date
        {
            get { return date; }
            set { Set(ref date, value); }
        }
    }
}
