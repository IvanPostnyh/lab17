using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17
{
    class ModelPPPSum
    {
        int N;
        int k1 = 0;
        int k2 = 0;
        int[] ArrayCountPoints;
        double LimitTime;
        double LambdaOne;
        double TimeOne = 0;
        double LambdaTwo;
        double TimeTwo = 0;
        Random rnd = new Random();
        Dictionary<int, int> DictFreq = new Dictionary<int, int>();
        Dictionary<int, double> DictFreqDivN = new Dictionary<int, double>();

        public double TimeOne1 { get => TimeOne; }
        public double LambdaOne1 { get => LambdaOne; }
        public double LambdaTwo1 { get => LambdaTwo;}
        public double TimeTwo1 { get => TimeTwo;}

        public void InitialData(double l1,double l2, double t, int n)
        {
            LambdaOne = l1;
            LambdaTwo = l2;
            LimitTime = t;
            N = n;
            ArrayCountPoints = new int[n];
            foreach (int i in ArrayCountPoints)
            {
                ArrayCountPoints[i] = 0;
            }
        }

        public int GetCountOne()
        {
            return ArrayCountPoints[k1];
        }

        public string ModelingOne()
        {
            if (k1 < N)
            {
                if (TimeOne < LimitTime)
                {
                    ArrayCountPoints[k1]++;
                    TimeOne += ExponentialRVOne();
                    return "Modeling";
                }
                else
                {
                    try
                    {
                        DictFreq.Add(ArrayCountPoints[k1], 0);
                        DictFreqDivN.Add(ArrayCountPoints[k1], 0d);
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

        private double ExponentialRVOne()
        {
            double Xi;
            double A = rnd.NextDouble();
            Xi = -Math.Log(A) / LambdaOne;
            return Xi;
        }

        public double StatisticsOne()
        {
            DictFreq[ArrayCountPoints[k1]]++;
            DictFreqDivN[ArrayCountPoints[k1]] = (double)DictFreq[ArrayCountPoints[k1]] / (double)(k1 + 1);
            k1++;
            TimeOne = 0;
            return DictFreqDivN[ArrayCountPoints[k1 - 1]];
        }

        public int GetCountTwo()
        {
            return ArrayCountPoints[k2];
        }

        public string ModelingTwo()
        {
            if (k2 < N)
            {
                if (TimeTwo < LimitTime)
                {
                    ArrayCountPoints[k2]++;
                    TimeTwo += ExponentialRVTwo();
                    return "Modeling";
                }
                else
                {
                    try
                    {
                        DictFreq.Add(ArrayCountPoints[k2], 0);
                        DictFreqDivN.Add(ArrayCountPoints[k2], 0d);
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

        private double ExponentialRVTwo()
        {
            double Xi;
            double A = rnd.NextDouble();
            Xi = -Math.Log(A) / LambdaTwo;
            return Xi;
        }

        public double StatisticsTwo()
        {
            DictFreq[ArrayCountPoints[k2]]++;
            DictFreqDivN[ArrayCountPoints[k2]] = (double)DictFreq[ArrayCountPoints[k2]] / (double)(k2 + 1);
            k2++;
            TimeTwo = 0;
            return DictFreqDivN[ArrayCountPoints[k2 - 1]];
        }
    }
}
