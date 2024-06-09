using Salon.OtherWindows;
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
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Window
    {
        private Order _order;
        public OrderEdit(Order order)
        {

           
            
            
            _order = order;
            InitializeComponent();
            Onload();
            DataContext = _order;

            var model = DealershipEntities1.GetContext().OrderStatus.ToList();
            model.Insert(0, new OrderStatus() { StatusName = "Не выбрано" });
            ClientStatusOrder.ItemsSource = model;

            
           
        }
        private void Onload()
        {


            BrandText.Content = _order.Cars.Brand;
            ModelText.Content = _order.Cars.Model;
            PriceText.Content = _order.Cars.Price;

            ClientName.Content = _order.ClientName;
            ClientSurname.Content = _order.ClientSurname;
            ClientPhone.Content = _order.ClientPhone;
            ClientSeria.Content = _order.ClientSerial;
            ClientPasport.Content = _order.ClientPasswordNubmer;
            
            Client10.Content = _order.Client10PercentEnter+ "рублей";
            ClientStatus.Content = _order.Creditor.CreditStatus;
            if (ClientStatusOrder == null)
            {
                ClientStatusOrder.SelectedIndex = 0;
            }
            else
            {
                ClientStatusOrder.SelectedIndex = _order.OrderStatus.ID;

            }
            

        }
        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = new Order()
                {
                    CarID = _order.CarID,
                   ID = _order.ID,
                   ClientName = _order.ClientName,
                   ClientSurname = _order.ClientSurname, ClientPhone = _order.ClientPhone,
                   ClientPasswordNubmer = _order.ClientPasswordNubmer,
                   Client10PercentEnter = _order.Client10PercentEnter,
                   ClientSerial = _order.ClientSerial,
                   CreditorID = _order.CreditorID,
                   Status = ClientStatusOrder.SelectedIndex
                };
                DealershipEntities1.GetContext().Order.AddOrUpdate(order);
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
