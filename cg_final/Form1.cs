using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 *  linkLabel1.Text = "Відвідати веб-сайт";
    
    // Додати лінк до початкового індексу 0 до кінця тексту
    linkLabel1.Links.Add(0, linkLabel1.Text.Length, "https://www.example.com");
    
    // Встановити подію LinkClicked
    linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
*/
namespace cg_final
{
    public enum ColorModel
    {
        RGB,
        HSL
    }
    public partial class Form1 : Form
    {
        private MatrixFigure hexagonFigure;
        private Point clickedPoint;
        private Point center = new Point();
        Point vertex1 = new Point();
        Bitmap bitmap;
        Color selectedColor; // Колір, обраний користувачем
        Rectangle selectedFragment;
        bool isSelectingFragment = false;
       ColorModel colorModel = ColorModel.RGB; // По
        public Form1()
        {
           

            InitializeComponent();
            this.Paint += Form1_Paint;
            hexagonFigure = new MatrixFigure();
            center.X =int.Parse(txtTransformCenterX.Text);
            center.Y = int.Parse(txtTransformCenterY.Text);
            vertex1.X = int.Parse(txtTransformVertexX.Text);
            vertex1.Y = int.Parse(txtTransformVertexY.Text);
        
       
            hexagonFigure.Path.AddPolygon(GetHexagonVerticesPointCenter(center, vertex1));
            hexagonFigure.updateHexagonPoints();
            updateComboBox();

            lblColorUnderCursor.Text = "";
            lblRGB.Text = "";
            lblCoordinates.Text = "";
            lblHSL.Text = "";
            linkLabel1.Links.Add(0, linkLabel1.Text.Length, "https://iternal.us/what-is-a-fractal/");
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);

            linkLabel2.Links.Add(0, linkLabel2.Text.Length, "https://science.howstuffworks.com/math-concepts/fractals.htm");
            linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);

