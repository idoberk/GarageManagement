using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 14;
        private float m_CargoVolume;
        private bool m_IsCargoCooled;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel};

        protected override Dictionary<eEngineType, float> MaxTankCapacity { get; } = new Dictionary<eEngineType, float>()
            { { eEngineType.Fuel, 125f } };

        internal override Dictionary<string, object> VehicleProperties { get; } = new Dictionary<string, object>()
            { { nameof(CargoVolume), 0f }, { nameof(IsCargoCooled), false } };

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{GetType().Name}'s cargo volume cannot be negative");
                }
                
                m_CargoVolume = value;
            }
        }

        internal bool IsCargoCooled
        {
            get { return m_IsCargoCooled; }
            set { m_IsCargoCooled = value; }
        }

        internal Truck(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, float i_CurrentTireAirPressure, eEngineType i_EngineType) 
            : base(i_LicensePlate, i_ModelName)
        {
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, i_CurrentTireAirPressure, (float)eMaxTireAirPressure.Truck));
            }

            InitializeVehicleEngine(i_EngineType);

            if (i_EngineType is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Soler;
            }
        }

        private void setCargoVolume(string i_CargoVolume)
        {
            ManageGarage.ParseToFloat(i_CargoVolume, out float parsedCargoVolume);

            CargoVolume = parsedCargoVolume;
        }

        private void setIsCargoCooled(string i_IsCargoCooled)
        {
            bool isCargoCooled = false;

            if(i_IsCargoCooled.ToUpper().Equals("Y"))
            {
                isCargoCooled = true;
            }
            else if(i_IsCargoCooled.ToUpper().Equals("N"))
            {
                isCargoCooled = false;
            }
            else
            {
                throw new FormatException();
            }

            IsCargoCooled = isCargoCooled;
        }

        internal override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            switch (i_PropertyName)
            {
                case nameof(IsCargoCooled):
                    setIsCargoCooled(i_PropertyValue);
                    break;

                case nameof(CargoVolume):
                    setCargoVolume(i_PropertyValue);
                    break;

                default:
                    throw new ArgumentException($"Property {i_PropertyName} is not supported for {GetType().Name}");
            }
        }

        internal static string GetCargoVolumePrompt()
        {
            string cargoVolumePrompt = "Please enter the cargo volume: ";

            return cargoVolumePrompt;
        }

        internal static string GetCargoCooledPrompt()
        {
            string cargoCooledPrompt = "Is the cargo cooled: (Y / N) ";

            return cargoCooledPrompt;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CargoVolume}, {IsCargoCooled}");
        }
    }
}
