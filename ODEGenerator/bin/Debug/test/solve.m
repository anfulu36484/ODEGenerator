function out=solve(tau,y,initialValues)
out=zeros(size(y));
%Список констант
k1 = initialValues.k1;
k2 = initialValues.k2;
%Система дифференциальных уравнений
out(1) = -k1*y(1)*y(2)+k2*y(3)*y(4);
out(2) = -k1*y(1)*y(2)+k2*y(3)*y(4);
out(3) = -k2*y(3)*y(4)+k1*y(1)*y(2);
out(4) = -k2*y(3)*y(4)+k1*y(1)*y(2);
