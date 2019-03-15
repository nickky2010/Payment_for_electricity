using Payment_for_electricity.DataTypes;
using System;

namespace Payment_for_electricity.AbstractClasses
{
    abstract class Client : IComparable
    {
        public ClientType Type { get; protected set; }
        public string Name { get; set; }
        public string Address { get; set; }
        double energyVolume;
        public double EnergyVolume
        {
            get => energyVolume;
            set
            {
                if (value >= 0)
                {
                    energyVolume = value;
                }
                else
                    throw new Exception("Количество потребленной энергии не может быть меньше нуля");
            }
        }
        protected Tariff tariff;
        protected abstract void SetTariff(Tariff t);
        public Tariff Tariff
        {
            get => tariff;
            set
            {
                SetTariff(value);
            }
        }
        public Client(string name, string address, Tariff tariff, double energyVolume)
        {
            Name = name;
            Address = address;
            Tariff = tariff;
            EnergyVolume = energyVolume;
        }
        public decimal GetEnergyPayment()
        {
            return (tariff.GetTotalCost(EnergyVolume));
        }

        public int CompareTo(object obj)
        {
            Client cl = obj as Client;
            if (cl != null)
                return (-this.EnergyVolume.CompareTo(cl.EnergyVolume));
            return 1;
        }
    }
}
