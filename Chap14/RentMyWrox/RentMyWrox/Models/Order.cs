﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentMyWrox.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PickupDate { get; set; }

        public string HowPaid { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public double DiscountAmount { get; set; }
    }
}