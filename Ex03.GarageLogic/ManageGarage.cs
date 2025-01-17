using System;
using System.Text;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.VehicleFactory;
using static Ex03.GarageLogic.VehicleRecord;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    // TODO: Consider try catch in certain places;
    // TODO: Consider refactoring some error messages to include spaces between words;
    // TODO: Check the parsing method
    public class ManageGarage
    {
        private readonly Dictionary<string, VehicleRecord> r_RecordsList = new Dictionary<string, VehicleRecord>();

        internal Dictionary<string, VehicleRecord> Records
        {
            get { return r_RecordsList;}
        }

        private bool isVehicleExists(string i_LicensePlate)
        {
            bool isExists = Records.ContainsKey(i_LicensePlate);

            return isExists;
        }

        public bool CheckVehicleExistenceAndUpdateStatus(string i_LicensePlate, out string o_Message)
        {
            bool isExists = isVehicleExists(i_LicensePlate);
            o_Message = string.Empty;

            if (isExists)
            {
                Records[i_LicensePlate].VehicleStatus = eVehicleStatus.BeingRepaired;
                o_Message = "Vehicle already exists in the garage. Status has been changed to 'Being Repaired'.";
            }

            return isExists;
        }

        public void AddVehicle(string i_VehicleToAdd, string i_EngineType, string i_OwnerName, string i_OwnerPhoneNumber,
            string i_LicensePlate, string i_VehicleModelName, string i_WheelManufacturerName)
        {
            bool isVehicleSuccessfullyAdded = false;

            if (!int.TryParse(i_VehicleToAdd, out int vehicleTypeChoice))
            {
                throw new FormatException($"An error occured when tried parsing {i_VehicleToAdd}");
            }

            if(!int.TryParse(i_EngineType, out int engineTypeChoice))
            {
                throw new FormatException($"An error occured when tried parsing {i_EngineType}");
            }


            Vehicle newVehicle = CreateVehicle(
                i_LicensePlate,
                i_VehicleModelName,
                i_WheelManufacturerName,
                (eVehicleType)vehicleTypeChoice,
                (eEngineType)engineTypeChoice);

            if (newVehicle.VehicleProperties.ContainsKey(eVehicleProperties.CarColor.ToString()))
            {
                newVehicle.VehicleProperties[eVehicleProperties.CarColor.ToString()] = "1";
            }
            VehicleRecord newRecord = new VehicleRecord(newVehicle, i_OwnerName, i_OwnerPhoneNumber);
            Records.Add(i_LicensePlate, newRecord);
        }

        public Dictionary<string, object> GetVehicleProperties(string i_LicensePlate)
        {
            Dictionary<string, object> vehicleProperties = Records[i_LicensePlate].Vehicle.VehicleProperties;


            return vehicleProperties;
        }

        public void SetCarProperties(string i_LicensePlate, Dictionary<string, object> i_VehicleProperties)
        {
            // Records[i_LicensePlate].Vehicle.
        }

        public string GetCarDoors()
        {
            StringBuilder doorOptions = new StringBuilder();

            foreach(eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
            {
                doorOptions.AppendLine(string.Format($"{(int)numOfDoors}. {numOfDoors.ToString()}"));
            }

            return doorOptions.ToString();
        }

        public string GetCarColors()
        {
            StringBuilder colorOptions = new StringBuilder();

            foreach(eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
            {
                colorOptions.AppendLine(string.Format($"{(int)carColor}. {carColor.ToString()}"));
            }

            return colorOptions.ToString();
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

        // Section 2.
        public string GetVehiclesByStatus(bool i_IsBeingRepaired, bool i_IsRepairComplete, bool i_IsRepairPaid)
        {
            StringBuilder filteredList = new StringBuilder();

            foreach (KeyValuePair<string, VehicleRecord> record in Records)
            {
                if (record.Value.VehicleStatus.Equals(eVehicleStatus.BeingRepaired) && i_IsBeingRepaired)
                {
                    filteredList.AppendLine($"{record.Key}");
                }

                if(record.Value.VehicleStatus.Equals(eVehicleStatus.RepairComplete) && i_IsRepairComplete)
                {
                    filteredList.AppendLine($"{record.Key}");
                }

                if (record.Value.VehicleStatus.Equals(eVehicleStatus.RepairPaid) && i_IsRepairPaid)
                {
                    filteredList.AppendLine($"{record.Key}");
                }
            }

            if (filteredList.Length == 0)
            {
                filteredList.AppendLine("No vehicles matching the selected filters were found in the garage.");
            }

            return filteredList.ToString();
        }

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
                    //tireAirPressureToAdd = wheel.MaxTireAirPressure - wheel.CurrentTireAirPressure;
                    //wheel.InflateWheel(tireAirPressureToAdd);
                    wheel.CurrentTireAirPressure = wheel.MaxTireAirPressure;
                }
            }
        }

        // Section 5.
        public void RefuelTank(string i_VehicleLicensePlate, string i_FuelType, string i_AmountOfFuelToAdd)
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
                    vehicle.UpdateEnergyPercentage();
                }
                else
                {
                    throw new ArgumentException($"{vehicle.Engine.EngineType} engine cannot be filled with fuel");
                }
            }
        }

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
                    vehicle.UpdateEnergyPercentage();
                }
                else
                {
                    throw new ArgumentException($"{vehicle.Engine} engine is not rechargeable");
                }
            }
        }

        public void ParseToInteger(string i_StringToParse, out int o_IntegerResult)
        {
            if (!int.TryParse(i_StringToParse, out o_IntegerResult))
            {
                throw new FormatException($"An error occured when tried parsing {i_StringToParse}");
            }
        }
    }
}