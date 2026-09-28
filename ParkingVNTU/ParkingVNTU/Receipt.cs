using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingVNTU
{
    public class Receipt
    {
        public string LicensePlate { get; }
        public TimeSpan Duration { get; }
        public decimal TotalCost { get; }
        public bool IsStudent { get; }
        public bool IsKudryavtsev { get; }

        public Receipt(string licensePlate, TimeSpan duration, decimal totalCost, bool isStudent, bool isKudryavtsev)
        {
            LicensePlate = licensePlate;
            Duration = duration;
            TotalCost = totalCost;
            IsStudent = isStudent;
            IsKudryavtsev = isKudryavtsev;
        }

        public void Printreceipt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=================");
            Console.WriteLine($"    ЧЕК ОПЛАТИ ПАРКОВКИ ВНТУ ");
            Console.WriteLine($"======================");
            Console.WriteLine($"Авто:{LicensePlate}");
            Console.WriteLine($"Час стоянки:{Math.Max(1, (int)Duration.TotalSeconds)}сек.(симуляція годин)");

            if (IsKudryavtsev)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("СТАТУС:VIP(Кудрявцев Дмитро Станіславович)");
                Console.WriteLine("ЗНИЖКА:100%(Почесному викладачу безкоштовно)");
            }
            else if (IsStudent)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("СТАТУС:Студент ВНТУ (Знижка 50%)");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"До сплати:{TotalCost:C}");
            Console.WriteLine("==================================\n");
            Console.ResetColor();
        }
    }
}



