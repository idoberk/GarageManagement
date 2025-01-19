using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        private string m_LicensePlate;
        private string m_ModelName;
        private float m_EnergyPercentage;
        private Engine m_Engine;
        private List<Wheel> m_Wheels;
        protected abstract List<eEngineType> SupportedEngineTypes { get; }
        protected abstract Dictionary<eEngineType, float> MaxTankCapacity { get; }

        internal abstract Dictionary<string, object> VehicleProperties { get; }

        internal string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        internal string LicensePlate
        {
            get { return m_LicensePlate; }
            set { m_LicensePlate = value; }
        }

        internal float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ValueOutOfRangeException(0, 100);
                } 

                m_EnergyPercentage = value;
            }
        }

        internal Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        internal List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        internal Vehicle(string i_LicensePlate, string i_VehicleModelName)
        {
            LicensePlate = i_LicensePlate;
            ModelName = i_VehicleModelName;
            Wheels = new List<Wheel>();
        }

        internal void InitializeVehicleEngine(eEngineType i_EngineType)
        {
            if (!SupportedEngineTypes.Contains(i_EngineType))
            {
                throw new ArgumentException($"{i_EngineType} engine is not supported for {GetType().Name}.");
            }

            if (i_EngineType == eEngineType.Fuel)
            {
               Engine = new FuelEngine(MaxTankCapacity[i_EngineType], 0f);

            } 
            else
            {
                Engine = new ElectricEngine(MaxTankCapacity[i_EngineType], 0f);
            }

            Engine.EngineType = i_EngineType;
        }

        internal void UpdateEnergyPercentage()
        {
            if (Engine is ElectricEngine electricEngine)
            {
                EnergyPercentage = (electricEngine.RemainingHoursInBattery / electricEngine.MaxBatteryCapacity) * 100f;
            }
            else if(Engine is FuelEngine fuelEngine)
            {
                EnergyPercentage = (fuelEngine.RemainingLitersInTank / fuelEngine.MaxTankCapacity) * 100f;
            }
        }

        internal abstract void SetProperty(string i_PropertyName, string i_PropertyValue);

        protected string VehicleInformation()
        {
            string vehicleInfo = string.Format(@"License plate: {0}
Model name: {1}", m_LicensePlate, m_ModelName); // add  owner name + status , wheels information, energy-type and status
            
            return vehicleInfo;
        }
    }
}
