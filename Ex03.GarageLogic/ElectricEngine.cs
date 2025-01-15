using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        //private Dictionary<string, float> m_TankCapacity = new Dictionary<string, float>()
        //{ { "Car", 5.4f }, { "Motorcycle", 2.9f } };

        //public Dictionary<string, float> TankCapacity
        //{
        //    get { return m_TankCapacity; }
        //    set { m_TankCapacity = value; }
        //}


        public void Recharge(float i_HoursToCharge)
        {
            if (this.CurrentEnergyAmount + i_HoursToCharge > this.MaxEnergyCapacity || this.CurrentEnergyAmount + i_HoursToCharge < 0)
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergyCapacity);
            }

            this.CurrentEnergyAmount += i_HoursToCharge;
        }

        //protected override void MaxCapacity(string i_VehicleType)
        //{
        //    if (EngineType == eEngineType.Electric)
        //    {
        //        this.MaxEnergyCapacity = TankCapacity[];
        //    }
        //}


    }
}
