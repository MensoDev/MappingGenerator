using MappingGenerator.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MappingGenerator.Models;

internal sealed class ClassDeclaration
{
    private readonly List<PropertyDeclaration> _properties;

    public ClassDeclaration(string name, string selfNamespace)
    {
        Name = name;
        CamelCaseName = name.ToCamelCase();
        SelfNamespace = selfNamespace;
        _properties = new List<PropertyDeclaration>();
    }

    public string Name { get; private set; }
    public string CamelCaseName { get; private set; }
    public string SelfNamespace { get; private set; }

    public IReadOnlyCollection<PropertyDeclaration> Properties => _properties;


    public void RegisterProperties(PropertyDeclaration[] properties)
    {
        _properties.AddRange(properties);
    }

    public PropertyDeclaration? FindEquivalentProperty(PropertyDeclaration property)
    {
        return _properties.FirstOrDefault(p => p.Equals(property));
    }
}
