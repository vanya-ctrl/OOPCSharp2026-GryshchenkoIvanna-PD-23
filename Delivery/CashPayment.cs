using System;


namespace Delivery
{
    public class CashPayment: IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплачено готівкою" + amount + "грн");
        }
    }
}
