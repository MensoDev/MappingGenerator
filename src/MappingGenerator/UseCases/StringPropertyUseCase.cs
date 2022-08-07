using MappingGenerator.Models;

namespace MappingGenerator.UseCases;

internal sealed class StringPropertyUseCase : IPropertyUseCase
{
    private const string NullableSuppressorToken = "!";

    public StringPropertyUseCase(
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
        return $"{entityCamelCaseName}.{EntityProperty.Name}{ValidateNullableToken()}";
    }

    private string ValidateNullableToken()
    {
        return EntityProperty.HasNullable && !ModelProperty.HasNullable ? NullableSuppressorToken : string.Empty;
    }
}
