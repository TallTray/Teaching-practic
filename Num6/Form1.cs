using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Num6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            double maxX = 0;
            int maxAngle = 0;
            InitializeComponent();
            for (int i = 0; i < 91; i++)
            {
                if (DrawTrajectory(i) > maxX)
                {
                    maxX = DrawTrajectory(i);
                    maxAngle = i;
                }
            }
            DrawTrajectory(maxAngle);
            label1.Text = "Max distance = " + maxX + " angle = " + maxAngle;
        }
        public double DrawTrajectory(double angle)
        {
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            double step = 0.01,
                m0 = 120,
                mk = 15,
                l = 10,
                vg = 1800,
                v0 = 20,
                c = 0.0075,
                g = 9.81,
                T = (m0 - mk) / l,
                vx = v0 * Math.Cos(angle),
                vy = v0 * Math.Sin(angle),
                mt, v, x = 0, y = 0, maxX = 0, t = 0;
            while (y >= 0)
            {
                v = Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2));
                if (t <= T)
                {
                    chart1.Series[0].Points.AddXY(x / 1000, y / 1000);
                    mt = m0 - t * l;
                    vx += (l * vg * vx / (v * mt) - c * v * vx / mt) * step;
                    vy += (-g + l * vg * vy / (v * mt) - c * v * vy / mt) * step;
                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }
                else
                {
                    chart1.Series[1].Points.AddXY(x / 1000, y / 1000);
                    vx += -c * v * vx / mk * step;
                    vy += (-g - c * v * vy / mk) * step;
                }
                x += vx * step;
                y += vy * step;
                t += step;
            }
            return maxX;
        }
    }
}
