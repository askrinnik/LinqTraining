using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
  public enum Sex { Male, Famile}

  public class Person
  {
    public string Name { get; set; }
    public string SurName { get; set; }
    public DateTime BirthDay { get; set; }
    public Sex Sex { get; set; }

    public Person(string name, string surName, DateTime birthDay, Sex sex)
    {
      Name = name;
      SurName = surName;
      BirthDay = birthDay;
      Sex = sex;
    }

    public override string ToString()
    {
      return $"{SurName} {Name} {BirthDay}";
    }
  }
}