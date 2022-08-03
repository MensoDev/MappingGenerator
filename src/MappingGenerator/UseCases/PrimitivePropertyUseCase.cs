using MappingGenerator.Models;

namespace MappingGenerator.UseCases;

internal sealed class PrimitivePropertyUseCase : IPropertyUseCase
{
    public PrimitivePropertyUseCase(
        PropertyDeclaration modelProperty,
        PropertyDeclaration entityProperty)
    {
        ModelProperty = modelProperty;
        EntityProperty = entityProperty;
    }

    public PropertyDeclaration ModelProperty { get; private set; }

    public PropertyDeclaration EntityProperty { get; private set; }


    public string CreateUseCaseTo(ClassDeclaration entity)
    {
        return $"{ModelProperty.Name} = {entity.CamelCaseName}.{EntityProperty.Name}";
    }
}
