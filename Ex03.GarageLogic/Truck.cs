using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;

namespace Ex03.GarageLogic
{
    // TODO: Check type validation in the CargoVolume;
    internal class Truck : Vehicle
    {
        private readonly int r_NumOfWheels = 14;
        private float m_CargoVolume;
        private bool m_IsCargoCooled;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel};

        protected override Dictionary<eEngineType, float> MaxTankCapacity { get; } = new Dictionary<eEngineType, float>()
        { { eEngineType.Fuel, 125f } };

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value >= 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException("Truck's cargo volume cannot be negative");
                }
            }
        }

        internal bool IsCargoCooled
        {
            get { return m_IsCargoCooled; }
            set { m_IsCargoCooled = value; }
        }

        internal Truck(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, (float)eMaxTireAirPressure.Car));
            }

            InitializeVehicleEngine(i_Engine);

            if (i_Engine is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Soler;
            }
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CargoVolume}, {IsCargoCooled}");
        }

        //internal static string GetCargoVolume()
        //{
        //    StringBuilder bla = new StringBuilder();

        //    //foreach (eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
        //    //{
        //        colorOptions.AppendLine(string.Format("cargo Volume"));
        //    //}
        //    string bla = "Please enter "

        //    return colorOptions.ToString();
        //}
    }
}
