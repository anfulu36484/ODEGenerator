using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ODEGenerator.Formatter;
using ODEGenerator.SyntaxTree.Numerical;

namespace ODEGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            Test1();
            Console.Read();
        }

        /// <summary>
        /// A+B->C
        /// </summary>
        static void Test0()
        {
            ODE ode = new ODE();

            Substance A = new Substance("A",0.1);
            Substance B = new Substance("B",0.3);
            Substance C = new Substance("C",0);

            Constant k = new Constant("k",1.2);

            ode.Add(A,B,k,C);
            ode.PrintResult(new MathVisitor());
        }

        /// <summary>
        /// A+B->C+D
        /// </summary>
        static void Test01()
        {
            ODE ode = new ODE();

            Substance A = new Substance("A", 0.1);
            Substance B = new Substance("B", 0.3);
            Substance D = new Substance("D", 0);
            Substance C = new Substance("C", 0);

            Constant k = new Constant("k", 1.2);

            ode.Add( A, B , k, D,C);
            ode.PrintResult(new MathVisitor());
        }

        /// <summary>
        /// A+B->C+D
        /// C+D->A+B
        /// </summary>
        static void Test1()
        {
            ODE ode = new ODE();

            Substance A = new Substance("A",10);
            Substance B = new Substance("B",20);
            Substance D = new Substance("D",0);
            Substance C = new Substance("C",0);

            Constant k1 = new Constant("k1",1.2);
            Constant k2 = new Constant("k2",1.7);

            ode.Add(A, B ,k1, D, C );
            ode.Add( D, C , k2, A, B );

            ode.PrintResult(new MathVisitor());

            MatlabODEGenerator matlabOdeGenerator = new MatlabODEGenerator(ode,new []{0.0,10,20});
            matlabOdeGenerator.Generate(@"D:\С_2013\ODEGenerator\test\test1");
        }

        /// <summary>
        /// Обратимая живущая полимеризация
        /// </summary>
        static void Test2()
        {
            ODE ode = new ODE();

            Substance M = new Substance("M",10);

            GroupOfSubstances R = new GroupOfSubstances("R");

            int L = 2000;

            for (int i = 1; i <= L; i++)
            {
                R.CreateSubstance(0.0, i);
            }
            R[1].Value = 0.01;

 
            Constant kp = new Constant("kp",2);
            Constant kd = new Constant("kd",1.3);


            ode.Add(R[1],M,kp,R[2]);

            for (int i = 2; i < L-1; i++)
            {
                ode.Add(R[i], M , kp, R[i+1]);
                ode.Add(R[i + 1] , kd, R[i], M );
            }

            ode.Add(R[L-1], M , kp,R[L]);

            ode.PrintResult(new MathVisitor());

            MatlabODEGenerator matlabOdeGenerator = new MatlabODEGenerator(ode, 
                Enumerable.Range(0,100).Select(n=>(double)n/10f).ToArray(),R);
            matlabOdeGenerator.Generate(@"D:\С_2013\ODEGenerator\test\Tonoyan");
        }

        static void Test3()
        {
            ODE ode = new ODE();
            Substance M = new Substance("M", 1);

            double Kt0 = 7.5 * Math.Pow(10,-4);
            double S = 0.66;

            Substance To = new Substance("To",(1-S)*Kt0);
            Substance Co = new Substance("Co",S*Kt0);

            int L = 2000;

            GroupOfSubstances Tt = new GroupOfSubstances("Tt");
            GroupOfSubstances Tc = new GroupOfSubstances("Tc");
            GroupOfSubstances Cc = new GroupOfSubstances("Cc");
            GroupOfSubstances Ct = new GroupOfSubstances("Ct");

            for (int j = 1; j <= L; j++)
            {
                Tt.CreateSubstance(0.0, j);
                Tc.CreateSubstance(0.0, j);
                Cc.CreateSubstance(0.0, j);
                Ct.CreateSubstance(0.0, j);
            }


            Constant Kk = new Constant("Kk",1);
            Constant Kd = new Constant("Kd",1);

            Constant KpT = new Constant("KpT",1);
            Constant KpC = new Constant("KpC",5);


            ode.Add(To,M,Kk,Co);
            ode.Add(Co, Kd,  To, M );

            ode.Add(To,M,KpT,Tt[1]);
            ode.Add( Co, M , KpC, Cc[1]);

            for (int j = 2; j <= L; j++)
            {
                //Реакции роста цепи
                ode.Add(Tt[j-1],M,KpT,Tt[j]);
                ode.Add(Tc[j - 1], M , KpT, Tt[j]);

                ode.Add( Cc[j - 1], M , KpC, Cc[j]);
                ode.Add(Ct[j - 1], M , KpC, Cc[j]);
            }


            for (int j = 1; j <= L; j++)
            {
                //Реакции превращения цис-стереорегулирующих активных центров, в транс-центры
                ode.Add(Tt[j], M , Kk, Ct[j]);
                ode.Add(Tc[j], M , Kk, Cc[j]);

                ode.Add(Cc[j], Kd,  Tc[j], M );
                ode.Add(Ct[j], Kd, Tt[j], M );
            }


            MatlabODEGenerator matlabOdeGenerator = new MatlabODEGenerator(ode,
                Enumerable.Range(0, 10000).Select(n => (double)n / 10f).ToArray(), Tt,Tc,Ct,Cc);
            matlabOdeGenerator.Generate(@"D:\С_2013\ODEGenerator\test\test3");
        }

        /// <summary>
        /// Передача цепи на мономер и низкомолекулярный агент
        /// Rajeev M. Modeling and simulation of poly(lactic acid) polymerization: thesis … PhD. – India, 2006. – 122 p.
        /// </summary>
        static void Test4()
        {
            ODE ode = new ODE();

            double MoCo = 3252.79;// Mo/Co

            Substance M = new Substance("M",10);//Мономер
            Substance C = new Substance("C",M.Value/MoCo);//Катализатор
            Substance H2O = new Substance("H2O", 0.0001);//Агент передачи - вода
            Substance H = new Substance("H",0);//H+
            Substance OH = new Substance("OH",0);//OH-

            GroupOfSubstances P = new GroupOfSubstances("P");//Живая полимерная цепь
            GroupOfSubstances D = new GroupOfSubstances("D");//Мертвая полимерная цепь

            Constant ko = new Constant("ko",0.003);//Константа инициирования
            Constant kj = new Constant("kj",0.9);//Константа роста
            Constant kt = new Constant("kt",Math.Pow(10,-6));//Константа обрыва
            Constant ktc= new Constant("ktc",9);//Константа передачи цепи на низкомолекулярный агент

            int L = 1200;

            for (int j = 1; j <= L; j++)
                P.CreateSubstance(0, j);

            for (int j = 1; j <= L-1; j++)
                D.CreateSubstance(0, j);


            ode.Add(M,C,ko,P[1]);
            for (int j = 1; j <= L-1; j++)
            {
                ode.Add(P[j],M,kj,P[j+1]);
                ode.Add(P[j],M,kt,D[j],P[1]);
                ode.Add(P[j],H2O,ktc,D[j],H,OH);
            }

            double[] timeAray = new double[] {0, 10, 90, 150, 200, 300, 400};

            MatlabODEGenerator matlabOdeGenerator = new MatlabODEGenerator(
                ode,
                timeAray,
                P,D);

            string nameOfDirectory = @"D:\С_2013\ODEGenerator\test\test4";

            matlabOdeGenerator.Generate(nameOfDirectory);

        }

        /// <summary>
        /// Полимеризация, протекающая по механизму живых цепей и осложненная  побочными реакциями обрыва цепи
        /// Rajeev M. Modeling and simulation of poly(lactic acid) polymerization: thesis … PhD. – India, 2006. – 122 p.
        /// </summary>
        static void Test5()
        {
            ODE ode = new ODE();

            double MoCo = 4007;// Mo/Co

            Substance M = new Substance("M",1);//Мономер
            Substance C = new Substance("C", M.Value / MoCo);//Катализатор

            GroupOfSubstances P = new GroupOfSubstances("P");//Живая полимерная цепь
            GroupOfSubstances D = new GroupOfSubstances("D");//Мертвая полимерная цепь

            Constant ko = new Constant("ko",0.003);
            Constant kp = new Constant("kp",0.9);
            Constant ktp = new Constant("ktp",Math.Pow(10,-6));
            Constant kts = new Constant("kts",Math.Pow(10,-7));

            int L = 1000;

            for (int j = 1; j <= L; j++)
                P.CreateSubstance(0, j);

            for (int j = 1; j <= L*2; j++)
                D.CreateSubstance(0, j);

            //Реакция инициирования
            ode.Add(M,C,ko,P[1]);

            //Реакция роста цепи
            for (int j = 1; j <= L-1; j++)
                ode.Add(P[j],M,kp,P[j+1]); 

            //Внутремолекулярный обрыв цепи
            for (int j = 1; j <= L; j++)
                ode.Add( P[j], M , kts, D[j]);

            
            for (int i = 1; i <= L; i++)
            {
                for (int j = i; j <= L; j++)
                {
                    //Взаимодействие двух живых макромолекул с образованием одной мертвой полимерной цепи
                    ode.Add(P[i],P[j],ktp,D[i+j]);
                    //Взаимодействие живой и мертвой макромолекулы с образованием одной мертвой полимерной цепи
                    ode.Add(P[i], D[j] , ktp, D[i + j]);
                }
            }

            double[] timeAray = { 0, 1,10,20,30 };

            MatlabODEGenerator matlabOdeGenerator = new MatlabODEGenerator(
                ode,
                timeAray,
                P, D);

            string nameOfDirectory = @"D:\С_2013\ODEGenerator\test\test5";

            matlabOdeGenerator.Generate(nameOfDirectory);
        }
    }
}
