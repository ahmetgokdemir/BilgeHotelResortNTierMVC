﻿using Project.DTO.Models;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class BookingVM
    {
        public PaymentDTO PaymentDTO { get; set; }
        public Booking Booking { get; set; }
    }
}