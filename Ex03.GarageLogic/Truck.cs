using System.Text;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Engine;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private readonly int r_NumOfWheels = 14;
        private float m_CargoVolume;
        private bool m_IsCargoCooled;

        protected override List<eEngineType> SupportedEngineTypes { get; } = new List<eEngineType>
                                                                               {eEngineType.Fuel};

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        internal bool IsCargoCooled
        {
            get { return m_IsCargoCooled; }
            set { m_IsCargoCooled = value; }
        }

        internal Truck(string i_LicensePlate, string i_ModelName, eEngineType i_Engine) : base(i_LicensePlate, i_ModelName, i_Engine)
        {
            return;
        }

        public override string ToString()
        {
            return string.Format($"{VehicleInformation()}, {CargoVolume}, {IsCargoCooled}");
        }

        //internal static string GetCargoVolume()
        //{
        //    StringBuilder bla = new StringBuilder();

        //    //foreach (eCarColor carColor in Enum.GetValues(typeof(eCarColor)))
        //    //{
        //        colorOptions.AppendLine(string.Format("cargo Volume"));
        //    //}
        //    string bla = "Please enter "

        //    return colorOptions.ToString();
        //}
    }
}
