# ODEGenerator

## Что это?

Библиотека, позволяющая легко сгененировать и получить решение системы дифференциальных уравнений, описывающих кинетику простых реакций и реакций полимеризации. Также возможно использовать эту библиотеку для моделирования процессов и других типов.

### Пример простой реакции

Вещество *А* взаимодейтвует с веществом *В* и образуется вещество *С*:

```
А + B → C
```

Константа скорости этой реакции - *k*;

Для генерирования системы дифференциальных уравнений вначале создадим объект класса ODEs, класса описывающего систему дифференциальных уранений:

```
ODEs odEs = new ODEs();
```

Создадим объекты класса Substance для каждого вещества, участвующего в реакции:

```
Substance A = new Substance("A", 0.1); 

Substance B = new Substance("B", 0.3); 

Substance C = new Substance("C", 0); 
```

где 0.1, 0.3, 0 - начальные концентрации веществ.

Создадим объект класса Constant, определяющего константу скорости реакции:

```
Constant k = new Constant("k", 1.2); 
```

где 1.2 - значение константы.


Добавим уранение *А* + *B* → *C* в систему

```
odEs.Add(A, B, k, C);
```

Выведем систему дифференциальных уравнений в консоль:

```
odEs.PrintResult(new MathVisitor());
```

Результат:

* d(A)/d(t)=(-1)*k*A*B
* d(B)/d(t)=(-1)*k*A*B
* d(C)/d(t)=k*A*B


Вид вывода системы определяется пользователем и зависит от конкретной реализации паттерна Visitor. В настоящей момент доступен вывод с использованием нескольких классов:

* MathVisitor - вывод в простом виде;
* MapleVisitor - вывод в виде легкоэкспортируемом в програмный пакет компьютерной алгебры Maple;
* MatlabVisitor - вывод в виде легкоэкспортируемом в пакет Matlab;
* PureCVisitor - вывод на языке C.


### Пример реакции полимеризации

