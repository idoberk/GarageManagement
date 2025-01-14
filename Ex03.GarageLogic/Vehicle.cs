using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_LicensePlate;
        private string m_ModelName;
        private float m_EnergyPercentage;
        private Engine m_Engine;
        private List<Wheel> m_Wheels;
        // private eVehicleType m_VehicleType;
        protected abstract List<eEngineType> SupportedEngineTypes { get; }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicensePlate
        {
            get { return m_LicensePlate; }
            set { m_LicensePlate = value; }
        }

        public float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set { m_EnergyPercentage = value; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        //public eVehicleType VehicleType
        //{
        //    get { return m_VehicleType; }
        //    set { m_VehicleType = value; }
        //}

        public Vehicle(string i_LicensePlate, string i_VehicleModelName, eEngineType i_Engine)
        {
            if (!SupportedEngineTypes.Contains(i_Engine))
            {
                throw new ArgumentException($"{i_Engine} is not supported for {this.GetType().Name}.");
            }

            LicensePlate = i_LicensePlate;
            ModelName = i_VehicleModelName;
            if (i_Engine == eEngineType.Fuel)
            {
                Engine = new FuelEngine();
            }
            else
            {
                Engine = new ElectricEngine();
            }
            Wheels = new List<Wheel>();
        }

        public string VehicleInformation()
        {
            string vehicleInfo = string.Format(@"License plate: {0}
Model name: {1}", m_LicensePlate, m_ModelName); // add  owner name + status , wheels information, energy-type and status
            
            return vehicleInfo;
        }
    }
}
