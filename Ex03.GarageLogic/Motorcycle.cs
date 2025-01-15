using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private readonly int r_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel, eEngineType.Electric};

        public enum eLicenseType
        {
            A1,
            A2,
            B1,
            B2
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        internal int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        internal Motorcycle(string i_LicensePlate, string i_ModelName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            return;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {EngineVolume}, {LicenseType}");
        }
    }
}
