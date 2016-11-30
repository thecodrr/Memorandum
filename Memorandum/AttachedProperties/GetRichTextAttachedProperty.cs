using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Memorandum.AttachedProperties
{
    public class GetRichTextAttachedProperty
    {
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for RichText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(GetRichTextAttachedProperty), new PropertyMetadata(string.Empty, callback));

        private static async void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reb = (RichEditBox)d;
            string content = (string)e.NewValue;
            if (content != null)
                reb.Document.SetText(TextSetOptions.FormatRtf, content);
            
            //var reb = (RichEditBox)d;
            //string fileURI = (string)e.NewValue;
            //if(fileURI != null)
            //using (var stream = new StreamReader(await(await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileURI)).OpenStreamForReadAsync()))
            //{
            //    string content = await stream.ReadToEndAsync();
            //    reb.Document.SetText(TextSetOptions.FormatRtf, content);
            //    reb.IsReadOnly = true;
            //}
        }

    }
}
