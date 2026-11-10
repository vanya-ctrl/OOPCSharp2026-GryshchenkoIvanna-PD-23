

namespace Delivery
{
    public class Bike : Transport
    {
        public Bike() : base(5, "Bike") { }

        public override double CalculateCost(Order order)
        {
            return 20 + 5 * order.Distance;
        }


        public override bool CanTake(Order order)
        {
            return base.CanTake(order) && order.Distance <= 10;
            // return order.Distance <= 10 || base.CanTake(order);
        }
    }

}
