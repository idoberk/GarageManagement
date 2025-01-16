using System;
using System.Text;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.VehicleFactory;
using static Ex03.GarageLogic.VehicleRecord;

namespace Ex03.GarageLogic
{
    // TODO: Consider try catch in certain places;
    // TODO: Consider refactoring some error messages to include spaces between words;
    public class ManageGarage
    {
        private Dictionary<string, VehicleRecord> m_RecordsList = new Dictionary<string, VehicleRecord>();

        internal Dictionary<string, VehicleRecord> Records
        {
            get
            {
                return m_RecordsList;
            }
        }

        private bool isVehicleExists(string i_LicensePlate)
        {
            bool isExists = Records.ContainsKey(i_LicensePlate);

            //if (!isExists)
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

        public void AddVehicle(
            int i_VehicleToAdd,
            int i_EngineType,
            string i_Name,
            string i_PhoneOwner,
            string i_LicensePlate)
        {
            Vehicle newVehicle = CreateVehicle(
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

        }

        public string GetVehicleTreatmentStatusOptions()
        {
            StringBuilder treatmentOptions = new StringBuilder();

            foreach (eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
            {
                treatmentOptions.AppendLine(string.Format($"{(int)status}. {status.ToString()}"));
            }

            return treatmentOptions.ToString();
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

        public string GetFuelTypes()
        {
            StringBuilder fuelTypes = new StringBuilder();

            foreach (eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                fuelTypes.AppendLine(string.Format($"{(int)fuelType}. {fuelType.ToString()}"));
            }

            return fuelTypes.ToString();
        }

        /// <summary>
        /// 
        /// We cannot set RepairPaid if the current status is BeingRepaired
        /// We can set RepairComplete only if the current status is BeingRepaired
        /// BeingRepaired = 1,
        /// RepairComplete,
        /// RepairPaid
        /// 
        /// </summary>
        // Section 3.
        public void ChangeVehicleStatus(string i_LicensePlate, string i_DesiredStatus)
        {
            if (!int.TryParse(i_DesiredStatus, out int desiredStatus))
            {
                throw new FormatException($"An error occured when tried parsing {i_DesiredStatus}");
            }

            if (isVehicleExists(i_LicensePlate))
            {
                eVehicleStatus currentStatus = Records[i_LicensePlate].VehicleStatus;
                eVehicleStatus newStatus = (eVehicleStatus)desiredStatus;
                bool isValidChange = true;
                string errorMessage = string.Empty;

                switch (currentStatus)
                {
                    case eVehicleStatus.BeingRepaired:
                        if (newStatus == eVehicleStatus.RepairPaid)
                        {
                            isValidChange = false;
                            errorMessage =
                                $"Cannot change {currentStatus} directly to {newStatus}. Must be {eVehicleStatus.RepairComplete} first.";
                        }

                        break;

                    case eVehicleStatus.RepairPaid:
                        if (newStatus == eVehicleStatus.RepairComplete)
                        {
                            isValidChange = false;
                            errorMessage =
                                $"Cannot change {currentStatus} directly to {newStatus}. Must be {eVehicleStatus.BeingRepaired} first.";
                        }

                        break;
                }

                if (!isValidChange)
                {
                    throw new ArgumentException(errorMessage);
                }

                Records[i_LicensePlate].VehicleStatus = newStatus;
            }
        }

        // Section 4.
        public void InflateWheelsToMax(string i_VehicleLicensePlate) // TODO: add validations
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

        // Section 5.
        public void FillTank(string i_VehicleLicensePlate, string i_FuelType, string i_AmountOfFuelToAdd)
        {
            if (!float.TryParse(i_AmountOfFuelToAdd, out float amountOfFuelToAdd))
            {
                throw new FormatException($"An error occured when tried parsing {i_AmountOfFuelToAdd}");
            }

            if (!int.TryParse(i_FuelType, out int fuelType))
            {
                throw new FormatException($"An error occured when tried parsing {i_FuelType}");
            }

            if (isVehicleExists(i_VehicleLicensePlate))
            {
                Vehicle vehicle = Records[i_VehicleLicensePlate].Vehicle;
                eFuelType selectedFuelType = (eFuelType)fuelType;

                if (vehicle.Engine is FuelEngine fuelEngine)
                {
                    if (fuelEngine.FuelType != selectedFuelType)
                    {
                        throw new ArgumentException($"Cannot refuel the vehicle with {selectedFuelType} as it doesn't match the fuel type");
                    }

                    fuelEngine.Refuel(amountOfFuelToAdd, fuelEngine.FuelType);
                }
                else
                {
                    throw new ArgumentException($"{vehicle.Engine.EngineType} engine cannot be filled with fuel");
                }
            }
        }

        // TODO: Add FillBattery method;
        // Section 6.
        public void RechargeBattery(string i_VehicleLicensePlate, string i_MinutesOfBatteryToCharge)
        {
            if (!float.TryParse(i_MinutesOfBatteryToCharge, out float minutesOfBatteryToCharge))
            {
                throw new FormatException($"An error occured when tried parsing {i_MinutesOfBatteryToCharge}");
            }

            if (isVehicleExists(i_VehicleLicensePlate))
            {
                Vehicle vehicle = Records[i_VehicleLicensePlate].Vehicle;

                if (vehicle.Engine is ElectricEngine electricEngine)
                {
                    float hoursToCharge = minutesOfBatteryToCharge / 60f;
                    electricEngine.Recharge(hoursToCharge);
                }
                else
                {
                    throw new ArgumentException($"{vehicle.Engine} engine is not rechargeable");
                }
            }
        }
    }
}