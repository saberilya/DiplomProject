using System.Windows;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ProductCardWindow.xaml
    /// </summary>
    public partial class ProductCardWindow : Window
    {
        private Cars _carmodel;

        public ProductCardWindow(Cars carmodel)
        {
            _carmodel = carmodel;

            InitializeComponent();
            DataContext = carmodel;

            ProductName.Text = carmodel.Brand;
            ProductModelNameLabel.Text = carmodel.Model;





            ProductYearLabel.Text = carmodel.Year.ToString();
            ProductHPLabel.Text = carmodel.HP + " Л.С";
            ProductPriceLabel.Text = carmodel.Price + "₽";
            ProductKilometreLabel.Text = carmodel.KM + " км";
            ProductBodyLabel.Text = carmodel.BodyType;
            ProductCompLabel.Text = carmodel.Complectation;
            ProductPTSOwnersLabel.Text = carmodel.PTS.ToString();
            ProductVINLabel.Text = carmodel.VIN.ToString();
            ProductCountryLabel.Text = carmodel.Country;
            ProductColorLabel.Text = carmodel.Color;




        }

        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void GoToOrderPicked_Click(object sender, RoutedEventArgs e)
        {

            OtherWindows.ClientOrderWindow clientOrderWindow = new OtherWindows.ClientOrderWindow(_carmodel);
            clientOrderWindow.ShowDialog();
            this.Close();

        }
    }
}
