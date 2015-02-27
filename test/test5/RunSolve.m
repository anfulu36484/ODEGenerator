clear
clc
tic

%Определение начальных значений констант
initialValues.ko=0.003;
initialValues.kp=0.9;
initialValues.kts=1E-07;
initialValues.ktp=1E-06;
Time

%Расчет системы дифференцтальных уравнений
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
