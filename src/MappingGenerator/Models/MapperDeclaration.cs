namespace MappingGenerator.Models;

internal class MapperDeclaration
{
    public MapperDeclaration(string name)
    {
        Name = name;
        ModelName = name.Remove(name.Length - 6, 6);
    }

    public string Name { get; private set; }
    public string ModelName { get; private set; }

}
