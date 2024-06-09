using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для PurchaseAgreementWindow.xaml
    /// </summary>
    public partial class PurchaseAgreementWindow : Window
    {

        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }
        public string ClientSerial { get; set; }
        public string ClientPassport { get; set; }
        public Cars SelectedCar { get; set; }

        public PurchaseAgreementWindow(Cars selectedCar, string clientName, string clientSurname, string clientPhone, string clientSerial, string clientPassport)
        {
            InitializeComponent();

            // Сохраняем выбранный автомобиль
            SelectedCar = selectedCar;
            ClientName = clientName;
            ClientSurname = clientSurname;
            ClientPhone = clientPhone;
            ClientSerial = clientSerial;
            ClientPassport = clientPassport;
            // Используйте SelectedCar для отображения характеристик автомобиля
            // Например:
            
            //CarModelTextBlock.Text = SelectedCar.Model;
            //CarYearTextBlock.Text = SelectedCar.Year.ToString();
            //// И так далее для остальных характеристик автомобиля
        }

        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnPech_Click(object sender, RoutedEventArgs e)
        {

            // Открытие PDF-файла
            PurchaseSellDoc purchaseSellDoc = new PurchaseSellDoc(SelectedCar, ClientName,ClientSurname, ClientPhone, ClientSerial, ClientPassport);
            purchaseSellDoc.Show();                               
                                                                  
        }                                                       
    }                                                             
}                                                                
