using System;
using System.Text;

namespace Ex03.GarageLogic
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

        private string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{value} is an invalid name (can't be empty).");
                }
                
                m_ManufacturerName = value;
            }
        }

        internal float CurrentTireAirPressure
        {
            get { return m_CurrentTireAirPressure; }
            private set
            {
                if (value < 0 || value > MaxTireAirPressure)
                {
                    throw new ValueOutOfRangeException(0, MaxTireAirPressure);
                }

                m_CurrentTireAirPressure = value;
            }
        }

        internal float MaxTireAirPressure
        {
            get { return m_MaxTireAirPressure; }
            private set { m_MaxTireAirPressure = value; }
        }

        internal Wheel(string i_ManufacturerName, float i_CurrentTireAirPressure, float i_MaxTireAirPressure)
        {
            ManufacturerName = i_ManufacturerName;
            MaxTireAirPressure = i_MaxTireAirPressure;       
            CurrentTireAirPressure = i_CurrentTireAirPressure;
        }

        internal void InflateWheel(float i_AddAirPressure)
        {
            if(CurrentTireAirPressure + i_AddAirPressure > MaxTireAirPressure || CurrentTireAirPressure + i_AddAirPressure < 0)
            {
                throw new ValueOutOfRangeException(0, MaxTireAirPressure);
            }

            CurrentTireAirPressure += i_AddAirPressure;
        }

        public override string ToString()
        {
            StringBuilder wheelInfo = new StringBuilder();

            wheelInfo.Append(string.Format("{0}'s manufacturer name is {1} "
                                            + "and the tire air pressure is {2}{3}"
                , GetType().Name, ManufacturerName, CurrentTireAirPressure, Environment.NewLine));

            return wheelInfo.ToString();
        }
    }

   
}