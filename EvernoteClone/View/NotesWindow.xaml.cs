using Azure.Storage.Blobs;
using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        //SpeechRecognitionEngine recognizer;
        NotesViewModel viewModel;

        public NotesWindow()
        {
            InitializeComponent();

            viewModel = Resources["NotesVM"] as NotesViewModel;
            //Subscribe to an event, the ViewModel_SelectedNoteChanged is the event handler to that event
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source); //ordered by its name
            fontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;

            //var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            //                     where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            //                     select r).FirstOrDefault();

            //recognizer = new SpeechRecognitionEngine(currentCulture);

            //GrammarBuilder builder = new GrammarBuilder();
            //builder.AppendDictation();
            //Grammar grammar = new Grammar(builder);

            ////Add grammar so that the recognizer can recognise the word you say
            //recognizer.LoadGrammar(grammar);
            //recognizer.SetInputToDefaultAudioDevice();
            ////Subscribe the event SpeechRecognized with an event handler:
            //recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                //if user id is null or empty, then launch login window
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                viewModel.GetNotebooks();
            }
        }

        private async void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        { //handler executed every single time the selected node changes

            contentRichTextBox.Document.Blocks.Clear(); //clear the rich text box before checking if a selected note is exists.
            //if the note was not saved, then it should be an empty rich text box

            if (viewModel.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
                {
                    //using (FileStream fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open))
                    //{
                    //    var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                    //    contents.Load(fileStream, DataFormats.Rtf);
                    //}; 

                    /*For using azure cloud container services to store the notes*/
                    //Download the blob from cloud into these new locations on your local machine
                    string downloadPath = $"{viewModel.SelectedNote.Id}.rtf";
                    await new BlobClient(new Uri(viewModel.SelectedNote.FileLocation)).DownloadToAsync(downloadPath);

                    using (FileStream fileStream = new FileStream(downloadPath, FileMode.Open))
                    {
                        var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                        contents.Load(fileStream, DataFormats.Rtf);
                    };
                }
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //string recognizedText = e.Result.Text;

            ////Adding the text result into the rich text box
            //contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        bool isRecognizing = false;
        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!isRecognizing)
            //{
            //    recognizer.RecognizeAsync(RecognizeMode.Multiple);
            //    isRecognizing = true;
            //} else
            //{
            //    recognizer.RecognizeAsyncStop();
            //    isRecognizing = false;
            //}
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {amountCharacters} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the text that are selected by the user
            //var textToBold = new TextRange(contentRichTextBox.Selection.Start, contentRichTextBox.Selection.End); 

            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false; //if IsChecked is null, return false to the boolean variable

            if(isButtonChecked)
                //Alternatively, there is an easier way:
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);

        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Event handler triggers everytime the selection inside the richtextbox changes
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            
            //check if the selected text's weight is equal to bold. If is bold, then the bold button is checked,
            //else the bold button is not checked
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold)); //check that selectedweight is not null since selectedweight property can be null.

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && (selectedDecoration.Equals(TextDecorations.Underline));

            //setting the font family to the family used in the richtextbox
            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);


        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);

            }
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text); //here we dont want the fontSizeComboBox selecteditem property because for this, the user can write their own value

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"{viewModel.SelectedNote.Id}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            

            //To save the file, we need a file stream
            // first params is the location of the file we are using
            //second params is the way that we are accessing this file
            using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
            {
                var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                contents.Save(fileStream, DataFormats.Rtf); //first params requires a stream that the content will be saving to, 2nd params is the data format being saved.
            };

            viewModel.SelectedNote.FileLocation = await UpdateFile(rtfFile, fileName);
            await DatabaseHelper.Update(viewModel.SelectedNote);
        }

        private async Task<string> UpdateFile(string rtfFilePath, string fileName)
        {// the rtfFilePath is going to help me access the actual file, but the file name is
         // going to help me set a name for the blob storage that I am going to be uploading.

            //First step: Create connection with the Azure blob storage service so that we can upload and store files online in the cloud
            string connectionString = ""; //get the API connection string from your azure blob storage account.
            string containerName = "notes"; //this name must be the same as the name in the azure blob storage container

            var container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExistsAsync(); //create the container with the containerName if it already not exists. If it does exists, nothing is going to happen

            var blob = container.GetBlobClient(fileName); //pass in the name of the blob
            await blob.UploadAsync(rtfFilePath);

            return $"https://evernotestoragelpa.blob.core.windows.net/notes/{fileName}"; //get the url of the container from your azure account and pass in the blob name which is our filename in this case
        }
    }
}
