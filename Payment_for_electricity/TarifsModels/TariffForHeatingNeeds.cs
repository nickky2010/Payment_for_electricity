using Payment_for_electricity.AbstractClasses;
using System;

namespace Payment_for_electricity.TarifsModels
{
    class TariffForHeatingNeeds : Tariff
    {
        double koeff;
        public double Koeff
        {
            get => koeff;
            set
            {
                if (value >= 0 && value < 1)
                {
                    koeff = value;
                }
                else
                {
                    throw new Exception("Коэффициент льготы задан неверно!");
                }
            }
        }
        public TariffForHeatingNeeds()
        {

        }

        public TariffForHeatingNeeds(double koeff = 1 / 15d)
        {
            Koeff = koeff;
        }
        public override decimal GetTotalCost(double energyVolume)
        {
            return ((decimal)energyVolume * EnergyPrice * (decimal)Koeff);
        }
    }
}
