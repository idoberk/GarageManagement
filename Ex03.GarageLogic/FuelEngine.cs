using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Motorcycle;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;
        //private static Dictionary<eVehicleType, eFuelType> VehicleFuelType { get; } = new Dictionary<eVehicleType, eFuelType>()
        //{ { eVehicleType.Car, eFuelType.Octan95 }, { eVehicleType.Motorcycle, eFuelType.Octan98 }, { eVehicleType.Truck, eFuelType.Soler } };

        //private Dictionary<string, float> m_TankCapacity = new Dictionary<string, float>()
        //{ { "Car", 52f }, { "Motorcycle", 6.2f }, { "Truck", 125f } };

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        //public Dictionary<string, float> TankCapacity
        //{
        //    get { return m_TankCapacity; }
        //    set { m_TankCapacity = value; }
        //}

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set
            {
                if (Enum.IsDefined(typeof(eFuelType), value))
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid fuel type input");
                }
            }
        }

        public void Refuel(float i_FuelAmountToFill, eFuelType i_FuelType)
        {
            if (CurrentEnergyAmount + i_FuelAmountToFill > MaxEnergyCapacity || CurrentEnergyAmount + i_FuelAmountToFill < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergyCapacity);
            }
            //else if (i_FuelType != FuelType)
            //{
            //    throw new ArgumentException("Incorrect fuel type.");
            //}

            CurrentEnergyAmount += i_FuelAmountToFill;
        }

        //protected override void MaxCapacity()
        //{
        //    if (EngineType == eEngineType.Fuel)
        //    {
        //        MaxEnergyCapacity = TankCapacity["Car"];
        //    }
        //}
    }
}