            linkLabel3.Links.Add(0, linkLabel3.Text.Length, "  https://www.3blue1brown.com/lessons/newtons-fractal");
            linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);

            linkLabel4.Links.Add(0, linkLabel4.Text.Length, "https://study.com/academy/lesson/what-is-a-color-model-uses-definition.html");
           linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);

            lblMain.Font = new(lblFractals.Font, FontStyle.Underline);
            button1.ForeColor = Color.Black;
            btnInfo.ForeColor=Color.FromArgb(0x47, 0x6C, 0x85);
            btnInstructions.ForeColor=Color.FromArgb(0x47, 0x6C, 0x85);
            btnCreate.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            btnTransformReset.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            btnRotateScale.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformCenterX.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformCenterY.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformRotate.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformScale.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);

            txtTransformVertexX.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformVertexY.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            txtTransformXY.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            btnFractalsCreate.ForeColor= Color.FromArgb(0x47, 0x6C, 0x85);
            btnColor.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            button4.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
            panelMain.Visible = true;
        }
        private void updateComboBox()
        {
            int n = comboBox1.SelectedIndex;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("O");
            string label;
            PointF p = new PointF();

            for (int i = 0; i < 6; ++i)
            {
                label = ((char)('A' + i)).ToString();
                p = (hexagonFigure.getHexagonPoints(i));
                label += ":  X=" + (int)p.X + ";  Y=" + (int)p.Y;
                // Генеруємо букву від 'A'
                comboBox1.Items.Add(label);
            }
            comboBox1.SelectedIndex = n;
        }
        // Додайте цей код туди, де ви хочете скопіювати елементи
        private PointF[] GetHexagonVerticesPointCenter(PointF center, PointF vertex)
        {
            // comboBox1.Items.Add("Ox=" + center.X + ";  Oy=" + center.Y);
            PointF[] vertices = new PointF[6];

            double angle = Math.PI / 3; // 60 degrees in radians

            // Визначаємо координати інших п'яти вершин шестикутника
            for (int i = 0; i < 6; i++)
            {
                double currentAngle = angle * i;
                float x = center.X + (float)((vertex.X - center.X) * Math.Cos(currentAngle) -
                                              (vertex.Y - center.Y) * Math.Sin(currentAngle));
                float y = center.Y + (float)((vertex.X - center.X) * Math.Sin(currentAngle) +
                                              (vertex.Y - center.Y) * Math.Cos(currentAngle));

                vertices[i] = new PointF(x, y);
              
            }

            return vertices;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // E6d7cd
            // Створити градієнтний пензель
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = new Color[] { Color.FromArgb(0xE6, 0xd7, 0xcd), Color.FromArgb(0xA5, 0x97, 0x8B), Color.FromArgb(0xE6, 0xd7, 0xcd) };
            colorBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };

            // Визначити прямокутник для верхніх 30 пікселів форми
            Rectangle top30PixelsRectangle = new Rectangle(0, 0, ClientRectangle.Width, 65);

            LinearGradientBrush brush = new LinearGradientBrush(
                top30PixelsRectangle,    // Прямокутник, в якому відбувається градієнт (верхні 30 пікселів)
                Color.LightSkyBlue,     // Початковий колір (не використовується через ColorBlend)
                Color.DarkSlateGray,    // Кінцевий колір (не використовується через ColorBlend)
                LinearGradientMode.Horizontal); // Орієнтація градієнта

            // Застосувати ColorBlend до градієнтного пензля
            brush.InterpolationColors = colorBlend;

            // Намалювати градієнт на формі
            e.Graphics.FillRectangle(brush, top30PixelsRectangle);
        }

        private void lblFractals_Click(object sender, EventArgs e)
        {
            lblFractals.Font = new Font(lblFractals.Font, FontStyle.Underline);
            lblTransformations.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblColorModels.Font = new(lblFractals.Font, FontStyle.Regular);
            lblMain.Font = new(lblFractals.Font, FontStyle.Regular);
            panelInsructions.Visible = false;
            panelExplanation.Visible = false;
            panelFractals.Visible = true;
            panelMain.Visible = false;
            panelTransformations.Visible = false;
            panelColorModels.Visible = false;
            panelFractals.BringToFront();
        //    panelTransformations.BringToFront();
        
        }

        private void lblTransformations_Click(object sender, EventArgs e)
        {
            lblFractals.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblTransformations.Font = new Font(lblFractals.Font, FontStyle.Underline);
            lblColorModels.Font=new(lblFractals.Font, FontStyle.Regular);
            lblMain.Font= new(lblFractals.Font, FontStyle.Regular);
            panelTransformations.Visible = true;
            panelTransformations.BringToFront();
            panelInsructions.Visible = false;
            panelExplanation.Visible = false;
            panelFractals.Visible = false;
            panelColorModels.Visible = false;
            panelMain.Visible = false;
            

            panelTransformations.Visible = true;
            panelTransformations.BringToFront();

        }

        private void btnFractalsCreate_Click(object sender, EventArgs e)
        {
            if (rdbtnNewton.Checked)
            {
                int c;// =Convert.ToInt32( richTextBox2.Text);
                if (!int.TryParse(txtFractalC.Text, out c) || Convert.ToInt32(txtFractalC.Text) == 0)
                {
                    c = 1;
                }
                else
                {
                    c = Convert.ToInt32(txtFractalC.Text);

                }
                Newton n = new Newton();
                Bitmap bitmap = n.main(c);
                n.setc(c);
                pictureBoxFractals.Image = bitmap;
            }
            else if (rdbtnIce.Checked)
            {
                pictureBoxFractals.BackgroundImage = null;
                var n = Convert.ToInt32(txtFractalNumIterations.Text);



                IceFractal ice = new IceFractal();
                Bitmap bitmap = ice.main(n);
                pictureBoxFractals.Image = bitmap;
            }
        }

        private void btnFractalColor_Click(object sender, EventArgs e)
        {
            Color[] selectedColor = new Color[5];
            for (int i = 0; i < 5; ++i)
            {
                colorDialog1.ShowDialog();
                selectedColor[i] = colorDialog1.Color;
            }
          
            int c;
            if (!int.TryParse(txtFractalC.Text, out c) || Convert.ToInt32(txtFractalC.Text) == 0)
            {
                c = 1;
            }
            else
            {
                c = Convert.ToInt32(txtFractalC.Text);

            }
            Newton n = new Newton();
            n.SetColor(selectedColor);
            Bitmap bitmap = n.main(c);
           pictureBoxFractals.Image = bitmap;
        }

        private void btnFractalZoomin_Click(object sender, EventArgs e)
        {
            
            if (pictureBoxFractals.Image != null)
            {
                // Отримайте поточне зображення
                Image currentImage = pictureBoxFractals.Image;

                // Обчисліть новий розмір для приближеного зображення
                int newWidth = (int)(currentImage.Width + 20);
                int newHeight = (int)(currentImage.Height + 20);

                // Створіть новий Bitmap для приближеного зображення
                Bitmap zoomedBitmap = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(zoomedBitmap))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(currentImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                // Оновіть pictureBox1, щоб показати приближене зображення
                pictureBoxFractals.Image = zoomedBitmap;
            }

        }

        private void btnFractalZoomout_Click(object sender, EventArgs e)
        {
            if (pictureBoxFractals.Image != null)
            {
                // Отримайте поточне зображення
                Image currentImage = pictureBoxFractals.Image;

                // Обчисліть новий розмір для приближеного зображення
                int newWidth = (int)(currentImage.Width - 20);
                int newHeight = (int)(currentImage.Height - 20);

                // Створіть новий Bitmap для приближеного зображення
                Bitmap zoomedBitmap = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(zoomedBitmap))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(currentImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                // Оновіть pictureBox1, щоб показати приближене зображення
                pictureBoxFractals.Image = zoomedBitmap;
            }
        }

      
        private void txtFractalC_Click(object sender, EventArgs e)
        {
            txtFractalC.Text = "";
            txtFractalC.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
        }

        private void txtFractalNumIterations_Click(object sender, EventArgs e)
        {
            txtFractalNumIterations.Text = "";
            txtFractalNumIterations.ForeColor = Color.FromArgb(0x47, 0x6C, 0x85);
        }

        private void pictureBoxTransform_Paint(object sender, PaintEventArgs e)
        {
            float centerX = pictureBoxTransform.Width / 2;
            float centerY = pictureBoxTransform.Height / 2;

            Graphics graphics = e.Graphics;
            DrawCoordinateAxes(graphics, pictureBoxTransform.Width, pictureBoxTransform.Height);
            hexagonFigure.Draw(graphics, new Pen(Color.Green, 2));
            pictureBoxTransform.MouseMove += new MouseEventHandler(pictureBoxTransform_MouseMove);
        }

        private void pictureBoxTransform_MouseMove(object sender, MouseEventArgs e)
        {
            float centerX = pictureBoxTransform.Width / 2;
            float centerY = pictureBoxTransform.Height / 2;

            // Отримання координат відносно центра шестикутника
            float relativeX = e.X - centerX;
            float relativeY = centerY - e.Y; // Інвертовано, оскільки ви масштабуєте Y відносно вісі Y

           txtTransformXY.Text = $"X: {(int)relativeX},  Y: {(int)relativeY}";
        }
        private void DrawCoordinateAxes(Graphics g, int width, int height)
        {
            float centerX = pictureBoxTransform.Width / 2;
            float centerY = pictureBoxTransform.Height / 2;

            g.TranslateTransform(centerX, centerY);
            g.ScaleTransform(1, -1);

            Pen pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, -centerX, 0, centerX, 0);
            g.DrawLine(pen, 0, -centerY, 0, centerY);
        }

        private void btnRotateScale_Click(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            int rotate = int.Parse(txtTransformRotate.Text);


            float scale = float.Parse(txtTransformScale.Text);

            if (i == 0)
            {
                hexagonFigure.ApplyScale(scale, scale);
                hexagonFigure.ApplyRotateAt(rotate, new Point(center.X, center.Y));

            }

            else
            {
                PointF point = hexagonFigure.Path.PathPoints[i - 1];
                hexagonFigure.ApplyScale(scale, scale);
                hexagonFigure.ApplyRotateAt(rotate, point);
            }



            pictureBoxTransform.Invalidate();

            updateComboBox();
        }

        private void btnTransformReset_Click(object sender, EventArgs e)
        {
            hexagonFigure.TransformationMatrix.Reset();
            hexagonFigure.ApplyScale(1, 1);

            pictureBoxTransform.Invalidate();
            updateComboBox();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            center.X = int.Parse(txtTransformCenterX.Text);
            center.Y = int.Parse(txtTransformCenterY.Text);
            vertex1.X = int.Parse(txtTransformVertexX.Text);
            vertex1.Y = int.Parse(txtTransformVertexY.Text);
            PointF[] resultArray = GetHexagonVerticesPointCenter(center, vertex1);
            // hexagonFigure.Path.Reset();
            //   pictureBoxTransform.
            // for(int i = 0; i < 6; ++i)
            //  {
            //      hexagonFigure.Path.PathPoints = resultArray;
            // }
         
            hexagonFigure.Path.Reset();
            hexagonFigure.Path.AddPolygon(GetHexagonVerticesPointCenter(center, vertex1));
            hexagonFigure.updateHexagonPoints();
            updateComboBox();
            hexagonFigure.TransformationMatrix.Reset();
            hexagonFigure.ApplyScale(1, 1);
            pictureBoxTransform.Invalidate();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

      
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            panelInsructions.Visible = true;
            panelExplanation.Visible = false;
            panelFractals.Visible = false;
            panelMain.Visible = false;
            panelTransformations.Visible = false;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            panelInsructions.Visible = false;
            panelExplanation.Visible = true;
            panelFractals.Visible = false;
            panelMain.Visible = false;
            panelTransformations.Visible = false;
            button1.ForeColor = Color.Black;
        }

        private void lblMain_Click(object sender, EventArgs e)
        {
            lblFractals.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblTransformations.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblColorModels.Font = new(lblFractals.Font, FontStyle.Regular);
            lblMain.Font = new(lblFractals.Font, FontStyle.Underline);
            panelInsructions.Visible = false;
            panelExplanation.Visible = false;
            panelFractals.Visible = false;
            panelMain.Visible = true;
            panelTransformations.Visible = false;
            panelColorModels.Visible = false;
        }

        private void lblColorModels_Click(object sender, EventArgs e)
        {
            lblFractals.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblTransformations.Font = new Font(lblFractals.Font, FontStyle.Regular);
            lblColorModels.Font = new(lblFractals.Font, FontStyle.Underline);
            lblMain.Font = new(lblFractals.Font, FontStyle.Regular);
            panelInsructions.Visible = false;
            panelExplanation.Visible = false;
            panelFractals.Visible = false;
            panelMain.Visible = false;
            panelTransformations.Visible = false;
            panelColorModels.Visible = true;
        }

        private void btnColorSave_Click(object sender, EventArgs e)
        {
            picBox.Image.Save("output.jpg");
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Зображення (*.jpg)|*.png|Всі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти зображення як...";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Отримайте шлях до вибраного файлу і збережіть ваше зображення
                string filePath = saveDialog.FileName;
                picBox.Image.Save(filePath);
            }
        }

        private void btnColorUpload_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                picBox.BackgroundImage = null;
                bitmap = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
              picBox.Image = bitmap;
            }
        }

        private void panelColorModels_Paint(object sender, PaintEventArgs e)
        {

        }

        private void traBrightness_Scroll(object sender, EventArgs e)
        {
            if (selectedColor != null)
            {
                picBox.Image = AdjustBrightnessForSimilarColors(bitmap, (float)(traBrightness.Value / 100.0), selectedColor, 30);
                picBox.Refresh();
            }
        }
        private Bitmap AdjustBrightnessForSimilarColors(Bitmap image, float brightness, Color targetColor, int tolerance)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap result = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    if (IsColorSimilar(pixelColor, targetColor, tolerance))
                    {
                        // Визначення колірної моделі і виконання змін
                        Color newColor;
                        if (colorModel == ColorModel.RGB)
                            newColor = ChangeBrightnessRGB(pixelColor, brightness);
                        else // За замовчуванням використовуємо RGB
                            newColor = ChangeBrightnessRGB(pixelColor, brightness);

                        result.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        result.SetPixel(x, y, pixelColor);
                    }
                }
            }

            return result;
        }

        private Color ChangeBrightnessRGB(Color color, float brightness)
        {
            // Використовуємо Color.HSV для зміни яскравості
            float hue, saturation, value;
            hue = color.GetHue();
            saturation = color.GetSaturation();
            value = brightness;

            return ColorFromHSV(hue, saturation, value);
        }
        private void ColorToHSL(Color color, out float h, out float s, out float l)
        {
            float r = color.R / 255.0f;
            float g = color.G / 255.0f;
            float b = color.B / 255.0f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            h = 0;
            s = 0;
            l = (max + min) / 2;

            if (max != min)
            {
                s = (l < 0.5) ? (max - min) / (max + min) : (max - min) / (2.0f - max - min);

                if (max == r)
                {
                    h = (g - b) / (max - min);
                }
                else if (max == g)
                {
                    h = 2.0f + (b - r) / (max - min);
                }
                else
                {
                    h = 4.0f + (r - g) / (max - min);
                }

                h *= 60;
                if (h < 0)
                {
                    h += 360;
                }
            }

            // Перетворення S та L в відсотки
            s *= 100;
            l *= 100;
        
    }


        private Color ColorFromHSL(float h, float s, float l)
        {
            if (s == 0)
            {
                byte val = (byte)(l * 255);
                return Color.FromArgb(255, val, val, val);
            }
            float t1, t2;
            float th = h / 6.0f;
            if (l < 0.5f)
            {
                t2 = l * (1 + s);
            }
            else
            {
                t2 = (l + s) - (l * s);
            }
            t1 = 2 * l - t2;
            float tr, tg, tb;
            tr = th + 1.0f / 3.0f;
            tg = th;
            tb = th - 1.0f / 3.0f;
            tr = ColorFromHue(tr, t1, t2);
            tg = ColorFromHue(tg, t1, t2);
            tb = ColorFromHue(tb, t1, t2);
            byte r = (byte)(tr * 255);
            byte g = (byte)(tg * 255);
            byte b = (byte)(tb * 255);
            return Color.FromArgb(255, r, g, b);
        }

        private float ColorFromHue(float c, float t1, float t2)
        {
            if (c < 0) c += 1.0f;
            if (c > 1) c -= 1.0f;
            if (c < 1.0f / 6.0f)
                return t1 + (t2 - t1) * 6.0f * c;
            if (c < 0.5f)
                return t2;
            if (c < 2.0f / 3.0f)
                return t1 + (t2 - t1) * (2.0f / 3.0f - c) * 6.0f;
            return t1;
        }
        private bool IsColorSimilar(Color color, Color targetColor, int tolerance)
        {
            int deltaR = Math.Abs(color.R - targetColor.R);
            int deltaG = Math.Abs(color.G - targetColor.G);
            int deltaB = Math.Abs(color.B - targetColor.B);

            return (deltaR <= tolerance && deltaG <= tolerance && deltaB <= tolerance);
        }
        private Color ColorFromHSV(float h, float s, float l)
        {
            if (s == 0)
            {
                byte val = (byte)(l * 255);
                return Color.FromArgb(255, val, val, val);
            }
            float t1, t2;
            float th = h / 6.0f;
            if (l < 0.5f)
            {
                t2 = l * (1 + s);
            }
            else
            {
                t2 = (l + s) - (l * s);
            }
            t1 = 2 * l - t2;
            float tr, tg, tb;
            tr = th + 1.0f / 3.0f;
            tg = th;
            tb = th - 1.0f / 3.0f;
            tr = ColorFromHue(tr, t1, t2);
            tg = ColorFromHue(tg, t1, t2);
            tb = ColorFromHue(tb, t1, t2);

            // Обмежте значення blue в межах від 0 до 255
            byte r = (byte)(tr * 255);
            byte g = (byte)(tg * 255);
            byte b = (byte)(tb * 255);

            r = Math.Min((byte)255, Math.Max((byte)0, r));
            g = Math.Min((byte)255, Math.Max((byte)0, g));
            b = Math.Min((byte)255, Math.Max((byte)0, b));

            return Color.FromArgb(255, r, g, b);
        }

        private void btnColorSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog2.Color;
                // Оновіть відображення вибраного кольору на вашій формі, якщо це потрібно
                // Наприклад, ви можете встановити фон кнопки або відображати вибраний колір десь на формі.
            }
        }
        private Color GetColorAtLocation(Point location)
        {
            if (bitmap != null && location.X >= 0 && location.X < bitmap.Width && location.Y >= 0 && location.Y < bitmap.Height)
            {
                return bitmap.GetPixel(location.X, location.Y);
            }
            return Color.Empty;
        }

        private string GetHSLFromColor(Color color)
        {
            float hue, saturation, lightness;
            ColorToHSL(color, out hue, out saturation, out lightness);
            return $"HSL: {hue:F2}, {saturation:F2}, {lightness:F2}";
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Перевірка, чи натиснута ліва кнопка миші
            if (e.Button == MouseButtons.Left)
            {
                // Отримайте колір на заданих координатах в picBox
                Color color = GetColorAtLocation(e.Location);

                // Оновіть текст лейбла для відображення координат та кольору
                lblCoordinates.Text = $"X: {e.X}, Y: {e.Y}";

            lblColorUnderCursor.Text = "Color";
                // Виведіть колір на який ви наводите курсором
                lblColorUnderCursor.BackColor = color;

                // Отримайте значення RGB та HSL
                string rgbValue = $"RGB: {color.R}, {color.G}, {color.B}";
                string hslValue = GetHSLFromColor(color);

                // Оновіть текст лейблів для відображення RGB та HSL
                lblRGB.Text = rgbValue;
                lblHSL.Text = hslValue;
            }
        }


    }
}
