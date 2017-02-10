using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Relationships : GameMatrix
    {
        public Relationships() {
            AlexFNC = -3.0;
            MomFNC = 0.0;
            DadFNC = 4.0;
        }

        double AlexFNC;
        double MomFNC;
        double DadFNC;

        public double getAlexFNC() {
            return AlexFNC;
        }
        public double getMomFNC()
        {
            return MomFNC;
        }
        public double getDadFNC()
        {
            return DadFNC;
        }

        public void setAlexFNC(double value)
        {
            AlexFNC = value;
        }
        public void setDadFNC(double value)
        {
            DadFNC = value;
        }
        public void setMomFNC(double value)
        {
            MomFNC = value;
        }

        //public override double GetValue(int x, int y)
        //{
        //    return matrix[x, y];
        //}

        //public override void SetValue(int x, int y, double value)
        //{
        //    matrix[x, y] = value;
        //}

        //public double getAlexFNC() {
        //    return matrix[0, 0];
        //}

        //public double getMomFNC()
        //{
        //    return matrix[1, 0];
        //}

        //public double getDadFNC()
        //{
        //    return matrix[2, 0];
        //}

        //public void setAlexFNC(double value)
        //{
        //     matrix[0, 0] = value;
        //}

        //public void setMomFNC(double value)
        //{
        //    matrix[1, 0] = value;
        //}

        //public void setDadFNC(double value)
        //{
        //    matrix[2, 0] = value;
        //}
    }
}
