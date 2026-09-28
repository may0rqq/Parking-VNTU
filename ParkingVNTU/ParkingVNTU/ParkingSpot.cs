using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingVNTU
{
    public class ParkingSpot
    {
        public int ID { get; }
        public VehicleType AllowedType { get; }
        public bool IsVipKudryavtsevSpot { get; }
        public Vehicle CurrentVehicle { get; private set; }
        public bool IsOccupied => CurrentVehicle != null;

        public ParkingSpot(int id, VehicleType allowedType, bool isVipKudryavtsevSpot = false)
        {
            ID = id;
            AllowedType = allowedType;
            IsVipKudryavtsevSpot = isVipKudryavtsevSpot;
        }

        public bool Park(Vehicle vehicle)
        {
            if (IsOccupied) return false;
            if (IsVipKudryavtsevSpot && !vehicle.IsKudryavtsev) return false;
            if (!IsVipKudryavtsevSpot && vehicle.Type != AllowedType) return false;

            CurrentVehicle = vehicle;
            return true;
        }

        public Vehicle Vacate()
        {
            var vehicle = CurrentVehicle;
            CurrentVehicle = null;
            return vehicle;
        }
    }
}
