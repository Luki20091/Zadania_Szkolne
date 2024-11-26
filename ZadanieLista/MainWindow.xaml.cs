using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZadanieLista
{
    public partial class MainWindow : Window
    {
        private List<Game> games = new List<Game>();
        private bool titleCleared = false;
        private bool yearCleared = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadGames();
            RefreshGameList();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Name == "TitleTextBox" && !titleCleared)
                {
                    textBox.Clear();
                    titleCleared = true;
                }
                if (textBox.Name == "YearTextBox" && !yearCleared)
                {
                    textBox.Clear();
                    yearCleared = true;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || TitleTextBox.Text == "Tytuł")
            {
                MessageBox.Show("Proszę wprowadzić tytuł.");
                return;
            }

            if (!int.TryParse(YearTextBox.Text, out int year))
            {
                MessageBox.Show("Proszę wprowadzić poprawny rok.");
                return;
            }

            var selectedComboBoxItem = TypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedComboBoxItem == null)
            {
                MessageBox.Show("Proszę wybrać typ gry.");
                return;
            }

            var game = new Game
            {
                Title = TitleTextBox.Text,
                Year = year,
                Type = (GameType)Enum.Parse(typeof(GameType), selectedComboBoxItem.Content.ToString())
            };
            games.Add(game);
            SaveGames();
            RefreshGameList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || TitleTextBox.Text == "Tytuł")
            {
                MessageBox.Show("Proszę wprowadzić tytuł.");
                return;
            }

            if (!int.TryParse(YearTextBox.Text, out int year))
            {
                MessageBox.Show("Proszę wprowadzić poprawny rok.");
                return;
            }

            if (GamesListView.SelectedItem is Game selectedGame)
            {
                var game = games.First(g => g.Title == selectedGame.Title);

                var selectedComboBoxItem = TypeComboBox.SelectedItem as ComboBoxItem;
                if (selectedComboBoxItem == null)
                {
                    MessageBox.Show("Proszę wybrać typ gry.");
                    return;
                }

                game.Title = TitleTextBox.Text;
                game.Year = year;
                game.Type = (GameType)Enum.Parse(typeof(GameType), selectedComboBoxItem.Content.ToString());
                SaveGames();
                RefreshGameList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListView.SelectedItem is Game selectedGame)
            {
                var game = games.First(g => g.Title == selectedGame.Title);
                games.Remove(game);
                SaveGames();
                RefreshGameList();
            }
        }

        private void LoadGames()
        {
            if (File.Exists("list.txt"))
            {
                var lines = File.ReadAllLines("list.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    var game = new Game
                    {
                        Title = parts[0],
                        Year = int.Parse(parts[1]),
                        Type = (GameType)Enum.Parse(typeof(GameType), parts[2])
                    };
                    games.Add(game);
                }
            }
        }

        private void SaveGames()
        {
            var lines = games.Select(g => $"{g.Title}|{g.Year}|{g.Type}");
            File.WriteAllLines("list.txt", lines);
        }

        private void RefreshGameList()
        {
            GamesListView.ItemsSource = null;

            if (games.Count == 0)
            {
                NoRecordsTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                NoRecordsTextBlock.Visibility = Visibility.Collapsed;
                GamesListView.ItemsSource = games;
            }
        }

        private void GamesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListView.SelectedItem is Game selectedGame)
            {
                TitleTextBox.Text = selectedGame.Title;
                YearTextBox.Text = selectedGame.Year.ToString();
                TypeComboBox.SelectedItem = TypeComboBox.Items.OfType<ComboBoxItem>().First(c => c.Content.ToString() == selectedGame.Type.ToString());

                foreach (var item in GamesListView.Items)
                {
                    var container = GamesListView.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                    container.Background = Brushes.White;
                }

                var selectedContainer = GamesListView.ItemContainerGenerator.ContainerFromItem(GamesListView.SelectedItem) as ListViewItem;
                selectedContainer.Background = Brushes.LightGreen;
            }
        }
    }
}
