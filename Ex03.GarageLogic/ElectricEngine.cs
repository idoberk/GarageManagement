using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        private float m_MaxBatteryCapacity;
        private float m_RemainingHoursInBattery;

        public float MaxBatteryCapacity
        {
            get { return m_MaxBatteryCapacity; }
            private set { m_MaxBatteryCapacity = value; }
        }

        public float RemainingHoursInBattery
        {
            get { return m_RemainingHoursInBattery; }
            private set
            {
                if(value < 0 || value > MaxBatteryCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxBatteryCapacity);
                }

                m_RemainingHoursInBattery = value;
            }
        }

        internal ElectricEngine(float i_MaxBatteryCapacity, float i_RemainingHoursInBattery)
        {
            MaxBatteryCapacity = i_MaxBatteryCapacity;
            RemainingHoursInBattery = i_RemainingHoursInBattery;
        }

        internal void SetRemainingMinutesInBattery(string i_RemainingMinutes)
        {
            ManageGarage.ParseToFloat(i_RemainingMinutes, out float parsedRemainingMinutes);

            RemainingHoursInBattery = parsedRemainingMinutes / 60f;
        }

        internal void Recharge(float i_HoursToCharge)
        {
            if (RemainingHoursInBattery + i_HoursToCharge > MaxBatteryCapacity || RemainingHoursInBattery + i_HoursToCharge < 0)
            {
                throw new ValueOutOfRangeException(0, (MaxBatteryCapacity - RemainingHoursInBattery) * 60f);
            }

            RemainingHoursInBattery += i_HoursToCharge;
        }

        internal static string GetRemainingMinutesInBatteryPrompt()
        {
            return "Please insert how many minutes remain for the battery: ";
        }

        public override string ToString()
        {
            StringBuilder electricEngineInfo = new StringBuilder();

            electricEngineInfo.Append(string.Format("{0}"
                                                    +"{1:0.00} hours remain in the battery "
                                                    + "out of a total capacity of {2} hours{3}"
                , base.ToString(), RemainingHoursInBattery, MaxBatteryCapacity, Environment.NewLine));

            return electricEngineInfo.ToString();
        }
    }
}
