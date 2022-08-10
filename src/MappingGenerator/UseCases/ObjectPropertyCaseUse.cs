using MappingGenerator.Models;

namespace MappingGenerator.UseCases;

internal class ObjectPropertyCaseUse : IPropertyUseCase
{
    public ObjectPropertyCaseUse(
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
        return $"{ModelProperty.Name} = {CreateRightMember(entity.CamelCaseName)}";
    }

    private string CreateRightMember(string entityCamelCaseName)
    {
        return $"{entityCamelCaseName}.{EntityProperty.Name}.To{ModelProperty.Mapper?.ModelName}()";
    }

    
}
