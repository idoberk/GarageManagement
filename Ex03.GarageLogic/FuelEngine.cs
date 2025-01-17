using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set
            {
                if (Enum.IsDefined(typeof(eFuelType), value))
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid fuel type input");
                }
            }
        }

        public void Refuel(float i_FuelAmountToFill, eFuelType i_FuelType)
        {
            if (CurrentEnergyAmount + i_FuelAmountToFill > MaxEnergyCapacity || CurrentEnergyAmount + i_FuelAmountToFill < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity);
            }

            CurrentEnergyAmount += i_FuelAmountToFill;

        }
    }
}
