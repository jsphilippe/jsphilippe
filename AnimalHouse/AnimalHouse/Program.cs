using System;
using System.Collections.Generic;
using Ninject;

public enum Animals
{
    Dog,
    Cat,
    Parrot
}

interface IAnimal
{
    void Speak();
}

class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Bow-wow!");
    }
}

class Cat : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow!");
    }
}

class Parrot : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Polly wants a cracker!");
    }
}

class AnimalFactory
{
    private static IKernel ninject_kernel;
    private static Dictionary<Animals, Func<IAnimal>> animalMaker;
    static AnimalFactory()
    {
        ninject_kernel = new StandardKernel();
        ninject_kernel.Bind<IAnimal>().To<Dog>().Named(Animals.Dog.ToString());
        ninject_kernel.Bind<IAnimal>().To<Cat>().Named(Animals.Cat.ToString());
        ninject_kernel.Bind<IAnimal>().To<Parrot>().Named(Animals.Parrot.ToString());
        animalMaker = new Dictionary<Animals, Func<IAnimal>>
        {
            {
                Animals.Dog,
                () => ninject_kernel.Get<IAnimal>(Animals.Dog.ToString())
            },
            {
                Animals.Cat,
                () => ninject_kernel.Get<IAnimal>(Animals.Cat.ToString())
            },
            {
                Animals.Parrot,
                () => ninject_kernel.Get<IAnimal>(Animals.Parrot.ToString())
            }
        };
    }

    public static IAnimal MakeAnimal(Animals animal)
    {
        if (animalMaker.TryGetValue(animal, out var creator))
        {
            return creator();
        }
        else
        {
            throw new ArgumentException("Invalid animal type", nameof(animal));
        }
    }
}

class AnimalHouse
{
    static void Main()
    {
        IAnimal dog = AnimalFactory.MakeAnimal(Animals.Dog);
        IAnimal cat = AnimalFactory.MakeAnimal(Animals.Cat);
        IAnimal parrot = AnimalFactory.MakeAnimal(Animals.Parrot);
        dog.Speak();
        cat.Speak();
        parrot.Speak();
    }
}