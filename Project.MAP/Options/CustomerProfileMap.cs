using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class CustomerProfileMap : SpecMap<CustomerProfile>
    {
        public CustomerProfileMap()
        {
            ToTable("Musteri Profilleri");

        }
    }
}
