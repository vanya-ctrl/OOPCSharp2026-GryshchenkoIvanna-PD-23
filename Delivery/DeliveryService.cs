using System;
using System.Collections.Generic;

namespace Delivery
{
    public class DeliveryService
    {
        public List<Transport> Transports = new List<Transport>();
        public List<Order> ActiveOrders = new List<Order>();
        public List<Order> History = new List<Order>();


        public void AddTransport(Transport t)
        {
            Transports.Add(t);
        }

        // ціна за трас який моде
        public void ShowCosts(Order order)
        {
            Console.WriteLine("Вартість замовлення №" + order.Number + ":");
            foreach (Transport t in Transports)
            {
                if (t.CanTake(order))
                {
                    Console.WriteLine("  " + t.Name + ": " + t.CalculateCost(order) + " грн");
                }
            }
        }

        // дать замовленб свій транс
        public void Assign(Order order, Transport t)
        {
            if (t.IsBusy)
            {
                Console.WriteLine("Не вийшло: " + t.Name + " зайнятий");
            }
            else if (!t.CanTake(order))
            {
                Console.WriteLine("Не вийшло: " + t.Name + " не може взяти замовлення №" + order.Number);
            }
            else
            {
                t.IsBusy = true;
                ActiveOrders.Add(order);
                Console.WriteLine("Замовлення №" + order.Number + " призначено на " + t.Name);
            }
        }

        //  
        public void Deliver(Order order, Transport t, IPayment payment)
        {
            if (!ActiveOrders.Contains(order))
            {
                Console.WriteLine("Замовлення №" + order.Number + " не є активним");
                return;
            }
            payment.Pay(t.CalculateCost(order));
            t.IsBusy = false;
            ActiveOrders.Remove(order);
            History.Add(order);
            Console.WriteLine("Замовлення №" + order.Number + " доставлено");
        }

        //вивод тексв
        public void PrintAll()
        {
            Console.WriteLine("--- Активні замовлення ---");
            foreach (Order o in ActiveOrders)
                Console.WriteLine("№" + o.Number + " " + o.Description);

            Console.WriteLine("--- Історія доставок ---");
            foreach (Order o in History)
                Console.WriteLine("№" + o.Number + " " + o.Description);
        }
    }
}
