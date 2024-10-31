using Liczby;

namespace Program
{
    public class Macierz
    {
        Liczba[,] macierz;
        public Macierz(int kolumny, int wiersze)
        {
            if (kolumny < 2 || wiersze < 2)
                throw new Exception("Złe wymiary Macierzy");

            macierz = new Liczba[kolumny, wiersze];
            Wypelnij();
        }
        void Wypelnij()
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    macierz[i, j] = new Liczba(0);
                }
            }
        }
        public void Add(int kolumna, int wiersze, Liczba a)
        {
            macierz[kolumna, wiersze] = a;
        }
        int MaxLength(int kolumna)
        {
            int leng = 0;
            string tem;

            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                tem = Convert.ToString(macierz[i, kolumna]);

                if (tem.Length > leng)
                    leng = tem.Length;
            }

            return leng;
        }
        static string Spaces(int num)
        {
            string spaces = "";

            for (int i = 0; i < num; i++)
                spaces += " ";

            return spaces;
        }
        public string String()
        {
            string[] m = StringTab();
            string wynik = "";

            for (int i = 0; i < m.Length; i++)
            {
                if (i != m.Length - 1)
                    wynik += m[i] + '\n';
                else
                    wynik += m[i];
            }

            return wynik;
        }
        public string[] StringTab()
        {
            string[] wynik = new string[macierz.GetLength(0)];

            int maxlen, tem;
            string element;

            for (int k = 0; k < macierz.GetLength(0); k ++)
                wynik[k] += "| ";

            for (int i = 0; i < macierz.GetLength(1); i++)
            {
                maxlen = MaxLength(i);

                for (int j = 0; j < macierz.GetLength(0); j++)
                {
                    element = Convert.ToString(macierz[j, i]);

                    tem = maxlen - element.Length;

                    wynik[j] += Spaces(tem / 2);
                    wynik[j] += element;
                    wynik[j] += Spaces(tem / 2);
                    wynik[j] += Spaces(tem % 2);
                    wynik[j] += ' ';
                }
            }

            for (int m = 0; m < macierz.GetLength(0); m++)
                wynik[m] += "|";

            return wynik;
        }
        public void TwoRowOperation(int row1, int row2, Liczba operation)
        {
            for (int i = 0; i < macierz.GetLength(1); i++)
            {
                macierz[row1, i] += operation * macierz[row2, i];
            }
        }
        public void OneRowOperation(int row, Liczba operation)
        {
            for (int i = 0; i < macierz.GetLength(1); i++)
            {
                macierz[row, i] = operation * macierz[row, i];
            }
        }
        public void TwoColumnOperation(int col1, int col2, Liczba operation)
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                macierz[i, col1] += operation * macierz[i, col2];
            }
        }
        public void OneColumnOperation(int col, Liczba operation)
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                macierz[i, col] = operation * macierz[i, col];
            }
        }
        public void RowDivision(int row, Liczba num)
        {
            for (int i = 0; i < macierz.GetLength(1); i++)
            {
                macierz[row, i] = macierz[row, i] / num;
            }
        }
        public void ColumnDivision(int col, Liczba num)
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                macierz[i, col] = macierz[i, col] / num;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Macierz macierz = PodajMacierz();
            MatrixOperations(macierz);
        }
        static int[] WymiaryInput()
        {
            Console.Write("Proszę podać wymiary macierzy(np. AxB): ");

            string wymiary = Console.ReadLine();
            Console.Clear();

            string[] tab = wymiary.Split("x");

            return new int[] { Convert.ToInt32(tab[0]), Convert.ToInt32(tab[1]) };
        }
        static Liczba MakeNumber(String text)
        {
            string[] strings = text.Split('/');

            if (strings.Length > 2)
                throw new Exception("Nieprawidłowy format liczby");

            if (strings.Length == 2)
                return new Liczba(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]));

            return new Liczba(Convert.ToInt32(strings[0]));
        }
        static Macierz PodajMacierz()
        {
            int[] wymiary = WymiaryInput();
            Macierz macierz = new Macierz(wymiary[0], wymiary[1]);
            string tem;

            for (int i = 0; i < wymiary[0]; i++)
            {
                for (int j = 0; j < wymiary[1]; j++)
                {
                    Console.WriteLine(macierz.String());
                    Console.WriteLine();
                    Console.Write("Proszę podać liczbe: ");
                    tem = Console.ReadLine();
                    try
                    {
                        macierz.Add(i, j, MakeNumber(tem));
                    }
                    catch
                    {
                        Console.WriteLine("Miepoprawna Liczba (kliklnij przycisk aby kontynuować).");
                        Console.ReadKey();
                        j--;
                    }

                    Console.Clear();
                }
            }

            return macierz;
        }
        static bool IsNumber(char h)
        {
            return '0' <= h && h <= '9';
        }
        static void MatrixDivide(Macierz macierz, string command)
        {
            bool row;

            if (command[0] == 'w')
                row = true;
            else if (command[0] == 'k')
                row = false;
            else
                throw new Exception("Niepoprawna komenda");

            string[] st = command.Split(':');

            string tem = "";

            for (int i = 1; i < st[0].Length; i++)
            {
                tem += st[0][i];
            }

            int pos = Convert.ToInt32(tem) - 1;

            if (row)
                macierz.RowDivision(pos, MakeNumber(st[1]));
            else
                macierz.ColumnDivision(pos, MakeNumber(st[1]));
        }
        static void SingleMatrixChange(Macierz macierz, string command)
        {
            // w1:2
            // -2w1

            string tem = Convert.ToString(command[0]);
            int index = 0;

            for (int i = 1; IsNumber(command[i]) || command[i] == '/'; i++)
            {
                tem += command[i];
                index = i;
            }

            Liczba num;

            if (tem == "-")
                num = MakeNumber("-1");
            else
                num = MakeNumber(tem);

            index++;
            bool row;

            if (command[index] == 'w')
                row = true;
            else if (command[index] == 'k')
                row = false;
            else
                throw new Exception("Nieprawidłowa komenda");

            tem = "";
            index++;

            for (int j = index; j < command.Length; j++)
            {
                tem += command[j];
            }

            if (row)
                macierz.OneRowOperation(Convert.ToInt32(tem) - 1, num);
            else
                macierz.OneColumnOperation(Convert.ToInt32(tem) - 1, num);
        }
        static void ChangeMatrix(Macierz macierz, string command)
        {
            // w1-4w2
            // k1-3/2k2
            // -1w1
            // -w1
            command = command.ToLower();

            bool row;

            if (command[0] == 'w')
                row = true;
            else if (command[0] == 'k')
                row = false;
            else
            {
                SingleMatrixChange(macierz, command);
                return;
            }

            int index = 1;
            string tem = "";
            int pos1, pos2;
            Liczba action;

            for (int i = 1; IsNumber(command[i]); i++)
            {
                tem += command[i];
                index = i;
            }

            pos1 = Convert.ToInt32(tem) - 1;
            index++;

            if (command[index] == ':')
            {
                MatrixDivide(macierz, command);
                return;
            }

            tem = Convert.ToString(command[index]);
            index++;

            for (int j = index; IsNumber(command[j]) || command[j] == '/'; j++)
            {
                tem += command[j];
                index = j;
            }

            if (tem == "-")
                action = MakeNumber("-1");
            else if (tem == "+")
                action = MakeNumber("1");
            else
            {
                action = MakeNumber(tem);
                index++;
            }

            tem = "";

            if ((command[index] == 'w' && !row) || 
                (command[index] == 'k' && row) ||
                (command[index] != 'w' && command[index] != 'k'))
                throw new Exception("Nieprawidłowa komenda");

            index++;

            for (int k = index; k < command.Length && IsNumber(command[k]); k++)
            {
                tem += command[k];
            }

            pos2 = Convert.ToInt32(tem) - 1;

            if (row)
                macierz.TwoRowOperation(pos1, pos2, action);
            else
                macierz.TwoColumnOperation(pos1, pos2, action);
        }
        static string RemoveSpaces(string s)
        {
            string text = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                    text += s[i];
            }

            return text;
        }
        static void MatrixOperations(Macierz macierz)
        {
            bool run = true;
            string text;
            string[] artxt;    // arraytext

            while (run)
            {
                Console.WriteLine(macierz.String());
                Console.WriteLine();
                Console.Write("Podaj operacje do wykonania (np. w1-w2, k3+3k4, w3:2): ");

                text = RemoveSpaces(Console.ReadLine());

                if (text.ToLower() == "exit")
                    run = false;
                else
                {
                    artxt = text.Split(',');
                    for (int i = 0; i < artxt.Length; i++)
                    {
                        try
                        {
                            ChangeMatrix(macierz, artxt[i]);
                        }
                        catch 
                        {
                            Console.WriteLine($"Komenda \"{artxt[i]}\" jest niepoprawna.");
                            Console.ReadKey();
                            break;
                        }
                    }
                }

                Console.Clear();
            }
        }
    }
}