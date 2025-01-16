using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;

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

        //private Dictionary<string, object> m_Properties = new Dictionary<string, object>()
        //{ { "car color", CarColor } };


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

        internal eCarColor CarColor
        {
            get { return m_CarColor; }
            set
            {
                if (Enum.IsDefined(typeof(eCarColor), value))
                {
                    m_CarColor = value;
                }
                else
                {
                    throw new ArgumentException("Invalid color input");
                }
            }
        }

        internal eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set
            {
                if (Enum.IsDefined(typeof(eNumOfDoors), value))
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    throw new ArgumentException("Invalid doors input");
                }
            }
        }

        internal Car(string i_LicensePlate, string i_ModelName, string i_ManufacturerName ,eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {

            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, (float)eMaxTireAirPressure.Car));
            }

            Initialize(i_Engine);

            if (i_Engine is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Octan95;
            }
        }

        internal void SetCarColor(string i_CarColor) 
        {
            int.TryParse(i_CarColor, out int intCarColor);
            //throw exception
            if (Enum.IsDefined(typeof(eCarColor), intCarColor))
            {
                CarColor = (eCarColor)intCarColor;
            }
        }

        

        internal static string GetDoors()
        {
            StringBuilder doorOptions = new StringBuilder();

            foreach (eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
            {
                doorOptions.AppendLine(string.Format($"{(int)numOfDoors}. {numOfDoors.ToString()}"));
            }

            return doorOptions.ToString();
        }

        internal static string GetColors()
        {
            StringBuilder colorOptions = new StringBuilder();

            foreach (eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
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
