using MappingGenerator.Enums;
using MappingGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MappingGenerator.Services;

internal sealed class PropertyDeclarationFactory
{
    public static PropertyDeclaration Create(PropertyDeclarationSyntax propertySyntax)
    {
        var mapperName = IdentifyUseMapperAttribute(propertySyntax);
        var mapper = mapperName is null ? null : new MapperDeclaration(mapperName);

        var propertyName = IdentifyPropertyName(propertySyntax);
        var modifierType = IdentifyPropertyModifier(propertySyntax);
        var baseType = IdentifyPropertyBaseType(propertySyntax);
        var hasNullable = IdentifyIfPropertyIsNullable(propertySyntax);

        return new PropertyDeclaration(propertyName, baseType, modifierType, hasNullable, mapper);
    }

    private static string IdentifyPropertyName(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax.Identifier.ValueText;
    }

    private static PropertyModifierType IdentifyPropertyModifier(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax.Modifiers[0].ValueText.ToLower() switch
        {
            "public" => PropertyModifierType.Public,
            "protected" => PropertyModifierType.Protected,
            _ => PropertyModifierType.Private,
        };
    }

    private static PropertyBaseType IdentifyPropertyBaseType(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax.Type.ToFullString().Trim().Replace("?","") switch
        {
            "string" or "String" => PropertyBaseType.String,
            "short" or "Int16" => PropertyBaseType.Short,
            "int" or "Int32" => PropertyBaseType.Int,
            "long" or "Int64" => PropertyBaseType.Long,
            "decimal" or "Decimal" => PropertyBaseType.Decimal,
            "double" or "Double" => PropertyBaseType.Double,
            "float" or "Float" => PropertyBaseType.Float,

            var type when  type.StartsWith("IList") || type.StartsWith("List")  => PropertyBaseType.List,

            _ => PropertyBaseType.Object
        };
    }

    private static bool IdentifyIfPropertyIsNullable(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax.Type is NullableTypeSyntax;
    }

    private static string? IdentifyUseMapperAttribute(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax
            .DescendantNodes()
            .OfType<IdentifierNameSyntax>()
            .Where(nameSyntax =>
            {
                var name = nameSyntax.ToFullString();
                return name.StartsWith("Use") && name.EndsWith("Mapper");
            })
            .Select(name => name.ToFullString().Remove(0,3))
            .FirstOrDefault();

    }
}
