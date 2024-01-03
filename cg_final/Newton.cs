using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cg_final
{
    class Newton
    {
        static List<Color> colors = new List<Color> { Color.Blue, Color.Red, Color.Green, Color.Yellow, Color.Purple };

        const double TOL = 1e-8;
        int C;

        static Complex Newtons(Complex z0, Func<Complex, Complex> f, Func<Complex, Complex> fprime, int MAX_IT = 1000)
        {
            Complex z = z0;
            for (int i = 0; i < MAX_IT; i++)
            {
                Complex dz = f(z) / fprime(z);
                if (dz.Magnitude < TOL)
                {
                    return z;
                }
                z -= dz;
            }
            return Complex.Zero;
        }
        public void setc(int C)
        {
            C = C;

        }
        static int GetRootIndex(List<Complex> roots, Complex r)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if ((roots[i] - r).Magnitude < TOL)
                {
                    return i;
                }
            }
            roots.Add(r);
            return roots.Count - 1;
        }

        static Bitmap PlotNewtonFractal(Func<Complex, Complex> f, Func<Complex, Complex> fprime, int n = 500, Tuple<double, double, double, double> domain = null)
        {
            List<Complex> roots = new List<Complex>();
            int width = 800;
            int height = 800;
            Complex[,] m = new Complex[width, height];

            if (domain == null)
            {
                domain = Tuple.Create(-1.0, 1.0, -1.0, 1.0);
            }
            double xmin = domain.Item1;
            double xmax = domain.Item2;
            double ymin = domain.Item3;
            double ymax = domain.Item4;

            for (int ix = 0; ix < width; ix++)
            {
                double x = xmin + ix * (xmax - xmin) / (width - 1);
                for (int iy = 0; iy < height; iy++)
                {
                    double y = ymin + iy * (ymax - ymin) / (height - 1);
                    Complex z0 = new Complex(x, y);
                    Complex r = Newtons(z0, f, fprime);

                    if (r != Complex.Zero)
                    {
                        int ir = GetRootIndex(roots, r);
                        m[iy, ix] = (Complex)ir;
                    }

                }
            }

            int nroots = roots.Count;
            Color[] rootColors = colors.ToArray();
            Bitmap image = new Bitmap(width, height);
            int prevIr = 0;
            for (int ix = 0; ix < width; ix++)
            {
                for (int iy = 0; iy < height; iy++)
                {
                    int ir = (int)m[iy, ix].Re;
                    if (ir >= 0 && ir < colors.Count)
                    {
                        Color color = colors[ir]; // Отримати кольор за індексом ir
                        image.SetPixel(ix, iy, color);
                        prevIr = ir;
                    }
                    else
                    {
                        image.SetPixel(ix, iy, colors[prevIr]);
                    }
                }
            }
            image.Save("output.png");

            return image;
            /*  Form form = new Form();
               form.ClientSize = new Size(width, height);
               PictureBox pictureBox = new PictureBox();
               pictureBox.Dock = DockStyle.Fill;
               pictureBox.Image = image;
               form.Controls.Add(pictureBox);

               string outputPath = @"E:\fractals\TSquare\TSquare\fractal.png"; // Задайте бажаний шлях та ім'я файлу
               image.Save("output.png");
               image.Save(outputPath, ImageFormat.Png);
               Application.Run(form);*/
        }
        public Bitmap main(int c)
        {

            Func<Complex, Complex> f = z => z * z * z * z * z + c; // Оновіть функцію f
            Func<Complex, Complex> fprime = z => 5 * z * z * z * z; // Оновіть похідну fprime

            Bitmap b = PlotNewtonFractal(f, fprime, n: 500);

            return b;
        }
        public void SetColor(Color[] selectedColor)
        {
            for (int i = 0; i < 5; ++i)
            {

                colors[i] = selectedColor[i];

            }
        }


    }
}
