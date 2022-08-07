namespace MappingGenerator.Application;

public class StringEntity
{
    public StringEntity(string name, string? nullable)
    {
        Name = name;
        Nullable = nullable;
    }

    public string Name { get; private set; }

    public string? Nullable { get; private set; }

}
