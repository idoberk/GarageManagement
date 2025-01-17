using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void Recharge(float i_HoursToCharge)
        {
            if (CurrentEnergyAmount + i_HoursToCharge > MaxEnergyCapacity || CurrentEnergyAmount + i_HoursToCharge < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity);
            }

            CurrentEnergyAmount += i_HoursToCharge;
        }
    }
}
