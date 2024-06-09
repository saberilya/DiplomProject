using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для AfterBuyWindow.xaml
    /// </summary>
    public partial class AfterBuyWindow : Window
    {
        public AfterBuyWindow(Order order)
        {
            InitializeComponent();
           


            if (order.CreditorID != 1)
            {
                CreditBuy.Visibility = Visibility.Visible;
                CashBuy.Visibility = Visibility.Hidden;

                AfterName.Text = order.ClientName;
                AfterSurname.Text = order.ClientSurname;
                AfterSeria.Text = order.ClientSerial;
                AfterPass.Text = order.ClientPasswordNubmer;
                AfterPhone.Text = order.ClientPhone;
                AfterCash.Text = order.Client10PercentEnter;
                AfterDest.Text = order.Creditor.ClientDestination;
                AfterJob.Text = order.Creditor.ClientJob;
                AfterCash.Text = order.Creditor.ClientSalary;

                carBrand.Text = order.Cars.Brand;
                carModel.Text = order.Cars.Model;
                carCountry.Text = order.Cars.Country;
                carYear.Text = order.Cars.Year.ToString();
                carColor.Text = order.Cars.Color;
                carBody.Text = order.Cars.BodyType;
                carKm.Text = order.Cars.KM.ToString();
                carPrice.Text = order.Cars.Price.ToString();
                carComp.Text = order.Cars.Complectation;
                carHp.Text = order.Cars.HP.ToString();
                title.Text = "Чек по вашей заявке!";

            }

            else
            {
                AfterDest.Text = "";
                AfterJob.Text = "";
                AfterCash.Text = order.Client10PercentEnter;


                AfterName.Text = order.ClientName;
                AfterSurname.Text = order.ClientSurname;
                AfterSeria.Text = order.ClientSerial;
                AfterPass.Text = order.ClientPasswordNubmer;
                AfterPhone.Text = order.ClientPhone;
                carBrand.Text = order.Cars.Brand;
                carModel.Text = order.Cars.Model;
                carCountry.Text = order.Cars.Country;
                carYear.Text = order.Cars.Year.ToString();
                carColor.Text = order.Cars.Color;
                carBody.Text = order.Cars.BodyType;
                carKm.Text = order.Cars.KM.ToString();
                carPrice.Text = order.Cars.Price.ToString();
                carComp.Text = order.Cars.Complectation;
                carHp.Text = order.Cars.HP.ToString();
                if (order.Cars.Price == order.Cars.Price)
                {
                    title.Text = "Поздравляем с покупкой, ваш чек!";
                }
               

            }


        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            pech.Visibility = Visibility.Visible;
            BtnOtchet.Visibility = Visibility.Hidden;
            // Захват содержимого окна
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 97, PixelFormats.Pbgra32);
            renderTarget.Render(this);

            // Создание изображения из захваченного содержимого
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));
            string imagePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "AfterBuyWindow.png");
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            // Сохранение изображения в PDF
            string pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AfterBuyWindow.pdf");
            SaveImageAsPdf(imagePath, pdfPath);

            // Открытие PDF-файла
            this.Close();
            
        }
        private void SaveImageAsPdf(string imagePath, string pdfPath)
        {
            PrintQueue printQueue = null;
            LocalPrintServer printServer = new LocalPrintServer();

            // Получение принтера Microsoft Print to PDF
            foreach (PrintQueue queue in printServer.GetPrintQueues())
            {
                if (queue.Name == "Microsoft Print to PDF")
                {
                    printQueue = queue;
                    break;
                }
            }

            if (printQueue == null)
            {
                MessageBox.Show("Microsoft Print to PDF не найден.");
                return;
            }

            PrintDialog printDialog = new PrintDialog
            {
                PrintQueue = printQueue
            };

            // Создание визуального элемента для печати
            Image image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            image.Source = bitmap;

            // Создание документа для печати
            FixedDocument fixedDoc = new FixedDocument();
            FixedPage page = new FixedPage();
            page.Width = printDialog.PrintableAreaWidth;
            page.Height = printDialog.PrintableAreaHeight;
            page.Children.Add(image);

            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);
            fixedDoc.Pages.Add(pageContent);

            // Печать документа в PDF
            XpsDocumentWriter xpsDocumentWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
            xpsDocumentWriter.Write(fixedDoc);

            // Переименование временного PDF-файла
            string tempPdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "example.pdf");
            if (File.Exists(tempPdfPath))
            {
                File.Move(tempPdfPath, pdfPath);
            }
        }
    }
    
}
