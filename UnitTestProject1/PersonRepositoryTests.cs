using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
  [TestClass]
  public class PersonRepositoryTests
  {
    private Person[] _persons;

    private static Person[] GetPersonCollection()
    {
      return new []
      {
        new Person("Ivan", "Petrenko", new DateTime(1972, 1, 1), Sex.Male),
        new Person("Maria", "Petrenko", new DateTime(1972, 2, 2), Sex.Famile),
        new Person("Petya", "Petrenko", new DateTime(1995, 3, 3), Sex.Male),
        new Person("Lena", "Petrenko", new DateTime(1996, 4, 4), Sex.Famile),

        new Person("Mark", "Zuckerberg", new DateTime(1984, 5, 14), Sex.Male),
        new Person("Priscilla", "Zuckerberg", new DateTime(1985, 2, 24), Sex.Famile),
        new Person("Maxima", "Zuckerberg", new DateTime(2015, 12, 2), Sex.Famile),
      };
    }
    [TestInitialize]
    public void TestInitialize()
    {
      _persons = GetPersonCollection();
    }
    [TestMethod]
    public void GetFamilyTest()
    {
      IEnumerable<Person> result = PersonService.GetFamily(_persons, "Petrenko");
      Assert.AreEqual(4, result.Count());
    }

    [TestMethod]
    public void GetMenTest()
    {
      IEnumerable<Person> result = PersonService.GetMen(_persons);
      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void GetAdultTest()
    {
      IEnumerable<Person> result = PersonService.GetAdult(_persons);
      Assert.AreEqual(5, result.Count());
    }

    [TestMethod]
    public void GetAdultMenTest()
    {
      IEnumerable<Person> result = PersonService.GetMen(PersonService.GetAdult(_persons));
      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void GetAdultMen2Test()
    {
      IEnumerable<Person> result = _persons.GetAdult().GetMen();
      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void GetPetrenkoTest()
    {
      IEnumerable<Person> result = _persons.GetByCondition(PetrenkoCondition);
      Assert.AreEqual(4, result.Count());
    }

    private static bool PetrenkoCondition(Person person)
    {
      return person.SurName == "Petrenko";
    }

    [TestMethod]
    public void GetZuckerbergTest()
    {
      IEnumerable<Person> result = _persons.GetByCondition(delegate (Person person) { return person.SurName == "Zuckerberg"; });
      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void GetZuckerbergTest2()
    {
      IEnumerable<Person> result = _persons.GetByCondition(person => person.SurName == "Zuckerberg");
      Assert.AreEqual(3, result.Count());
    }

    [TestMethod]
    public void GetAdultZuckerbergTest()
    {
      IEnumerable<Person> result = _persons.GetByCondition(person => person.SurName == "Zuckerberg").GetByCondition(person => (DateTime.Now - person.BirthDay).Days / 365 >= 21);
      Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetAdultZuckerbergTest2()
    {
      IEnumerable<Person> result = _persons.GetByCondition(person => person.SurName == "Zuckerberg" && (DateTime.Now - person.BirthDay).Days / 365 >= 21);
      Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetAdultZuckerbergTest3()
    {
      IEnumerable<Person> result = _persons.Where(person => person.SurName == "Zuckerberg" && (DateTime.Now - person.BirthDay).Days / 365 >= 21);
      Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void GetFirstOrNullTest()
    {
      Person result = _persons.Where(person => person.SurName == "Zuckerberg" && (DateTime.Now - person.BirthDay).Days / 365 >= 21).GetFirstOrNull();
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void GetFirstOrNullTest2()
    {
      Person result = _persons.Where(person => person.SurName == "Pupkin").GetFirstOrNull();
      Assert.IsNull(result);
    }


    [TestMethod]
    public void GetFirstTest()
    {
      Person result = _persons.Where(person => person.SurName == "Zuckerberg" && (DateTime.Now - person.BirthDay).Days / 365 >= 21).GetFirst();
      Assert.IsNotNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetFirstTest2()
    {
      _persons.Where(person => person.SurName == "Pupkin").GetFirst();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetFirstTest3()
    {
      _persons.Where(person => person.SurName == "Pupkin").First();
    }

    [TestMethod]
    public void GetSingleTest()
    {
      Person result = _persons.Where(person => person.SurName == "Zuckerberg" && person.Name == "Mark").GetSingle();
      Assert.IsNotNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetSingleTest2()
    {
      _persons.Where(person => person.SurName == "Zuckerberg").GetSingle();
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetSingleTest3()
    {
      _persons.Where(person => person.SurName == "Pupkin").GetSingle();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetSingleTest4()
    {
      _persons.Where(person => person.SurName == "Pupkin").Single();
    }

    [TestMethod]
    public void OrderTest()
    {
      var result = _persons.Where(person => person.SurName == "Zuckerberg").OrderBy( person => person.Name);
      result = _persons.Where(person => person.SurName == "Zuckerberg").OrderByDescending(person => person.BirthDay);
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SumTest()
    {
      var zuckerbergFamily = _persons.Where(person => person.SurName == "Zuckerberg");
      var years = zuckerbergFamily.Select(person => (DateTime.Now - person.BirthDay).Days / 365);
      var result = years.Sum();

      Assert.AreEqual(64, result);

      var result2 = _persons.Where(person => person.SurName == "Zuckerberg").Sum(person => (DateTime.Now - person.BirthDay).Days / 365);
      Assert.AreEqual(64, result2);
    }
  }
}
