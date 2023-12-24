using System;

namespace AnimalLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommentAtt : Attribute
    {
        public string Comment { get; set; }

        public CommentAtt(string comment)
        {
            Comment = comment;
        }
    }

    public enum AnimalClassification
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    public enum FavouriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [CommentAtt("Абстрактный класс для объектов, представляющих животных")]
    public abstract class Animal
    {
        private AnimalClassification classification;

        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public bool HideFromOtherAnimals { get; set; }

        public abstract void SayHello();
        public abstract FavouriteFood GetFavouriteFood();

        public Animal(string country, string name, string description, bool hideFromOtherAnimals)
        {
            Name = name;
            Country = country;
            Description = description;
            HideFromOtherAnimals = hideFromOtherAnimals;
        }

        public void Deconstruct(out string out_name)
        {
            out_name = Name;
        }

        public void Deconstruct(out string out_name, out string out_desc)
        {
            out_name = Name;
            out_desc = Description;
        }

        public void Deconstruct(out string out_name, out string out_desc, out string out_count)
        {
            out_name = Name;
            out_count = Country;
            out_desc = Description;
        }

        public void Deconstruct(out string out_name, out string out_desc, out string out_count, out bool out_hide)
        {
            out_name = Name;
            out_count = Country;
            out_desc = Description;
            out_hide = HideFromOtherAnimals;
        }
    }

    [CommentAtt("Класс описания коровы")]
    public class Cow : Animal
    {
        public Cow(string country, string name, string description, bool hideFromOtherAnimals) :
            base(country, name, description, hideFromOtherAnimals)
        {
        }

        public override FavouriteFood GetFavouriteFood()
        {
            return FavouriteFood.Plants;
        }

        public override void SayHello()
        {
            Console.WriteLine("Moo\n");
        }
    }

    [CommentAtt("Класс описания льва")]
    public class Lion : Animal
    {
        public Lion(string country, string name, string description, bool hideFromOtherAnimals) :
            base(country, name, description, hideFromOtherAnimals)
        {
        }

        public override FavouriteFood GetFavouriteFood()
        {
            return FavouriteFood.Meat;
        }

        public override void SayHello()
        {
            Console.WriteLine("Roar\n");
        }
    }

    [CommentAtt("Класс описания свиньи")]
    public class Pig : Animal
    {
        public Pig(string country, string name, string description, bool hideFromOtherAnimals) :
            base(country, name, description, hideFromOtherAnimals)
        {
        }

        public override FavouriteFood GetFavouriteFood()
        {
            return FavouriteFood.Everything;
        }

        public override void SayHello()
        {
            Console.WriteLine("Oink\n");
        }
    }
}
