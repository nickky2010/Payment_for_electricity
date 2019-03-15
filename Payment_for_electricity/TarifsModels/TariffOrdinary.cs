using Payment_for_electricity.AbstractClasses;

namespace Payment_for_electricity.TarifsModels
{
    class TariffOrdinary : Tariff
    {
        public TariffOrdinary()
        {

        }
        public override decimal GetTotalCost(double energyVolume)
        {
            return (base.GetTotalCost(energyVolume));
        }
    }
}
