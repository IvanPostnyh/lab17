using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Моделирование MMPP

namespace Lab17
{
    class Model
    {
        double[,] MatrixQ = { { -0.2d, 0.2d }, { 0.2d, -0.2d } };
        double LambdaOne;
        double LambdaTwo;
        double[] P = new double[] { 0.5d, 0.5d };
        Dictionary<double, int> StateModelDict = new Dictionary<double, int>();
        int State;
        double Time;
        Random rnd = new Random();
        double Tau;//τ
        double Eta;//η
        int PreState;
        public Model()
        {
            Time = 0;
            StateModelDict.Clear();
            double A = rnd.NextDouble();
            int k;
            for (k=-1;A>0;k++)
            {
                A -= P[k + 1];
            }
            State = k+1;
            StateModelDict.Add(Time1, State);
        }

        public double Time1 { get => Time; }
        public int PreState1 { get => PreState;}

        public int GetState(double t)
        {
            return StateModelDict[t];
        }

        public void InitialLambda(double l1,double l2)
        {
            LambdaOne = l1;
            LambdaTwo = l2;
        }
        public void InitialEta()
        {
            double Xi;
            double A = rnd.NextDouble();
            Xi = -Math.Log(A) / Math.Abs(MatrixQ[State - 1, State - 1]);
            Eta = Time + Xi;
        }
        public void InitialTau()
        {
            double Xi;
            double A = rnd.NextDouble();
            if (State == 1)
            {
                Xi = -Math.Log(A) / LambdaOne;
            }
            else
            {
                Xi = -Math.Log(A) / LambdaTwo;
            }
            Tau = Time + Xi;
        }
        public string Modeling()
        {
            if(Tau<Eta)
            {
                Time = Tau;
                StateModelDict.Add(Time, State);
                InitialTau();
                return "arrival";
            }
            else
            {
                Time = Eta;
                GenerateState();
                StateModelDict.Add(Time, State);
                InitialEta();
                InitialTau();
                return "nextState";
            }
        }

        private void GenerateState()
        {
            PreState = State;
            double[] mass = new double[MatrixQ.GetLength(0)];
            double[] prob = new double[mass.Length];
            for(int i =0;i<mass.Length;i++)
            {
                mass[i] = MatrixQ[State - 1,i];
            }
            for(int i=0;i<prob.Length;i++)
            {
                if(i==(State-1))
                {
                    prob[i] = 0;
                }
                else
                {
                    prob[i] = Math.Abs(mass[i] / mass[State - 1]);
                }
            }
            int k;
            double A = rnd.NextDouble();
            for (k = -1; A > 0; k++)
            {
                A -= prob[k + 1];
            }
            State = k + 1;
        }
    }
}
/*
switch(md.Modeling())
            {
                case "arrival":
                    chart1.Series[0].MarkerStyle = MarkerStyle.Star4;
                    chart1.Series[0].MarkerColor = Color.Aqua;
                    chart1.Series[0].Points.AddXY(md.Time1, md.GetState(md.Time1));
                    break;
                case "nextState":
                    chart1.Series[0].MarkerStyle = MarkerStyle.None;
                    chart1.Series[0].MarkerColor = Color.Empty;
                    chart1.Series[0].Points.AddXY(md.Time1, md.PreState1);
                    chart1.Series[0].Points.AddXY(md.Time1, md.GetState(md.Time1));
                    break;
            }
*/
