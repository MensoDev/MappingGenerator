using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MappingGenerator.Helpers;

internal class MapFromAttributeHelper
{
    public static AttributeSyntax GetMapFromAttribute(ClassDeclarationSyntax classDeclaration)
        => CreateQueryInternal(classDeclaration).First();
    

    public static bool ContainsMapFromAttribute(ClassDeclarationSyntax classDeclaration)
        => CreateQueryInternal(classDeclaration).Any();
    

    private static IEnumerable<AttributeSyntax> CreateQueryInternal(ClassDeclarationSyntax classDeclaration)
    {
        return classDeclaration
            .AttributeLists
            .SelectMany(attrList => attrList.Attributes)
            .Where(attr => attr.ToFullString().Contains("MapFrom"))
            .OfType<AttributeSyntax>();
    }

    public static IdentifierNameSyntax GetEntityIdentifierNameSyntax(AttributeSyntax attribute)
    {
        return ((GenericNameSyntax)attribute.Name).TypeArgumentList
                .ChildNodes()
                .OfType<IdentifierNameSyntax>()
                .First();
    }
}
