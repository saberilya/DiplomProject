using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();
            DealershipEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
            var context = DealershipEntities1.GetContext();

            // Запрос LINQ для загрузки всех марок автомобилей без дубликатов
            var distinctCarBrands = context.Cars.Select(car => car.Brand).Distinct().ToList();

            // Присваиваем список марок автомобилей без дубликатов к свойству ItemsSource ComboBox (ComboAllAutos)
            ComboAllAutos.ItemsSource = distinctCarBrands;



            distinctCarBrands.Insert(0, "Не фильтровать");

            var currentCars = DealershipEntities1.GetContext().Cars.ToList();
            ListOfModels.ItemsSource = currentCars;
            
        }
        
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            OtherWindows.StartupWindowLogin startupWindowLogin = new OtherWindows.StartupWindowLogin();
            startupWindowLogin.Show();
            this.Close();
        }
       
        private void ProductTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (e.ClickCount == 2 && sender is TextBlock textBlock && textBlock.DataContext is Cars selectedProduct)
            {
               
                ProductCardWindow cardWindow = new ProductCardWindow(selectedProduct);
                cardWindow.ShowDialog();
            }
        }
        
        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {               
                ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.Where
                    (item => item.Brand == TboxSearch.Text
                || item.Brand.Contains(TboxSearch.Text)
                || item.Model == TboxSearch.Text
                || item.Model.Contains(TboxSearch.Text)).ToList();

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Не работает");

            }
        }
        

        private void BtnGoToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ListOfModels.SelectedItem is Cars selectedcar)
            {
                OtherWindows.ClientOrderWindow clientOrderWindow = new OtherWindows.ClientOrderWindow(selectedcar);
                clientOrderWindow.Show();
            }
            else
            {
                OtherWindows.ClientOrderWindow clientOrderWindow = new OtherWindows.ClientOrderWindow(null);
                clientOrderWindow.Show();

            }

        }

        private void BtnSolve_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SummaBox.Text) && string.IsNullOrEmpty(SrokBox.Text) && string.IsNullOrEmpty(StavkaBox.Text))
            {
                System.Windows.MessageBox.Show("Заполните поля 'Сумма', 'Срок', 'Ставка'");

            }

            else
            {
                ulong summa = Convert.ToUInt64(SummaBox.Text);
                ushort srok = Convert.ToUInt16(SrokBox.Text);
                double stavka = Convert.ToDouble(StavkaBox.Text);
                double plata = (summa + summa * stavka * srok / 100) / (srok * 12);
                PlataBox.Text = "" + Math.Round(plata, 2);
            }
        }

      
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DealershipEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
            }
        }

        private void ComboAllAutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            string selectedBrand = ComboAllAutos.SelectedItem.ToString();
            if (ComboAllAutos.SelectedItem is "Не фильтровать")
            {
                var context = DealershipEntities1.GetContext();

                // Запрос LINQ для получения всех марок автомобилей, соответствующих выбранной марке
                var carnotfiltered = context.Cars.ToList();

                // Присваиваем список марок автомобилей, соответствующих выбранной марке, к свойству ItemsSource ComboBox (comboBoxCars)
                ListOfModels.ItemsSource = carnotfiltered;
            }
            else
            {
                var context = DealershipEntities1.GetContext();

                // Запрос LINQ для получения всех марок автомобилей, соответствующих выбранной марке
                var carsByBrand = context.Cars.Where(car => car.Brand.Equals(selectedBrand)).ToList();

                // Присваиваем список марок автомобилей, соответствующих выбранной марке, к свойству ItemsSource ComboBox (comboBoxCars)
                ListOfModels.ItemsSource = carsByBrand;
            }
           
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendInfoWindow sendInfoWindow = new SendInfoWindow();
            sendInfoWindow.Show();

        }

        private void TboxSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[А-Яа-я]");

            // If the input text matches the regex, set Handled to true to cancel the input
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            DealershipEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListOfModels.ItemsSource = DealershipEntities1.GetContext().Cars.ToList();
        }
    }
}
