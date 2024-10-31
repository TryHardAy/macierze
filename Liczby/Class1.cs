using Liczby;

namespace Liczby
{
    public class Liczba
    {
        int licznik;
        int mianownik;

        public Liczba(int _licznik, int _mianownik = 1)
        {
            if (_mianownik == 0)
                throw new DivideByZeroException();

            licznik = _licznik;
            mianownik = _mianownik;

            uprosc();
        }
        
        void uprosc()
        {
            if (mianownik < 0)
            {
                mianownik = -mianownik;
                licznik = -licznik;
            }

            if (mianownik == 1)
                return;

            int nwd = NWD(mianownik, licznik);

            mianownik /= nwd;
            licznik /= nwd;
        }
        int NWD(int num1, int num2)
        {
            if (num1 == 0 && num2 == 0)
                return 1;

            if (num1 == 0)
                return num2;
            if (num2 == 0)
                return num1;

            if (num1 < 0)
                num1 = -num1;

            if (num2 < 0)
                num2 = -num2;

            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 -= num2;
                else
                    num2 -= num1;
            }

            return num1;
        }
        public double Double()
        {
            return licznik / (double) mianownik;
        }
        public int Int()
        {
            return licznik / mianownik;
        }
        public override string ToString()
        {
            if (mianownik == 1)
                return $"{licznik}";

            return $"{licznik}/{mianownik}";
        }
        public static Liczba operator +(Liczba a, Liczba b)
        {
            if (a.mianownik == b.mianownik)
                return new Liczba(a.licznik + b.licznik,a.mianownik);

            int licznik = a.licznik * b.mianownik + b.licznik * a.mianownik;
            int mianownik = a.mianownik * b.mianownik;

            return new Liczba(licznik, mianownik);
        }
        public static Liczba operator -(Liczba a, Liczba b)
        {
            Liczba c = new Liczba(-b.licznik, b.licznik);
            return a + c;
        }
        public static Liczba operator *(Liczba a, Liczba b)
        {
            return new Liczba(a.licznik * b.licznik, a.mianownik * b.mianownik);
        }
        public static Liczba operator /(Liczba a, Liczba b)
        {
            Liczba c = new Liczba(b.mianownik, b.licznik);
            return a * c;
        }
        public static Liczba operator +(Liczba a)
        {
            return a;
        }
        public static Liczba operator -(Liczba a)
        {
            return new Liczba(-a.licznik, a.mianownik);
        }
        public static Liczba operator +(Liczba a, int b)
        {
            return new Liczba(a.licznik + b*a.mianownik, a.mianownik);
        }
        public static Liczba operator -(Liczba a, int b)
        {
            b = -b;
            return a + b;
        }
        public static Liczba operator *(Liczba a, int b)
        {
            return new Liczba(a.licznik * b, a.mianownik);
        }
        public static Liczba operator /(Liczba a, int b)
        {
            Liczba c = new Liczba(1, b);
            return a * c;
        }
        public static Liczba operator +(int a, Liczba b)
        {
            return b + a;
        }
        public static Liczba operator -(int a, Liczba b)
        {
            return b-a;
        }
        public static Liczba operator *(int a, Liczba b)
        {
            return b * a;
        }
        public static Liczba operator /(int a, Liczba b)
        {
            Liczba c = new Liczba(b.mianownik, b.licznik);

            return b * a;
        }
    }
}