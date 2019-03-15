using Payment_for_electricity.AbstractClasses;
using System.Collections;

namespace Payment_for_electricity.Comparators
{
    class ClientComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Client c1 = x as Client;
            Client c2 = y as Client;
            return (c1 == null && c2 == null) ? 0 : (c1 == null ? -1 : ((c2 == null) ? 1 : c1.Type.CompareTo(c2.Type)));
        }

        // по возрастанию по счету за потреблённую клиентом электроэнергию
        public static int CompareByEnergyPayment(Client c1, Client c2)
        {
            return (c1 == null && c2 == null) ? 0 : (c1 == null ? -1 : ((c2 == null) ? 1 : c1.GetEnergyPayment().CompareTo(c2.GetEnergyPayment())));
        }

        // по убыванию по счету за потреблённую клиентом электроэнергию
        public static int CompareByEnergyPaymentBack(Client c1, Client c2)
        {
            return (c1 == null && c2 == null) ? 0 : (c1 == null ? -1 : ((c2 == null) ? 1 : -(c1.GetEnergyPayment().CompareTo(c2.GetEnergyPayment()))));
        }

        // по возрастанию по количеству потреблённой клиентами электроэнергии
        public static int CompareByEnergyVolume(Client c1, Client c2)
        {
            return (c1 == null && c2 == null) ? 0 : (c1 == null ? -1 : ((c2 == null) ? 1 : c1.EnergyVolume.CompareTo(c2.EnergyVolume)));
        }

        // по убыванию по количеству потреблённой клиентами электроэнергии
        public static int CompareByEnergyVolumeBack(Client c1, Client c2)
        {
            return (c1 == null && c2 == null) ? 0 : (c1 == null ? -1 : ((c2 == null) ? 1 : -(c1.EnergyVolume.CompareTo(c2.EnergyVolume))));
        }
    }
}
