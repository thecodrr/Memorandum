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
using System.Text.RegularExpressions;
using Memorandum.ViewModels;
using Windows.UI.Xaml.Media.Animation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Memorandum
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditView : Page
    {
        EditViewModel EditVM = new EditViewModel();
        public EditView()
        {
            this.InitializeComponent();
            this.DataContext = EditVM;
            TransitionCollection collection = new TransitionCollection();
            NavigationThemeTransition theme = new NavigationThemeTransition();
            var info = new EntranceNavigationTransitionInfo();
            theme.DefaultNavigationTransitionInfo = info;
            collection.Add(theme);
            this.Transitions = collection;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Note)
            {
                EditVM.Note = e.Parameter as Note;
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

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void editBox_TextChanged(object sender, RoutedEventArgs e)
        {           
            string content = "";
            editBox.Document.GetText(TextGetOptions.NoHidden, out content);
            EditVM.Content = content.Trim();
        }
    }
}
