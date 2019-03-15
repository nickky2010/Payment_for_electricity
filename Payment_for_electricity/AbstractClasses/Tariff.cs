using System;

namespace Payment_for_electricity.AbstractClasses
{
    abstract class Tariff
    {
        private static decimal energyPrice;

        public Tariff()
        {

        }
        public Tariff(decimal energyPrice)
        {
            EnergyPrice = energyPrice;
        }

        public static decimal EnergyPrice
        {
            get => energyPrice;
            set
            {
                if (value >= 0)
                {
                    energyPrice = value;
                }
                else
                    throw new Exception("Цена не может быть отрицательной");
            }
        }
        public virtual decimal GetTotalCost(double energyVolume)
        {
            return ((decimal)energyVolume * EnergyPrice);
        }
    }
}
