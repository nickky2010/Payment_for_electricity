using Payment_for_electricity.AbstractClasses;
using System;

namespace Payment_for_electricity.TarifsModels
{
    class TariffPreferential : Tariff
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
        public TariffPreferential(double koeff = 2 / 3d)
        {
            Koeff = koeff;
        }
        public TariffPreferential()
        {

        }
        public override decimal GetTotalCost(double energyVolume)
        {
            return ((decimal)energyVolume * EnergyPrice * (decimal)Koeff);
        }
    }
}
