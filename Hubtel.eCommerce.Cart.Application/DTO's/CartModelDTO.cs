using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hubtel.eCommerce.Cart.Application.DTO_s
{
    public class CartModelDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string PhoneNumber { get; set; }
    }
}
