clear
clc
tic

%����������� ��������� �������� ��������
initialValues.kp=2;
initialValues.kd=1.3;
Time

%������ ������� ���������������� ���������
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
