using System;
using System.Linq;

namespace Payment_for_electricity.AbstractClasses
{
    abstract class Table
    {
        //  Создаем класс «Столбец», описывающий объекты-столбцы
        public class Column
        {
            public string ColumnHeader { get; set; }  // заголовок столбца
            private int columnWidth;
            public int ColumnWidth                    // ширина столбца
            {
                get => columnWidth;
                set
                {
                    if (ColumnHeader.Length >= value)
                        columnWidth = ColumnHeader.Length;
                    else
                        columnWidth = value;
                }
            }
            // Конструктор с параметрами
            public Column(string columnHeader = "", int columnWidth = 1)
            {
                ColumnHeader = columnHeader;
                ColumnWidth = columnWidth;
            }
        }
        //  Закрытые поля: заголовок таблицы, заголовки столбцов, ширина первого столбца, ширина второго столбца.
        public string TableHeader { get; set; }          // заголовок таблицы
        public Column[] Columns;
        protected int TotalColumnLength { get; set; }        // общая длина колонок
        protected int CountOfColumns { get; set; }           // количество колонок
        protected int TableWidth { get; set; }               // длина таблицы (без учета 2 боковых линий=+2)
        //  Конструктор с параметрами.
        protected Table(string headTable = "", params Column[] column)
        {
            TableHeader = headTable;
            // считаем данные по колонкам
            CountOfColumns = column.Count();
            Columns = column;
            //this.column = new Column[CountCol];
            //for (int i = 0; i < CountCol; i++)
            //{
            //    this.column[i] = new Column();
            //    this.column[i] = column[i];
            //    TotalLenghtCol += this.Columns[i].ColumnWidth;
            //}
            for (int i = 0; i < CountOfColumns; i++)
            {
                TotalColumnLength += Columns[i].ColumnWidth;
            }
            TableWidth = TotalColumnLength + CountOfColumns - 1;
        }
        public Column this[int columnNumber]
        {
            get
            {
                if (Columns.Length > 0 && columnNumber > 0 && columnNumber <= Columns.Length)
                {
                    return Columns[columnNumber - 1];
                }
                else
                    throw new Exception("Ошибка! Невозможно обратиться к колонке №" + columnNumber + " таблицы " + TableHeader + ", так как ее не существует!");
            }
            set
            {
                if (Columns.Length > 0 && columnNumber > 0 && columnNumber <= Columns.Length)
                {
                    Columns[columnNumber - 1] = value;
                }
                else
                    throw new Exception("Ошибка! Невозможно обратиться к колонке №" + columnNumber + " таблицы " + TableHeader + ", так как ее не существует!");
            }
        }

        //  Метод для вывода шапки таблицы.
        protected void PrintHead(bool printHeadTable = true)
        {
            // первая строчка
            if (printHeadTable)
            {
                Console.Write("╔");
                for (int i = 0; i < TableWidth; i++)
                    Console.Write("═");
                Console.WriteLine("╗");
                // вторая строчка - заголовок таблицы
                ShowCol(TableWidth - ShowColLeft(TableWidth, TableHeader) - TableHeader.Length, TableHeader, TableWidth);
                Console.WriteLine("║");
                // третья строчка
                Console.Write("╠");
            }
            else
                Console.Write("╔");
            for (int i = 0; i < CountOfColumns; i++)
            {
                for (int m = 0; m < Columns[i].ColumnWidth; m++)
                    Console.Write("═");
                if (i != CountOfColumns - 1)
                    Console.Write("╦");
            }
            if (printHeadTable)
                Console.WriteLine("╣");
            else
                Console.WriteLine("╗");
            // четвертая строчка заголовки столбцов
            for (int i = 0; i < CountOfColumns; i++)
            {
                ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, Columns[i].ColumnHeader) - Columns[i].ColumnHeader.Length, Columns[i].ColumnHeader, Columns[i].ColumnWidth);
            }
            Console.WriteLine("║");
            // пятая строчка
            Console.Write("╠");
            for (int i = 0; i < CountOfColumns; i++)
            {
                for (int m = 0; m < Columns[i].ColumnWidth; m++)
                    Console.Write("═");
                if (i != CountOfColumns - 1)
                    Console.Write("╬");
                else
                    Console.WriteLine("╣");
            }
        }

        //  Метод для вывода строки таблицы (возможные входные параметры: string, int, double, decimal).
        protected void PrintString(params string[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountOfColumns; i++)
            {
                // если количество колонок меньше или равно количеству переданных значений то записываем все значения (остальные будут отброшены)
                if (i < countValue)
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, value[i]) - value[i].Length, value[i], Columns[i].ColumnWidth);
                // если количество переданных значений меньше количества колонок, то колонки пустые
                else
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, "") - 0, "", Columns[i].ColumnWidth);
            }
            Console.WriteLine("║");
        }

        protected void PrintString(params int[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountOfColumns; i++)
            {
                if (i < countValue)
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), Columns[i].ColumnWidth);
                else
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, "") - 0, "", Columns[i].ColumnWidth);
            }
            Console.WriteLine("║");
        }

        //  Переопределенный метод для вывода строки таблицы (входные параметры – double).
        protected void PrintString(params double[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountOfColumns; i++)
            {
                if (i < countValue)
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), Columns[i].ColumnWidth);
                else
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, "") - 0, "", Columns[i].ColumnWidth);
            }
            Console.WriteLine("║");
        }

        //  Переопределенный метод для вывода строки таблицы (входные параметры – decimal).
        protected void PrintString(params decimal[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountOfColumns; i++)
            {
                if (i < countValue)
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), Columns[i].ColumnWidth);
                else
                    ShowCol(Columns[i].ColumnWidth - ShowColLeft(Columns[i].ColumnWidth, "") - 0, "", Columns[i].ColumnWidth);
            }
            Console.WriteLine("║");
        }

        //  Метод для вывода низа таблицы.
        protected void PrintBottom()
        {
            Console.Write("╚");
            for (int i = 0; i < CountOfColumns; i++)
            {
                for (int m = 0; m < Columns[i].ColumnWidth; m++)
                    Console.Write("═");
                if (i != CountOfColumns - 1)
                    Console.Write("╩");
            }
            Console.WriteLine("╝");
        }

        // находим количество пробелов слева, печатаем их и возвращаем их количество
        private int ShowColLeft(int COL, string str)
        {
            int spLeft = (COL - str.Length) / 2;
            Console.Write("║");
            for (int i = 0; i < spLeft; i++)
                Console.Write(" ");
            return spLeft;
        }

        // печатаем значение и пробелы справа
        private void ShowCol(int spRight, string str, int COL)
        {
            if (str.Length > COL)
                Console.Write(str.Substring(0, COL));
            else
                Console.Write(str);
            for (int i = 0; i < spRight; i++)
                Console.Write(" ");
        }
    }
}
