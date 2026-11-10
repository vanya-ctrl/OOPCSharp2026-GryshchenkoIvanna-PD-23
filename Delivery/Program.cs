using System;
using System.Collections.Generic;

namespace Delivery
{
    
    class Program
    {
       
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DeliveryService service = new DeliveryService();
            Bike bike = new Bike();
            Car car = new Car();
            Truck truck = new Truck();
            service.AddTransport(bike);
            service.AddTransport(car);
            service.AddTransport(truck);

            Order o1 = new Order(1, 2, "Документи", 5);    // вєлік
            Order o2 = new Order(2, 50, "Телевізор", 8);   // важкий для вєліка
            Order o3 = new Order(3, 3, "Піца", 20);        // далеко для вєліка
            Order o4 = new Order(4, 300, "Меблі", 15);     // ток груз

            // цена на різний транс
            service.ShowCosts(o1);
            service.ShowCosts(o2);
            service.ShowCosts(o3);
            service.ShowCosts(o4);

            // придназначение
            service.Assign(o1, bike);    
            service.Assign(o2, bike);    // занят
            service.Assign(o4, car);     // нет
            service.Assign(o4, truck);   

            service.PrintAll();

            // оплат
            service.Deliver(o1, bike, new CardPayment());
            service.Deliver(o4, truck, new CashPayment());
            service.Deliver(o3, car, new CardPayment());  // не активне замовлення

            service.PrintAll();
        }
    }

    
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

    // папа
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

    
    public class Car : Transport
    {
        public Car() : base(200, "Car") { }

        public override double CalculateCost(Order order)
        {
            return 50 + 8 * order.Distance + 1 * order.Weight;
        }
    }

    
    public class Truck : Transport
    {
        public Truck() : base(1000, "Truck") { }

        public override double CalculateCost(Order order)
        {
            return 200 + 15 * order.Distance + 0.5 * order.Weight;
        }
    }

    // интерфейс  контракт, кожен спосіб оплати мусить мати метод Pay
    public interface IPayment
    {
        void Pay(double amount);
    }

    public class CardPayment : IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплачено карткою: " + amount + " грн");
        }
    }

    public class CashPayment : IPayment
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплачено готівкою: " + amount + " грн");
        }
    }

    // вссе про доставку
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