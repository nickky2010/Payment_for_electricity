using Payment_for_electricity.AbstractClasses;
using System;

namespace Payment_for_electricity.TarifsModels
{
    class TariffLimit : Tariff
    {
        int limit;
        double koeff;

        public int Limit
        {
            get => limit;
            set
            {
                if (value >= 0)
                    limit = value;
                else
                    throw new Exception("Отрицательный лимит");
            }
        }
        public double Koeff
        {
            get => koeff;
            set
            {
                if (value >= 0)
                    koeff = value;
                else
                    throw new Exception("Отрицательный коэфициент");
            }
        }
        public TariffLimit(int limit = 250, double koeff = 1 / 3d)
        {
            Limit = limit;
            Koeff = koeff;
        }
        public TariffLimit()
        {

        }
        public override decimal GetTotalCost(double energyVolume)
        {
            decimal newPrice = EnergyPrice * (decimal)(1 + Koeff);
            decimal temp = base.GetTotalCost(energyVolume);
            return ((energyVolume <= limit) ? temp : temp + (decimal)(energyVolume - limit) * newPrice);
        }
    }
}
