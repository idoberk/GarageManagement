using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class ManageGarage
    {
        private Dictionary<string, VehicleRecord> m_RecordsList = new Dictionary<string, VehicleRecord>();

        internal Dictionary<string, VehicleRecord> Records
        {
            get { return m_RecordsList; }
        }

        public void ChangeVehicleStatus(string i_LicensePlate) //, VehicleRecord.eVehicleStatus i_DesiredStatus)
        {
            // Records[i_LicensePlate].VehicleStatus = i_DesiredStatus;
        }

        private bool isVehicleExists(string i_LicensePlate)
        {
            bool isExists = Records.ContainsKey(i_LicensePlate);
            //if (!Records.ContainsKey(i_LicensePlate))
            //{
            //    throw new ArgumentException($"Error occurred. {i_LicensePlate} doesn't exist in the system.");
            //}

            return isExists;
        }

        //public void AddVehicle(Vehicle i_VehicleToAdd, string i_Name, string i_PhoneOwner)
        //{
        //    VehicleRecord vehicleRecordToAdd = new VehicleRecord(i_VehicleToAdd, i_Name, i_PhoneOwner);
        //    //CheckIfVehicleExists(i_VehicleToAdd.LicensePlate);
        //    Records.Add(i_VehicleToAdd.LicensePlate, vehicleRecordToAdd);
        //}

        public void AddVehicle(int i_VehicleToAdd, int i_EngineType, string i_Name, string i_PhoneOwner, string i_LicensePlate)
        {
            Vehicle newVehicle = VehicleFactory.CreateVehicle(
                i_LicensePlate,
                "mazda",
                "michelin",
                (eVehicleType)i_VehicleToAdd,
                (eEngineType)i_EngineType);

            VehicleRecord newRecord = new VehicleRecord(newVehicle, i_Name, i_PhoneOwner);
            Records.Add(i_LicensePlate, newRecord);
        }

        public List<string> GetPropertiesValuesFromUser(int i_VehicleType)
        {
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;
            string property = string.Empty;
            List<string> properties = new List<string>();

            switch (vehicleType)
            {
                case eVehicleType.Car:
                    properties.Add(GetColors());
                    properties.Add(GetDoors());
                    break;
                case eVehicleType.Truck:

                    break;
            }
            return properties;
            //case Motorcycle:
            //Motorcycle.getlicensetypes();
        }

        public string GetVehicleTypes()
        {
            StringBuilder vehicleTypes = new StringBuilder();

            foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                vehicleTypes.AppendLine(string.Format($"{(int)vehicleType}. {vehicleType.ToString()}"));
            }

            return vehicleTypes.ToString();
        }

        public string GetEngineTypes()
        {
            StringBuilder engineTypes = new StringBuilder();

            foreach (eEngineType engineType in Enum.GetValues(typeof(eEngineType)))
            {
                engineTypes.AppendLine(string.Format($"{(int)engineType}. {engineType.ToString()}"));
            }

            return engineTypes.ToString();
        }

        public void InflateWheelsToMax(string i_VehicleLicensePlate)
        {
            float tireAirPressureToAdd = 0f;

            if (isVehicleExists(i_VehicleLicensePlate))
            {
                List<Wheel> vehicleWheels = Records[i_VehicleLicensePlate].Vehicle.Wheels;
                foreach (Wheel wheel in vehicleWheels)
                {
                    tireAirPressureToAdd = wheel.MaxTireAirPressure - wheel.CurrentTireAirPressure;
                    wheel.InflateWheel(tireAirPressureToAdd);
                }
            }
        }

        public void FillTank(string i_VehicleLicensePlate)
        {
            float amountOfFuelToAdd = 0f;

            if (isVehicleExists(i_VehicleLicensePlate))
            {
                Vehicle vehicle = Records[i_VehicleLicensePlate].Vehicle;

                if (vehicle.Engine is FuelEngine fuelEngine)
                {
                    //eVehicleType vehicleType = VehicleFactory.GetVehicleType(vehicle);
                    amountOfFuelToAdd = fuelEngine.MaxEnergyCapacity - fuelEngine.CurrentEnergyAmount;
                    fuelEngine.Refuel(amountOfFuelToAdd, fuelEngine.FuelType);
                }
            }
        }
    }
}