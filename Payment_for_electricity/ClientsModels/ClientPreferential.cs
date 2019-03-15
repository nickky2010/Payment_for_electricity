using Payment_for_electricity.AbstractClasses;
using Payment_for_electricity.DataTypes;
using System;

namespace Payment_for_electricity.ClientsModels
{
    class ClientPreferential : Client
    {
        public ClientPreferential(string name, string address, Tariff tariff, double energyVolume) : base(name, address, tariff, energyVolume)
        {
            Type = ClientType.ClientPreferential;
        }
        protected override void SetTariff(Tariff t)
        {
            Type typeThis = GetType();
            Type typeTariff = t.GetType();
            if (typeTariff.Name == TariffType.TariffPreferential.ToString())
                tariff = t;
            else
                throw new Exception("Клиенту " + Name + " с типом клиента " + typeThis.Name + " невозможно установить тариф " + typeTariff.Name +
                    ".\nТариф должен быть " + TariffType.TariffPreferential.ToString());
        }
    }
}
