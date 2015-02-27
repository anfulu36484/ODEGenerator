clear
clc
tic

%Определение начальных значений констант
initialValues.ko=0.003;
initialValues.kj=0.9;
initialValues.kt=1E-06;
initialValues.ktc=9;
Time

%Расчет системы дифференцтальных уравнений
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
