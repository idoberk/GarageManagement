﻿namespace Ex03.GarageLogic
{
    internal class Wheel
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

        internal string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        internal float CurrentTireAirPressure
        {
            get { return m_CurrentTireAirPressure; }
            set { m_CurrentTireAirPressure = value; }
        }

        internal float MaxTireAirPressure
        {
            get { return m_MaxTireAirPressure; }
            set { m_MaxTireAirPressure = value; }
        }

        internal Wheel(string i_ManufacturerName, float i_MaxTireAirPressure)
        {
            ManufacturerName = i_ManufacturerName;
            MaxTireAirPressure = i_MaxTireAirPressure;       
        }

        internal void InflateWheel(float i_AddAirPressure)
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