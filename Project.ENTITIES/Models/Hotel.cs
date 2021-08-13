using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }

        public virtual List<StaffProfile> StaffProfiles { get; set; }
        public virtual List<Room> Rooms { get; set; }


    }
}
