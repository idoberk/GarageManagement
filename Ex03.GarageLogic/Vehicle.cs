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

        internal Dictionary<string, object> VehicleProperties { get; } = new Dictionary<string, object>();

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
                if (value >= 0 && value <= 100)
                {
                    m_EnergyPercentage = value;
                } 
                else
                {
                    throw new ValueOutOfRangeException(0, 100);
                }
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

        internal Vehicle(string i_LicensePlate, string i_VehicleModelName, eEngineType i_Engine)
        {
            LicensePlate = i_LicensePlate;
            ModelName = i_VehicleModelName;
            Wheels = new List<Wheel>();
        }

        internal void InitializeVehicleEngine(eEngineType i_Engine)
        {
            if (!SupportedEngineTypes.Contains(i_Engine))
            {
                throw new ArgumentException($"{i_Engine} engine is not supported for {GetType().Name}.");
            }

            if (i_Engine == eEngineType.Fuel)
            {
                Engine = new FuelEngine();
            }
            else
            {
                Engine = new ElectricEngine();
            }

            if (MaxTankCapacity.ContainsKey(i_Engine))
            {
                Engine.MaxEnergyCapacity = MaxTankCapacity[i_Engine];
            }

            Engine.EngineType = i_Engine;
        }

        internal void UpdateEnergyPercentage()
        {
            EnergyPercentage = (Engine.CurrentEnergyAmount / Engine.MaxEnergyCapacity) * 100f;
        }

        public string VehicleInformation()
        {
            string vehicleInfo = string.Format(@"License plate: {0}
Model name: {1}", m_LicensePlate, m_ModelName); // add  owner name + status , wheels information, energy-type and status
            
            return vehicleInfo;
        }
    }
}
