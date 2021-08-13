using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class RoomDetailMap : BaseMap<RoomDetail>
    {
        public RoomDetailMap()
        {
            ToTable("Oda Detay");

            Property(x => x.RoomNo).HasColumnName("Oda No").IsRequired();
            Property(x => x.Situation).HasColumnName("Oda Durumu").IsRequired();
            Property(x => x.isMaintenance).HasColumnName("Oda Bakımı").IsRequired();


        }
    }
}
