using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab17
{
    public partial class Form1 : Form
    {
        //ModelPPP P1 = new ModelPPP();
        //ModelPPP P2 = new ModelPPP();
        ModelPPPSum P1P2= new ModelPPPSum();
        ModelPPP P1AddP2 = new ModelPPP();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<4;i++)
            {
                chart1.Series[i].Points.Clear();
            }
            P1P2.InitialData((double)numericUpDown1.Value, (double)numericUpDown2.Value, (double)numericUpDown4.Value,(int)numericUpDown3.Value);
            P1AddP2.InitialData((double)(numericUpDown1.Value + numericUpDown2.Value), (double)numericUpDown4.Value, (int)numericUpDown3.Value);
            chart1.Series[0].Points.AddXY(P1P2.TimeOne1, P1P2.LambdaOne1);
            chart1.Series[1].Points.AddXY(P1P2.TimeTwo1, P1P2.LambdaTwo1);
            chart1.Series[4].Points.AddXY(P1AddP2.Time1, P1AddP2.Lambda1);
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) timer1.Start();
            else timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            switch(P1P2.ModelingOne())
            {
                case "Modeling":
                    chart1.Series[0].Points.AddXY(P1P2.TimeOne1, P1P2.LambdaOne1);
                    chart1.Series[2].Points.AddXY(P1P2.TimeOne1, P1P2.LambdaOne1);
                    break;
                case "Statistics":
                    chart2.Series[0].Points.AddXY(P1P2.GetCountOne(),P1P2.StatisticsOne());
                    chart1.Series[0].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    break;
                case "EndModeling":
                    break;
            }

            switch (P1P2.ModelingTwo())
            {
                case "Modeling":
                    chart1.Series[1].Points.AddXY(P1P2.TimeTwo1, P1P2.LambdaTwo1);
                    chart1.Series[3].Points.AddXY(P1P2.TimeTwo1, P1P2.LambdaTwo1);
                    break;
                case "Statistics":
                    chart2.Series[0].Points.AddXY(P1P2.GetCountTwo(), P1P2.StatisticsTwo());
                    chart1.Series[1].Points.Clear();
                    chart1.Series[3].Points.Clear();
                    break;
                case "EndModeling":
                    break;
            }

            switch (P1AddP2.Modeling())
            {
                case "Modeling":
                    chart1.Series[4].Points.AddXY(P1AddP2.Time1, P1AddP2.Lambda1);
                    chart1.Series[5].Points.AddXY(P1AddP2.Time1, P1AddP2.Lambda1);
                    break;
                case "Statistics":
                    chart2.Series[1].Points.AddXY(P1AddP2.GetCount(), P1AddP2.Statistics());
                    chart1.Series[4].Points.Clear();
                    chart1.Series[5].Points.Clear();
                    break;
                case "EndModeling":
                    break;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
