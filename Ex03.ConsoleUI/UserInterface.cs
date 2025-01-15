using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly ManageGarage garage = new ManageGarage();

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
        // הדפסה של סוגי הרכבים תהיה מתוך מפעל כלי הרכב ותעבור על הETYPE ככה לא נצטרך לשנות את הUI בטרקטור

        public void RunProgram()
        {
            // createNewVehicle("hello", "Mazda", "Michelin");
            createNewVehicle();
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

        }

        private void createNewVehicle()
        {
            getLicensePlate(out string licensePlateInput);
            getOwnerInformation(out string ownerName, out string ownerPhoneNumber);
            getVehicleType(out int vehicleType);
            getEngineType(out int engineType);
            garage.AddVehicle(vehicleType, engineType, ownerName, ownerPhoneNumber, licensePlateInput);

            getVehicleProperties(vehicleType, licensePlateInput);

        }

        private void getLicensePlate(out string o_LicensePlateInput)
        {
            printPrompt("Please enter the license plate number: ");
            o_LicensePlateInput = getUserInput();
        }

        private void getOwnerInformation(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            printPrompt("Please enter your name: ");
            o_OwnerName = getUserInput();

            printPrompt("Please enter your phone number: ");
            o_OwnerPhoneNumber = getUserInput();
        }

        private void getVehicleType(out int o_VehicleType)
        {
            printPrompt("Please choose your vehicle type: ");
            printPrompt(garage.GetVehicleTypes());
            int.TryParse(getUserInput(), out o_VehicleType);
        }

        private void getEngineType(out int o_EngineType)
        {
            printPrompt("Please choose your vehicle's engine type: ");
            printPrompt(garage.GetEngineTypes());
            int.TryParse(getUserInput(), out o_EngineType);
        }

        private void getVehicleProperties(int i_VehicleType, string i_LicensePlate)
        {
            //entermodelname
            //    enterwheelmanuname
            //        enterfuelamount
            //            enterwheelairpressure


            // { "Car Color": setColor  };
            // print "please nter key;
            // string bla = userinput;
            // stringbuilder func = "methodName" + "(" + bla + ");";
            //{ func }
        }
    }
}
