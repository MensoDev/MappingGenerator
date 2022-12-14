using MappingGenerator.Extensions;
using MappingGenerator.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MappingGenerator.Services;

internal sealed class ClassDeclarationFactory
{

    public static ClassDeclaration Create(ClassDeclarationSyntax classSyntax)
    {
        string name = IdentifyClassName(classSyntax);
        string selfNamespace = IdentifyNamespace(classSyntax);
        PropertyDeclaration[] properties = CreateProperties(classSyntax);

        ClassDeclaration classDeclaration = new(name, selfNamespace);
        classDeclaration.RegisterProperties(properties);

        return classDeclaration;
    }

    private static string IdentifyClassName(ClassDeclarationSyntax classSyntax)
    {
        return classSyntax.Identifier.ValueText;
    }

    public static PropertyDeclaration[] CreateProperties(ClassDeclarationSyntax classSyntax)
    {
        return classSyntax
            .Members
            .OfType<PropertyDeclarationSyntax>()
            .Select(PropertyDeclarationFactory.Create)
            .ToArray();
    }

    private static string IdentifyNamespace(ClassDeclarationSyntax classSyntax)
    {
        return classSyntax.GetNamespace();
    }

    
}
