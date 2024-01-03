using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cg_final
{
    class MatrixFigure
    {
        public PointF[] hexagonPoints = new PointF[6];
        public GraphicsPath Path { get;  set; }
        public Matrix TransformationMatrix { get; private set; }

        public MatrixFigure()
        {
            Path = new GraphicsPath();
            TransformationMatrix = new Matrix();
        }

        public void updateHexagonPoints()
        {

            using (GraphicsPath transformedPath = new GraphicsPath())
            {
                transformedPath.AddPath(Path, false);
                transformedPath.Transform(TransformationMatrix);


                for (int i = 0; i < transformedPath.PathPoints.Length; i++)
                {
                    PointF vertex = transformedPath.PathPoints[i];
                    hexagonPoints[i].X = vertex.X;
                    hexagonPoints[i].Y = vertex.Y;
                }
            }
        }
        public PointF getHexagonPoints(int i)
        {

            return hexagonPoints[i];
        }
        public void ApplyTranslation(float dx, float dy)
        {
            TransformationMatrix.Translate(dx, dy);

        }

        public void ApplyScale(float sx, float sy)
        {

            TransformationMatrix.Scale(sx, sy);
            updateHexagonPoints();
        }

        public void ApplyRotation(float angle)
        {
            TransformationMatrix.Rotate(angle);
        }
        // Повворот вокруг указанной точки
        public void ApplyRotateAt(float angle, PointF point)
        {

            TransformationMatrix.RotateAt(angle, point);
            updateHexagonPoints();
        }
        // Преобразование - "Скос"
        public void ApplyShear(float shearX, float shearY)
        {
            TransformationMatrix.Shear(shearX, shearY);
        }

        public void Draw(Graphics graphics, Pen pen)
        {
            using (GraphicsPath transformedPath = new GraphicsPath())
            {
                transformedPath.AddPath(Path, false);
                transformedPath.Transform(TransformationMatrix);

                graphics.DrawPath(pen, transformedPath);

                for (int i = 0; i < transformedPath.PathPoints.Length; i++)
                {
                    PointF vertex = transformedPath.PathPoints[i];
                    string label = ((char)('A' + i)).ToString(); // Генеруємо букву від 'A'

                    // Визначаємо координати для розміщення тексту над літерою
                    float labelX = vertex.X + 2;
                    float labelY = vertex.Y; // Встановлюємо відстань над літерою

                    // Малюємо літеру, перевернуту вгору донизу
                    using (GraphicsPath letterPath = new GraphicsPath())
                    {
                        letterPath.AddString(label, new FontFamily("Arial"), (int)FontStyle.Bold, 14, new PointF(0, 0), StringFormat.GenericDefault);
                        Matrix letterTransform = new Matrix();
                        letterTransform.Translate(labelX, labelY);
                        letterTransform.Scale(1, -1); // Відображення по вертикалі
                        letterPath.Transform(letterTransform);
                        graphics.FillPath(Brushes.Black, letterPath);
                    }
                }
            }


        }
   
 

    }
}
