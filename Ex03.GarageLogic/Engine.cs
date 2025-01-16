
namespace Ex03.GarageLogic
{
    public abstract class Engine
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

        //protected abstract void MaxCapacity(string i_VehicleType);
    }
}