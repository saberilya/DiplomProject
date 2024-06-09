using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Salon.AdminCategory.AdminFrames
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private DispatcherTimer timer;
        public OrdersPage()
        {
            InitializeComponent();
            this.Loaded += OrdersPage_Loaded;


            

        }
        private void OrdersPage_Loaded(object sender, RoutedEventArgs e)
        {
            var listoforders = DealershipEntities1.GetContext().Order.ToList();
            ListOfOrders.ItemsSource = listoforders;

            
        }

        private void BtndDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderfordelete = ListOfOrders.SelectedItems.Cast<Order>().ToList();
            if (MessageBox.Show($"Вы уверены что хотите удалить след {orderfordelete.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    
                    DealershipEntities1.GetContext().Order.RemoveRange(orderfordelete);
                    DealershipEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListOfOrders.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                ListOfOrders.ItemsSource = DealershipEntities1.GetContext().Order.Where(item => item.ClientName == TboxSearch.Text || item.ClientName.Contains(TboxSearch.Text) || item.ClientSurname == TboxSearch.Text || item.ClientSurname.Contains(TboxSearch.Text)).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Не работает");

            }
        }
        private void ProductTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && sender is TextBlock textBlock && textBlock.DataContext is Order selectedProduct)
            {
                EditWindows.OrderEdit credit = new EditWindows.OrderEdit(selectedProduct);
                credit.ShowDialog();
            }
        }
        private void UpdateOrdersList()
        {
            try
            {
                // Получение обновленных данных о заказах и обновление списка
                var listoforders = DealershipEntities1.GetContext().Order.ToList();
                ListOfOrders.ItemsSource = listoforders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении списка заказов: " + ex.Message);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateOrdersList();
        }
    }



}
