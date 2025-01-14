using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private readonly int r_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected override List<Engine.eEngineType> SupportedEngineTypes { get; } = new List<Engine.eEngineType>
                                                                               {Engine.eEngineType.Fuel, Engine.eEngineType.Electric};

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

        public Motorcycle(string i_LicensePlate, string i_ModelName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            return;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {EngineVolume}, {LicenseType}");
        }
    }
}
