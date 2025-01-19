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
                throw new ValueOutOfRangeException(0, MaxBatteryCapacity);
            }

            RemainingHoursInBattery += i_HoursToCharge;
        }

        internal static string GetRemainingMinutesInBatteryPrompt()
        {
            return "Please insert how many minutes remain for the battery: ";
        }
    }
}
