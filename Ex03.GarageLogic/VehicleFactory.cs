using System;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleProperties
        {
            CarColor = 1,
            NumberOfDoors,
            EngineVolume,
            LicenseType,
            CargoVolume,
            CargoCooled
        }

        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        internal static Vehicle CreateVehicle(string i_LicensePlate, string i_VehicleModelName, string i_ManufacturerName, eVehicleType i_VehicleType, eEngineType i_EngineType) 
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicle = new Car(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    vehicle.VehicleProperties.Add(eVehicleProperties.CarColor.ToString(), string.Empty);
                    vehicle.VehicleProperties.Add(eVehicleProperties.NumberOfDoors.ToString(), string.Empty);
                    break;

                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    vehicle.VehicleProperties.Add(eVehicleProperties.EngineVolume.ToString(), string.Empty);
                    vehicle.VehicleProperties.Add(eVehicleProperties.LicenseType.ToString(), string.Empty);
                    break;

                case eVehicleType.Truck:
                    vehicle = new Truck(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, i_EngineType);
                    vehicle.VehicleProperties.Add(eVehicleProperties.CargoVolume.ToString(), string.Empty);
                    vehicle.VehicleProperties.Add(eVehicleProperties.CargoCooled.ToString(), string.Empty);
                    break;
            }

            return vehicle;
        }

        //private void getInput(string i_PromptMessage, out string o_ParsedInput)
        //{
        //    printPrompt(i_PromptMessage);
        //    o_ParsedInput = getUserInput();
        //}

        ///summary
        /// print - requested property
        ///if enum - print enum
        ///get user input
        ///set user input in property
        ///summary

        //car - maybe print prompt centense - think how to connect sentences to car\motor\car
        //maybe dict --to print --  <sentence , enum converted to string\null>  ---> allways get user input
        //maybe dict <evehicleType , to string>

        //UI:
        //Get all vehicle data
        //inside of Get all vehicle data:

    }
}
 