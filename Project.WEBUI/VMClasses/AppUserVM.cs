using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class AppUserVM
    {
        public Customer CustomerUser { get; set; }
        public CustomerProfile Profile { get; set; }
        public List<StaffProfile> StaffUsers { get; set; }
        public StaffProfile StaffUser { get; set; }
    }
}