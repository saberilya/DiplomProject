using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace Salon.AdminCategory.AdminFrames.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для CreditEdit.xaml
    /// </summary>
    public partial class CreditEdit : Window
    {
        private Order _order;
        public CreditEdit(Order order)
        {
            _order = order;
            InitializeComponent();
            DataContext = _order;
            BrandText.Content = _order.Cars.Brand;
            ModelText.Content = _order.Cars.Model;
            PriceText.Content = _order.Cars.Price;
           
            ClientName.Content = _order.ClientName;
            ClientSurname.Content = _order.ClientSurname;
            ClientPhone.Content = _order.ClientPhone;
            ClientSeria.Content = _order.ClientSerial;
            ClientPasport.Content = _order.ClientPasswordNubmer;
            ClientDest.Content = _order.Creditor.ClientDestination;
            ClientJob.Content = _order.Creditor.ClientJob;
            ClientSalary.Content = _order.Creditor.ClientSalary+"рублей";
            ClientStatus.Text = _order.Creditor.CreditStatus;

        }
        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Creditor creditor = new Creditor()
                {
                    ID = _order.Creditor.ID,
                    CreditStatus = ClientStatus.Text,
                    ClientDestination = _order.Creditor.ClientDestination,
                    ClientSalary = _order.Creditor.ClientSalary,
                    ClientJob = _order.Creditor.ClientJob,
                };
                DealershipEntities1.GetContext().Creditor.AddOrUpdate(creditor);
                DealershipEntities1.GetContext().SaveChanges();
                MessageBox.Show("Сохранено");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
