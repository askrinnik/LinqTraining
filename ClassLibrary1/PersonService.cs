using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
  public static class PersonService
  {
    public static IEnumerable<Person> GetFamily(IEnumerable<Person> persons, string surName)
    {
      var result = new List<Person>();
      foreach (var person in persons)
        if (person.SurName == surName)
          result.Add(person);
      return result;
    }

    public static IEnumerable<Person> GetMen(this IEnumerable<Person> persons)
    {
      var result = new List<Person>();
      foreach (var person in persons)
        if (person.Sex == Sex.Male)
          result.Add(person);
      return result;
    }

    public static IEnumerable<Person> GetAdult(this IEnumerable<Person> persons)
    {
      var result = new List<Person>();
      foreach (var person in persons)
        if ((DateTime.Now - person.BirthDay).Days/365 >= 21)
          result.Add(person);
      return result;
    }

    public delegate bool ConditionDelegate(Person person);

    public static IEnumerable<Person> GetByCondition(this IEnumerable<Person> persons, ConditionDelegate func)
    {
      var result = new List<Person>();
      foreach (var person in persons)
        if (func(person))
          result.Add(person);
      return result;
    }

    public static Person GetFirstOrNull(this IEnumerable<Person> persons)
    {
      foreach (var person in persons)
        return person;

      return null;
    }

    public static Person GetFirst(this IEnumerable<Person> persons)
    {
      Person result = persons.GetFirstOrNull();
      if(result == null)
        throw new Exception("Ooops!!!");
      return result;
    }

    public static Person GetSingle(this IEnumerable<Person> persons)
    {
      var result = persons.ToArray();
      if (result.Length == 0 || result.Length > 1)
        throw new Exception("Ooops!!!");

      return result[0];
    }
  }
}
