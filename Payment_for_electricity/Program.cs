using Payment_for_electricity.AbstractClasses;
using Payment_for_electricity.ClientsModels;
using Payment_for_electricity.Collections;
using Payment_for_electricity.Comparators;
using Payment_for_electricity.TarifsModels;
using Payment_for_electricity.VisualExtends;
using System;

namespace Payment_for_electricity
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tariff.EnergyPrice = 0.15m;
                Tariff[] tariffs = {
                    new TariffOrdinary(),
                    new TariffLimit(250, 1/3d),
                    new TariffPreferential (2/3d),
                    new TariffForHeatingNeeds (1/15d)
                };
                Client[] clients = {
                    new ClientOrdinary("Ужиков Е.Е.", "Кирова 43", tariffs[0], 200d),
                    new ClientOrdinary("Чижиков В.В.", "Советская 133/7", tariffs[0], 330d),
                    new ClientOrdinary("Пыжиков В.Ф.","Ватрушкина 2/4", tariffs[0], 150d),
                    new ClientLimit ("Иванов А.А.", "Юбилейная 8/56", tariffs[1], 250d),
                    new ClientLimit ("Петров Е.В.", "Советская 133/7", tariffs[1], 2000d),
                    new ClientLimit ("Сидоров А.А.", "Междугородняя 8/33", tariffs[1], 300d),
                    new ClientPreferential("ОАО \"Химзавод\"", "Химзаводская 5", tariffs[2], 140000d),
                    new ClientPreferential("КУП \"ГОРЭЛЕКТРОТРАНСПОРТ\"", "Троллейбусная 1", tariffs[2], 110000d),
                    new ClientPreferential("ОАО \"Гомсельмаш\"", "Шоссейная 41", tariffs[2], 155000d),
                    new ClientForHeatingNeeds ("СОАО \"Гомелькабель\"","Советская 151", tariffs[3], 40000d),
                    new ClientForHeatingNeeds ("УO \"ГГУ им. Ф.Скорины\"", "ул. Советская, 104", tariffs[3], 14000d),
                    new ClientForHeatingNeeds ("ОАО \"Электроаппаратура\"", "Советская 157", tariffs[3], 31000d),
                };
                Table table = new ClientTable("Потребители электроэнергии:", new Table.Column("№", 3), new Table.Column("Имя клиента", 27),
                    new Table.Column("Адрес клиента", 20), new Table.Column("Тип клиента", 23), new Table.Column("Потребленная энергия,кВт/ч", 15),
                    new Table.Column("Стоимость", 13));
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                ((ClientTable)table).Print(clients);
                                break;
                            }
                        case 1:
                            {
                                //  1.	Отсортировать массив по количеству потреблённой клиентами энергии по убыванию.
                                Array.Sort(clients, ClientComparer.CompareByEnergyVolumeBack);
                                table.TableHeader = "Сортировка по количеству потреблённой клиентами энергии по убыванию";
                                ((ClientTable)table).Print(clients);
                                break;
                            }
                        case 2:
                            {
                                //  2.	Отсортировать массив по величине оплаты клиентами по возрастанию.
                                Array.Sort(clients, ClientComparer.CompareByEnergyPayment);
                                table.TableHeader = "Сортировка по величине оплаты клиентами по возрастанию";
                                ((ClientTable)table).Print(clients);
                                break;
                            }
                        case 3:
                            {
                                //  3.	Упорядочить массив по типу клиентов (первыми – обычные, затем – с лимитом, затем – льготные и последними – для нужд отопления)
                                Array.Sort(clients, new ClientComparer().Compare);
                                table.TableHeader = "Сортировка по типу клиентов (первыми – обычные, 2-ми - с лимитом, затем – льготные и последними – для нужд отопления)";
                                ((ClientTable)table).Print(clients);
                                //  4.	Вычислить общую сумму SUM оплаты всех клиентов за потреблённую энергию.
                                Console.WriteLine("Общая сумма оплаты всех клиентов за потреблённую энергию: " + ClientCollection.SumPayment(clients).ToString("f2"));
                                //  5.	Вычислить общий размер льготы LG= SUM0–SUM. Где SUM0 – общая сумма оплаты всех клиентов за потребленную энергию, 
                                //  если бы они все были обычными (без льгот и лимитов).
                                decimal lg = ClientCollection.SumConcession(clients);
                                if (lg <= 0)
                                    Console.WriteLine("Льготы отсутствуют. Общий размер переплат составил: " + Math.Abs(lg).ToString("f2"));
                                else
                                    Console.WriteLine("Общий размер льготы: " + lg.ToString("f2"));
                                break;
                            }
                    }
                    PauseAndClearConsole(i);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PauseAndClearConsole(int n)
        {
            if (n != 3)
                Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            else
                Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
