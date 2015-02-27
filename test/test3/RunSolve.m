clear
clc
tic

%Определение начальных значений констант
initialValues.Kk=1;
initialValues.Kd=1;
initialValues.KpT=1;
initialValues.KpC=5;
Time

%Расчет системы дифференцтальных уравнений
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
