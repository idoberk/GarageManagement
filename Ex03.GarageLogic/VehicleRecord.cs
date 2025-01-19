using System;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleRecord
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

        private string VehicleOwnerName
        {
            get { return m_VehicleOwnerName; }
            set
            {
                if (string.IsNullOrEmpty(value) || !value.All(char.IsLetter))
                {
                    throw new ArgumentException($"{value} is an invalid name (must only contain letters and can't be empty).");
                }

                m_VehicleOwnerName = value;
            }
        }

        private string PhoneOwner
        {
            get { return m_PhoneOwner; }
            set
            {
                if (value.Length != 10 || !value.All(char.IsDigit))
                {
                    throw new ArgumentException($"{value} is an invalid phone number (Must contain only digits and be of length 10).");
                }

                m_PhoneOwner = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set 
            {
                if(!Enum.IsDefined(typeof(eVehicleStatus), value))
                {
                    throw new ArgumentException($"{value} is an invalid status");
                }

                m_VehicleStatus = value;
            }
        }

        internal Vehicle Vehicle
        {
            get { return m_Vehicle; }
            private set { m_Vehicle = value; }
        }

        internal VehicleRecord(Vehicle i_VehicleToAdd, string i_Name, string i_PhoneOwner)
        {
            VehicleOwnerName = i_Name;
            PhoneOwner = i_PhoneOwner;
            VehicleStatus = eVehicleStatus.BeingRepaired;
            Vehicle = i_VehicleToAdd;
        }

        internal static string GetVehicleTreatmentStatusOptions()
        {
            StringBuilder treatmentOptions = new StringBuilder();

            foreach(eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
            {
                treatmentOptions.AppendLine(string.Format($"{(int)status}. {status.ToString()}"));
            }

            return treatmentOptions.ToString();
        }

        public override string ToString()
        {
            StringBuilder vehicleRecordInfo = new StringBuilder();

            vehicleRecordInfo.Append(string.Format("{0}'s owner name: {1}{5}"
                                            + "Owner's phone number: {2}{5}"
                                            + "{0}'s status: {3}{5}"
                                            + "==============={5}"
                                            + "{4}"
                , Vehicle.GetType().Name, VehicleOwnerName, PhoneOwner, VehicleStatus, Vehicle, Environment.NewLine)); 

            return vehicleRecordInfo.ToString();
        }
    }
}
