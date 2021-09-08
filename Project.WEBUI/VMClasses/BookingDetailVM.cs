using Project.DTO.Models;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class BookingDetailVM
    {
        public List<ActiveRezervationSecondDTO> ActiveRezervationDTOs { get; set; }
        public List<BookingDetail> BookingDetails { get; set; }

    }
}