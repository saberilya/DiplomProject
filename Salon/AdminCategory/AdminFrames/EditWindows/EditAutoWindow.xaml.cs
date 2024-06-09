using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Salon.AdminCategory.AdminFrames.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для EditAutoWindow.xaml
    /// </summary>
    public partial class EditAutoWindow : Window
    {

        private Cars _newmodel = new Cars();
        public EditAutoWindow(Cars carmodel)
        {
            InitializeComponent();


            if (carmodel != null)
            {
                _newmodel = carmodel;
            }
            DataContext = _newmodel;
                var status = DealershipEntities1.GetContext().Status.ToList();
                status.Insert(0, new Status() { StatusName = "Не выбрано" });
                StatusBox.ItemsSource = status;
            BrandText.Text = _newmodel.Brand;
            ModelText.Text = _newmodel.Model;
            CountryText.Text = _newmodel.Country;
            YearText.Text = _newmodel.Year.ToString();
            HPText.Text = _newmodel.HP.ToString();
            PriceText.Text = _newmodel.Price.ToString();
            KMText.Text = _newmodel.KM.ToString();
            BodyText.Text = _newmodel.BodyType;
            ComplectText.Text = _newmodel.Complectation;
            PTSText.Text = _newmodel.PTS.ToString();
            VINText.Text = _newmodel.VIN.ToString();
            StatusBox.SelectedIndex = _newmodel.StatusID;
            ColorText.Text = _newmodel.Color;

        }
        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_newmodel.ID == 0)
                {
         
                    
                    Cars newcars = new Cars()
                    {
                        
                        Brand = BrandText.Text,
                        Model = ModelText.Text,
                        Country = CountryText.Text,
                        Year = Convert.ToInt32(YearText.Text),
                        HP = Convert.ToInt32(HPText.Text),
                        Price = Convert.ToInt32(PriceText.Text),
                        KM = Convert.ToInt32(KMText.Text),
                        BodyType = BodyText.Text,
                        Complectation = ComplectText.Text,
                        PTS = Convert.ToInt32(PTSText.Text),
                        VIN = Convert.ToInt32(VINText.Text),
                        StatusID = StatusBox.SelectedIndex,
                        Color = ColorText.Text,
                    };
                    DealershipEntities1.GetContext().Cars.Add(newcars);
                    DealershipEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Автомобиль добавлен");
                }
                else
                {
                    Cars editedcar = new Cars()
                    {
                        ID = _newmodel.ID,
                        Brand = BrandText.Text,
                        Model = ModelText.Text,
                        Country = CountryText.Text,
                        Year = Convert.ToInt32(YearText.Text),
                        HP = Convert.ToInt32(HPText.Text),
                        Price = Convert.ToInt32(PriceText.Text),
                        KM = Convert.ToInt32(KMText.Text),
                        BodyType = BodyText.Text,
                        Complectation = ComplectText.Text,
                        PTS = Convert.ToInt32(PTSText.Text),
                        VIN = Convert.ToInt32(VINText.Text),
                        StatusID = StatusBox.SelectedIndex,
                        Color = ColorText.Text,
                        Picture = _newmodel.Picture,
                    };
                    DealershipEntities1.GetContext().Cars.AddOrUpdate(editedcar);
                    DealershipEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Автомобиль отредактирован");
                }
                

            }
            catch (Exception)
            {

                throw;
            }
            DealershipEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

        }

        private void BrandText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ является цифрой, игнорируем его
            }
        }

        private void ModelText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void CountryText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ является цифрой, игнорируем его
            }
        }

        private void YearText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void HPText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void PriceText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void KMText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void BodyText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ является цифрой, игнорируем его
            }
        }

        private void ComplectText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ является цифрой, игнорируем его
            }
        }

        private void ColorText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ является цифрой, игнорируем его
            }
        }

        private void PTSText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }

        private void VINText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если символ не является цифрой, игнорируем его
            }
        }
    }
}
