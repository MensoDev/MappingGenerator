namespace MappingGenerator.Application;

public class Person
{

    public Person(string firstName, int age)
    {
        FirstName = firstName;
        Age = age;
    }

    public string FirstName { get; private set; }
    public int Age { get; private set; }


}
