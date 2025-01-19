using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly ManageGarage r_GarageManagement = new ManageGarage();

        private enum eMenuChoices
        {
            InsertVehicle = 1,
            DisplayVehicleLicensePlates,
            ChangeStatus,
            InflateWheelsToMaxAirPressure,
            RefuelTank,
            RechargeBattery,
            DisplayVehicleInformation,
            Quit
        }

        public UserInterface()
        {
            bool programIsRunning = true;
            string userInput = string.Empty;


            while (programIsRunning)
            {
                printMainMenu();
                userInput = getUserInput();

                try
                {
                    int userChoice = int.Parse(userInput);

                    switch ((eMenuChoices)userChoice)
                    {
                        case eMenuChoices.InsertVehicle:
                            insertVehicle();
                            break;

                        case eMenuChoices.DisplayVehicleLicensePlates:
                            filterVehicles();
                            break;

                        case eMenuChoices.ChangeStatus:
                            changeVehicleStatus();
                            break;

                        case eMenuChoices.InflateWheelsToMaxAirPressure:
                            inflateWheelsToMax();
                            break;

                        case eMenuChoices.RefuelTank:
                            refuelVehicle();
                            break;

                        case eMenuChoices.RechargeBattery:
                            rechargeVehicle();
                            break;

                        case eMenuChoices.DisplayVehicleInformation:
                            displayVehicleInformation();
                            break;

                        case eMenuChoices.Quit:
                            programIsRunning = false;
                            break;

                        default:
                            displayInvalidInput();
                            break;
                    }
                } catch(Exception ex)
                {
                    displayPrompt(ex.Message);
                }
            }
        }

        private void printMainMenu()
        {
            string menuString = string.Format("1. Insert your vehicle{0}"
                                              + "2. Display existing vehicles license plates{0}"
                                              + "3. Change vehicle status{0}"
                                              + "4. Inflate wheels to maximum air pressure{0}"
                                              + "5. Refuel tank{0}"
                                              + "6. Recharge battery{0}"
                                              + "7. Display vehicle information{0}"
                                              + "8. Quit{0}"
                                              + "======================{0}"
                                              + "Please select what you would like to do: ", Environment.NewLine);
            displayPrompt(menuString);
        }

        private static string getUserInput()
        {
            return Console.ReadLine();
        }

        private static void displayPrompt(string i_PromptToDisplay)
        {
            Console.WriteLine(i_PromptToDisplay.TrimEnd());
        }

        private static void displayInvalidInput()
        {
            displayPrompt("Invalid input. Please try again.");
        }

        private void insertVehicle()
        {
            getLicensePlate(out string vehicleLicensePlateInput);
            bool isVehicleExists =
                r_GarageManagement.CheckVehicleExistenceAndUpdateStatus(vehicleLicensePlateInput, out string message);

            if (isVehicleExists)
            {
                displayPrompt(message);
            }
            else
            {
                createNewVehicle(vehicleLicensePlateInput);
            }
        }

        private void createNewVehicle(string i_VehicleLicensePlate)
        {
            bool isAddedSuccessfully = false;

            while (!isAddedSuccessfully)
            {
                try
                {
                    getOwnerInformation(out string ownerName, out string ownerPhoneNumber);
                    getVehicleType(out string vehicleType);
                    getEngineType(out string engineType);
                    getVehicleModelName(out string vehicleModelName);
                    getWheelManufacturerName(out string wheelManufacturerName);
                    getWheelAirPressure(out string tireAirPressure);
                    r_GarageManagement.AddVehicle(vehicleType, engineType, ownerName, ownerPhoneNumber,
                        i_VehicleLicensePlate, vehicleModelName, wheelManufacturerName, tireAirPressure);

                    Dictionary<string, string> propertyValues = getVehicleProperties(i_VehicleLicensePlate);

                    r_GarageManagement.SetVehicleProperties(i_VehicleLicensePlate, propertyValues);
                    isAddedSuccessfully = true;
                }
                catch (Exception ex)
                {
                    displayPrompt(ex.Message);
                    displayPrompt("Please repeat the process.");
                }
            }

            displayPrompt($"Vehicle has been successfully added!");
        }

        private Dictionary<string, string> getVehicleProperties(string i_VehicleLicensePlate)
        {
            Dictionary<string, object> requiredProperties = r_GarageManagement.GetVehicleProperties(i_VehicleLicensePlate);
            Dictionary<string, string> propertyValues = new Dictionary<string, string>();

            foreach (KeyValuePair<string, object> property in requiredProperties)
            {
                displayPromptAndGetUserInput((string)property.Value, out string propertyInput);
                propertyValues.Add(property.Key, propertyInput);
            }

            return propertyValues;
        }

        private static void displayPromptAndGetUserInput(string i_PromptMessage, out string o_ParsedInput)
        {
            displayPrompt(i_PromptMessage);
            o_ParsedInput = getUserInput();
        }

        private static void getVehicleModelName(out string o_VehicleModelName)
        {
            displayPromptAndGetUserInput("Please enter your vehicle's model name: ", out o_VehicleModelName);
        }

        private static void getWheelManufacturerName(out string o_WheelManufacturerName)
        {
            displayPromptAndGetUserInput("Please enter your wheel's manufacturer name: ", out o_WheelManufacturerName);
        }

        private static void getWheelAirPressure(out string o_WheelWheelAirPressure)
        {
            displayPromptAndGetUserInput("Please enter your wheel's air pressure: ", out o_WheelWheelAirPressure);
        }

        private static void getLicensePlate(out string o_VehicleLicensePlateInput)
        {
            displayPromptAndGetUserInput("Please enter the license plate number: ", out o_VehicleLicensePlateInput);
        }

        private static void getOwnerInformation(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            displayPromptAndGetUserInput("Please enter your name: ", out o_OwnerName);
            displayPromptAndGetUserInput("Please enter your phone number: ", out o_OwnerPhoneNumber);
        }

        private void getVehicleType(out string o_VehicleType)
        {
            displayPrompt("Please choose your vehicle type: ");
            displayPromptAndGetUserInput(r_GarageManagement.GetVehicleTypes(), out o_VehicleType);
        }

        private void getEngineType(out string o_EngineType)
        {
            displayPrompt("Please choose your vehicle's engine type: ");
            displayPromptAndGetUserInput(r_GarageManagement.GetEngineTypes(), out o_EngineType);
        }

        private void filterVehicles()
        {
            bool isInRepair = true, isRepairComplete = true, isRepairPaid = true;
            string vehiclesByStatus = string.Empty;

            displayPrompt("Would you like to see all license plates of vehicles that are currently in the garage: (Y / N)");
            checkIfYorN(out bool isFilterList);

            if (!isFilterList)
            {
                displayPrompt(
                    "Would you like to see all license plates of vehicles that are currently under 'BeingRepaired' status: (Y / N)");
                checkIfYorN(out isInRepair);

                displayPrompt(
                    "Would you like to see all license plates of vehicles that are currently under 'RepairCompleted' status: (Y / N)");
                checkIfYorN(out isRepairComplete);

                displayPrompt(
                    "Would you like to see all license plates of vehicles that are currently under 'RepairPaid' status: (Y / N)");
                checkIfYorN(out isRepairPaid);
            }

            vehiclesByStatus = r_GarageManagement.GetVehiclesByStatus(isInRepair, isRepairComplete, isRepairPaid);
            displayPrompt(vehiclesByStatus);
        }

        private static void checkIfYorN(out bool o_IsInputYes)
        {
            string userInput = getUserInput();

            while (!userInput.ToUpper().Equals("Y") && !userInput.ToUpper().Equals("N"))
            {
                displayInvalidInput();
                userInput = getUserInput();
            }

            o_IsInputYes = userInput.ToUpper().Equals("Y");
        }

        private void changeVehicleStatus()
        {
            getLicensePlate(out string vehicleLicensePlate);
            displayPrompt("Please choose the new status of the vehicle: ");
            displayPromptAndGetUserInput(r_GarageManagement.GetVehicleTreatmentStatusOptions(), out string desiredStatus);
            r_GarageManagement.ChangeVehicleStatus(vehicleLicensePlate, desiredStatus);
        }

        private void refuelVehicle()
        {
            getLicensePlate(out string vehicleLicensePlate);
            displayPrompt("Please choose the type of fuel: ");
            displayPromptAndGetUserInput(r_GarageManagement.GetFuelTypes(), out string selectedFuelType);
            displayPromptAndGetUserInput("Please enter the amount of fuel you would like to add: ", out string amountOfFuel);
            r_GarageManagement.RefuelTank(vehicleLicensePlate, selectedFuelType, amountOfFuel);
        }

        private void rechargeVehicle()
        {
            getLicensePlate(out string vehicleLicensePlate);
            displayPromptAndGetUserInput("Please enter the amount of minutes you would like to charge: ", out string minutesToCharge);
            r_GarageManagement.RechargeBattery(vehicleLicensePlate, minutesToCharge);
        }

        private void inflateWheelsToMax()
        {
            getLicensePlate(out string vehicleLicensePlate);
            r_GarageManagement.InflateWheelsToMax(vehicleLicensePlate);
        }

        private void displayVehicleInformation()
        {
            getLicensePlate(out string vehicleLicensePlate);

            string vehicleInformation = r_GarageManagement.GetVehicleInformation(vehicleLicensePlate);

            displayPrompt(vehicleInformation);
        }
    }
}
