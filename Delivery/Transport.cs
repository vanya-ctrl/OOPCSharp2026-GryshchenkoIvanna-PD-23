

namespace Delivery
{
    public abstract class Transport
    {
        //влас гет сеь
        public bool IsBusy { get; set; }
        public double MaxWeight { get; set; }
        public string Name { get; set; }

        //конс
        public Transport(double maxweight, string name)
        {
            MaxWeight = maxweight;
            Name = name;
        }

        // абстракт мет без тіла, кожен син пише свій
        public abstract double CalculateCost(Order order);

        // вірт мет є базова логіка, син може помінять
        public virtual bool CanTake(Order order)
        {
            return order.Weight <= MaxWeight;
        }
    }
}
