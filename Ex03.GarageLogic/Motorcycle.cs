using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car;
using System.Text;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;

namespace Ex03.GarageLogic
{
    // TODO: Check type validation in the Engine Volume;
    // TODO: Check if it is possible to make the wheels and fuel type abstract methods in the constructor;
    internal class Motorcycle : Vehicle
    {
        private readonly int r_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType> 
        {eEngineType.Fuel, eEngineType.Electric};

        protected override Dictionary<eEngineType, float> MaxTankCapacity { get; } = new Dictionary<eEngineType, float>()
        { { eEngineType.Fuel, 6.2f }, { eEngineType.Electric, 2.9f } };

        public enum eLicenseType
        {
            A1,
            A2,
            B1,
            B2
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid license type input");
                }
            }
        }

        internal int EngineVolume
        {
            get { return m_EngineVolume; }
            set
            {
                if (value >= 0)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new ArgumentException("Motorcycle's engine capacity cannot be negative");
                }
            }
        }

        internal Motorcycle(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, (float)eMaxTireAirPressure.Motorcycle));
            }

            InitializeVehicleEngine(i_Engine);

            if (i_Engine is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Octan98;
            }
        }

        internal static string GetLicenseTypes()
        {
            StringBuilder licenseTypes = new StringBuilder();

            foreach(eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                licenseTypes.AppendLine(string.Format($"{(int)licenseType}. {licenseType.ToString()}"));
            }

            return licenseTypes.ToString();
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {EngineVolume}, {LicenseType}");
        }
    }
}
