using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class SeatStatus
    {
        public static Guid Empty = new Guid("6354B081-AA9D-477B-8CE8-BA3FA48717DE");
        public static Guid Declined = new Guid("E5B21BC3-766B-439A-916C-9B71007C179B");
        public static Guid Requested = new Guid("83277A21-E427-498A-942D-7E11AA35EEE3");
        public static Guid AlreadyTaken = new Guid("99FCE37F-0AF8-4273-8CDF-6FA1783C854C");
        public static Guid Confirmed = new Guid("F4F99B6D-4FC7-4881-8CD2-280F82C25C45");

        public Guid SeatStatusId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
