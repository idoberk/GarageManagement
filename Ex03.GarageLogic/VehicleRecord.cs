using System;

namespace Ex03.GarageLogic
{
    internal class VehicleRecord
    {
        private string m_VehicleOwnerName;
        private string m_PhoneOwner;
        private eVehicleStatus m_VehicleStatus;
        private Vehicle m_Vehicle;

        public enum eVehicleStatus
        {
            BeingRepaired = 1,
            RepairComplete,
            RepairPaid
        }

        public string VehicleOwnerName
        {
            get { return m_VehicleOwnerName; }
            set { m_VehicleOwnerName = value; }
        }

        public string PhoneOwner
        {
            get { return m_PhoneOwner; }
            set { m_PhoneOwner = value; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        internal Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        internal VehicleRecord(Vehicle i_VehicleToAdd, string i_Name, string i_PhoneOwner)
        {
            VehicleOwnerName = i_Name;
            PhoneOwner = i_PhoneOwner;
            // VehicleStatus = BeingTreated;
            Vehicle = i_VehicleToAdd;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleOwnerName}, {PhoneOwner}, {VehicleStatus}");
        }
    }
}
