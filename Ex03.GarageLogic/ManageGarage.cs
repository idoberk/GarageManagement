using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ManageGarage
    {
        private Dictionary<string, VehicleRecord> m_RecordsList = new Dictionary<string, VehicleRecord>();

        public Dictionary<string, VehicleRecord> Records
        {
            get { return m_RecordsList; }
        }

        public void ChangeVehicleStatus(string i_LicensePlate, VehicleRecord.eVehicleStatus i_DesiredStatus)
        {
            Records[i_LicensePlate].VehicleStatus = i_DesiredStatus;
        }

        public void CheckIfVehicleExists(string i_LicensePlate)
        {
            if (!Records.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException($"Error occurred. {i_LicensePlate} doesn't exist in the system.");
            }
        }

        public void AddVehicle(Vehicle i_VehicleToAdd, string i_Name, string i_PhoneOwner)
        {
            VehicleRecord vehicleRecordToAdd = new VehicleRecord(i_VehicleToAdd, i_Name, i_PhoneOwner);
            //CheckIfVehicleExists(i_VehicleToAdd.LicensePlate);
            Records.Add(i_VehicleToAdd.LicensePlate, vehicleRecordToAdd);
        }
    }
}