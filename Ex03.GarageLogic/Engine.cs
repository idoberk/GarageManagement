using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        //private float m_MaxEnergyCapacity;
        //private float m_CurrentEnergyAmount;
        private eEngineType m_EngineType;

        //public float MaxEnergyCapacity
        //{
        //    get { return m_MaxEnergyCapacity; }
        //    set { m_MaxEnergyCapacity = value; }
        //}

        //public float CurrentEnergyAmount
        //{
        //    get { return m_CurrentEnergyAmount; }
        //    set { m_CurrentEnergyAmount = value; }
        //}

        public eEngineType EngineType
        {
            get { return m_EngineType; }
            set { m_EngineType = value; }
        }

        public enum eEngineType
        {
            Fuel = 1,
            Electric
        }

        internal static string GetEngineTypes()
        {
            StringBuilder engineTypes = new StringBuilder();

            foreach (eEngineType engineType in Enum.GetValues(typeof(eEngineType)))
            {
                engineTypes.AppendLine(string.Format($"{(int)engineType}. {engineType.ToString()}"));
            }

            return engineTypes.ToString();
        }
    }
}