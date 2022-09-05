using System.Linq;

namespace ChamzyK_ForwardTest.Engines
{
    public class CombustionEngine : IEngine
    {
        public double I { get; set; }
        public double[] ArrayM { get; set; }
        public double[] ArrayV { get; set; }
        public double AmbientT { get; set; }
        public double OverheatsT { get; set; }
        public double Hm { get; set; }
        public double Hv { get; set; }
        public double C { get; set; }


        public double[] Dependencies { get; set; }
        public double T { get; set; }
        public double CurrentV { get; set; }
        public double CurrentM { get; set; }


        public double Acceleration(double m) => m / I;
        public double Vh(double m, double v) => m * Hm + v * v * Hv;
        public double Vc(double ambientT) => C * (ambientT - T);


        public void StartEngine(double ambientT)
        {
            T = ambientT;
            AmbientT = ambientT;
            FillDependencies();
        }


        public void DoIteration()
        {
            CurrentM = CalcM(SlitV());

            T += Vh(CurrentM, CurrentV) + Vc(AmbientT);
            var v = CurrentV + Acceleration(CurrentM);
            CurrentV = v > ArrayV.Last() ? ArrayV.Last() : v;
        }


        private double CalcM(double[] slittedV)
        {
            var result = 0d;
            for (int i = 0; i < Dependencies.Length; i++)
            {
                result += slittedV[i] * Dependencies[i];
            }

            return result + ArrayM[0];
        }
        private double[] SlitV()
        {
            var slittedV = new double[ArrayV.Length - 1];
            var v = CurrentV;
            for (int i = 0; i < ArrayV.Length - 1; i++)
            {
                var differenceV = ArrayV[i + 1] - ArrayV[i];

                v -= differenceV;
                if (v > 0)
                {
                    slittedV[i] = differenceV;
                }
                else
                {
                    slittedV[i] = v + differenceV;
                    break;
                }
            }

            return slittedV;
        }


        private void FillDependencies()
        {
            Dependencies = new double[ArrayV.Length - 1];
            for (int i = 0; i < Dependencies.Length; i++)
            {
                var differenceV = ArrayV[i + 1] - ArrayV[i];
                var differenceM = ArrayM[i + 1] - ArrayM[i];
                Dependencies[i] = differenceM / differenceV;
            }
        }
    }
}
