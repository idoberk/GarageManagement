using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private float m_RemainingLitersInTank;
        private float m_MaxTankCapacity;
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        public float MaxTankCapacity
        {
            get { return m_MaxTankCapacity; }
            set { m_MaxTankCapacity = value; }
        }

        public float RemainingLitersInTank
        {
            get { return m_RemainingLitersInTank; }
            set 
            {
                if (value < 0 || value > MaxTankCapacity)
                {
                    throw new ValueOutOfRangeException(0, MaxTankCapacity);
                }

                m_RemainingLitersInTank = value;
            }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set
            {
                if (!Enum.IsDefined(typeof(eFuelType), value))
                {
                    throw new ArgumentException("Invalid fuel type input");
                }
                
                m_FuelType = value;
            }
        }

        internal FuelEngine(float i_MaxTankCapacity, float i_RemainingLitersInTank)
        {
            MaxTankCapacity = i_MaxTankCapacity;
            RemainingLitersInTank = i_RemainingLitersInTank;
        }

        internal void SetRemainingLitersInTank(string i_RemainingLiters)
        {
            ManageGarage.ParseToFloat(i_RemainingLiters, out float parsedRemainingLiters);

            RemainingLitersInTank = parsedRemainingLiters;
        }

        internal void Refuel(float i_FuelAmountToFill, eFuelType i_FuelType)
        {
            if (RemainingLitersInTank + i_FuelAmountToFill > MaxTankCapacity || RemainingLitersInTank + i_FuelAmountToFill < 0)
            {
                throw new ValueOutOfRangeException(0, MaxTankCapacity);
            }

            RemainingLitersInTank += i_FuelAmountToFill;
        }

        internal static string GetFuelTypes()
        {
            StringBuilder fuelTypes = new StringBuilder();

            foreach(eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                fuelTypes.AppendLine(string.Format($"{(int)fuelType}. {fuelType.ToString()}"));
            }

            return fuelTypes.ToString();
        }

        internal static string GetRemainingLitersInTankPrompt()
        {
            return "Please insert how many liters remain in the tank: ";
        }
    }
}
