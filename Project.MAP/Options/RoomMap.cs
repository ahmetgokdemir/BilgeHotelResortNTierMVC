using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class RoomMap : BaseMap<Room>
    {
        public RoomMap()
        {
            ToTable("Odalar");

            Property(x => x.RoomType).HasColumnName("Oda Turu").IsRequired();
            Property(x => x.RoomDescription).HasColumnName("Oda Aciklama");
            Property(x => x.RoomCount).HasColumnName("Oda Sayisi").IsRequired();
            Property(x => x.RoomAvailable).HasColumnName("Hazirda Oda Sayisi").IsRequired();
            Property(x => x.ImagePath).HasColumnName("Resim Yolu");
            Property(x => x.availableBalcony).HasColumnName("Balkon Mevcudiyet").IsRequired();
            Property(x => x.availableMinibar).HasColumnName("Minibar Mevcudiyet").IsRequired();
            Property(x => x.Price).HasColumnName("Oda Fiyati").HasColumnType("money"); //**


        }
    }
}
