using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class CustomerMap : BaseMap<Customer>
    {
        public CustomerMap()
        {
            ToTable("Musteriler");

            HasOptional(x => x.CustomerProfile).WithRequired(x => x.Customer);

            Property(x => x.UserName).HasColumnName("Kullanici Adi").IsRequired();  
            Property(x => x.Password).HasColumnName("Sifre").IsRequired();
            Property(x => x.ActivationCode).HasColumnName("Aktivasyon Kodu").IsRequired();
            Property(x => x.Email).HasColumnName("E-mail");
            Property(x => x.Role).HasColumnName("Rol").IsRequired();

        }
    }
}
