using System;
using static Ex03.GarageLogic.Wheel;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        private float m_MaxBatteryCapacity;
        private float m_RemainingHoursInBattery;

        public float MaxBatteryCapacity
        {
            get { return m_MaxBatteryCapacity; }
            set { m_MaxBatteryCapacity = value; }
        }

        public float RemainingHoursInBattery
        {
            get { return m_RemainingHoursInBattery; }
            set
            {
                if(value < 0 || value > MaxBatteryCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxBatteryCapacity);
                }

                m_RemainingHoursInBattery = value;
            }
        }

        public ElectricEngine(float i_MaxBatteryCapacity, float i_RemainingHoursInBattery)
        {
            MaxBatteryCapacity = i_MaxBatteryCapacity;
            RemainingHoursInBattery = i_RemainingHoursInBattery;
        }

        public void Recharge(float i_HoursToCharge)
        {
            if (RemainingHoursInBattery + i_HoursToCharge > MaxBatteryCapacity || RemainingHoursInBattery + i_HoursToCharge < 0)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryCapacity);
            }

            RemainingHoursInBattery += i_HoursToCharge;
        }
    }
}
