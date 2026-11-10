using System;


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
   
}