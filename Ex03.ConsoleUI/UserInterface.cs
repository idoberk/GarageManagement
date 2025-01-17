using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    // TODO: Remove parsing in the UI and do it in the ManageGarage;
    public class UserInterface
    {
        private readonly ManageGarage r_GarageManagement = new ManageGarage();

        public enum MenuChoices
        {
            InsertVehicle = 1,
            DisplayVehicleLicensePlates,
            ChangeStatus,
            InflateWheelsToMaxAirPressure,
            RefuelTank,
            RechargeBattery,
            DisplayVehicleInformation
        }

        public void RunProgram()
        {
            // createNewVehicle("hello", "Mazda", "Michelin");
            insertVehicle();
            // rechargeVehicle();
            // refuelVehicle();
            // changeVehicleStatus();

            //printMainMenu();

            //string userInput = getUserInput();
            //List<string> bla = garage.GetPropertiesValuesFromUser(1);
            //foreach (string property in bla)
            //{
            //    printPrompt("Choose from list");
            //    printPrompt(property);
            //    userInput = getUserInput();
            //}


            //switch (userInput)
            //{
            //    case "1":
            //        insertVehicle();



            //}
        }
       

        private void printMainMenu()
        {
            string formatedString = string.Format(@"1. Insert your vehicle.
2. Display existing vehicles license plates.
3. Change vehicle status.
4. Inflate wheels to maximum air pressure.
5. Refuel tank.
6. Recharge battery.
7. Display vehicle information.");
            Console.WriteLine(formatedString);
        }

        private string getUserInput()
        {
            return Console.ReadLine();
        }

        private void printPrompt(string i_PromptToPrint)
        {
            Console.WriteLine(i_PromptToPrint);
        }

        //private int getLicenseType()
        //{
        //    eLicenseType licenseTypes = new eLicenseType();

        //    string[] list = (string[])Enum.GetNames(typeof(eLicenseType));
        //    printPrompt("Please choose your license type: ");

        //    bool isValidInput = false;
        //    int userChoice = 0;


        //    while (!isValidInput)
        //    {
        //        string userInput = getUserInput();
        //        isValidInput = Enum.IsDefined(typeof(eLicenseType), userInput);
        //        isValidInput = int.TryParse(userInput, out userChoice);
        //        if (!isValidInput)
        //        {   
        //            displayInvalidInput();
        //        }
        //    }
            
        //    return userChoice;
        //}

        private float getCargoCapacity()
        {
            printPrompt("Please enter the cargo volume capacity: ");
            bool isValidInput = false; 
            float cargoVolume = 0;

            while (!isValidInput) 
            {
                string cargoVolumeInput = getUserInput();
                isValidInput = float.TryParse(cargoVolumeInput, out cargoVolume);

                if (!isValidInput)
                {
                    displayInvalidInput();
                }
            }

            return cargoVolume;
        }

        private int getEngineVolume()
        {
            printPrompt("Please enter the engine's volume: ");
            bool isValidInput = false;
            int engineVolume = 0;

            while (!isValidInput)
            {
                string engineVolumeInput = getUserInput();
                isValidInput = int.TryParse(engineVolumeInput, out engineVolume);

                if (!isValidInput)
                {
                    displayInvalidInput();
                }
            }

            return engineVolume;
        }

        private void displayInvalidInput()
        {
            printPrompt("Invalid input. Please try again");
        }

        private void insertVehicle()
        {
            getLicensePlate(out string licensePlateInput);
            bool isVehicleExists = r_GarageManagement.CheckVehicleExistenceAndUpdateStatus(licensePlateInput, out string message);

            if (isVehicleExists)
            {
                printPrompt(message);
            }
            else
            {
                createNewVehicle(licensePlateInput);
            }
        }

        private void createNewVehicle(string i_LicensePlate)
        {
            getOwnerInformation(out string ownerName, out string ownerPhoneNumber);
            getVehicleType(out string vehicleType);
            getEngineType(out string engineType);
            getVehicleModelName(out string vehicleModelName);
            getWheelManufacturerName(out string wheelManufacturerName);
            getWheelAirPressure(out string tireAirPressure);
            r_GarageManagement.AddVehicle(vehicleType, engineType, ownerName, ownerPhoneNumber, i_LicensePlate, vehicleModelName, wheelManufacturerName, tireAirPressure);

            Dictionary<string, object> requiredProperties = r_GarageManagement.GetVehicleProperties(i_LicensePlate);
            Dictionary<string, string> propertyValues = new Dictionary<string, string>();

            foreach (KeyValuePair<string, object> property in requiredProperties)
            {
                getInput((string)property.Value, out string propertyInput);
                propertyValues.Add(property.Key, propertyInput);
            }

            r_GarageManagement.SetVehicleProperties(i_LicensePlate, propertyValues);
        }

        //private void getVehicleProperties(string i_LicensePlate)
        //{
        //    Dictionary<string, object> vehicleProperties = r_GarageManagement.GetVehicleProperties(i_LicensePlate);

        //    if (vehicleProperties.ContainsKey(VehicleFactory.eVehicleProperties.CarColor.ToString()))
        //    {
        //        printPrompt("Please select the color of the car: ");
        //        printPrompt(r_GarageManagement.GetCarColors());
        //        vehicleProperties[VehicleFactory.eVehicleProperties.CarColor.ToString()] = getUserInput();
        //    }
        //}

        private void getInput(string i_PromptMessage, out string o_ParsedInput)
        {
            printPrompt(i_PromptMessage);
            o_ParsedInput = getUserInput();
        }

        private void getVehicleEnergyPrecentage(out string o_VehicleModelName)
        {
            getInput("Please enter your vehicle's model name: ", out o_VehicleModelName);
            //printPrompt("Please enter your vehicle's model name: ");
            //o_VehicleModelName = getUserInput();
        }

        private void getVehicleModelName(out string o_VehicleModelName)
        {
            getInput("Please enter your vehicle's model name: ", out o_VehicleModelName);
            //printPrompt("Please enter your vehicle's model name: ");
            //o_VehicleModelName = getUserInput();
        }

        private void getWheelManufacturerName(out string o_WheelManufacturerName)
        {
            getInput("Please enter your wheel's manufacturer name: ", out o_WheelManufacturerName);
            //printPrompt("Please enter your wheel's manufacturer name: ");
            //o_WheelManufacturerName = getUserInput();
        }

        private void getWheelAirPressure(out string o_WheelWheelAirPressure)
        {
            getInput("Please enter your wheel's air pressure: ", out o_WheelWheelAirPressure);
            
        }

        private void getLicensePlate(out string o_LicensePlateInput)
        {
            getInput("Please enter the license plate number: ", out o_LicensePlateInput);
            //printPrompt("Please enter the license plate number: ");
            //o_LicensePlateInput = getUserInput();
        }

        private void getOwnerInformation(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            getInput("Please enter your name: ", out o_OwnerName);
            getInput("Please enter your phone number: ", out o_OwnerPhoneNumber);
            //printPrompt("Please enter your name: ");
            //o_OwnerName = getUserInput();

            //printPrompt("Please enter your phone number: ");
            //o_OwnerPhoneNumber = getUserInput();
        }

        private void getVehicleType(out string o_VehicleType)
        {
            printPrompt("Please choose your vehicle type: ");
            printPrompt(r_GarageManagement.GetVehicleTypes());
            o_VehicleType = getUserInput();
        }

        private void getEngineType(out string o_EngineType)
        {
            printPrompt("Please choose your vehicle's engine type: ");
            printPrompt(r_GarageManagement.GetEngineTypes());
            o_EngineType = getUserInput();
        }

        private void filterVehicles()
        {
            bool isInRepair = true, isRepairComplete = true, isRepairPaid = true;
            string userInput = string.Empty;
            string vehiclesByStatus = string.Empty;

            printPrompt("Would you like to see ALL the vehicles currently in the garage: (Y / N)");
            checkIfYorN(out bool isFilterList);

            if (!isFilterList)
            {
                printPrompt("Would you like to see all the vehicles that are currently under 'BeingRepaired' status: (Y / N)");
                checkIfYorN(out isInRepair);

                printPrompt("Would you like to see all the vehicles that are currently under 'RepairCompleted' status: (Y / N)");
                checkIfYorN(out isRepairComplete);

                printPrompt("Would you like to see all the vehicles that are currently under 'RepairPaid' status: (Y / N)");
                checkIfYorN(out isRepairPaid);
            }

            vehiclesByStatus = r_GarageManagement.GetVehiclesByStatus(isInRepair, isRepairComplete, isRepairPaid);
            printPrompt(vehiclesByStatus);
        }

        private void checkIfYorN(out bool o_IsInputYes)
        {
            string userInput = string.Empty;
            
            while (!userInput.ToUpper().Equals("Y") && !userInput.ToUpper().Equals("N"))
            {
                displayInvalidInput();
                userInput = getUserInput();
            }

            o_IsInputYes = userInput.ToUpper().Equals("Y");
        }

        private void changeVehicleStatus()
        {
            getLicensePlate(out string licensePlate);
            printPrompt("Please choose the new status of the vehicle: ");
            printPrompt(r_GarageManagement.GetVehicleTreatmentStatusOptions());
            string desiredStatus = getUserInput();
            r_GarageManagement.ChangeVehicleStatus(licensePlate, desiredStatus);
        }

        private void refuelVehicle()
        {
            getLicensePlate(out string licensePlate);
            printPrompt("Please choose the type of fuel: ");
            printPrompt(r_GarageManagement.GetFuelTypes());
            string selectedFuelType = getUserInput();
            printPrompt("Please enter the amount of fuel you would like to add: ");
            string amountOfFuel = getUserInput();
            r_GarageManagement.RefuelTank(licensePlate, selectedFuelType, amountOfFuel);

        }

        private void rechargeVehicle()
        {
            getLicensePlate(out string licensePlate);
            printPrompt("Please enter the amount of minutes you would like to charge: ");
            string minutesToCharge = getUserInput();
            r_GarageManagement.RechargeBattery(licensePlate, minutesToCharge);
        }
    }
}
