using System;

namespace Ex03.GarageLogic
{
    public class Truck
    {
        private float m_CargoVolume;
        private bool m_IsCargoCooled;


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
    }
}
