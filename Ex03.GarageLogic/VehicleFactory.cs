using System;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        //public enum eEngineType
        //{
        //    Fuel = 1,
        //    Electric
        //}

        //public static List<eEngineType> GetSupportedEngineTypes(eVehicleType VehicleType)
        //{
        //    List<eEngineType> supportEngineTypes = new List<eEngineType>();

        //    switch (VehicleType)
        //    {
        //        case eVehicleType.Car:
        //        case eVehicleType.Motorcycle:
        //            supportEngineTypes.Add(eEngineType.Fuel);
        //            supportEngineTypes.Add(eEngineType.Electric);
        //            break;
        //        case eVehicleType.Truck:
        //            supportEngineTypes.Add(eEngineType.Fuel);
        //            break;
        //    }

        //    return supportEngineTypes;
        //}

        internal static Vehicle CreateVehicle(string i_LicensePlate, string i_VehicleModelName, string i_ManufacturerName, eVehicleType i_VehicleType, eEngineType i_EngineType) 
        {
            Vehicle vehicle = null;
            //Engine engine = null;

            //switch (i_IsFuelEngine)
            //{
            //    case Engine.eEngineType.Fuel
            //}

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    
                    vehicle = new Car(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    break;

                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    break;

                case eVehicleType.Truck:
                    vehicle = new Truck(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    break;
            }

            return vehicle;
        }

        //internal static eVehicleType GetVehicleType(Vehicle i_Vehicle)
        //{
        //    eVehicleType vehicleType;

        //    if (i_Vehicle is Car)
        //    {
        //        vehicleType = eVehicleType.Car;
        //    } else if (i_Vehicle is Motorcycle)
        //    {
        //        vehicleType = eVehicleType.Motorcycle;
        //    } else if (i_Vehicle is Truck)
        //    {
        //        vehicleType = eVehicleType.Truck;
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"{i_Vehicle.GetType().Name} is not supported vehicle type");
        //    }

        //    return vehicleType;
        //}
    }
}
 