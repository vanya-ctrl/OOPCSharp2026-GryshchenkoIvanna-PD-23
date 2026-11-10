using System;
using System.Collections.Generic;


namespace Delivery
{
    public class Order
    {
        public int Number { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }


        public Order(int number, double weight, string description, double distance)
        {
            Number = number;
            Weight = weight;
            Description = description;
            Distance = distance;
        }
    }
}
