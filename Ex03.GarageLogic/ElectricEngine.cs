using System;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public void Recharge(float i_HoursToCharge)
        {
            if (this.CurrentEnergyAmount + i_HoursToCharge > this.MaxEnergyCapacity || this.CurrentEnergyAmount + i_HoursToCharge < 0)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergyCapacity);
            }

            this.CurrentEnergyAmount += i_HoursToCharge;
        }

    }
}
