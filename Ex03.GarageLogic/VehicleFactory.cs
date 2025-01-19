using System;
using System.Text;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        internal static Vehicle CreateVehicle(string i_VehicleLicensePlate, string i_VehicleModelName, string i_ManufacturerName,
                                              float i_CurrentTireAirPressure, eVehicleType i_VehicleType, eEngineType i_EngineType) 
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicle = new Car(i_VehicleLicensePlate, i_VehicleModelName, i_ManufacturerName, i_CurrentTireAirPressure, i_EngineType);
                    break;

                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(i_VehicleLicensePlate, i_VehicleModelName, i_ManufacturerName, i_CurrentTireAirPressure, i_EngineType);
                    break;

                case eVehicleType.Truck:
                    vehicle = new Truck(i_VehicleLicensePlate, i_VehicleModelName, i_ManufacturerName, i_CurrentTireAirPressure, i_EngineType);
                    break;

                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            return vehicle;
        }

        internal static string GetVehicleTypes()
        {
            StringBuilder vehicleTypes = new StringBuilder();

            foreach(eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                vehicleTypes.AppendLine(string.Format($"{(int)vehicleType}. {vehicleType.ToString()}"));
            }

            return vehicleTypes.ToString();
        }
    }
}
 