В качестве примера использована [обратимая живущая полимеризация]
(https://books.google.ru/books?id=_gv6-DiAOFUC&pg=PA25&lpg=PA25&dq=reversible+living+polymerization+of+lactones&source=bl&ots=g433ZSATZf&sig=ZKXaxqEVxtu54MomoI0_c7L1-sg&hl=ru&sa=X&ved=0ahUKEwi_89XX6pzKAhVF4XIKHTaFCBwQ6AEITTAG#v=onepage&q=reversible%20living%20polymerization%20of%20lactones&f=false)

Схема реакции:

R0+M → R1

Ri + M → Ri+1   i ∈ [1;L-1]

Ri+1 → Ri+M

где *M* - мономер, *Ri* - полимерная цепь с число мономерных звеньев равных *i*.


Также как и в случае простой реакции создаем объекты классов ODEs и Substance, но для  Ri в этом случае используем класс GroupOfSubstances, в противном случае пришлось бы создавать столько объектов класса Substance сколько звеньев в полимерной цепи.

```
ODEs odEs = new ODEs();

Substance M = new Substance("M", 10);

GroupOfSubstances R = new GroupOfSubstances("R");
```

Определяем значение максимальной длины рассчитываемой цепи (*L*), в теории *L* равна бесконечности:

```
int L = 2000;
```

Создаем и задаем значения начальных концентраций для цепей с различной длиной:

```
for (int i = 1; i <= L; i++)
{
    R.CreateSubstance(0.0, i);
}

R[1].Value = 0.01;
```


Определяем константы полимеризации (*kp*) и деполимеризации (*kd*)

```
Constant kp = new Constant("kp", 2);

Constant kd = new Constant("kd", 1.3);
```

Добавляем уравнения в систему

```
odEs.Add(R[1], M, kp, R[2]);

for (int i = 2; i < L - 1; i++)

{

    odEs.Add(R[i], M, kp, R[i + 1]);
	
    odEs.Add(R[i + 1], kd, R[i], M);
	
}

odEs.Add(R[L - 1], M, kp, R[L]);
```

Выводим итоговую систему в консоль:

odEs.PrintResult(new MathVisitor());


Используем класс MatlabCodeGenerator для создания программы, позволяющей решить данную систему в пакете Matlab

```
MatlabCodeGenerator matlabCodeGenerator = new MatlabCodeGenerator(odEs,
Enumerable.Range(0, 100).Select(n => (double)n / 10f).ToArray(), R);
matlabCodeGenerator.Generate(Environment.CurrentDirectory + @"\test\test3");
```

В папке \test\test3 будет создано 5 файлов:

* Ics.m - в файле будет описана функция определяющая начальные значения концентраций веществ вступающих в реакцию.

* Time.m - в файле будет представлен массив значений времени, его мы определяли строчкой: Enumerable.Range(0, 100).Select(n => (double)n / 10f).ToArray()

* solve.m - функция, в которой юудет определена система дифференциальных уравнений.

* Output.m - файл используется для вывода решения системы дифференциальных уравнений 

* RunSolve.m - файл, позволяющий запустить расчет системы дифференциальных уравнений.


Помимо класса MatlabCodeGenerator существуют класс CsharpCodeManager, позволяющий произвести расчет с использованием языка C# (с использованием библиотеки [DotNumerics](http://www.dotnumerics.com/))



# ODEGenerator

## What is it?

Library for easy generation and calculation a system of ordinary differential equations describing the kinetics of simple reactions and polymerization reactions.

### Example of a simple reaction

```
ODEs odEs = new ODEs(); //system of differential equations

Substance A = new Substance("A", 0.1); //starting (initial) concentration of A - 0.1

Substance B = new Substance("B", 0.3); //starting (initial) concentration of A - 0.3

Substance C = new Substance("C", 0); //starting (initial) concentration of A - 0

Constant k = new Constant("k", 1.2); //reaction rate constant

odEs.Add(A, B, k, C); //adding to the reaction system (A+B→C)

odEs.PrintResult(new MathVisitor());
```

###  Example of a polymerization reaction

[Reversible living polymerization]
(https://books.google.ru/books?id=_gv6-DiAOFUC&pg=PA25&lpg=PA25&dq=reversible+living+polymerization+of+lactones&source=bl&ots=g433ZSATZf&sig=ZKXaxqEVxtu54MomoI0_c7L1-sg&hl=ru&sa=X&ved=0ahUKEwi_89XX6pzKAhVF4XIKHTaFCBwQ6AEITTAG#v=onepage&q=reversible%20living%20polymerization%20of%20lactones&f=false)

```
R0+M -> R1

Ri + M -> Ri+1   i ∈ [1;L-1]

Ri+1 ->Ri+M
```

*M* - monomer

*R* - the polymer chain, *i* - number of monomeric units in the chain

```
ODEs odEs = new ODEs();

Substance M = new Substance("M", 10);

GroupOfSubstances R = new GroupOfSubstances("R");

int L = 2000;

for (int i = 1; i <= L; i++)
{

    R.CreateSubstance(0.0, i);
	
}

R[1].Value = 0.01;


Constant kp = new Constant("kp", 2);

Constant kd = new Constant("kd", 1.3);


odEs.Add(R[1], M, kp, R[2]);

for (int i = 2; i < L - 1; i++)

{

    odEs.Add(R[i], M, kp, R[i + 1]);
	
    odEs.Add(R[i + 1], kd, R[i], M);
	
}

odEs.Add(R[L - 1], M, kp, R[L]);

odEs.PrintResult(new MathVisitor());

MatlabCodeGenerator matlabCodeGenerator = new MatlabCodeGenerator(odEs,
Enumerable.Range(0, 100).Select(n => (double)n / 10f).ToArray(), R);
matlabCodeGenerator.Generate(Environment.CurrentDirectory + @"\test\test3");
```
