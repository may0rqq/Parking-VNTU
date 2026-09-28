using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingVNTU
{
    public class BillingSystem
    {
        private readonly Dictionary<VehicleType, decimal> _hourlyRates = new()
        {
            { VehicleType.Sedan, 30.0m },
            { VehicleType.SUV, 45.0m },
            { VehicleType.Electric, 20.0m  },
            { VehicleType.Truck, 70.0m }
        };

        public ParkingSpot CalculateFee(Vehicle vehicle)
        {
            var duration = DateTime.Now - vehicle.EntryTime;
            int hours = Math.Max(1, (int)Math.Ceiling(duration.TotalSeconds));
            decimal cost = hours * _hourlyRates[vehicle.Type];

            if (vehicle.IsKudryavtsev) cost *= 0m;
            else if (vehicle.IsStudent) cost *= 0.5m;

            return new Receipt(vehicle.LicensePlate, duration, cost, vehicle.IsKudryavtsev, vehicle.IsStudent);
        }
    }
}
