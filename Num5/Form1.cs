using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Num5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double maxX = 0;
            int maxAngle = 0;
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
                m = 104,
                d = 0.2,
                v0 = 1800,
                p0 = 1.2,
                cd = 0.2,
                g = 9.81,
                a = Math.PI * Math.Pow(d, 2) / 4,
                k = 0.5 * p0 * cd * a,
                vx = v0 * Math.Cos(angle),
                vy = v0 * Math.Sin(angle),
                x = 0, y = 0, v, p;
            while (y >= 0)
            {
                chart1.Series[0].Points.AddXY(x / 1000, y / 500);
                v = -k * Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2)) / m;
                vx += v * vx * step;
                vy += (-g + v * vy) * step;
                x += vx * step;
                y += vy * step;
            }

            vx = v0 * Math.Cos(angle);
            vy = v0 * Math.Sin(angle);
            x = 0;
            y = 0;
            while (y >= 0)
            {
                p = p0 * Math.Exp(-y / 80000);
                k = 0.5 * p * cd * a;
                chart1.Series[1].Points.AddXY(x / 1000, y / 500);
                v = -k * Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2)) / m;
                vx += v * vx * step;
                vy += (-g + v * vy) * step;
                x += vx * step;
                y += vy * step;

            }
            return x;
        }
    }
}
