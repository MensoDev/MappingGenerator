using MappingGenerator.Enums;
using MappingGenerator.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MappingGenerator.Services;

internal sealed class PropertyDeclarationFactory
{
    public static PropertyDeclaration Create(PropertyDeclarationSyntax propertySyntax)
    {
        string propertyName = IdentifyPropertyName(propertySyntax);
        PropertyModifierType modifierType = IdentifyPropertyModifier(propertySyntax);
        PropertyBaseType baseType = IdentifyPropertyBaseType(propertySyntax);
        bool hasNullabe = IdentifyIfPropertyIsNullable(propertySyntax);

        return new PropertyDeclaration(propertyName, baseType, modifierType, hasNullabe);
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

            _ => PropertyBaseType.Object
        };
    }

    private static bool IdentifyIfPropertyIsNullable(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax.Type is NullableTypeSyntax;
    }
}
