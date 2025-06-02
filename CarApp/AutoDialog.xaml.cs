using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarApp
{
    public partial class AutoDialog : Window
    {
        public Auto Auto { get; private set; }

        public AutoDialog()
        {
            InitializeComponent();
            cmbRodzaj.ItemsSource = Enum.GetValues(typeof(RodzajNapedu));
            cmbRodzaj.SelectedIndex = 0;
        }

        public AutoDialog(Auto auto) : this()
        {
            Auto = auto;
            txtMarka.Text = auto.Marka;
            txtModel.Text = auto.Model;
            cmbRodzaj.SelectedItem = auto.RodzajNapedu;
            txtRok.Text = auto.Rok.ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMarka.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                cmbRodzaj.SelectedItem == null ||
                !int.TryParse(txtRok.Text, out int rok))
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola poprawnie.", "Błąd",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Auto == null)
                Auto = new Auto();

            Auto.Marka = txtMarka.Text;
            Auto.Model = txtModel.Text;
            Auto.RodzajNapedu = (RodzajNapedu)cmbRodzaj.SelectedItem;
            Auto.Rok = rok;

            this.DialogResult = true;
        }
    }
}
