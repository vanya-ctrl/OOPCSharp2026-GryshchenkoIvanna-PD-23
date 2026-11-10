using System;


namespace Delivery
{
    public class CardPayment : IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплачено карткою: " + amount + " грн");
        }
    }
}
