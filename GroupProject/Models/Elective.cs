using System.Collections.Generic;

namespace GroupProject.Models
{
    public class Elective
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
    }



    abstract class Animal
    {
        private string _name;

        public abstract void Byte();
    }

    class Dog : Animal
    {
        public override void Byte()
        {
            
        }

        public void Run()
        {
            
        }
    }

    class MyClass
    {
        private void DoSomething()
        {
            List<Animal> animals = new List<Animal>();
            Dog dog = new Dog();
            animals.Add(dog);
            animals[0].Byte();
        }
    }
}

