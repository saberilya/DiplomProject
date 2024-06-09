//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Salon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int HP { get; set; }
        public int Price { get; set; }
        public int KM { get; set; }
        public string Complectation { get; set; }
        public string BodyType { get; set; }
        public string Country { get; set; }
        public int PTS { get; set; }
        public int VIN { get; set; }
        public int StatusID { get; set; }
        public string Color { get; set; }
        public byte[] Picture { get; set; }
        public override string ToString()
        {
            string statusName = "";
            try
            {
                // Получаем контекст данных
                var context = DealershipEntities1.GetContext();

                // Получаем объект статуса по его ID
                var status = context.Status.FirstOrDefault(s => s.ID == StatusID);

                // Если статус найден, получаем его имя
                if (status != null)
                {
                    statusName = status.StatusName;
                }
                else
                {
                    statusName = "Статус неизвестен";
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при доступе к базе данных
                Console.WriteLine($"Ошибка при доступе к базе данных: {ex.Message}");
                statusName = "Ошибка при доступе к базе данных";
            }

            return $"Марка:  {Brand} \nМодель:  {Model} \nСтатус :  {statusName}";
        }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
