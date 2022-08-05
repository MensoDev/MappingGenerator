using MappingGenerator.Abstraction;

namespace MappingGenerator.Application;

[MapFrom<Person>]
public class PersonModel
{
    public string FirstName { get; set; } = default!;
    public int Age { get; set; }

}
