using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;
        //private Dictionary<string, float> m_TankCapacity = new Dictionary<string, float>()
        //{ { "Car", 52f }, { "Motorcycle", 6.2f }, { "Truck", 125f } };

        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        //public Dictionary<string, float> TankCapacity
        //{
        //    get { return m_TankCapacity; }
        //    set { m_TankCapacity = value; }
        //}

        public eFuelType FuelType
        {
            get { return m_FuelType; }
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

        //protected override void MaxCapacity()
        //{
        //    if (EngineType == eEngineType.Fuel)
        //    {
        //        this.MaxEnergyCapacity = TankCapacity["Car"];
        //    }
        //}
    }
}
