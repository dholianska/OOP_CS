using System;
using System.Collections.Generic;

class Program
{
    class Person
    {
        private string _name; //field
        private int _age;

        public string Name //property
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public Person() //default
        {
            _name = "unknown";
            _age = 0;
        }
        public Person(string name, int age) //parameter
        {
            _name = name;
            _age = age;
        }

        public Person(bool useNew) : this("no name", 1) //task 2
        {

        }

        public Person(int age) : this("no name", age)
        {
        
        }

        public Person(string name, int age, bool p = true) : this(name, age)
        {

        }

    }

    class Family
    {
        private List<Person> members = new List<Person>();

        public void AddMember(Person member)
        {
            members.Add(member);
        }

        public Person GetOldestMember()
        {
            if (members.Count == 0)
                return null;

            Person oldest = members[0];
            foreach (var member in members)
            {
                if (member.Age > oldest.Age)
                    oldest = member;
            }
            return oldest;
        }
    }

    static void Main()
    {
        Person firstPerson = new Person(); //default
        Console.WriteLine($"{firstPerson.Name} {firstPerson.Age}");

        Person secondPerson = new Person("maria", 20); //parameter
        Console.WriteLine($"{secondPerson.Name} {secondPerson.Age}");
        secondPerson.Name = "ulyana"; //set
        Console.WriteLine($"{secondPerson.Name} {secondPerson.Age}");

        Person thirdPerson = new Person //вбудована ініціалізація
        {
            Name = "alice",
            Age = 22
        };
        Console.WriteLine($"{thirdPerson.Name} {thirdPerson.Age}");

        Console.WriteLine(); 

        Person p1 = new Person(true);
        Console.WriteLine($"{p1.Name} {p1.Age}");

        Person p2 = new Person(25);
        Console.WriteLine($"{p2.Name} {p2.Age}");

        Person p3 = new Person("alice", 30, true);
        Console.WriteLine($"{p3.Name} {p3.Age}");


        Console.WriteLine();
        Family family = new Family();

        Console.Write("how many people are in the family? n = ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.Write("enter the name and age with space: ");
            string[] input = Console.ReadLine().Split(' ');

            string name = input[0];
            int age = int.Parse(input[1]);

            Person person = new Person(name, age);
            family.AddMember(person);
        }

        Person oldest = family.GetOldestMember();

        if (oldest != null)
            Console.WriteLine($"{oldest.Name} {oldest.Age}");

    }
}