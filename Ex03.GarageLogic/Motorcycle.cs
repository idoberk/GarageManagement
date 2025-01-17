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

        internal override Dictionary<string, object> VehicleProperties { get; } = new Dictionary<string, object>()
            { { nameof(eLicenseType), string.Empty }, { nameof(EngineVolume), 0 } };

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
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new ArgumentException("Invalid license type input");
                }
                
                m_LicenseType = value;
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
                    throw new ArgumentException($"{GetType().Name}'s engine capacity cannot be negative");
                }
            }
        }

        internal Motorcycle(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, float i_CurrentTireAirPressure, eEngineType i_Engine) 
            : base(i_LicensePlate, i_ModelName)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, i_CurrentTireAirPressure, (float)eMaxTireAirPressure.Motorcycle));
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

        internal static string GetEngineVolume()
        {
            string engineVolumePrompt = "Please enter the engine volume: (An integer) ";

            return engineVolumePrompt;
        }

        private void setLicenseType(string i_LicenseType)
        {
            ManageGarage.ParseToInteger(i_LicenseType, out int parsedLicenseType);

            LicenseType = (eLicenseType)parsedLicenseType;
        }

        private void setEngineVolume(string i_EngineVolume)
        {
            ManageGarage.ParseToInteger(i_EngineVolume, out int parsedEngineVolume);

            EngineVolume = parsedEngineVolume;
        }

        internal override void SetProperty(string i_PropertyName, string i_Value)
        {
            switch(i_PropertyName)
            {
                case nameof(eLicenseType):
                    setLicenseType(i_Value);
                    break;

                case nameof(EngineVolume):
                    setEngineVolume(i_Value);
                    break;

                default:
                    throw new ArgumentException($"Property {i_PropertyName} is not supported for {GetType().Name}");
            }
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {EngineVolume}, {LicenseType}");
        }
    }
}
