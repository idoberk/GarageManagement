using System;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
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
            createNewVehicle("hello", "Mazda", "Michelin");
            printMainMenu();
            string userInput = getUserInput();

            

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

        private int getLicenseType()
        {
            Motorcycle.eLicenseType licenseTypes = new Motorcycle.eLicenseType();

            string[] list = (string[])Enum.GetNames(typeof(Motorcycle.eLicenseType));
            printPrompt("Please choose your license type: ");

            bool isValidInput = false;
            int userChoice = 0;


            while (!isValidInput)
            {
                string userInput = getUserInput();
                isValidInput = Enum.IsDefined(typeof(Motorcycle.eLicenseType), userInput);
                isValidInput = int.TryParse(userInput, out userChoice);
                if (!isValidInput)
                {   
                    displayInvalidInput();
                }
            }
            
            return userChoice;
        }

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

        }

        private Vehicle createNewVehicle(string i_LicensePlate, string i_VehicleModelName, string i_ManufacturerName)
        {
            Vehicle newVehicle = VehicleFactory.CreateVehicle(i_LicensePlate, i_VehicleModelName, i_ManufacturerName, eVehicleType.Truck, eEngineType.Electric);
            // VehicleFactory.eVehicleType vehicleType = new VehicleFactory.eVehicleType();
            return newVehicle;
        }
    }
}
