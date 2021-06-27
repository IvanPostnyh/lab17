using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17
{
    class ModelPPP
    {
        int N;
        int k = 0;
        int[] ArrayCountPoints;
        double LimitTime;
        double Lambda;
        double Time = 0;
        Random rnd = new Random();
        Dictionary<int, int> DictFreq = new Dictionary<int, int>();
        Dictionary<int, double> DictFreqDivN = new Dictionary<int, double>();

        public double Time1 { get => Time;}
        public double Lambda1 { get => Lambda;}

        public void InitialData(double l,double t,int n)
        {
            Lambda = l;
            LimitTime = t;
            N = n;
            ArrayCountPoints = new int[n];
            foreach(int i in ArrayCountPoints)
            {
                ArrayCountPoints[i] = 0;
            }
        }

        public int GetCount()
        {
            return ArrayCountPoints[k];
        }

        public string Modeling()
        {if (k < N)
            {
                if (Time < LimitTime)
                {
                    ArrayCountPoints[k]++;
                    Time += ExponentialRV();
                    return "Modeling";
                }
                else
                {
                    try
                    {
                        DictFreq.Add(ArrayCountPoints[k], 0);
                        DictFreqDivN.Add(ArrayCountPoints[k], 0d);
                    }
                    catch
                    {
                        Console.WriteLine("Попытка добавить существующий элемент");
                    }
                    finally
                    {
                        Console.WriteLine("Исключение обработано");
                    }
                    return "Statistics";
                }
            }
            else
            {
                return "EndModeling";
            }
            
        }

        private double ExponentialRV()
        {
            double Xi;
            double A = rnd.NextDouble();
            Xi = -Math.Log(A) / Lambda;
            return Xi;
        }

        public double Statistics()
        {
            DictFreq[ArrayCountPoints[k]]++;
            DictFreqDivN[ArrayCountPoints[k]] = (double)DictFreq[ArrayCountPoints[k]] / (double)(k + 1);
            k++;
            Time = 0;
            return DictFreqDivN[ArrayCountPoints[k-1]];
        }
    }
}
