﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OredrAggregate
{
	public class Order : BaseEntity
	{
        public Order()
        {
            
        }
        public Order(string buyerEmail,Adress shippingadress,DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
		{
            ShippingAdress = shippingadress;
			BuyerEmail = buyerEmail;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatues Statues { get; set; } = OrderStatues.Pending;
        public Adress ShippingAdress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

        public string PaymetnIntentId { get; set; } = string.Empty;
    }
}