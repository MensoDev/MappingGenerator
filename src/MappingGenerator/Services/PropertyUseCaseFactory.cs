using MappingGenerator.Enums;
using MappingGenerator.Models;
using MappingGenerator.UseCases;

namespace MappingGenerator.Services;

internal sealed class PropertyUseCaseFactory
{
    public static IPropertyUseCase? CreatePropertyUseCase(
        PropertyDeclaration modelProperty,
        PropertyDeclaration entityProperty)
    {
        return modelProperty.BaseType switch
        {
            PropertyBaseType.String or
            PropertyBaseType.Short or
            PropertyBaseType.Int or
            PropertyBaseType.Long
                => new PrimitivePropertyUseCase(modelProperty, entityProperty),

            _ => null
        };
    }
}
