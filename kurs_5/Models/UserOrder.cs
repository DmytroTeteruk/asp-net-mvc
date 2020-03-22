using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kurs_5.Models
{
    public class UserOrder
    {
        public long ProductId { get; }
        public string ProductName { get; }
        public long? ProductPrice { get; }
        public string StatusName { get; }

        public UserOrder(long productId, string productName, long? productPrice, string statusName)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            StatusName = statusName;
        }

        public override bool Equals(object obj)
        {
            return obj is UserOrder other &&
                   ProductId == other.ProductId &&
                   ProductName == other.ProductName &&
                   ProductPrice == other.ProductPrice &&
                   StatusName == other.StatusName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, ProductName, ProductPrice, StatusName);
        }
    }
}