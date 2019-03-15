using Payment_for_electricity.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Payment_for_electricity.VisualExtends
{
    class ClientTable :Table
    {
        public ClientTable (string tableHeader = "", params Column[] columns) : base(tableHeader, columns)
        {

        }

        public void Print(Client[] clients)
        {
            PrintHead(TableHeader != "");
            int n = 1;
            foreach (Client client in clients)
            {
                if (client!=null)
                {
                    PrintString(n++.ToString(), client.Name, client.Address, client.Type.ToString(), 
                        client.EnergyVolume.ToString(), client.GetEnergyPayment().ToString("f2"));
                }
            }
            PrintBottom();
        }
    }
}
