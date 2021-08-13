using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class StaffProfileMap : SpecMap<StaffProfile>
    {
        public StaffProfileMap()
        {
            ToTable("Elemanlar");

            Property(x => x.UserName).HasColumnName("Kullanici Adi").IsRequired();
            Property(x => x.Password).HasColumnName("Sifre").IsRequired();
            Property(x => x.JobTitle).HasColumnName("Meslek").IsRequired();
            Property(x => x.Salary).HasColumnName("Maas").IsRequired();
            Property(x => x.HireDate).HasColumnName("Ise Baslangic").HasColumnType("datetime2").IsRequired(); //**
            Property(x => x.Role).HasColumnName("Rol").IsRequired();

        }
    }
}
