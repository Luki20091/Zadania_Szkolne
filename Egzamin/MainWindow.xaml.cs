using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Egzamin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtNumer.PreviewTextInput += TxtNumer_PreviewTextInput;
            DataObject.AddPastingHandler(txtNumer, TxtNumer_Pasting);
            {
                InitializeComponent();
                string number = "000";
                LoadImagesForNumber(number);
            }
        }

        private void TxtNumer_LostFocus(object sender, RoutedEventArgs e)
        {
            string number = txtNumer.Text.Trim();
            LoadImagesForNumber(number);
        }

        private void LoadImagesForNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                imgZdjecie.Source = null;
                imgOdcisk.Source = null;
                return;
            }

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folder = Path.Combine(baseDir, "materialy");
            string photoPath = Path.Combine(folder, $"{number}-zdjecie.jpg");
            string fingerprintPath = Path.Combine(folder, $"{number}-odcisk.jpg");

            imgZdjecie.Source = LoadImage(photoPath);
            imgOdcisk.Source = LoadImage(fingerprintPath);
        }

        private BitmapImage LoadImage(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Nie ma takiego zdjęcia w bazie", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtImie.Text.Trim();
            string lastName = txtNazwisko.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Wprowadź dane", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string eyeColor;
            if (rbNiebieskie.IsChecked == true)
                eyeColor = "niebieskie";
            else if (rbZielone.IsChecked == true)
                eyeColor = "zielone";
            else
                eyeColor = "piwne";

            string message = $"{firstName} {lastName} kolor oczu {eyeColor}";
            MessageBox.Show(message, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtNumer_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void TxtNumer_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                if (!IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
