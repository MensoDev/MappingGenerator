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
            PropertyBaseType.String => new StringPropertyUseCase(modelProperty, entityProperty),

            PropertyBaseType.Short or
            PropertyBaseType.Int or
            PropertyBaseType.Long or
            PropertyBaseType.Float or
            PropertyBaseType.Double or
            PropertyBaseType.Decimal
                => new PrimitivePropertyUseCase(modelProperty, entityProperty),

            _ => null
        };
    }
}
