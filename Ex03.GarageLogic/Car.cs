using System;

namespace Ex03.GarageLogic
{
    public class Car
    {
        private eCarColor m_CarColor;
        private eNumOfDoors m_NumOfDoors;

        public enum eCarColor
        {
            Blue,
            Black,
            White,
            Grey
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }
    }
}
