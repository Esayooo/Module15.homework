using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module15.homework
{
    class Program
    {
        static void Main()
        {
            // 1. Получение списка методов класса Console
            Type consoleType = typeof(Console);
            MethodInfo[] consoleMethods = consoleType.GetMethods();

            Console.WriteLine("Методы класса Console:");
            foreach (var method in consoleMethods)
            {
                Console.WriteLine(method.Name);
            }

            // 2. Описание класса с несколькими свойствами и использование рефлексии
            MyClass myObject = new MyClass
            {
                MyProperty1 = 42,
                MyProperty2 = "Hello, Reflection!"
            };

            Type myType = typeof(MyClass);
            PropertyInfo[] myProperties = myType.GetProperties();

            Console.WriteLine("\nСвойства объекта MyClass и их значения:");
            foreach (var property in myProperties)
            {
                object value = property.GetValue(myObject);
                Console.WriteLine($"{property.Name}: {value}");
            }

            // 3. Вызов метода Substring класса String с использованием рефлексии
            string myString = "Hello, Reflection!";
            Type stringType = typeof(string);
            MethodInfo substringMethod = stringType.GetMethod("Substring", new Type[] { typeof(int), typeof(int) });

            if (substringMethod != null)
            {
                object[] parameters = { 7, 5 }; // начальный индекс и длина подстроки
                object result = substringMethod.Invoke(myString, parameters);

                Console.WriteLine("\nПодстрока: " + result);
            }

            // 4. Получение списка конструкторов класса List<T>
            Type listType = typeof(List<>);
            Type genericArgument = typeof(int);

            Type closedType = listType.MakeGenericType(genericArgument);
            ConstructorInfo[] listConstructors = closedType.GetConstructors();

            Console.WriteLine("\nКонструкторы класса List<T>:");
            if (listConstructors.Length == 0)
            {
                Console.WriteLine("Класс не имеет конструкторов.");
            }
            else
            {
                foreach (var constructor in listConstructors)
                {
                    Console.WriteLine($"Конструктор: {constructor}");

                    // Вывод параметров конструктора
                    ParameterInfo[] parameters = constructor.GetParameters();
                    if (parameters.Length > 0)
                    {
                        Console.WriteLine("Параметры конструктора:");
                        foreach (var parameter in parameters)
                        {
                            Console.WriteLine($" - {parameter.ParameterType} {parameter.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Конструктор не имеет параметров.");
                    }

                    Console.WriteLine();
                }
            }
        }
    }

    class MyClass
    {
        public int MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
    }
}
