using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly int r_NumOfWheels = 14;
        private float m_CargoVolume;
        private bool m_IsCargoCooled;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel};

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public bool IsCargoCooled
        {
            get { return m_IsCargoCooled; }
            set { m_IsCargoCooled = value; }
        }

        public Truck(string i_LicensePlate, string i_ModelName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            return;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CargoVolume}, {IsCargoCooled}");
        }
    }
}
