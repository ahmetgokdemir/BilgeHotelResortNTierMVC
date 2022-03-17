using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Room : BaseEntity
    {
        /*
            //MyInit'de kullanılabilecek diğer yöntem..
            public Room()
            {
                RoomDetails = new List<RoomDetail>();
            }          
        */

        // Strategy Patternde, MyInit sınıfında proje çalışır çalışmaz ürünlerin hazır gelmesini - Bogus kütüphanesi (fake data kütüphanesi) bize hazır Data sunacaktır- Product ekleyerek comboboxdan category'i seçince EF sql ile haberleşip Product ekleyip, categorysinide verip normanizasyondan bunu bağlayabiliyor.. Fakat bu işi sql ile değilde de, Initte veritabanı oluştururken product ekleyeceğiz o zaman direkt RAM den çalıştığınız durumda bir category'e ürün eklemek isteyince C# taradında olunduğu için ' public virtual List<Product> Products { get; set; }' default olarak referans tipi olduğu için null gelir.. RAM de bu işlem yapılmaya calışılırsa  c.Products.Add(p); denilince Products null olduğu için Null Reference exception verir.. EF'e emir vermeden (sql ile ilişki kurulmadan) C# kendi içersinde null olan bir şeyi methodda kullanılmasından dolayı hata verir.. O yüzden aşağıdaki kod Category instance alınınca ctor'da  koleksiyonun instance alınsın.. RAM de null kalmayacak.. DBFirst te buna ihtiyaç olmaz..

        public string RoomType { get; set; }
        public string RoomDescription { get; set; }
        public short RoomCount { get; set; }
        public short RoomAvailable { get; set; }
        public string ImagePath { get; set; }
        public bool availableBalcony { get; set; }
        public bool availableMinibar { get; set; }
        [Required(ErrorMessage = "Oda ücreti boş bırakılamaz")]
        [Range(100, 1000, ErrorMessage = "Ücret 100 ile 1000 arasında olmalıdır.")]
        public decimal Price { get; set; }

        public int HotelID { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
        public virtual List<RoomDetail> RoomDetails { get; set; }


    }
}
