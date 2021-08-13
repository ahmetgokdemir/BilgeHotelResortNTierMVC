using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class RoomDetail : BaseEntity
    {
        public string RoomNo { get; set; }
        public bool Situation { get; set; }
        public bool isMaintenance { get; set; }
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }

    }
}
