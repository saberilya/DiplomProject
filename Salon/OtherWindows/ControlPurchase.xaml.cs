using System;
using System.Collections.Generic;
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

namespace Salon.OtherWindows
{
    /// <summary>
    /// Логика взаимодействия для ControlPurchase.xaml
    /// </summary>
    public partial class ControlPurchase : Window
    {
        public ControlPurchase(Order order)
        {
            InitializeComponent();
            brand.Text = $"ДОГОВОР КУПЛИ-ПРОДАЖИ\n\n" +
                                 $"г. Место заключения договора, {DateTime.Today.ToShortDateString()}\n\n" +
                                 $"Продавец: Наименование продавца, именуемый в дальнейшем «Продавец», с одной стороны, и\n" +
                                 $"Покупатель: {order.ClientName} {order.ClientSurname},номер телефона {order.ClientPhone} паспорт серии {order.ClientSerial} № {order.ClientPasswordNubmer}, именуемый в дальнейшем \"Покупатель\", с другой стороны, заключили настоящий договор о нижеследующем:\n\n" +
                                 $"1. Предмет договора:\n" +
                                 $"Продавец обязуется передать в собственность Покупателю, а Покупатель обязуется принять и оплатить автомобиль следующих характеристик:\n" +
                                 $"- Марка: {order.Cars.Brand}\n" +
                                 $"- Модель: {order.Cars.Model}\n" +
                                 $"- Год выпуска: {order.Cars.Year}\n" +
                                 $"- VIN-номер: {order.Cars.VIN}\n" +
                                 $"- Цвет: {order.Cars.Color}\n" +
                                 $"- Пробег: {order.Cars.KM}\n" +
                                 $"- Стоимость: {order.Cars.Price}\n\n" +
                                 $"2. Цена и порядок оплаты:\n" +
                                 $"Цена продажи автомобиля составляет {order.Cars.Price} рублей. Оплата производится Покупателем в полном объеме наличными/безналичным способом на момент передачи автомобиля.\n\n" +
                                 $"3. Передача прав собственности и рисков:\n" +
                                 $"Права собственности на автомобиль переходят к Покупателю с момента подписания настоящего договора.\n\n" +
                                 $"4. Гарантии и обязательства сторон:\n" +
                                 $"Продавец гарантирует, что автомобиль находится в хорошем рабочем состоянии и не имеет скрытых дефектов, которые могли бы повлиять на его надлежащее использование. Покупатель обязуется оплатить стоимость автомобиля в согласованные сроки и обеспечить его правильное использование и уход.\n\n" +
                                 $"5. Ответственность сторон:\n" +
                                 $"За нарушение условий настоящего договора стороны несут ответственность в соответствии с действующим законодательством Российской Федерации.\n\n" +
                                 $"6. Прочие условия:\n" +
                                 $"Любые изменения и дополнения к настоящему договору действительны только в письменной форме и подписаны обеими сторонами.\n\n" +
                                 $"7. Заключительные положения:\n" +
                                 $"Настоящий договор вступает в силу с момента его подписания обеими сторонами и действует до полного выполнения своих условий.\n\n" +
                                 $"Продавец: АвтосалонДилом          Покупатель: _______________\n\n" +
                                 $"[Подпись продавца]                        [Подпись покупателя]";

        }
        private void ExitClick_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void pech_Click(object sender, RoutedEventArgs e)
        {
            ExitClick.Visibility = Visibility.Hidden;
            pech.Visibility = Visibility.Hidden;
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
