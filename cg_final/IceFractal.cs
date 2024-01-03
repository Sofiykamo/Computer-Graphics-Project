using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cg_final
{
    class IceFractal
    {
        public Bitmap main(int t)
        {
            int imageWidth = 600; // Ширина изображения
            int imageHeight = 600; // Высота изображения
            Bitmap bitmap = new Bitmap(imageWidth, imageHeight); // Создаем изображение

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Настройка параметров рисования
                Pen pen = new Pen(Color.Black);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Рисуем изображение
                Draw(g, imageWidth - 10, 0, imageHeight - 10, -Math.PI, t);
                Draw(g, 0, imageWidth - 10, imageHeight - 10, 0, t);
                Draw(g, 0, 0, imageHeight - 10, -Math.PI / 2, t);
                Draw(g, imageWidth - 10, imageWidth - 10, imageHeight - 10, Math.PI / 2, t);

                // Сохраняем изображение
                bitmap.Save("output.png");
                return bitmap;

            }

            Console.WriteLine("Изображение сохранено как output.png");
        }

        static void Draw(Graphics g, double x, double y, double l, double u, int t)
        {
            if (t > 0)
            {
                l *= 0.5;
                Draw2(g, ref x, ref y, l, u, t - 1);
                Draw2(g, ref x, ref y, l * 0.8, u + Math.PI / 2, t - 1);
                Draw2(g, ref x, ref y, l * 0.8, u - Math.PI / 2, t - 1);
                Draw2(g, ref x, ref y, l, u, t - 1);
            }
            else
            {
                g.DrawLine(Pens.Black, (float)x, (float)y, (float)(x + l * Math.Cos(u)), (float)(y - l * Math.Sin(u)));
            }
        }

        static void Draw2(Graphics g, ref double x, ref double y, double l, double u, int t)
        {
            Draw(g, x, y, l, u, t);
            x += l * Math.Cos(u);
            y -= l * Math.Sin(u);
        }
    }
}
