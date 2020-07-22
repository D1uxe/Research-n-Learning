using System;
using System.Collections.Generic;
using System.Linq;

/*                  HOT KEYS
     * В режиме отладки: Watch Ctr+D,W QuickWatch Ctr+D,Q
     * Форматирование выбранной части Ctr+E+F, форматирование всего Ctr+E+D
     * Удалить строку Ctr+Shift+L, вырезать строку Ctrl+L поднять/опустить строку Alt+стрелка вверх/вниз
     * Добавить строку сверзу Ctrl+Enter, добавить строку снизу Ctrl+Shift+Enter
     * Транспонировать строку Shift+ALT+T
     * Буфер обмена Ctr+Shift+V
     * Свернуть/развернуть блок Ctr+M,M
     * Свернуть/развернуть ВСЕ блоки Ctr+M,O
     * Начало/окончание блоков {}, комментирования, или #region Ctr+]
     * Перейти к номеру строки Ctr+G
     * Перейти к определению класса/метода/свойства F12
     * Помощник Ctr+Space, Ctr+J
     * Показ всех снипетов Ctr+K,X
     * Закоментировать выбранные линии Ctr+E,C;  раскомментировать Ctr+E,U
     * Окружить выделенный текст  Ctr+K,S
     * Дублировать строку Ctr+E,V
     * Быстрые действия Ctr+.
     * Несколько точек вставки Ctr+ALT+нажатие
     * Выбор блока ALT и тащим мышь или SHIFT+ALT+стрелки
     * Удалить все точки останова Ctrl+Shift+F9
     * 
     */


namespace ConsoleApp1
{

    public struct S : IDisposable
    {
        private bool dispose;
        public void Dispose()
        {
            dispose = true;
        }
        public bool GetDispose()
        {
            return dispose;
        }
    }


    public class Animal
    {
        public int nameAnimal { get; set; }
        public void Meow()
        {
            Console.WriteLine("Class Animal");
        }
        public void RunAnimal() { }

    }
    public class Cat : Animal
    {
        public int NameCat { get; set; }
        public new void Meow()
        {
            Console.WriteLine("Class Cat МЯУ");
        }
        public void RunCat() { }
    }

    public class Dog
    {
        public int j;
    }



    public class Family
    {
        public string Name { get; set; }
        List<Family> _children = new List<Family>();

        public List<Family> Children
        {
            get { return _children; }
        }
    }



    class Program
    {

        private static bool Method(int x)
        {
            return x > 0;
        }
        static void Main(string[] args)
        {

            //исследование делегатов, анонимных методов, лямбд. Здесь в linq-метод пытался передать не лямбду а метод.
            Func<int, bool> del3;
            Func<int, bool> del2;

            del3 = Method;


            List<int> lst = new List<int>() { 2, 5, 3, 4, 7, 8 };
            int y = 2;
            del2 = delegate (int x) { return x > y; };

            // Вариант по умолчанию с лямбдой
            int result_1 = lst.First(x => x > y);
            Console.WriteLine(result_1);

            //Вариант с анонимным методом. Т.к. для сравнения используется локальная переменная y (да и любая другая переменная),
            // то ее нужно передать в метод (в данном случае Method), а делегат Func<int,bool> linq-метода First принимает всего один параметр
            // Поэтому в данном случае просто заменить лямбду на метод не получится. Рихтер, глава 17, стр 458.
            int result_2 = lst.First(del2);
            Console.WriteLine(result_2);


            //Вариант с обычным методом. Здесь нет других переменных, поэтому замена на обычный метод успешна.
            int result_3 = lst.First(del3);
            Console.WriteLine(result_3);

            Console.ReadKey();



            var familyRoot = new Family() { Name = "FamilyRoot" };

            var familyB = new Family() { Name = "FamilyB" };
            familyRoot.Children.Add(familyB);

            var familyC = new Family() { Name = "FamilyC" };
            familyB.Children.Add(familyC);

            var familyD = new Family() { Name = "FamilyD" };
            familyC.Children.Add(familyD);

            Func<Family, IEnumerable<Family>> child_Delegat;
            child_Delegat = delegate (Family f) { return f.Children; };

            List<Family> FamilylLst = new List<Family>() { familyRoot };

            //с подстановкой делегата
            var res = FamilylLst.Flatten(child_Delegat);
            //или что тоже самое с подстановкой лямбда-выражения
           // var res = FamilylLst.Flatten(i=>i.Children);



            foreach (var item in res)
            {
                Console.WriteLine(item.Name);
            }
            

            Console.ReadKey();

            /* 

            Dog sharik;             // объявили переменную
            sharik = new Dog();    // выделили ей память, sharik ссылочный тип
            sharik.j = 11;         // присвоили полю число

            object obj2 = sharik;  // теперь obj2 принимает ссылку на sharik. Обе переменные указывают на одну и туже память
            sharik.j++; 

            int i = 1;       //объявили переменную тип-значения
            object obj1 =i; // объявили всепринимающий тип object и присвоили ему i. Упаковка (boxing) предполагает преобразование объекта значимого типа (например, типа int) 
                            // к типу object. При упаковке общеязыковая среда CLR обертывает значение в объект типа System.Object и сохраняет его в управляемой куче (хипе). 
                            // Распаковка (unboxing), наоборот, предполагает преобразование объекта типа object к значимому типу. 
                            // В данном случает в obj1 хранится ссылка на КОПИЮ значения i в куче
            
            
            ++i;

            Console.WriteLine(i);    // выводит 2
            Console.WriteLine(obj1); // почему выводит 1, т.к. в obj1 лежит копия

            Console.WriteLine(sharik.j);    // выводит 12
            Console.WriteLine( ( (Dog)obj2 ).j ); // выводит тоже 12, так как ссылки одинаковые. Т.к. тип object его надо привести к типу Dog
            */









            /*

            // Если в наследнике метод переопределен override. То вызывая этот метод из базового класса выполнится метод наследника.
            // Различие переопределения и сокрытия методов https://metanit.com/sharp/tutorial/3.42.php

                        // Cat obj1 = new Animal(); // так нельзя

                        // Cat barsik = new Cat();
                        // barsik.Meow(); // выводит Class Cat

                        Animal an = new Cat();
                        Console.WriteLine(an.GetType());
                        an.Meow(); // выводит Cat, а должен Animal, потому что члены Cat из Animal не доступны
                        Console.WriteLine(an.ToString());


            // здесь локальная переменная count после завершения цикла не уничтожается, а сохраняет свое последнее значение (count=10)
            //Объяснение в главе 17, стр 458 примечание Рихтер
            //Когда лямбда-выражение заставляет компилятор генерировать класс с превращенными в поля параметрами/локальными переменными, увеличивается время жизни 
            //объекта, на который ссылаются эти переменные. Обычно параметры/локальные переменные уничтожаются после завершения метода, в котором они используются.
            //В данном же случае они остаются, пока не будет уничтожен объект, содержащий поле.
            //В большинстве приложений это не имеет особого значения, тем не менее этот факт следует знать.
            
                        List<Action> actions = new List<Action>();

                        for (var count = 0; count < 10; count++)
                        {
                            actions.Add( 
                                () => Console.WriteLine(count)
                                );
                        }
                        foreach (var action in actions)
                        {
                            action();
                        }


            //----------------------------------------
                        var s = new S();
                        using (s)
                        {
                            Console.WriteLine(s.GetDispose());
                        }
                        Console.WriteLine(s.GetDispose());

                        //-----------------------------------------------

                        */

        }
    }
}
