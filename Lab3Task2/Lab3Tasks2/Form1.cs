using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3Tasks2
{
    public partial class Form1 : Form
    {

        bool isDrawing = false;
        Point p;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Bresenham(int x1, int y1, int x2, int y2, System.Drawing.Pen pen)
        {
            var pic = (pictureBox1.Image as Bitmap);
            int dirx, diry;
            var deltay = Math.Abs(y2 - y1);
            var deltax = Math.Abs(x2 - x1);

            if (x1 < x2)
            {
                dirx = 1;
            }
            else
            {
                dirx = -1;
            };

            if (y1 < y2)
            {
                diry = 1;
            }
            else
            {
                diry = -1;
            };

            int error = deltax - deltay;

            pic.SetPixel(x2, y2, pen.Color);
            pictureBox1.Invalidate();
            while (x1 != x2 || y1 != y2)
            {
                pic.SetPixel(x1, y1, pen.Color);
                pictureBox1.Invalidate();

                int error2 = error * 2;

                if (error2 > -deltay)
                {
                    x1 += dirx;
                    error -= deltay;
                }
                if (error2 < deltax)
                {
                    y1 += diry;
                    error += deltax;
                }
            }
        }

        private void Wu(int x1, int y1, int x2, int y2, System.Drawing.Pen pen)
        {
            float deltax = x2 - x1; float deltay = y2 - y1;
            var pic = (pictureBox1.Image as Bitmap);
            if (Math.Abs(deltax) > Math.Abs(deltay))
            {
                if (x1 > x2)
                {
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }
                pic.SetPixel(x2, y2, pen.Color);
                pictureBox1.Invalidate();

                float gradient = deltay / deltax;
                float y = y1 + gradient;
                for (var x = x1 + 1; x <= x2 - 1; x++)
                {
                    pic.SetPixel(x, (int)y, Color.FromArgb((int)((1 - (y - (int)y)) * 255), pen.Color.R, pen.Color.G, pen.Color.B));
                    pic.SetPixel(x, (int)y + 1, Color.FromArgb((int)((y - (int)y) * 255), pen.Color.R, pen.Color.G, pen.Color.B));
                    pictureBox1.Invalidate();
                    y += gradient;
                }
            }
            else
            {
                if (y1 > y2)
                {
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }

                pic.SetPixel(x2, y2, pen.Color);
                pictureBox1.Invalidate();

                float gradient = deltax / deltay;
                float x = x1 + gradient;

                for (var y = y1 + 1; y <= y2 - 1; y++)
                {
                    pic.SetPixel((int)x, y, Color.FromArgb((int)((1 - (x - (int)x)) * 255), pen.Color.R, pen.Color.G, pen.Color.B));
                    pic.SetPixel((int)x + 1, y, Color.FromArgb((int)((x - (int)x) * 255), pen.Color.R, pen.Color.G, pen.Color.B));
                    pictureBox1.Invalidate();
                    x += gradient;
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                if (checkBox1.Checked)
                {
                    Wu(p.X, p.Y, e.X, e.Y, Pens.Yellow);
                }
                else
                {
                    Bresenham(p.X, p.Y, e.X, e.Y, Pens.Blue);
                }
            }
            else
            {
                p = e.Location;
            }

            isDrawing = !isDrawing;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

    }
}
