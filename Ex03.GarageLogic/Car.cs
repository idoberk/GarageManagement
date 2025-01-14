using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        // TODO: FormatException when parsing;
        // TODO: All validations needs to happen in the logic;

        private readonly int r_NumOfWheels = 5;
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel, eEngineType.Electric};

        private Dictionary<eEngineType, float> m_TankCapacity = new Dictionary<eEngineType, float>()
        { { eEngineType.Fuel, 5.4f }, { eEngineType.Electric, 2.9f } };

        public enum eCarColor
        {
            Blue,
            Black,
            White,
            Grey
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
      
        public Car(string i_LicensePlate, string i_ModelName, string i_ManufacturerName ,eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {

            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, (float)Wheel.eMaxTireAirPressure.Car));
            }

            if (m_TankCapacity.ContainsKey(i_Engine))
            {
                Engine.MaxEnergyCapacity = m_TankCapacity[i_Engine];
            }



            //if (i_Engine is FuelEngine)
            //{
            //    ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Octan95;
            //    Engine.MaxEnergyCapacity = fuelEngine.TankCapacity["Car"];
            //}
            //else if (Engine is ElectricEngine electricEngine)
            //{
            //    Engine.MaxEnergyCapacity = electricEngine.TankCapacity["Car"];
            //}

            return;
        }

        protected override void SetVehicleProperties()
        {
            setDoors = 
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CarColor}, {NumOfDoors} "); 
        }
    }
}
