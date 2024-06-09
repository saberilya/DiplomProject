using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Salon.OtherWindows
{
    /// <summary>
    /// Логика взаимодействия для ClientOrderWindow.xaml
    /// </summary>
    public partial class ClientOrderWindow : Window
    {
        private const int MaxPhoneDigits = 11;
        private const int MaxSeriaDigits = 4;
        private const int MaxNumberDigits = 6;
        private const int MaxCardNubmer = 16;
        private const int MaxCVC = 3;
        private const int MaxDAte = 6;
        public DealershipEntities1 dbContext; // Контекст базы данных

        private Cars _icarmodel;
        private Cars _selectedcar;

        public ClientOrderWindow(Cars carmodel)
        {
            _icarmodel = carmodel;
            _selectedcar = carmodel;


            InitializeComponent();
            dbContext = new DealershipEntities1();

            if (_icarmodel == null)
            {
                ComboAutoModel.SelectedIndex = 0;
            }
            else
            {
                ComboAutoModel.SelectedIndex = _icarmodel.ID;
            }
            if (_selectedcar == null)
            {
                ComboAutoModel.SelectedIndex = 0;
            }
            else
            {
                ComboAutoModel.SelectedIndex = _selectedcar.ID;
            }
            var model = DealershipEntities1.GetContext().Cars.ToList();
            model.Insert(0, new Cars() { Brand = "Не выбрано", Model = "Не выбрано" });
            ComboAutoModel.ItemsSource = model;
          



        }

        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboAutoModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboAutoModel.SelectedItem is Cars carmodel)
            {
                ProductNameLabel.Content = carmodel.Brand;
                ProductModelNameLabel.Content = carmodel.Model;
                ProductYearLabel.Content = carmodel.Year + " год выпуска";
                ProductHPLabel.Content = carmodel.HP + " Л.С";
                ProductPriceLabel.Content = carmodel.Price + " рублей";
                ProductKilometreLabel.Content = carmodel.KM + " км";
                ProductBodyLabel.Content = carmodel.BodyType;
                ProductCompLabel.Content = carmodel.Complectation;
                ProductPTSOwnersLabel.Content = carmodel.PTS;
                ProductVINLabel.Content = carmodel.VIN;
                ProductCountryLabel.Content = carmodel.Country;
                ProductColorLabel.Content = carmodel.Color;
                string carprice = carmodel.Price.ToString();
                int total10pec = carmodel.Price / 10;
                PriceAutoForCalc.Text = carmodel.Price.ToString() + "₽";
                CalculatorFor10pec.Text = $"Цена авто = {carprice}₽ / 10 = {total10pec}₽ - сумма взноса ";
                
            }
        }

        private void OrderClick_Click(object sender, RoutedEventArgs e)
        {
            //int carprice = _icarmodel.Price / 10;
            string name = ClientName.Text;
            string surname = ClientSurname.Text;
            string phone = ClientPhone.Text;
            string seria = ClientSerial.Text;
            string passport = ClientPassport.Text;
            DataContext = DealershipEntities1.GetContext().Cars.ToList();
            bool carStatus = Classes.Context._context.Cars.Any(p => p.StatusID == 3 || p.StatusID == 2);


            try
            {
                if (DocYEs.IsChecked == true)
                {


                    if (PersonalCheck.IsChecked == true)
                    {
                        bool IsValid = true;
                        if (name.Length < 4)
                        {
                            ClientName.ToolTip = "Нужно ввести минимум 4 символа";
                            ClientName.Background = Brushes.LightPink;
                            IsValid = false;

                        }

                        if (surname.Length < 4)
                        {
                            ClientSurname.ToolTip = "Нужно ввести мининум 4 символа";
                            ClientSurname.Background = Brushes.LightPink;
                            IsValid = false;

                        }

                        if (phone.Length < 11)
                        {
                            ClientPhone.ToolTip = "Номер введен некорректно";
                            ClientPhone.Background = Brushes.LightPink;
                            IsValid = false;

                        }

                        if (seria.Length < 4)
                        {
                            ClientSerial.ToolTip = "Нужно ввести 4 символа";
                            ClientSerial.Background = Brushes.LightPink;
                            IsValid = false;

                        }

                        if (passport.Length < 6)
                        {
                            ClientPassport.ToolTip = "Нужно ввести минимум 6 символов";
                            ClientPassport.Background = Brushes.LightPink;
                            IsValid = false;

                        }

                        if (ComboAutoModel.SelectedIndex != -1)
                        {
                            int selectedCarId = (int)ComboAutoModel.SelectedIndex;
                            var selectedCar = Classes.Context._context.Cars.FirstOrDefault(c => c.ID == selectedCarId);

                            if (selectedCar != null && (selectedCar.StatusID == 2 || selectedCar.StatusID == 3))
                            {
                                System.Windows.MessageBox.Show("Данный автомобиль уже занят");
                                IsValid = false;
                            }

                        }

                        if (IsValid)
                        {
                            Order order = null;

                            if (bordercredit.Visibility == Visibility.Visible)
                            {
                                int selectedCarId = (int)ComboAutoModel.SelectedIndex;
                                int selcar = ComboAutoModel.SelectedIndex;
                                
                                Creditor creditor = new Creditor()
                                {
                                    ClientSalary = ClientSalaryText.Text,
                                    ClientJob = ClientJobText.Text,
                                    CreditStatus = "В обработке",
                                    ClientDestination = ClientDestText.Text,

                                };
                                DealershipEntities1.GetContext().Creditor.Add(creditor);
                                DealershipEntities1.GetContext().SaveChanges();
                                order = new Order()
                                {

                                    ClientName = ClientName.Text,
                                    ClientSurname = ClientSurname.Text,
                                    ClientPhone = ClientPhone.Text,
                                    ClientSerial = ClientSerial.Text,
                                    ClientPasswordNubmer = ClientPassport.Text,
                                    CarID = selectedCarId,
                                    Status = 1,

                                    CreditorID = creditor.ID,

                                };
                                if (ComboAutoModel.SelectedIndex != -1)
                                {
                                  
                                    var car = dbContext.Cars.FirstOrDefault(c => c.ID == selectedCarId);
                                    if (car != null)
                                    {

                                        var newStatus = dbContext.Status.FirstOrDefault(s => s.StatusName == "Продана");
                                        if (newStatus != null)
                                        {
                                            car.StatusID = newStatus.ID;
                                            dbContext.SaveChanges();

                                        }
                                        else { System.Windows.MessageBox.Show("Новый статус не найден"); }


                                    }
                                    else { System.Windows.MessageBox.Show("Не удалось найти выбранный авто в БД"); }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Выбериьте авто из комбобокс");
                                }
                                DealershipEntities1.GetContext().Order.Add(order);
                                DealershipEntities1.GetContext().SaveChanges();
                                System.Windows.MessageBox.Show("Отлично, для долнейшей инструкции с вами свяжется менеджер в течении 2-3 дней!");
                                AfterBuyWindow afterBuyWindow = new AfterBuyWindow(order);
                                afterBuyWindow.Show();


                            }
                            else
                            {
                                bool IsBank = true;
                                if (BackCard.Text.Length < 16)
                                {
                                    BackCard.ToolTip = "Введите номер карты";
                                    BackCard.Background = Brushes.LightPink;
                                    IsBank = false;
                                }
                                if (BankCVC.Text.Length < 3)
                                {
                                    BankCVC.ToolTip = "Введите защитный код";
                                    BankCVC.Background = Brushes.LightPink;
                                    IsBank = false;
                                }
                                if (BankOwner.Text.Length < 10)
                                {
                                    BankOwner.ToolTip = "Введите владельца карты в формате Имя Фамилия";
                                    BankOwner.Background = Brushes.LightPink;
                                    IsBank = false;
                                }
                                if (BankDate.Text.Length < 6)
                                {
                                    BankDate.ToolTip = "Введите владельца карты в формате мясяц год (00 0000)";
                                    BankDate.Background = Brushes.LightPink;
                                    IsBank = false;
                                }
                                if (IsBank)
                                {
                                    int enteredCash;
                                    if (int.TryParse(EnterCash.Text, out enteredCash))
                                    {
                                        int selectedCarId = (int)ComboAutoModel.SelectedIndex;
                                        var selectedCar = Classes.Context._context.Cars.FirstOrDefault(c => c.ID == selectedCarId);
                                        int carPrice = selectedCar.Price;
                                        int requiredCash = carPrice / 10;

                                        if (enteredCash < requiredCash)
                                        {
                                            EnterCash.ToolTip = $"Нужно внести 10% от стоимости авто ({requiredCash} рублей)";
                                            EnterCash.Background = Brushes.LightPink;
                                        }
                                        else
                                        {
                                            if (enteredCash == carPrice)
                                            {
                                                if (System.Windows.MessageBox.Show($"Вы уверены что хотите внести {enteredCash} рублей?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                                {
                                                    order = new Order()
                                                    {
                                                        ClientName = ClientName.Text,
                                                        ClientSurname = ClientSurname.Text,
                                                        ClientPhone = ClientPhone.Text,
                                                        ClientSerial = ClientSerial.Text,
                                                        ClientPasswordNubmer = ClientPassport.Text,
                                                        CarID = selectedCarId,
                                                        Status = 2,
                                                        Client10PercentEnter = EnterCash.Text,
                                                        CreditorID = 1,
                                                    };
                                                    if (ComboAutoModel.SelectedIndex != -1)
                                                    {
                                                        int selcar = ComboAutoModel.SelectedIndex;
                                                        var car = dbContext.Cars.FirstOrDefault(c => c.ID == selectedCarId);
                                                        if (car != null)
                                                        {

                                                            var newStatus = dbContext.Status.FirstOrDefault(s => s.StatusName == "Продана");
                                                            if (newStatus != null)
                                                            {
                                                                car.StatusID = newStatus.ID;
                                                                dbContext.SaveChanges();

                                                            }
                                                            else { System.Windows.MessageBox.Show("Новый статус не найден"); }


                                                        }
                                                        else { System.Windows.MessageBox.Show("Не удалось найти выбранный авто в БД"); }
                                                    }
                                                    else
                                                    {
                                                        System.Windows.MessageBox.Show("Выбериьте авто из комбобокс");
                                                    }
                                                    DealershipEntities1.GetContext().Order.Add(order);
                                                    DealershipEntities1.GetContext().SaveChanges();
                                                    System.Windows.MessageBox.Show("ПОЗДРАВЛЯЕМ с покупкой, с дальнейшими интсрукции с вами свяжется менеджер в течении 2-3 дней");
                                                }
                                            }
                                            else
                                            {
                                                order = new Order()
                                                {
                                                    ClientName = ClientName.Text,
                                                    ClientSurname = ClientSurname.Text,
                                                    ClientPhone = ClientPhone.Text,
                                                    ClientSerial = ClientSerial.Text,
                                                    ClientPasswordNubmer = ClientPassport.Text,
                                                    CarID = selectedCarId,
                                                    Status = 1,
                                                    Client10PercentEnter = EnterCash.Text,
                                                    CreditorID = 1,
                                                };
                                                if (ComboAutoModel.SelectedIndex != -1)
                                                {
                                                    int selCarId = ComboAutoModel.SelectedIndex;
                                                    var car = dbContext.Cars.FirstOrDefault(c => c.ID == selectedCarId);
                                                    if (car != null)
                                                    {

                                                        var newStatus = dbContext.Status.FirstOrDefault(s => s.StatusName == "На удержании");
                                                        if (newStatus != null)
                                                        {
                                                            car.StatusID = newStatus.ID;
                                                            dbContext.SaveChanges();

                                                        }
                                                        else { System.Windows.MessageBox.Show("Новый статус не найден"); }


                                                    }
                                                    else { System.Windows.MessageBox.Show("Не удалось найти выбранный авто в БД"); }
                                                }
                                                else
                                                {
                                                    System.Windows.MessageBox.Show("Выбериьте авто из комбобокс");
                                                }
                                                DealershipEntities1.GetContext().Order.Add(order);
                                                DealershipEntities1.GetContext().SaveChanges();
                                                System.Windows.MessageBox.Show("Ваша заявка в обработке, с дальнейшими интсрукции с вами свяжется менеджер в течении 2-3 дней");
                                            }
                                            // Pass data to AfterBuyWindow
                                            AfterBuyWindow afterBuyWindow = new AfterBuyWindow(order);
                                            afterBuyWindow.Show();
                                        }
                                    }

                                    else
                                    {
                                        EnterCash.ToolTip = "Введите корректную сумму";
                                        EnterCash.Background = Brushes.LightPink;
                                    }
                                }

                            }


                        }

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Для покупки автомобиля вы должны согласиться на обработку персональных данных");
                    }
                }
                else
                {

                    System.Windows.MessageBox.Show("Для покупки автомобиля вы должны согласиться и ознакомиться с условиями договора");

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Что пошло не так! {ex} ");

            }




        }

        private void ClientPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientPhone_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ClientPhone.Text.Length >= MaxPhoneDigits && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void ClientSerial_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ClientSerial.Text.Length >= MaxSeriaDigits && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void ClientSerial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientPassport_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (ClientPassport.Text.Length >= MaxNumberDigits && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void ClientPassport_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
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

        private void PersonalCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PersonalCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bordercredit.Visibility = Visibility.Visible;
            bordercash.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            bordercredit.Visibility = Visibility.Hidden;
            bordercash.Visibility = Visibility.Visible;
        }

        private void EnterCash_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientSalaryText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientJobText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void ClientDestText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }



        private void DocYEs_MouseDownClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (ComboAutoModel.SelectedItem is Cars selectedCar)
                {

                    PurchaseAgreementWindow purchaseAgreementWindow = new PurchaseAgreementWindow(selectedCar,ClientName.Text,
                ClientSurname.Text,
                ClientPhone.Text,
                ClientSerial.Text,
                ClientPassport.Text);
                    purchaseAgreementWindow.Show();
                }
                else
                {
                    MessageBox.Show("Please select a car.");
                }
            }
        }
        private void BackCard_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void BackCard_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (BackCard.Text.Length >= MaxCardNubmer && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void BankDate_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (BankDate.Text.Length >= MaxDAte && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void BankDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void BankOwner_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ  является цифрой, игнорируем его
            }
        }

        private void BankCVC_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (BankCVC.Text.Length >= MaxCVC && e.Key != Key.Back && e.Key != Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void BankCVC_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }
    }
}
