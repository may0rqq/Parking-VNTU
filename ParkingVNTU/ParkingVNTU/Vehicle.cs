using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingVNTU
{
    public class Vehicle
    {
        public string LicensePlate { get; }
        public VehicleType Type { get; }
        public DateTime EntryTime { get; }
        public bool IsStudent { get; }
        public bool IsKudryavtsev { get; }

        public Vehicle(string licensePlate, VehicleType type
            , bool isStudent = false, bool isKudryavtsev = false)
        {
            LicensePlate = licensePlate;
            Type = type;
            EntryTime = DateTime.Now;
            IsStudent = isStudent;
            IsKudryavtsev = isKudryavtsev;
        }
    }
}


    

