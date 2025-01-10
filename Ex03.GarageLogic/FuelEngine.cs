using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;
       
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; ; }
            set { m_FuelType = value; }
        }

        public void Refuel(float i_FuelAmountToFill, eFuelType i_FuelType)
        {
            if (this.CurrentEnergyAmount + i_FuelAmountToFill > this.MaxEnergyCapacity || this.CurrentEnergyAmount + i_FuelAmountToFill < 0)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergyCapacity);
            }
            else if (i_FuelType != FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }

            this.CurrentEnergyAmount += i_FuelAmountToFill;

        }
        
    }
}
