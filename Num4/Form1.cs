using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Num4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double t1 = 10,
               step = 0.01,
               t = 0,
                h = 3000,
                m = 80,
                a0 = 0.3,
                a1 = 60,
                cd = 0.95,
                p = 1.2,
                vy = 0,
                g = 9.81,
                y = h,
                k0 = 0.5 * p * cd * a0,
                k1 = 0.5 * p * cd * a1,
                a, k;
            while (y >= 0)
            {
                chart1.Series[0].Points.AddXY(t, y/50);
                chart1.Series[1].Points.AddXY(t, -vy);
                if (t <= t1)
                {
                    k = k0;
                }
                else if (t1 < t && t <= t1)
                {
                    k = k0 + (k1 - k0)*(t - t1);
                }
                else
                {
                    k = k1;
                }
                a = -g + k * Math.Pow(vy, 2) / m;
                vy += a * step;
                y += vy * step;
                chart1.Series[2].Points.AddXY(t, -a);
                t += step;
            }
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
        }

        
    }
}
