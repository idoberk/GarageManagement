using System;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private readonly int m_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public enum eLicenseType
        {
            A1,
            A2,
            B1,
            B2
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public Motorcycle(string i_ModelName, string i_LicensePlate)
        {
            //foreach Wheel in
            //    Wheels.add(manufacturer, Wheel.eMaxTireAirPressure.Motorcycle)
        
        }
    }
}
