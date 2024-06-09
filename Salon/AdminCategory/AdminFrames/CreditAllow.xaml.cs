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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Salon.AdminCategory.AdminFrames
{
    /// <summary>
    /// Логика взаимодействия для CreditAllow.xaml
    /// </summary>
    public partial class CreditAllow : Page
    {
        public CreditAllow()
        {
            InitializeComponent();
            var context = DealershipEntities1.GetContext();
            var sortedorders = context.Order.Where(car => car.CreditorID != 1).ToList();
            ListOfOrders.ItemsSource = sortedorders;

        }
        private void BtndDelete_Click(object sender, RoutedEventArgs e)
        {
            var orderfordelete = ListOfOrders.SelectedItems.Cast<Creditor>().ToList();
            if (MessageBox.Show($"Вы уверены что хотите удалить след {orderfordelete.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DealershipEntities1.GetContext().Creditor.RemoveRange(orderfordelete);
                    DealershipEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListOfOrders.ItemsSource = DealershipEntities1.GetContext().Order.ToList();
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
                EditWindows.CreditEdit credit = new EditWindows.CreditEdit(selectedProduct);
                credit.ShowDialog();
            }
        }
    }
}
