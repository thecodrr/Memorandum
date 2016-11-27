using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Memorandum.Model;
using Windows.UI.Text;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Memorandum
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditView : Page
    {
        public EditView()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Note)
            {
                string fileURI = (e.Parameter as Note).FileName;
                if (fileURI != null)
                    using (var stream = new StreamReader(await(await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileURI)).OpenStreamForReadAsync()))
                    {
                        string content = await stream.ReadToEndAsync();
                        editBox.Document.SetText(TextSetOptions.FormatRtf, content);                        
                    }
            }
            base.OnNavigatedTo(e);
        }
        private void editBox_Paste(object sender, TextControlPasteEventArgs e)
        {
            var editBox = sender as RichEditBox;
            if (editBox.Document.CanPaste())
            {
                editBox.Document.Selection.Paste(13);
                e.Handled = true;
            }
        }
    }
}
