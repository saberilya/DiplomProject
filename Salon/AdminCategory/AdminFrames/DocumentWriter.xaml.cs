using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для DocumentWriter.xaml
    /// </summary>
    public partial class DocumentWriter : Page
    {
        public DocumentWriter()
        {
            InitializeComponent();
            this.Loaded += OrdersPage_Loaded;
        }
        private void OrdersPage_Loaded(object sender, RoutedEventArgs e)
        {
            var listoforders = DealershipEntities1.GetContext().Order.ToList();
            ListOfOrders.ItemsSource = listoforders;


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
                OtherWindows.ControlPurchase purchaseSellDoc = new OtherWindows.ControlPurchase(selectedProduct);
                purchaseSellDoc.Show();
               
            }
        }
    }
}
