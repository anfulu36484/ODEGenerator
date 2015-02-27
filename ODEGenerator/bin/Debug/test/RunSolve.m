clear
clc
tic

%Определение начальных значений констант
initialValues.k1=1.2;
initialValues.k2=1.7;
Time

%Расчет системы дифференцтальных уравнений
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
