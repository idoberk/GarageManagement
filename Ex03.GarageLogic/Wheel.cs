using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentTireAirPressure;
        private float m_MaxTireAirPressure;

        public enum eMaxTireAirPressure
        {
            Truck = 29,
            Motorcycle = 32,
            Car = 34
        }
        
        public void InflateWheel(float i_AddAirPressure)
        {
            if(m_CurrentTireAirPressure + i_AddAirPressure > m_MaxTireAirPressure || m_CurrentTireAirPressure + i_AddAirPressure < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxTireAirPressure);
            }

            m_CurrentTireAirPressure += i_AddAirPressure;
        }

        // TODO: Override ToString (functionality section 7).
    }

   
}