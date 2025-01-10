using System;

namespace Ex03.GarageLogic
{
    public class Engine
    {
        private float m_MaxEnergyCapacity;
        private float m_CurrentEnergyAmount; 
        private eEngineType m_EngineType;

        public float MaxEnergyCapacity
        {
            get { return m_MaxEnergyCapacity; }
            set { m_MaxEnergyCapacity = value; }
        }

        public float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set { m_CurrentEnergyAmount = value; }
        }

        public enum eEngineType
        {
            FuelEngine, 
            ElctricEngine
        }

        if(input == FuelEngine)
            {
                m_EngineType = FuelEngine;
    }



    }
}