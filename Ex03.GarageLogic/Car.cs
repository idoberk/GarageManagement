using System;
using System.Text;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        // TODO: FormatException when parsing;
        // TODO: All validations needs to happen in the logic;

        private readonly int r_NumOfWheels = 5;
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel, eEngineType.Electric};

        protected override Dictionary<eEngineType, float> MaxTankCapacity { get; } = new Dictionary<eEngineType, float>()
        { { eEngineType.Fuel, 52f }, { eEngineType.Electric, 5.4f } };

        internal override Dictionary<string, object> VehicleProperties { get; } = new Dictionary<string, object>()
            { { nameof(eCarColor), string.Empty }, { nameof(eNumOfDoors), string.Empty } };

        public enum eCarColor
        {
            Blue = 1,
            Black,
            White,
            Grey
        }

        public enum eNumOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private eCarColor CarColor
        {
            get { return m_CarColor; }
            set
            {
                if (!Enum.IsDefined(typeof(eCarColor), value))
                {
                    throw new ArgumentException("Invalid color input");
                }

                m_CarColor = value;
            }
        }

        private eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set
            {
                if (!Enum.IsDefined(typeof(eNumOfDoors), value))
                {
                    throw new ArgumentException("Invalid doors input");
                }

                m_NumOfDoors = value;
            }
        }

        internal Car(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, float i_CurrentTireAirPressure, eEngineType i_Engine) :
            base(i_LicensePlate, i_ModelName)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, i_CurrentTireAirPressure, (float)eMaxTireAirPressure.Car));
            }

            InitializeVehicleEngine(i_Engine);

            if (i_Engine is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Octan95;
            }
        }

        private void setCarColor(string i_CarColor)
        {
            ManageGarage.ParseToInteger(i_CarColor, out int parsedCarColor);

            CarColor = (eCarColor)parsedCarColor;
        }

        private void setNumberOfDoors(string i_NumOfDoors)
        {
            ManageGarage.ParseToInteger(i_NumOfDoors, out int parsedNumOfDoors);

            NumOfDoors = (eNumOfDoors)parsedNumOfDoors;
        }

        internal override void SetProperty(string i_PropertyName, string i_Value)
        {
            switch (i_PropertyName)
            {
                case nameof(eCarColor):
                    setCarColor(i_Value);
                    break;

                case nameof(eNumOfDoors):
                    setNumberOfDoors(i_Value);
                    break;

                default:
                    throw new ArgumentException($"Property {i_PropertyName} is not supported for {GetType().Name}");
            }
        }

        internal static string GetDoors()
        {
            StringBuilder doorOptions = new StringBuilder();

            foreach(eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
            {
                doorOptions.AppendLine(string.Format($"{(int)numOfDoors}. {numOfDoors.ToString()}"));
            }

            return doorOptions.ToString();
        }

        internal static string GetColors()
        {
            StringBuilder colorOptions = new StringBuilder();

            foreach(eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
            {
                colorOptions.AppendLine(string.Format($"{(int)carColor}. {carColor.ToString()}"));
            }

            return colorOptions.ToString();
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CarColor}, {NumOfDoors} "); 
        }
    }
}
