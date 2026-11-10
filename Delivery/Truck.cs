
namespace Delivery
{
    public class Truck : Transport
    {
        public Truck() : base(1000, "Truck") { }

        public override double CalculateCost(Order order)
        {
            return 200 + 15 * order.Distance + 0.5 * order.Weight;
        }
    }

}
