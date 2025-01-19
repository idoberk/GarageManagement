using System;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        private eEngineType m_EngineType;

        public eEngineType EngineType
        {
            get { return m_EngineType; }
            set
            {
                if(!Enum.IsDefined(typeof(eEngineType), value))
                {
                    throw new ArgumentException($"{value} is an invalid engine type.");
                }

                m_EngineType = value;
            }
        }

        public enum eEngineType
        {
            Fuel = 1,
            Electric
        }

        internal static string GetEngineTypes()
        {
            StringBuilder engineTypes = new StringBuilder();

            foreach (eEngineType engineType in Enum.GetValues(typeof(eEngineType)))
            {
                engineTypes.AppendLine(string.Format($"{(int)engineType}. {engineType.ToString()}"));
            }

            return engineTypes.ToString();
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.Append(string.Format("{0} Engine's information {1}"
                                            + "==============={1}"
            , EngineType, Environment.NewLine));

            return engineInfo.ToString();
        }
    }
}