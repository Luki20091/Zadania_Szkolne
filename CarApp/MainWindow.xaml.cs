using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace CarApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Auto> ListaAut { get; set; }

        private string _currentSortColumn = null;
        private ListSortDirection _currentSortDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();

            ListaAut = new ObservableCollection<Auto>();
            DataContext = this;

            PobierzDanePrzykladowe();

            listViewAut.AddHandler(
                GridViewColumnHeader.ClickEvent,
                new RoutedEventHandler(GridViewColumnHeader_Click));
        }

        private void PobierzDanePrzykladowe()
        {
            ListaAut.Clear();
            ListaAut.Add(new Auto { Marka = "Fiat", Model = "Panda", RodzajNapedu = RodzajNapedu.benzynowy, Rok = 2010 });
            ListaAut.Add(new Auto { Marka = "Opel", Model = "Astra", RodzajNapedu = RodzajNapedu.diesel, Rok = 2015 });
            ListaAut.Add(new Auto { Marka = "Fiat", Model = "Linea", RodzajNapedu = RodzajNapedu.benzynowy, Rok = 2008 });
            ListaAut.Add(new Auto { Marka = "Opel", Model = "Mokka", RodzajNapedu = RodzajNapedu.hybrydowy, Rok = 2023 });
            ListaAut.Add(new Auto { Marka = "BMW", Model = "X3", RodzajNapedu = RodzajNapedu.benzynowy, Rok = 2018 });
            ListaAut.Add(new Auto { Marka = "Nissan", Model = "Leaf", RodzajNapedu = RodzajNapedu.elektryczny, Rok = 2025 });
        }


        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AutoDialog
            {
                Owner = this
            };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ListaAut.Add(dlg.Auto);
            }
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listViewAut.SelectedItem is Auto wybraneAuto)
            {
                var klon = new Auto
                {
                    Marka = wybraneAuto.Marka,
                    Model = wybraneAuto.Model,
                    RodzajNapedu = wybraneAuto.RodzajNapedu,
                    Rok = wybraneAuto.Rok
                };

                var dlg = new AutoDialog(klon)
                {
                    Owner = this
                };
                bool? result = dlg.ShowDialog();
                if (result == true)
                {
                    wybraneAuto.Marka = dlg.Auto.Marka;
                    wybraneAuto.Model = dlg.Auto.Model;
                    wybraneAuto.RodzajNapedu = dlg.Auto.RodzajNapedu;
                    wybraneAuto.Rok = dlg.Auto.Rok;
                }
            }
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listViewAut.SelectedItem is Auto wybraneAuto)
            {
                ListaAut.Remove(wybraneAuto);
            }
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Pliki XML (*.xml)|*.xml",
                Title = "Zapisz listę aut"
            };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(ObservableCollection<Auto>));
                    using (var fs = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        serializer.Serialize(fs, ListaAut);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd zapisu do pliku:\n{ex.Message}",
                                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Pliki XML (*.xml)|*.xml",
                Title = "Wczytaj listę aut"
            };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(ObservableCollection<Auto>));
                    using (var fs = new FileStream(dlg.FileName, FileMode.Open))
                    {
                        var dane = (ObservableCollection<Auto>)serializer.Deserialize(fs);
                        ListaAut.Clear();
                        foreach (var a in dane)
                            ListaAut.Add(a);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd wczytywania pliku:\n{ex.Message}",
                                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader header
                && header.Tag is string sortBy
                && !string.IsNullOrEmpty(sortBy))
            {
                SortujPoKolumnie(sortBy);
            }
        }

        private void SortujPoKolumnie(string nazwaWlasciwosci)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(listViewAut.ItemsSource);

            if (_currentSortColumn == nazwaWlasciwosci)
            {
                _currentSortDirection =
                    _currentSortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                _currentSortColumn = nazwaWlasciwosci;
                _currentSortDirection = ListSortDirection.Ascending;
            }

            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(
                new SortDescription(nazwaWlasciwosci, _currentSortDirection));
            view.Refresh();
        }

    }
}
