using Payment_for_electricity.AbstractClasses;

namespace Payment_for_electricity.Collections
{
    class ClientCollection
    {
        // метод для вычисления общей суммы оплаты всех клиентов за потреблённую энергию.
        public static decimal SumPayment(params Client[] clients)
        {
            decimal sum = 0;
            foreach (Client client in clients)
            {
                if (client != null)
                {
                    sum += client.GetEnergyPayment();
                }
            }
            return sum;
        }
        // метод для вычисления общего размера льгот всех клиентов
        public static decimal SumConcession(params Client[] clients)
        {
            decimal sumPayment = 0;
            decimal sumPaymentWithoutConcession = 0;
            foreach (Client client in clients)
            {
                if (client != null)
                {
                    sumPayment += client.GetEnergyPayment();
                    sumPaymentWithoutConcession += (decimal)client.EnergyVolume * Tariff.EnergyPrice;
                }
            }
            return (sumPaymentWithoutConcession - sumPayment);
        }
    }
}
