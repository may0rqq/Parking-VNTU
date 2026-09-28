using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ParkingVNTU
{
    public class ParkingManager
    {
        private readonly List<ParkingSpot> _spots = new();
        private readonly BillingSystem _billing = new();
        public decimal TotalRevenue { get; private set; }

        public ParkingManager(int sedanSpots, int suvSpots, int electricSpots)
        {
            _spots.Add(new ParkingSpot(1, VehicleType.Sedan, isVipKudryavtsevSpot: true));

            int id = 2;
            for(int i = 0; i < sedanSpots; i++) _spots.Add(new ParkingSpot(id++, VehicleType.Sedan));
            for(int i = 0; i < suvSpots; i++) _spots.Add(new ParkingSpot(id++, VehicleType.SUV));
            for(int i = 0; i < electricSpots; i++) _spots.Add(new ParkingSpot(id++, VehicleType.Electric));
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            if(vehicle.IsKudryavtsev)
            {
                var vipSpot = _spots.FirstOrDefault(s => s.IsVipKudryavtsevSpot);
                if (vipSpot != null && !vipSpot.IsOccupied)
                {
                    vipSpot.Park(vehicle);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[👑 VIP] Ласкаво просимо, Дмитре Станіславовичу! Запарковано на персональне місце #1.");
                    Console.ResetColor();
                    return true;
                }
            }

            var spot = _spots.FirstOrDefault(s => !s.IsOccupied && s.AllowedType == vehicle.Type && !s.IsVipKudryavtsevSpot);
            if(spot == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[❌] Немає вільних місць для {vehicle.Type} ({vehicle.LicensePlate})");
                Console.ResetColor();
                return false;
            }

            spot.Park(vehicle);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[✅] Запарковано {vehicle.Type} ({vehicle.LicensePlate}) на місце #{spot.ID}");
            Console.ResetColor();
            return true;
        }

        public bool UnparkVehicle(string licensePlate)
        {
            var spot = _spots.FirstOrDefault(s => s.IsOccupied && s.CurrentVehicle.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
            if (spot == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[❌] Автомобіль з номером {licensePlate} не знайдено на парковці.");
                Console.ResetColor();
                return false;
            }
            var vehicle = spot.Vacate();
            var receipt = _billing.CalculateFee(vehicle);
            TotalRevenue += receipt.TotalCost;
            Console.WriteLine($"[💰] Автомобіль {vehicle.Type} ({vehicle.LicensePlate}) виїхав. Час паркування: {receipt.Duration.TotalSeconds:F0} секунд. Вартість: {receipt.TotalCost:C2}");
            Console.ResetColor();
            return true;
        }

        public void CheckAndEvacuate()
        {
            var violators = _spots
                .Where(s => s.IsOccupied && !s.CurrentVehicle.IsKudryavtsev && (DateTime.Now - s.CurrentVehicle.EntryTime).TotalSeconds > 120)
                .ToList();

            if(!violators.Any())
            {
                Console.WriteLine("Порушників не знайдено.");
                return;
            }

            foreach(var spot in violators)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[ЕВАКУАТОР] Авто {spot.CurrentVehicle.LicensePlate} перевищило ліміт (120 сек) і відправлено на штрафмайданчик!");
                Console.ResetColor();

                spot.Vacate();
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine("=== Статус парковки ===");
            foreach(var spot in _spots)
            {
                string spotInfo = spot.IsVipKudryavtsevSpot
                    ? "RESERVED [VIP  Кудрявцев Д. С.]"
                    : $"Тип : {spot.AllowedType, -8}";
                string status = spot.IsOccupied
                    ? $"[ЗАЙНЯТО {spot.CurrentVehicle.LicensePlate}]" 
                    : "[ВІЛЬНО]";
                Console.WriteLine($"Місце #{spot.ID, -2} | {spotInfo, 28} - {status}");
            }
            Console.WriteLine($"Загальний дохід: {TotalRevenue:C}");
        }
    }
}
