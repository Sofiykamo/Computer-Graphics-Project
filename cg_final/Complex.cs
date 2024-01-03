using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cg_final
{
    class Complex
    {
        public Complex(double re, double im)
        {
            Re = re;
            Im = im;
        }
        public static bool operator ==(Complex a, Complex b)
        {
            return (a.Re == b.Re) && (a.Im == b.Im);
        }

        public static bool operator !=(Complex a, Complex b)
        {
            return !(a == b);
        }

        public static explicit operator Complex(int value)
        {
            return new Complex((double)value, 0.0);
        }

        public static Complex operator *(int a, Complex b)
        {
            return new Complex(a * b.Re, a * b.Im);
        }
        public static Complex Zero => new Complex(0, 0);
        public static Complex One => new Complex(1, 0);
        public static Complex ImaginaryOne => new Complex(0, 1);
        public double Magnitude => Math.Sqrt(Re * Re + Im * Im);
        /// <summary>
        /// Реальная часть комплексного числа.
        /// </summary>
        public double Re { get; private set; }

        /// <summary>
        /// Мнимая часть комплекстного числа.
        /// </summary>
        public double Im { get; private set; }

        /// <summary>
        /// Модуль комплексного числа.
        /// </summary>
        public double Module => Math.Sqrt(Re * Re + Im * Im);

        /// <summary>
        /// Квадрат модуля комплексного числа.
        /// </summary>
        public double ModuleInSquare => Re * Re + Im * Im;

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Re + b.Re, a.Im + b.Im);
        }

        public static Complex operator +(Complex a, double b)
        {
            return new Complex(a.Re + b, a.Im);
        }
        public static Complex operator -(Complex a, double b)
        {
            return new Complex(a.Re - b, a.Im);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.Re - b.Re, a.Im - b.Im);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.Re * b.Re - a.Im * b.Im, a.Im * b.Re + a.Re * b.Im);
        }

        public static Complex operator *(double a, Complex b)
        {
            return new Complex(b.Re * a, b.Im * a);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            double m2 = b.ModuleInSquare;
            return new Complex((a.Re * b.Re + a.Im * b.Im) / m2, (a.Im * b.Re - a.Re * b.Im) / m2);
        }

        public static Complex operator ^(Complex a, int power)
        {
            Complex result = new Complex(1, 0);

            for (int i = 0; i < power; i++)
            {
                result *= a;
            }

            return result;
        }
    }
}
