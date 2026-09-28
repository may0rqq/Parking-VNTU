using System;

namespace ParkingVNTU
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Smart Parking VNTU System";

            ParkingManager parking = new ParkingManager(sedanSpots: 2, suvSpots: 1, electricSpots: 1);

            bool running = true;
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РОЗУМНА ПАРКОВКА ВНТУ ===");
                Console.ResetColor();
                Console.WriteLine("1. Запаркувати авто");
                Console.WriteLine("2. Забрати авто та оплатити");
                Console.WriteLine("3. Показати карту парковки");
                Console.WriteLine("4. 🚨 Викликати евакуатор (перевірка порушників)");
                Console.WriteLine("5. Вихід");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть номер авто (наприклад, AB1234CE): ");
                        string plate = Console.ReadLine();

                        Console.WriteLine("\nХто водій?");
                        Console.WriteLine("1 - Звичайний водій");
                        Console.WriteLine("2 - Студент ВНТУ (знижка 50%)");
                        Console.WriteLine("3 - 👑 Кудрявцев Дмитро Станіславович (VIP)");
                        Console.Write("Ваш вибір: ");
                        string driverChoice = Console.ReadLine();

                        bool isStudent = driverChoice == "2";
                        bool isKudryavtsev = driverChoice == "3";

                        Console.WriteLine("\nВиберіть тип авто: 0 - Sedan, 1 - SUV, 2 - Electric");
                        if (int.TryParse(Console.ReadLine(), out int typeIndex) && Enum.IsDefined(typeof(VehicleType), typeIndex))
                        {
                            Vehicle v = new Vehicle(plate, (VehicleType)typeIndex, isStudent, isKudryavtsev);
                            parking.ParkVehicle(v);
                        }
                        else
                        {
                            Console.WriteLine("Некоректний тип авто!");
                        }
                        break;

                    case "2":
                        Console.Write("Введіть номер авто, яке виїжджає: ");
                        string leavePlate = Console.ReadLine();
                        parking.UnparkVehicle(leavePlate);
                        break;

                    case "3":
                        parking.DisplayStatus();
                        break;

                    case "4":
                        parking.CheckAndEvacuate();
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Дякуємо за використання парковки ВНТУ!");
                        break;

                    default:
                        Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатисніть Enter для продовження...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}