using System;
using System.Text;
using System.Collections.Generic;
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
            string i_LicensePlate, string i_VehicleModelName, string i_WheelManufacturerName, string i_CurrentTireAirPressure)
        {
            bool isVehicleSuccessfullyAdded = false;

            ParseToInteger(i_VehicleToAdd, out int vehicleTypeChoice);
            ParseToInteger(i_EngineType, out int engineTypeChoice);
            ParseToFloat(i_CurrentTireAirPressure, out float tireAirPressure);


            Vehicle newVehicle = CreateVehicle(
                i_LicensePlate,
                i_VehicleModelName,
                i_WheelManufacturerName,
                tireAirPressure,
                (eVehicleType)vehicleTypeChoice,
                (eEngineType)engineTypeChoice);

            VehicleRecord newRecord = new VehicleRecord(newVehicle, i_OwnerName, i_OwnerPhoneNumber);
            Records.Add(i_LicensePlate, newRecord);
        }

        public Dictionary<string, object> GetVehicleProperties(string i_LicensePlate)
        {
            Dictionary<string, object> propertyPrompts = new Dictionary<string, object>();
            Vehicle vehicle = Records[i_LicensePlate].Vehicle;

            if (vehicle.Engine is ElectricEngine)
            {
                propertyPrompts.Add("BatteryPercentage", GetRemainingMinutesInBatteryPrompt());
            }
            else if (vehicle.Engine is FuelEngine)
            {
                propertyPrompts.Add("CurrentFuel", GetRemainingLitersInTankPrompt());
            }

            foreach (KeyValuePair<string, object> property in vehicle.VehicleProperties)
            {
                switch (property.Key)
                {
                    case nameof(Car.eCarColor):
                        propertyPrompts.Add(property.Key, GetCarColors());
                        break;

                    case nameof(Car.eNumOfDoors):
                        propertyPrompts.Add(property.Key, GetCarDoors());
                        break;

                    case nameof(Motorcycle.eLicenseType):
                        propertyPrompts.Add(property.Key, GetLicenseTypes());
                        break;

                    case nameof(Motorcycle.EngineVolume):
                        propertyPrompts.Add(property.Key, GetEngineVolumePrompt());
                        break;

                    case nameof(Truck.CargoVolume):
                        propertyPrompts.Add(property.Key, GetCargoVolumePrompt());
                        break;

                    case nameof(Truck.IsCargoCooled):
                        propertyPrompts.Add(property.Key, GetCargoCooledPrompt());
                        break;
                }
            }

            return propertyPrompts;
        }

        public void SetVehicleProperties(string i_LicensePlate, Dictionary<string, string> i_VehiclePropertiesPrompts)
        {
            Vehicle vehicle = Records[i_LicensePlate].Vehicle;
            Engine vehicleEngine = vehicle.Engine;

            foreach (KeyValuePair<string, string> property in i_VehiclePropertiesPrompts)
            {
                if (property.Key == "CurrentFuel")
                {
                    if (vehicleEngine is FuelEngine fuelEngine)
                    {
                        fuelEngine.SetRemainingLitersInTank(property.Value);
                    }
                    else
                    {
                        throw new Exception(); // TODO: Add exception message
                    }
                }
                else if (property.Key == "BatteryPercentage")
                {
                    if(vehicleEngine is ElectricEngine electricEngine)
                    {
                        electricEngine.SetRemainingMinutesInBattery(property.Value);
                    } 
                    else
                    {
                        throw new Exception(); // TODO: Add exception message
                    }
                }
                else if (vehicle.VehicleProperties.ContainsKey(property.Key))
                {
                    vehicle.SetProperty(property.Key, property.Value);
                }
                else
                {
                    throw new Exception(); // TODO: Add exception message
                }
            }
        }

        public string GetCarDoors()
        {
            return Car.GetDoors();
        }

        public string GetCarColors()
        {
            return Car.GetColors();
        }

        public string GetVehicleTreatmentStatusOptions()
        {
            return VehicleRecord.GetVehicleTreatmentStatusOptions();
        }

        public string GetVehicleTypes()
        {
            return VehicleFactory.GetVehicleTypes();
        }

        public string GetEngineTypes()
        {
            return Engine.GetEngineTypes();
        }

        public string GetFuelTypes()
        {
            return FuelEngine.GetFuelTypes();
        }

        public string GetLicenseTypes()
        {
            return Motorcycle.GetLicenseTypes();
        }

        public static string GetEngineVolumePrompt()
        {
            return Motorcycle.GetEngineVolumePrompt();
        }

        public static string GetCargoVolumePrompt()
        {
            return Truck.GetCargoVolumePrompt();
        }

        public static string GetCargoCooledPrompt()
        {
            return Truck.GetCargoCooledPrompt();
        }

        public static string GetRemainingMinutesInBatteryPrompt()
        {
            return ElectricEngine.GetRemainingMinutesInBatteryPrompt();
        }

        public static string GetRemainingLitersInTankPrompt()
        {
            return FuelEngine.GetRemainingLitersInTankPrompt();
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
            ParseToFloat(i_AmountOfFuelToAdd, out float amountOfFuelToAdd);
            ParseToInteger(i_FuelType, out int fuelType);

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
            ParseToFloat(i_MinutesOfBatteryToCharge, out float minutesOfBatteryToCharge);

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

        public static void ParseToInteger(string i_StringToParse, out int o_IntegerResult)
        {
            if (!int.TryParse(i_StringToParse, out o_IntegerResult))
            {
                throw new FormatException($"An error occured when tried parsing {i_StringToParse}");
            }
        }

        public static void ParseToFloat(string i_StringToParse, out float o_FloatResult)
        {
            if(!float.TryParse(i_StringToParse, out o_FloatResult))
            {
                throw new FormatException($"An error occured when tried parsing {i_StringToParse}");
            }
        }
    }
}