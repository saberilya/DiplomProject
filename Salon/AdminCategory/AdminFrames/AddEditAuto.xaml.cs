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
    /// Логика взаимодействия для AddEditAuto.xaml
    /// </summary>
    public partial class AddEditAuto : Page
    {
        public AddEditAuto()
        {
            InitializeComponent();
            var currentCars = DealershipEntities1.GetContext().Cars.ToList();
            ListOfModels.ItemsSource = currentCars;
    
        }
        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.Where(item => item.Brand == TboxSearch.Text || item.Brand.Contains(TboxSearch.Text)).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Не работает");

            }
        }
        private void ProductTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && sender is TextBlock textBlock && textBlock.DataContext is Cars selectedProduct)
            {
                EditWindows.EditAutoWindow editAutoWindow = new EditWindows.EditAutoWindow(selectedProduct);
                editAutoWindow.ShowDialog();
            }
        }

        private void BtnAddAuto_Click(object sender, RoutedEventArgs e)
        {
            EditWindows.EditAutoWindow editAutoWindow = new EditWindows.EditAutoWindow(null);
            editAutoWindow.ShowDialog();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                DealershipEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
            }
        }

        private void BtndDelete_Click(object sender, RoutedEventArgs e)
        {
            var modelfordelete = ListOfModels.SelectedItems.Cast<Cars>().ToList();
            if (MessageBox.Show($"Вы уверены что хотите удалить след {modelfordelete.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DealershipEntities1.GetContext().Cars.RemoveRange(modelfordelete);
                    DealershipEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
    
}
