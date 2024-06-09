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

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для SendInfoWindow.xaml
    /// </summary>
    public partial class SendInfoWindow : Window
    {
        private const int MaxPhoneDigits = 11;
        public SendInfoWindow()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string mashina = auto.Text;
            string phone = ClientPhone.Text;
           
            if (auto.Text == "" && ClientPhone.Text == "")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show($"При поступлении {mashina} , мы свяжемся по номеру : {phone}");
                this.Close();
            }
        }
        private void ClientPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientPhone_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (ClientPhone.Text.Length >= MaxPhoneDigits && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }
        private void ClientName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void ClientSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
