clear
clc
tic

%����������� ��������� �������� ��������
initialValues.k1=1.2;
initialValues.k2=1.7;
Time

%������ ������� ���������������� ���������
[tau,y]=ode45(@solve,tauRange,Ics(),odeset('RelTol',1e-13,'AbsTol',1e-13),initialValues);

Output

toc
