using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MappingGenerator.Helpers;

internal class UseMapperAttributeHelper
{
    public static AttributeSyntax? GetUseMapperAttribute(PropertyDeclarationSyntax propertySyntax)
        => CreateQueryInternal(propertySyntax).FirstOrDefault();


    public static bool ContainsUseMapperAttribute(PropertyDeclarationSyntax propertySyntax)
        => CreateQueryInternal(propertySyntax).Any();

    private static IEnumerable<AttributeSyntax> CreateQueryInternal(PropertyDeclarationSyntax propertySyntax)
    {
        return propertySyntax
            .AttributeLists
            .SelectMany(attrList => attrList.Attributes)
            .Where(attr => attr.ToFullString().StartsWith("Use") && attr.ToFullString().EndsWith("Mapper"))
            .OfType<AttributeSyntax>();
    }
}
