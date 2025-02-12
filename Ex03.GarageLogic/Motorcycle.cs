﻿using System;
using System.Text;
using System.Collections.Generic;
using static Ex03.GarageLogic.Engine;
using static Ex03.GarageLogic.Wheel;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType> 
            {eEngineType.Fuel, eEngineType.Electric};

        protected override Dictionary<eEngineType, float> MaxTankCapacity { get; } = new Dictionary<eEngineType, float>()
            { { eEngineType.Fuel, 6.2f }, { eEngineType.Electric, 2.9f } };

        internal override Dictionary<string, object> VehicleProperties { get; } = new Dictionary<string, object>()
            { { nameof(eLicenseType), string.Empty }, { nameof(EngineVolume), 0 } };

        public enum eLicenseType
        {
            A1 = 1,
            A2,
            B1,
            B2
        }

        private eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new ArgumentException("Invalid license type input.");
                }
                
                m_LicenseType = value;
            }
        }

        internal int EngineVolume
        {
            get { return m_EngineVolume; }
            set
            {
                if (value >= 0)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new ArgumentException($"{GetType().Name}'s engine capacity cannot be negative.");
                }
            }
        }

        internal Motorcycle(string i_LicensePlate, string i_ModelName, string i_ManufacturerName, float i_CurrentTireAirPressure, eEngineType i_EngineType) 
            : base(i_LicensePlate, i_ModelName)
        {
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, i_CurrentTireAirPressure, (float)eMaxTireAirPressure.Motorcycle));
            }

            InitializeVehicleEngine(i_EngineType);

            if (i_EngineType is eEngineType.Fuel)
            {
                ((FuelEngine)Engine).FuelType = FuelEngine.eFuelType.Octan98;
            }
        }

        internal static string GetLicenseTypes()
        {
            StringBuilder licenseTypes = new StringBuilder();

            licenseTypes.AppendLine("Please choose the license type: ");
            foreach(eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                licenseTypes.AppendLine(string.Format($"{(int)licenseType}. {licenseType.ToString()}"));
            }

            return licenseTypes.ToString();
        }

        internal static string GetEngineVolumePrompt()
        {
            string engineVolumePrompt = "Please enter the engine volume: (An integer) ";

            return engineVolumePrompt;
        }

        private void setLicenseType(string i_LicenseType)
        {
            ManageGarage.ParseToInteger(i_LicenseType, out int parsedLicenseType);

            LicenseType = (eLicenseType)parsedLicenseType;
        }

        private void setEngineVolume(string i_EngineVolume)
        {
            ManageGarage.ParseToInteger(i_EngineVolume, out int parsedEngineVolume);

            EngineVolume = parsedEngineVolume;
        }

        internal override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            switch(i_PropertyName)
            {
                case nameof(eLicenseType):
                    setLicenseType(i_PropertyValue);
                    break;

                case nameof(EngineVolume):
                    setEngineVolume(i_PropertyValue);
                    break;

                default:
                    throw new ArgumentException($"Property {i_PropertyName} is not supported for {GetType().Name}");
            }
        }

        public override string ToString()
        {
            StringBuilder motorcycleInfo = new StringBuilder();

            motorcycleInfo.Append(string.Format("{0}{4}"
                                                + "The {1}'s engine volume is {2} CC {4}"
                                                + "The {1}'s license type is {3}{4}"
                , base.ToString(), GetType().Name, EngineVolume, LicenseType.ToString(), Environment.NewLine));

            return motorcycleInfo.ToString();
        }
    }
}
